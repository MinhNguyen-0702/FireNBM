using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Đại diện cho một nhà máy sản xuất đối tượng unit theo công việc của nó.
    /// </summary>
    public abstract class UnitRaceTypeFactory
    {
        private UnitRaceDataSO m_dataCurrent;                           
        private Dictionary<TypeNameUnit, UnitRaceDataSO> m_dataMap;     

        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public UnitRaceTypeFactory()
        {
            m_dataMap = new Dictionary<TypeNameUnit, UnitRaceDataSO>();
            m_dataCurrent = null;
        }

        // --------------------------------------------------------------------------------
        // FUNSTION ABSTRACT
        // -----------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo dữ liệu đặc trưng cho loại unit cụ thể (Hàm cần được override bởi lớp con). </summary>
        /// -----------------------------------------------------------------------------------------------
        protected abstract bool InitializeDataTypeUnit(GameObject newUnit);



        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo dữ liệu từ danh sách các đối tượng UnitRaceDataSO. </summary>
        /// --------------------------------------------------------------------------
        public bool FunInitializeData(List<UnitRaceDataSO> listUnitDataSO)
        {
            // Gắn lắng nghe sự kiện để xử lý thông báo khởi tạo unit.
            MessagingSystem.Instance.FunAttachListener(typeof(MessageGetNewObject), OnGetObjectUnitToInitBase);
            foreach (var unitDataSO in listUnitDataSO)
            {
                if (unitDataSO == null)
                {
                    DebugUtils.FunLogError("Lỗi, dữ liệu đưa vào không tồn tại, vui lòng kiểm tra phần nhập dữ liệu");
                    return false;
                }

                if (m_dataMap.ContainsKey(unitDataSO.FlyweightData.NameUnit) == false)
                {
                    m_dataCurrent = unitDataSO;
                    m_dataMap.Add(unitDataSO.FlyweightData.NameUnit, unitDataSO);
                    PrefabPoolingSystem.FunPrespawn(unitDataSO.Prefab, unitDataSO.SizePool);
                }
                else
                    Debug.LogWarning("Dữ liệu unit đã tồn tại: " + unitDataSO.FlyweightData.NameUnit);
            }
            // Bỏ gắn lắng nghe sự kiện sau khi hoàn thành khởi tạo.
            MessagingSystem.Instance.FunDetachListener(typeof(MessageGetNewObject), OnGetObjectUnitToInitBase);
            return true;
        }

        /// <summary>
        ///     Tạo một unit mới dựa trên tên của unit. </summary>
        /// ------------------------------------------------------
        public GameObject FunCreateUnit(TypeNameUnit name)
        {
            if (m_dataMap.TryGetValue(name, out var dataUnit) == false)
                return null;

            
            var newSpawn = PrefabPoolingSystem.FunSpawn(dataUnit.Prefab);
            var unitDataComp = newSpawn.GetComponent<UnitDataComp>();
            unitDataComp.FunSetData(m_dataCurrent.Data);

            return newSpawn;
        }

        /// <summary>
        ///     Hủy một unit đã được tạo. </summary>
        /// ----------------------------------------
        public bool FunDisposeUnit(GameObject unit)
        {
            if (PrefabPoolingSystem.FunDespawn(unit) == false)
            {
                Debug.LogWarning("Không thể hủy unit: " + unit.name);
                return false;
            }
            return true;
        }

        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER 
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        // Hàm xử lý khi nhận thông báo khởi tạo unit mới.
        // ----------------------------------------------
        private bool OnGetObjectUnitToInitBase(IMessage message)
        {
            var messageResult = message as MessageGetNewObject;
            if (m_dataCurrent == null || m_dataCurrent.Prefab != messageResult.Prefab)
            {
                Debug.LogError("Dữ liệu unit không khớp với thông báo.");
                return false;
            }

            GameObject unit = messageResult.NewObject;
            if (SetDataUnitInitializeBase(unit, messageResult.Prefab) == false)
            {
                DebugUtils.FunLog("Thiết lập dữ liệu cho unit được tạo ra thất bại.");
                return false;
            }

            // Khởi tạo dữ liệu cụ thể của loại unit.
            InitializeDataTypeUnit(unit);
            return true;
        }

        // Thiết lập dữ liệu cơ bản cho một unit cần có.
        // ---------------------------------------------
        private bool SetDataUnitInitializeBase(GameObject unit, GameObject prefab)
        {
            // Lấy dữ liệu dùng chung cho unit.
            if (unit.TryGetComponent<UnitFlyweightComp>(out var unitFlyweight) == false)
                unitFlyweight = unit.AddComponent<UnitFlyweightComp>();

            unitFlyweight.FunSetDataFlyweight(m_dataCurrent.FlyweightData);

            var unitActionComp = unit.GetComponent<UnitActionComp>();
            unitActionComp.FunSetActionData(m_dataCurrent.ActionData);

            var unitStateComp = unit.GetComponent<UnitStateComp>();
            unitStateComp.FunRegisterState(new StateUnitBaseFree(unit));
            
            return true;
        }
    }
}
