using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Hệ thống quản lý Factory (xưởng sản xuất) cho các chủng tộc (Race) trong game RTS.
    ///     Mỗi chủng tộc có một Factory cụ thể để xử lý logic khởi tạo, cấu hình và các hành động liên quan.
    /// </summary>
    public class RaceFactorySystem
    {
        private Dictionary<TypeRaceRTS, IRaceTypeFactory> m_factoryMap;


        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Hàm khởi tạo hệ thống Factory cho các chủng tộc.
        ///     Tạo bản đồ giữa loại chủng tộc (TypeRaceRTS) và Factory tương ứng. </summary>
        /// ---------------------------------------------------------------------------------
        public RaceFactorySystem()
        {
            m_factoryMap = new Dictionary<TypeRaceRTS, IRaceTypeFactory>
            {
                { TypeRaceRTS.Terran, new RaceTerrainFactory() },
                { TypeRaceRTS.Zerg,   new RaceZergFactory()    },
            };
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo dữ liệu cho tất cả các chủng tộc từ danh sách RaceDataSO.
        ///     Xử lý việc khởi tạo dữ liệu cụ thể cho từng chủng tộc dựa trên Factory tương ứng. </summary>
        /// ------------------------------------------------------------------------------------------------
        public bool FunInitializeData(List<RaceDataSO> dataRaces)
        {
            foreach (var dataRace in dataRaces)
            {
                var factoryRace = FunGetRaceFactory(dataRace.NameRaceRTS);
                if (factoryRace == null)
                {
                    Debug.LogError($"Error: Could not found factory for: {dataRace.NameRaceRTS} to Initialize Data");
                    return false;
                }
                factoryRace.FunInitializeDataRace(dataRace);
            }
            return true;
        }
        
        /// <summary>
        ///     Tạo một đơn vị (unit) dựa trên loại chủng tộc (TypeRaceRTS).
        ///     Hàm sẽ kiểm tra và gọi Factory tương ứng để xử lý logic tạo đơn vị. </summary>
        /// ---------------------------------------------------------------------------------- 
        public GameObject FunCreateUnitRace(TypeNameUnit name, TypeRaceUnit typeUnit, TypeRaceRTS raceRTS)
        {
            return PerformRaceAction(raceRTS, factory => 
            {
                GameObject unitSpawn = factory.FunGetUnitManagerFactory().FunCreateUnitRace(name, typeUnit);
                if (unitSpawn == null)
                {
                    Debug.LogWarning($"In the race '{raceRTS}',  don't have object unit with name '{name}' to create");
                    return null;
                }
                return unitSpawn;

            }, "create unit");
        }  

        /// <summary>
        ///     Lấy đối tượng tòa nhà (building) dựa trên loại chủng tộc (TypeRaceRTS).
        ///     Hàm này sử dụng Factory tương ứng để xử lý logic tạo tòa nhà. </summary>
        /// --------------------------------------------------------------------------- 
        public PairObjectBuiding FunGetBuildingRace(TypeNameBuilding name, TypeRaceBuilding typeBuilding, TypeRaceRTS raceRTS)
        {
            return PerformRaceAction(raceRTS, factory =>
            {
                var buildingSpawn = factory.FunGetBuildingManagerFactory().FunCreateBuildingRace(name, typeBuilding);
                if (buildingSpawn == null)
                {
                    Debug.LogWarning($"In the race '{raceRTS}',  don't have object unit with name '{name}' to create");
                    return null;
                }
                return buildingSpawn;

            }, "create building");
        }   

        /// <summary>
        ///     Hủy một đơn vị (unit) đã tạo từ trước đó.
        ///     Hàm kiểm tra và sử dụng Factory phù hợp để xử lý logic hủy. </summary>
        /// -------------------------------------------------------------------------- 
        public bool FunDisposeUnitRace(GameObject unit, TypeRaceUnit typeUnit, TypeRaceRTS raceRTS)
        {
            return PerformRaceAction(raceRTS, factory => 
            {
                if (factory.FunGetUnitManagerFactory().FunDisposeUnitRace(unit, typeUnit) == false)
                {
                    Debug.LogWarning($"Unable to dispose the unit object belonging to the race '{raceRTS}' with the name '{unit.name}");
                    return false;
                }
                return true;

            }, "dispose unit");
        }

        /// <summary>
        ///     Hủy một tòa nhà (building) đã tạo từ trước đó.
        ///     Hàm sử dụng Factory tương ứng để xử lý logic hủy. </summary>
        /// ---------------------------------------------------------------- 
        public bool FunDisposeBuildingRace(GameObject building, TypeRaceBuilding typeBuilding, TypeRaceRTS raceRTS)
        {
            return PerformRaceAction(raceRTS, factory => 
            {
                if (factory.FunGetBuildingManagerFactory().FunDisposeBuildingRace(building, typeBuilding) == false)
                {
                    Debug.LogWarning($"Unable to dispose the building object belonging to the race '{raceRTS}' with the name '{building.name}");
                    return false;
                }
                return true;

            }, "dispose building");
        }  

        public bool FunDisposeUnderConstructionRace(GameObject underConstruction, TypeRaceBuilding typeBuilding, TypeRaceRTS raceRTS)
        {
            return PerformRaceAction(raceRTS, factory => 
            {
                if (factory.FunGetBuildingManagerFactory().FunDisposeUnderConstructionRace(underConstruction, typeBuilding) == false)
                {
                    Debug.LogWarning($"Unable to dispose the building object belonging to the race '{raceRTS}' with the name '{underConstruction.name}");
                    return false;
                }
                return true;

            }, "dispose UnderConstruction");
        }  

        /// <summary>
        ///     Lấy Factory tương ứng với một loại chủng tộc (TypeRaceRTS).
        ///     Nếu không tìm thấy Factory, sẽ trả về null và ghi log lỗi.
        /// </summary>
        public IRaceTypeFactory FunGetRaceFactory(TypeRaceRTS typeRace)
        {
            if (m_factoryMap.TryGetValue(typeRace, out IRaceTypeFactory factory) == false)
            {
                Debug.LogError($"Error: No have Factory Race for {typeRace}");
                return null;
            }
            return factory;
        }  


        // --------------------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Hàm trợ giúp mục đích tìm chủng tộc thể thực hiện hành động được định nghĩa.
        /// </summary>
        private T PerformRaceAction<T>(TypeRaceRTS raceRTS, Func<IRaceTypeFactory ,T> action, string logAction)
        {
            var factory = FunGetRaceFactory(raceRTS);
            if (factory == null)
            {
                Debug.LogWarning($"There is no factory for the race '{raceRTS}' to {logAction}.");
                return default;
            }
            return action(factory);
        }
    }
}
