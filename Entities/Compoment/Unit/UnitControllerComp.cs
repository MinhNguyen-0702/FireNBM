using UnityEngine;
using FireNBM.Pattern;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Collections;
using System;
using System.Linq;

namespace FireNBM
{
    [AddComponentMenu("FireNBM/RaceRTS/Unit/Unit Controller Comp")]
    [RequireComponent(typeof(UnitDataComp))]

    public class UnitControllerComp : MonoBehaviour
    {
        // Owner
        private GameObject m_owner;
        private UnitDataComp m_data;
        private UnitAudioComp m_audio;

        // Enemy
        private GameObject m_enemy;
        private GameObject m_enemyDeath;
        private UnitControllerComp m_enemyControllerComp;
        private UnitHeathComp m_enemyHealthComp;


        // Use Trigger.
        private Action m_actionOntrigger;
        private GameObject m_objTargetTrigger;
        
        // Common.
        private bool m_isMoving;
        private bool m_hasTargetAttack;
        private bool m_isAttackEnemy;

        private TypeRaceRTS m_raceUnit;
        private UnitManager m_managerUnits;
        private Vector3 m_posMouseClick;
        private NavMeshAgent m_agent;


        public bool NewDestination { get; set; }


        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_owner = gameObject;
            m_data = m_owner.GetComponent<UnitDataComp>();
            DebugUtils.HandleErrorIfNullGetComponent<UnitDataComp, UnitControllerComp>(m_data, this, gameObject);

            m_isMoving = false;
            NewDestination = false;
            m_hasTargetAttack = false;
            m_isAttackEnemy = false;
        
            m_objTargetTrigger = null;
            m_actionOntrigger = null;
            m_posMouseClick = Vector3.zero;
        }

        private void Start()
        {
            m_agent = m_owner.GetComponent<NavMeshAgent>();
            DebugUtils.HandleErrorIfNullGetComponent<NavMeshAgent, UnitControllerComp>(m_agent, this, gameObject);

            m_audio = m_owner.GetComponent<UnitAudioComp>();
            DebugUtils.HandleErrorIfNullGetComponent<UnitAudioComp, UnitControllerComp>(m_audio, this, gameObject);

            var flyweightComp = gameObject.GetComponent<UnitFlyweightComp>();
            DebugUtils.HandleErrorIfNullGetComponent<UnitFlyweightComp, UnitControllerComp>(flyweightComp, this, gameObject);

            m_managerUnits = UnitManager.Instance;
            m_raceUnit = flyweightComp.FunGetRaceRTS();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_objTargetTrigger == null)
                return;
            
