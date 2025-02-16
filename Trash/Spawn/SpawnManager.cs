// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using FireNBM.Pattern;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Một lớp quản lý đối tượng được sản sinh sử dụng ObjectPool 
//     /// </summary>
//     public class SpawnManager
//     {
//         private Dictionary<Enum, SpawnData> m_mapTypeToSpawn;     
        
//         // Một hệ thống sản sinh đối tượng có dữ liệu dùng chung.
//         private FlyweightFactory m_flyweightSpawn;                  

//         private static SpawnManager m_instance;
//         public static SpawnManager Instance { get => m_instance; }


//         // --------------------------------------------------------------
//         // CONSTRUCTOR
//         // -----------
//         //////////////////////////////////////////////////////////////////

//         public SpawnManager()
//         {
//             m_mapTypeToSpawn = new Dictionary<Enum, SpawnData>();
//         }


//         // -----------------------------------------------------------------------------------
//         // FUNCTION PUBLIC
//         // ---------------
//         // ////////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Lấy một đối tượng mới từ spawn Unit. </summary>
//         /// ---------------------------------------------------
//         public GameObject FunSpawnUnit(TypeNameUnit typeName)
//         {
//             if (m_mapTypeToSpawn.TryGetValue(typeName, out SpawnData spawnObject) == false)
//             {
//                 Debug.Log($"No unit spawn object the enum type: '{typeName}' that you provided.");
//                 return null;
//             }

//             GameObject newSpawnUnit = null;
//             if (spawnObject is SpawnDataUnitSO spawnObjectUnit)
//             {
//                 newSpawnUnit = m_flyweightSpawn.FunSpawn(spawnObjectUnit.PrefabUnit);
//                 UnitDataComp unitComp = newSpawnUnit.GetComponent<UnitDataComp>();
//                 if (unitComp == null)
//                     newSpawnUnit.AddComponent<UnitDataComp>();
                
//                 unitComp.FunSetData(spawnObjectUnit.Data);
//                 UnitManager.Instance.FunAddUnitActive(newSpawnUnit, unitComp.Controller);
//             }
//             return newSpawnUnit;
//         }

//         /// <summary>
//         ///     Lấy một đối tượng mới từ spawn Building. </summary>
//         /// -------------------------------------------------------
//         public GameObject FunSpawnBuilding(TypeNameBuilding typeName)
//         {
//             if (m_mapTypeToSpawn.TryGetValue(typeName, out SpawnData spawnObject) == false)
//             {
//                 Debug.Log($"No building spawn object the enum type: '{typeName}' that you provided.");
//                 return null;
//             }

//             GameObject newSpawnBuilding = null;
//             if (spawnObject is SpawnDataBuildingSO spawnObjectBuilding)
//             {
//                 newSpawnBuilding = m_flyweightSpawn.FunSpawn(spawnObjectBuilding.PrefabBuilding);
//             }
//             return newSpawnBuilding;
//         }




//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         // Thiết lập dữ liệu được cung cấp vào map để giúp dễ dàng cho quá trình truy vấn.
//         // -------------------------------------------------------------------------------
//         private void SetDataToMap()
//         {
//             // SetUnitDataToMap(m_unitPlayerSpawns);
//             // SetUnitDataToMap(m_unitAISpawns);

//             // SetBuildingDataToMap(m_buildingPlayerSpawns);
//             // SetBuildingDataToMap(m_buildingAISpawns);
//         }

//         // Thêm dữ liệu từ danh sách unit vào map.
//         // private void SetUnitDataToMap(List<SpawnDataUnitHelper> spawnDataUnits)
//         // {
//         //     foreach (var unit in spawnDataUnits)
//         //     {
//         //         if (m_mapTypeToSpawn.ContainsKey(unit.TypeName) == false && unit.DataSpawn != null)
//         //         {
//         //             m_mapTypeToSpawn.Add(unit.TypeName, unit.DataSpawn);
//         //             m_flyweightSpawn.FunPrespawn(unit.DataSpawn.PrefabUnit, unit.SizePool);
//         //         }
//         //         Debug.LogWarning($"The Unit object for the enum type '{unit.TypeName}' already exists in the spawn map." + 
//         //                           "Or File DataSpawnUnitSO is NULL. Skipping...");
//         //     }
//         // }

//         // // Thêm dữ liệu từ danh sách building vào map.
//         // private void SetBuildingDataToMap(List<SpawnDataBuildingHelper> spawnDataBuildings)
//         // {
//         //     foreach (var building in spawnDataBuildings)
//         //     {
//         //         if (m_mapTypeToSpawn.ContainsKey(building.TypeName) == false && building.DataSpawn != null)
//         //         {
//         //             m_mapTypeToSpawn.Add(building.TypeName, building.DataSpawn);
//         //             m_flyweightSpawn.FunPrespawn(building.DataSpawn.PrefabBuilding, building.SizePool);
//         //         }
//         //         Debug.LogWarning($"The Building object for the enum type '{building.TypeName}' already exists in the spawn map." + 
//         //                           "Or File DataSpawnBuildingSO is NULL. Skipping...");
//         //     }
//         // }


//         // private void Start()
//         // {
//         //     m_flyweightSpawn = FlyweightFactory.Instance;
//         //     if (m_flyweightSpawn == null)
//         //     {
//         //         Debug.LogError("Error, The variable 'FlyweightFactory.Instance' is NULL!");
//         //         return;
//         //     }
//         //     m_mapTypeToSpawn = new Dictionary<Enum, SpawnData>();
//         //     SetDataToMap();
//         // }
//     }
// }