// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;
// using System;

// namespace FireNBM
// {
//     [CustomEditor(typeof(RaceDataSO))]
//     public class RaceDataSOEditor : Editor
//     {
//         private void OnEnable()
//         {
//             var targetData = (RaceDataSO)target;   
//             SetDataUnitDefault(targetData);
//             SetDataBuildingDefault(targetData);
//         }

//         private void Reset()
//         {
//             var targetData = (RaceDataSO)target;   
//             SetDataUnitDefault(targetData);
//             SetDataBuildingDefault(targetData);
//         }

//         public override void OnInspectorGUI()
//         {
//             GUIStyleCustom.Label.FunSetTitleScript("Race RTS Spawn");
//             var targetData = (RaceDataSO)target;   

//             // For Unit
//             // --------
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("List Race Unit Data Spawn", -3, TextAnchor.LowerLeft, 0);
//             GUIStyleCustom.Label.FunSetTitleGroupBox("-------------------------", -3, TextAnchor.LowerLeft);

//             // Duyệt qua từng phần tử trong danh sách
//             for (int i = 0; i < targetData.ListRaceUnitData.Count; i++)
//             {
//                 GUILayout.BeginVertical("", "GroupBox");
//                 EditorGUILayout.BeginVertical("box");
//                 GUILayout.Label(targetData.ListRaceUnitData[i].TypeNameUnit.ToString(), EditorStyles.boldLabel);
//                 EditorGUILayout.EndVertical();  

//                 EditorGUILayout.BeginHorizontal();
//                 GUILayout.Space(40);
//                 GUILayout.BeginVertical();

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Size Pool", 85);
//                 targetData.ListRaceUnitData[i].SizePool = EditorGUILayout.IntField(GUIContent.none, targetData.ListRaceUnitData[i].SizePool);
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Data Spawn", 85);
//                 targetData.ListRaceUnitData[i].UnitDataSpawn = (SpawnDataUnitSO)EditorGUILayout.ObjectField(
//                     GUIContent.none, 
//                     targetData.ListRaceUnitData[i].UnitDataSpawn, 
//                     typeof(SpawnDataUnitSO), 
//                     false
//                 );
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.EndVertical();  
//                 EditorGUILayout.EndHorizontal();

//                 GUILayout.EndVertical();
//                 GUILayout.Space(7);
//             }
//             GUILayout.EndVertical();


//             // For Building
//             // ------------
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("List Race Building Data Spawn", -3, TextAnchor.LowerLeft, 0);
//             GUIStyleCustom.Label.FunSetTitleGroupBox("-----------------------------", -3, TextAnchor.LowerLeft);

//             // Duyệt qua từng phần tử trong danh sách
//             for (int i = 0; i < targetData.ListRaceBuildingData.Count; i++)
//             {
//                 GUILayout.BeginVertical("", "GroupBox");
//                 EditorGUILayout.BeginVertical("box");
//                 GUILayout.Label(targetData.ListRaceBuildingData[i].TypeNameBuilding.ToString(), EditorStyles.boldLabel);
//                 EditorGUILayout.EndVertical();  

//                 EditorGUILayout.BeginHorizontal();
//                 GUILayout.Space(40);
//                 EditorGUILayout.BeginVertical("box");

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Size Pool", 85);
//                 targetData.ListRaceBuildingData[i].SizePool = EditorGUILayout.IntField(GUIContent.none, targetData.ListRaceBuildingData[i].SizePool);
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Data Spawn", 85);
//                 targetData.ListRaceBuildingData[i].BuildingDataSpawn = (SpawnDataBuildingSO)EditorGUILayout.ObjectField(
//                     GUIContent.none, 
//                     targetData.ListRaceBuildingData[i].BuildingDataSpawn, 
//                     typeof(SpawnDataBuildingSO), 
//                     false
//                 );
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.EndVertical();  
//                 EditorGUILayout.EndHorizontal();

//                 GUILayout.EndVertical();
//                 GUILayout.Space(7);
//             }
//             GUILayout.EndVertical();



//             if (GUI.changed)
//             {
//                 EditorUtility.SetDirty(targetData);     // Thông báo nó đã thay đổi và chưa được lưu.
//                 AssetDatabase.SaveAssets();             // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//                 AssetDatabase.Refresh();                // Báo cho Unity Editor biết và lưu chúng.
//             }
//         }

//         private void SetDataUnitDefault(RaceDataSO targetData)
//         {
//             var sizeTypeUnit = Enum.GetValues(typeof(TypeRaceUnit)).Length;
//             if (targetData.ListRaceUnitData.Count >= (sizeTypeUnit - 2)) // Trừ None, Base.
//                 return;

//             var dataListUnit = Enum.GetNames(typeof(TypeRaceUnit));
//             foreach (var value in dataListUnit)
//             {
//                 if (value == "None" || value == "Base")
//                     continue;
                
//                 var unitData = new RaceUnitDataHelper();
//                 unitData.TypeNameUnit = (TypeRaceUnit)Enum.Parse(typeof(TypeRaceUnit), value);
//                 targetData.ListRaceUnitData.Add(unitData);
//             } 
//         }

//         private void SetDataBuildingDefault(RaceDataSO targetData)
//         {
//             var sizeTypeBuilding = Enum.GetValues(typeof(TypeRaceBuilding)).Length;
//             if (targetData.ListRaceBuildingData.Count >= (sizeTypeBuilding - 1))
//                 return;

//             var dataListBuilding = Enum.GetNames(typeof(TypeRaceBuilding));
//             foreach (var value in dataListBuilding)
//             {
//                 if (value == "None") 
//                     continue;
                
//                 var BuildingData = new RaceBuildingDataHelper();
//                 BuildingData.TypeNameBuilding = (TypeRaceBuilding)Enum.Parse(typeof(TypeRaceBuilding), value);
//                 targetData.ListRaceBuildingData.Add(BuildingData);
//             } 
//         }
//     }
// }