            if (other.gameObject == m_objTargetTrigger)
            {
                m_actionOntrigger?.Invoke();
                m_objTargetTrigger = null;
                m_actionOntrigger = null;
            }
        }

        private void Update()
        {
            #if UNITY_EDITOR
                if (m_isMoving == false || m_agent.path.corners.Length < 2)
                    return;
                
                var path = m_agent.path;
                for (int i = 0; i < path.corners.Length - 1; ++i)
                {
                    DebugUtils.FunDrawLine(path.corners[i], path.corners[i + 1], Color.green);
                }
            #endif
        }

 
        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Nơi đội hình gửi vị trí để đơn vị di chuyển đến.</summary>
        /// --------------------------------------------------------------
        public void FunSetPosMouseTarget(Location target)
        {
            // Bỏ qua nếu khoảng các quá gần.
            if (Vector3.Distance(m_posMouseClick, target.Position) < ConstantFireNBM.TARGET_PROXIMITY_THRESHOLD)
                return;
            
            NewDestination = true;
            m_posMouseClick = target.Position;
        }

        /// <summary>
        ///     Gửi vị trí cần di chuyển. </summary>
        /// ---------------------------------------- 
        public void FunSetNewDestination(Vector3 target)
        {
            // Bỏ qua nếu khoảng các quá gần.
            if (Vector3.Distance(m_posMouseClick, target) < ConstantFireNBM.TARGET_PROXIMITY_THRESHOLD)
                return;

            NewDestination = true;
            m_posMouseClick = target;
        }

        /// <summary>
        ///     Thiết lập vùng va chạm khi unit di chuyển đến và chạm dính, action sẽ được gọi. </summary>
        /// ---------------------------------------------------------------------------------------------- 
        public void FunSetOnTriggerEnter(GameObject objTrigger, Action actionOntrigger)
        {
            m_objTargetTrigger = objTrigger;
            m_actionOntrigger = actionOntrigger;
        }

        public void FunLookTo(Vector3 dir) => m_owner.transform.LookAt(dir);
        public void FunStartCoroutine(IEnumerator routine) => StartCoroutine(routine);

        /// <summary>
        ///     Thiết lập trạng thái di chuyển cho unit. </summary>
        /// --------------------------------------------------------
        public void FunSetMoving(bool isMoving)
        {
            m_isMoving = isMoving;
            if (m_isMoving == false && m_agent != null && m_agent.isOnNavMesh && m_agent.enabled)
            {
                m_agent.ResetPath();
            }
        }

        /// <summary>
        ///     Di chuyển đến vị trí mục tiêu.</summary>
        /// --------------------------------------------
        public void FunMoveTo(Vector3 target, bool useNavMesh = true, TypeUnitAnimState animState = TypeUnitAnimState.Move)
        {
            if (m_isMoving == false)
                return;
            
            FunLookTo(target);
            m_data.FunSetAnimState(animState);

            if (useNavMesh == true || m_agent != null)
            {
                m_agent.speed = m_data.WalkSpeed;
                m_agent.SetDestination(target);
            }
            else 
            {
                m_owner.transform.position = Vector3.MoveTowards(m_owner.transform.position, 
                    target, m_data.WalkSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        ///     Reset dữ liệu của unit về trạng thái mặc định.</summary>
        /// ------------------------------------------------------------
        public void FunResetDefaultController()
        {
            if (m_enemy != null)
            {
                m_enemy.GetComponent<UnitDeathComp>().FunRemoveAction(OnEnemyDeath);
            }
            m_enemy = null;
            m_enemyControllerComp = null;
            m_enemyHealthComp = null;
    
            FunSetMoving(false);
            StopAllCoroutines();
            m_data.FunSetAnimState(TypeUnitAnimState.Idle);
        }

        /// <summary>
        ///     Lấy đối tượng mục tiêu để thực hiện hành động. </summary>
        /// -------------------------------------------------------------
        public GameObject FunGetTarget(float range)
        {
            var targets = FunGetTargesInRange();
            return targets.FirstOrDefault(ememy => FunIsCloseToTarget(ememy.transform.position, range));
        }

        /// <summary>
        ///     Kiểm tra xem đã đến vị trí mục tiêu với khoảng cách đã cho chưa.</summary>
        /// -----------------------------------------------------------------------------
        public bool FunIsCloseToTarget(Vector3 target, float distance)
        {
            return Vector3.Distance(m_owner.transform.position, target) < distance;
        }

        /// <summary>
        ///     Kiểm tra xem đã đến vị trí mục tiêu chưa.</summary>
        /// -------------------------------------------------------
        public bool FunIsCloseToTarget(Vector3 target)
        {
            return Vector3.Distance(m_owner.transform.position, target) < ConstantFireNBM.TARGET_REACHED_THRESHOLD;
        }

        /// <summary>
        ///     Lấy tất cả các mục tiêu nằm trong khu vục hiện tại.</summary>
        /// -----------------------------------------------------------------
        public List<GameObject> FunGetTargesInRange()
        {
            var raceEnemy = GameSystem.Instance.FunGetSettingGame().FunGetRaceOpponent(m_raceUnit);
            return m_managerUnits.FunGetAllUnitActive(raceEnemy);
        }

        /// <summary>
        ///     Lấy vị trí nhấn chuột từ người dùng cần di chuyển.</summary>
        /// ---------------------------------------------------------------
        public Vector3 FunGetPosMouseClick() => m_posMouseClick;

        /// <summary>
        ///     Cho biết unit có di chuyển hay ko. </summary>
        /// -------------------------------------------------
        public bool FunIsMoving() => m_isMoving;

        /// <summary>
        ///     Cho biết unit có tấn công hay ko. </summary>
        /// ------------------------------------------------
        public bool FunIsTargetAttack() => m_hasTargetAttack;

        // Giúp kiểm tra có enemy ko.
        // -------------------------
        public bool FunIsHaveEnemy() => m_enemy;

        // Lấy đối tượng enemy để xứ lý.
        // -----------------------------
        public GameObject FunTryFindEnemy()
        {
            if (m_enemy != null)
                return m_enemy;

            var enemy = FunGetTarget(m_data.RangeAttack);
            if (m_enemyDeath == enemy)
                return null;

            if (enemy != null)
            {
                m_enemy = enemy;
                m_enemyControllerComp = enemy.GetComponent<UnitControllerComp>();
                m_enemyHealthComp = enemy.GetComponent<UnitHeathComp>();

                var deathComp = enemy.GetComponent<UnitDeathComp>();
                deathComp.FunAddAction(OnEnemyDeath);
            }
            return enemy;
        }

        // Nhận thông báo khi đối enemy bị tiêu diệt.
        // ----------------------------------------
        private void OnEnemyDeath()
        {
            m_enemyDeath = m_enemy;
            m_enemy = null;
            m_enemyControllerComp = null;
            m_enemyHealthComp = null;
            m_isAttackEnemy = false;

            FunSetMoving(false);
            StopAllCoroutines();
            m_data.FunSetAnimState(TypeUnitAnimState.Idle);

            // Gọi lại tìm enemy mới ngay lập tức
            if (FunTryFindEnemy() != null)
                FunHandleAttackEnemy();
        }

        public void FunHandleAttackEnemy()
        {
            if (m_isAttackEnemy == true)
                return;

            if (FunIsCloseToTarget(m_enemy.transform.position, 2f) == true)
            {
                m_isAttackEnemy = true;
                HandleAttackEnemyWhenEnemyIdle();
                return;
            }
            else 
            {
                // Di chuyển đến mục tiêu nếu chưa đủ gần
                FunSetMoving(true);
                FunMoveTo(m_enemy.transform.position);
            }
        }


        private void HandleAttackEnemyWhenEnemyIdle()
        {
            FunLookTo(m_enemy.transform.position);
            m_data.FunSetAnimState(TypeUnitAnimState.Attack);
            StartCoroutine(HandleAttack());
        }

        private IEnumerator HandleAttack()
        {   
            FunSetMoving(false);
            m_enemyHealthComp.FunSetHealth(m_data.Attack);

            var timeAttack = m_data.AttackSpeed;
            while (timeAttack >= 0.0f)
            {
                timeAttack -= Time.deltaTime;
                yield return null;
            }
            m_isAttackEnemy = false;
            m_enemyHealthComp.FunSetHealth(m_data.Attack);
            yield return new WaitForSeconds(0.5f);
        }   
        
        // private IEnumerator HandleAttackWhenEnemyMove()
        // {
        //     m_isAttackEnemy = true;
        //     FunSetMoving(false);
        
        //     float timeCount = 0.0f;
        //     float attackCount = 0.0f;
        //     bool firstAttack = false;

        //     while (timeCount <= 1.2f)
        //     {
        //         timeCount += Time.deltaTime;
        //         attackCount += Time.deltaTime;

        //         if (m_raceUnit == TypeRaceRTS.Zerg)
        //             DebugUtils.FunLog($"Name: {gameObject.name}, Distance: {Vector3.Distance(m_owner.transform.position, m_enemy.transform.position)}");

        //         if (FunIsCloseToTarget(m_enemy.transform.position, 2f) == true)
        //         {
        //             if (m_raceUnit == TypeRaceRTS.Zerg)
        //                 DebugUtils.FunLog("Attack");

        //             if (firstAttack == false || attackCount >= m_data.AttackSpeed)
        //             {
        //                 if (m_raceUnit == TypeRaceRTS.Zerg)
        //                     DebugUtils.FunLog("---------------------Handle Attack");

        //                 firstAttack = true;
        //                 attackCount = 0.0f;

        //                 m_data.FunSetAnimState(TypeUnitAnimState.Attack);
        //                 FunLookTo(m_enemy.transform.position);
        //                 m_enemyHealthComp.FunSetHealth(m_data.Attack);
        //             }
        //         }
        //         yield return null;
        //     }
        //     m_isAttackEnemy = false;
        //

        // if (!isEnemyIdle && distance <= 4f)
        // {
        //     DebugUtils.FunLog("Attack Move: " + gameObject.name);
        //     FunStartCoroutine(HandleAttackWhenEnemyMove());
        //     return;
        // }

        // /// <summary>
        // ///     Thông báo đội hình rằng mình đã đến vị trí trong đội.</summary>
        // /// ------------------------------------------------------------------
        // public void FunNotifyFormationArrivedTarget()
        // {
        //     if (m_hasSendMessage == true)
        //         return;
    
        //     m_hasSendMessage = true;
        //     m_messageSystem.FunTriggerMessage(new MessageMemberAtTarget(m_owner, m_idForm), false);
        // }

        /// <summary>
        // ///     Nhận ID của đội hình mà nó chủ thể của nó tham gia.</summary>
        // /// -----------------------------------------------------------------
        // public void FunSetIdForm(int ID) => m_idForm = ID;

        // /// <summary>
        // ///     Kiểm tra xem unit có thuộc về đội hình nào không. </summary>
        // /// ----------------------------------------------------------------
        // public bool FunHasForm() => m_idForm != -1;

        // private int m_idForm;               // ID của đội hình dùng để thông báo cho đội khi đến nơi.
        // private bool m_hasSendMessage;      // Thông báo chủ thể đã gửi tin nhắn đến đội hình.
    }
}

 // m_messageSystem.FunTriggerMessage(new MessageDisableObjectsHUD(), false);
// DebugUtils.FunLog($"Distance: {Vector3.Distance(m_enemy.transform.position, m_owner.transform.position)}, Moving: {m_enemyControllerComp.FunIsMoving()}");