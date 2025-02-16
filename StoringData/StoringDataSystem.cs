using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Được dùng để khởi tạo dữ liệu cho trận đấu.
    /// </summary>
    [AddComponentMenu("FireNBM/System/Storing Data System")]
    public class StoringDataSystem : Singleton<StoringDataSystem>
    {
        [SerializeField] private StorageSO m_data;

        // TODO: Sẽ triển khai dữ liệu dùng chung ở đây.
        public static StoringDataSystem Instance { get => InstanceSingletonInScene; } 


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            if (InitializeData() == false)
            {
                Debug.LogError("Lỗi khi thiết lập dữ liệu cho trận đấu. Vui lòng kiểm tra lại.");
                return;
            }
        }


        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy Action dùng chung của các unit. </summary>
        /// --------------------------------------------------
        public ActionData FunGetUnitActionDefault() => m_data.UnitActionDefault;

        /// <summary>
        ///     Lấy Action dùng chung của các building. </summary>
        /// ------------------------------------------------------
        public ActionData FunGetBuildingActionDefault() => m_data.BuildingActionDefault;


        // ---------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo dữ liệu cho trận đấu. </summary>
        /// ---------------------------------------------
        private bool InitializeData()
        {
            // Khởi tạo dữ liệu cho các chủng tộc.
            if (FactorySystem.Instance.FunInitializeDataRace(m_data.DataRaces) == false)
                return false;

            return true;
        }
    }
}