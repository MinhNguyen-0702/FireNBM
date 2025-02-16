using System;
using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class UnderConstructionComp : MonoBehaviour
    {
        private PairObjectBuiding m_buildingRTS;
        private Vector3 m_positionPlaceable;

        private bool m_isStartBuilding;
        private bool m_handleBuilding;
        private int m_currResourceBuild;

        private Transform m_transformBuilding;
        private readonly int m_maxResourceBuild = 30;
        private HashSet<Action> m_listAction;


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_buildingRTS = null;
            m_currResourceBuild = 0;
            m_isStartBuilding = false;
            m_positionPlaceable = Vector3.zero;
            m_handleBuilding = false;

            m_listAction = new HashSet<Action>();
        }

        private void FixedUpdate()
        {
            if (m_isStartBuilding == false)
                return;
            
            if (m_handleBuilding == false)
                UpdateBuilding();
            else 
                HandleEffectBuilding();
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///    Thiết lập công trình để khi công nhân xây xong thì nó sẽ xuất hiện tại vị trí đã chỉ định. </summary>
        /// --------------------------------------------------------------------------------------------------------
        public void FunSetDataBuildingPlace(PairObjectBuiding buildingRTS, Vector3 pos)
        {
            m_buildingRTS = buildingRTS;
            m_positionPlaceable = pos;

            if (buildingRTS.ObjectBuilding == null)
            {
                Debug.Log("Công trình cần xây dựng không tồn tại.");
                return;
            }
            m_transformBuilding = buildingRTS.ObjectBuilding.transform;

            // Gửi thông báo đến công nhân đang đợi để xây công trình.
            MessagingSystem.Instance.FunTriggerMessage(new MessageBuildStructure(this), true);
        }

        /// <summary>
        ///     Được công nhân trong trạng thái xây dựng sử dụng khi hoàn thành 1 phiên làm việc. </summary>
        /// ------------------------------------------------------------------------------------------------ 
        public void FunUpdateResourceBuilding() => m_currResourceBuild += 2;

        /// <summary>
        ///     Công nhân nhận được công trình, công trình nhận được thông báo và bắt đầu xây dựng. </summary>
        /// -------------------------------------------------------------------------------------------------- 
        public void FunUnitWorkerStartBuid() => m_isStartBuilding = true;

        /// <summary>
        ///     Lấy vị trí cần xây dựng. </summary>
        /// ---------------------------------------
        public Vector3 FunGetPosUnderConstruction() => m_positionPlaceable;

        /// <summary>
        ///     Đăng ký các hành động khi khi công trình được xây xong. </summary>
        /// ---------------------------------------------------------------------
        public void FunAttachActionCreateBuildingSuccess(Action action)
        {
            if (action == null || m_listAction.Contains(action) == true)
                return;
            
            m_listAction.Add(action);
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Cập nhật thời lượng mà công nhân đã xây dựng được.
        // -------------------------------------------------
        private void UpdateBuilding()
        {
            if (m_currResourceBuild >= m_maxResourceBuild)
            {
                Build();
                m_handleBuilding = true;
            }
        }

        // Thiết lập hiệu ứng di chuyển từ dưới lên trên.
        // ----------------------------------------------
        private void HandleEffectBuilding()
        {
            m_transformBuilding.Translate(Vector3.up * 3f * Time.deltaTime);
            if (Vector3.Distance(m_transformBuilding.position, m_positionPlaceable) <= 0.25f)
            {
                m_transformBuilding.position = m_positionPlaceable;
                Shutdown();
            }
        }

        // Thực hiện đặt công trình khi công nhân xây dựng hoàn tất.
        // ---------------------------------------------------------
        private void Build()
        {
            // Thông báo đến các công nhân, công trình xây xong.
            foreach (var action in m_listAction)
                action.Invoke();

            m_listAction.Clear();
            
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            var size = m_buildingRTS.ObjectBuilding.GetComponent<BoxCollider>().size;
            m_transformBuilding.position = new Vector3(m_positionPlaceable.x, -size.y, m_positionPlaceable.z);
            m_buildingRTS.ObjectBuilding.SetActive(true);
        }

        // Rọn rẹp tài nguyên, xóa thành phần xây dựng.
        // --------------------------------------------
        private void Shutdown()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            FactorySystem.Instance.RaceFactory.FunDisposeUnderConstructionRace(m_buildingRTS.ObjectUnderConstruction, TypeRaceBuilding.Townhall, TypeRaceRTS.Terran);
            Destroy(this);
        }
    }
}