using System;
using System.Collections;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái thu thập tài nguyên của đơn vị.
    /// </summary>
    public class StateUnitWorkerGather : IUnitState
    {
        // Dữ liệu liệu quan đến chính chủ unit.
        private GameObject m_owner;
        private UnitDataComp m_data;
        private UnitControllerComp m_controller;
        private UnitWorkerComp m_worker;
        private UnitAudioComp m_audio;

        // Dữ liệu liên quan đế công trình thu thập và xử lý tài nguyên.
        private Vector3 m_posTownhall;
        private GameObject m_objectTownhall;
        private BuildingResourceComp m_townhallResComp;

        // Dữ liệu liên quan đến bãi tài nguyên.
        private Vector3 m_posResource;
        private GameObject m_objectResource;
        private ResourceMineralsObjectRTS m_resourceComp;


        // Liên quan đến quá trình xử lý tài nguyên.
        private int m_currExploitRes;               // Tài nguyên mà công nhân có thể khai thác được.
        private int m_maxExploitRes;                // Lượng tài nguyên tối đa mà công nhân có thể khai thác trong một phiên.

        private int m_timeExploit;                  // Thời gian khai thác.
        private int m_timeReturnTownhall;           // Thời gian đưa tài nguyên cho công trình.

        private bool m_isHandleBuilding;
        private bool m_isHandleResource;
        private bool m_isExploitingResources;       // Công nhân đang khai thác hay đưa tài nguyên đến nhà máy.

 
        // ---------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ////////////////////////////////////////////////////////////////////////

        public StateUnitWorkerGather(GameObject owner)
        {
            if (owner == null)
            {
                DebugUtils.FunLogError("Object owner is NULL");
                return;
            }
            m_owner = owner;
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();
            m_audio = m_owner.GetComponent<UnitAudioComp>();

            m_worker = owner.GetComponent<UnitWorkerComp>();
            if (m_worker == null)
            {
                DebugUtils.FunLogError("Lỗi đối tượng không có thành phần: UnitWorkerComp cho công nhân unit");
                return;
            }
            m_timeExploit = m_worker.FunGetTimeExploit();
            m_maxExploitRes = m_worker.FunGetMaxExploitRes();
            m_timeReturnTownhall = m_worker.FunGetTimeReturnTownhall();
            
            m_posTownhall = Vector3.zero;
            m_objectTownhall = null;
            m_townhallResComp = null;

            m_posResource = Vector3.zero;
            m_objectResource = null;
    
            m_isExploitingResources = false;  
            m_currExploitRes = 0;  
        }


        // -----------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu trạng thái thu thập tài nguyên.</summary>
        /// ---------------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitWorker.Gather;
        public void FunOnExit() => m_controller.FunResetDefaultController();

        public void FunOnEnter()
        {
            // Trở về trạng thái mặc định nếu không có đủ dữ liệu khai thác tài nguyên.
            if (UpdateDataTownhall() == false || UpdateDataResource() == false)
            {
                var stateOwnerComp = m_owner.GetComponent<UnitStateComp>();
                if (stateOwnerComp == null)
                {
                    DebugUtils.FunLogError("Lỗi đối tượng không có thành phần quản lý trạng thái");
                    return;
                }
                stateOwnerComp.FunChangeState(TypeRaceUnitBase.Free);
            }
            StartMoveToResource();
        }
 
        public void FunHandle()
        {
            if (m_isExploitingResources == false)
                ExtractResource();
            else 
                BringResourceToBuilding();
        }


        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        // Đưa những tài nguyên mà công nhân đa khai thác được cho công trình để xử lý.
        // ----------------------------------------------------------------------------
        private void BringResourceToBuilding()
        {
            if (m_isHandleBuilding == false)
                return;

            m_isHandleBuilding = false;
            m_controller.FunSetMoving(false);
            m_controller.FunLookTo(m_posTownhall);
            m_data.FunSetAnimState(TypeUnitAnimState.Idle);
            m_controller.FunStartCoroutine(WorkerBringResourceToBuilding());
        }

        // Công nhân di chuyển đến bãi tài nguyên để khai thác.
        // ----------------------------------------------------
        private void ExtractResource()
        {
            if (m_isHandleResource == false)
                return;

            m_isHandleResource = false;
            m_controller.FunSetMoving(false);
            m_controller.FunLookTo(m_posResource);
            m_data.FunSetAnimState(TypeUnitAnimState.Worker);
            m_audio.FunPlayAudio(TypeAudioUnit.Worker);
            m_controller.FunStartCoroutine(WorkerExtractResource());
        }

        // Công nhân thực hiện công việc thu hoạch tài nguyên.
        // --------------------------------------------------
        private IEnumerator WorkerExtractResource()
        {
            yield return new WaitForSeconds(m_timeExploit);

            m_audio.FunStopAudio();
            m_currExploitRes = m_resourceComp.FunExploitResources(m_maxExploitRes);
            StartMoveToTownhall();
        }

        // Công nhận thực hiện đưa tài nguyên cho công trình xử lý tài nguyên.
        // ------------------------------------------------------------------
        private IEnumerator WorkerBringResourceToBuilding()
        {
            yield return new WaitForSeconds(m_timeReturnTownhall);
            m_townhallResComp.FunHandleResource(m_currExploitRes);
            StartMoveToResource();
        }

        // Cập nhật dữ liệu công trình thu thập và khai thác tài nguyên cho công nhân.
        // ---------------------------------------------------------------------------
        private bool UpdateDataTownhall()
        {   
            var workerComp = m_owner.GetComponent<UnitWorkerComp>();
            if (workerComp == null)
            {
                DebugUtils.FunLogError("Lỗi đối tượng worker này không có thành phần 'UnitWorkerComp'.");
                return false;
            }
            m_maxExploitRes = workerComp.FunGetMaxExploitRes();

            m_objectTownhall = workerComp.FunGetTownhall();
            if (m_objectTownhall == null)
            {
                DebugUtils.FunLogError("Công trình Townhall mà unit worker thuộc về là không tồn tại.");
                return false;
            }
            m_posTownhall = m_objectTownhall.transform.position;

            m_townhallResComp = m_objectTownhall.GetComponent<BuildingResourceComp>();
            if (m_townhallResComp == null)
            {
                DebugUtils.FunLogError("Lỗi công trình không có thành phần xử lý tài nguyên.");
                return false;
            }

            return true;
        }

        // Cập nhật dữ liệu tài nguyên khai thác cho công nhân.
        // ----------------------------------------------------
        private bool UpdateDataResource()
        {
            // Lấy vị trí và đối tượng tài nguyên để công nhân khai thác tài nguyên.
            m_objectResource = m_worker.FunGetNearestResourceMinerals();
            if (m_objectResource == null)
            {
                DebugUtils.FunLog("Không còn tài nguyên nào để khai thác.");
                return false;
            }
            m_posResource = m_objectResource.transform.position;

            m_resourceComp = m_objectResource.GetComponent<ResourceMineralsObjectRTS>();
            if (m_resourceComp == null)
            {
                DebugUtils.FunLog("Lỗi, tài nguyên này không có thành phần quản lý tài nguyên.");
                return false;
            }

            var resourceDestroy = m_objectResource.GetComponent<ResourceMineralsDestroy>();
            if (resourceDestroy == null)
            {
                DebugUtils.FunLog("Tài nguyên không có thành phần phá hủy đối tượng.");
                return false;
            }
            resourceDestroy.FunAddActionDestroy(ResourceDestroy);

            return true;
        }

        // Đăng ký hành động khi đối tượng bị hủy.
        // --------------------------------------
        private void ResourceDestroy()
        {
            if (UpdateDataResource() == false)
            {
                var stateOwnerComp = m_owner.GetComponent<UnitStateComp>();
                if (stateOwnerComp == null)
                {
                    DebugUtils.FunLogError("Lỗi đối tượng không có thành phần quản lý trạng thái");
                    return;
                }
                stateOwnerComp.FunChangeState(TypeRaceUnitBase.Free);
            }
            
            if (m_currExploitRes > 0)
                StartMoveToTownhall();
            else 
                StartMoveToResource();
        }

        private void StartMoveToResource()
        {
            m_isExploitingResources = false;
            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_posResource);
            m_controller.FunSetOnTriggerEnter(m_objectResource, () => { m_isHandleResource = true; });
        }

        private void StartMoveToTownhall()
        {
            m_isExploitingResources = true;
            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_posTownhall);
            m_controller.FunSetOnTriggerEnter(m_objectTownhall, () => { m_isHandleBuilding = true; });
        }
    }
}