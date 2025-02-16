// using System.Collections.Generic;
// using UnityEngine;
// using FireNBM.Pattern;

// namespace FireNBM.UI
// {
//     /// <summary>
//     ///     Tạo hiệu ứng fog of war trên minimap.
//     /// </summary>
//     [AddComponentMenu("FireNBM/Effects/Minimap/Minimap FogOfWar Comp")]
//     public class MinimapFogOfWarComp : MonoBehaviour
//     {
//         // Material chứa hiệu ứng.
//         [SerializeField] private Material m_minimapFowMaterial;             

//         // Biến đếm và lượng thời gian cập nhật định kỳ.
//         private float m_timeCount;
//         private readonly float TIME_UPDATE = 0.5f;

//         // Lưu trữ các vị trí của unit để hiện thị vùng nhìn thấy trên bản đồ minimap.
//         private Texture2D m_arrPos;       

//         // lấy tham chiếu đến id của shader.
//         private readonly int m_IdFOWMinimapPosTexture = Shader.PropertyToID("_FOWMinimapPositionsTexture");



//         // ---------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // /////////////////////////////////////////////////////////////////////////////////

//         private void Awake()
//         {
//             m_timeCount = 0f;
//             m_arrPos = new Texture2D(FogOfWar.UNITS_LIMIT, 1, TextureFormat.RGBAFloat, false, true);

//             // Thiết lập các giá trị cho shader.
//             m_minimapFowMaterial.SetFloat("_Enabled", 1f);
//             m_minimapFowMaterial.SetFloat("_MapSize", 512);
//             m_minimapFowMaterial.SetFloat("_MinimapSize", 256);
//         }

//         private void Update()
//         {
//             // Chỉ cập nhật trong một khoảng thời gian nhất định.
//             if (m_timeCount >= TIME_UPDATE)
//             {
//                 m_timeCount -= Time.deltaTime;
//                 return;
//             }

//             m_timeCount = TIME_UPDATE;
//             RecalculateUnitsVisibilityInFOW();
//         }

//         // -----------------------------------------------------------------------------
//         // PUNSTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Tính toán và cập nhật lại các đơn vị có thể nhìn thấy trong FOW trên minimap. </summary>
//         private void RecalculateUnitsVisibilityInFOW()
//         {
//             var unitsPlayer = UnitManager.Instance.FunGetAllUnitActive(TypeController.Player);
//             for (int i = 0; i < unitsPlayer.Count; ++i)
//             {
//                 if (i >= FogOfWar.UNITS_LIMIT)
//                     break;
                
//                 // var icon = m_minimap.FUn;

                
//             }

//             // Cập nhật giá trị cho shader.
//             m_arrPos.Apply();
//             Shader.SetGlobalTexture(m_IdFOWMinimapPosTexture, m_arrPos);
//         }



//         // -----------------------------------------------------------------------------
//         // HANDLE MESSAGE
//         // --------------
//         // //////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Thêm unit mới của người chơi vào FOW minimap. </summary>
//         private bool OnAddUnitToListFOW(IMessage message)
//         {
//             return true;
//         }
//     }
// }