// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Xử lý hiệu ứng Fog of War trên bản đồ, dữ liệu thu được sẽ được gửi cho shader sử lý.
//     /// </summary>
//     [AddComponentMenu("FireNBM/Effects/ForOfWar")]
//     public class FogOfWar : MonoBehaviour
//     {
//         [SerializeField] Material m_material;           // Dùng để áp dụng hiệu ứng Fog Of War 
//         [SerializeField] bool m_useFogOfWar;            // Có nên áp dụng hiệu ứng hay ko.
//         [SerializeField] float m_radiusUnit;            // Bán kính hiệu ứng.

//         private float m_timeCount;                      // Theo dõi tiến trình cập nhật.
//         private readonly float TimeUpdate = 0.1f;       // Thời gian dùng để cập nhật định kỳ. 

//         private Texture2D m_arrPosUnits;                // Lưu trữ vị trí của các unit.
//         private Texture2D m_arrVisionRadiusesUnits;     // Lưu trữ bán kính của unit.
    
//         // Lấy các id của shader sử dụng fog of war.
//         private readonly int m_idMaxUnits                  = Shader.PropertyToID("_MaxUnits");
//         private readonly int m_idActualUnitsCount          = Shader.PropertyToID("_ActualUnitsCount");
//         private readonly int m_idFOWVisionRadiusesTexture  = Shader.PropertyToID("_FOWVisionRadiusesTexture");
//         private readonly int m_idFOWPositionsTexture       = Shader.PropertyToID("_FOWPositionsTexture");


//         /// <summary>
//         ///     Giới hạn số lượng unit có thể hiển thị trên bản đồ. </summary>
//         public const int UNITS_LIMIT = 1000;



//         // ---------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // /////////////////////////////////////////////////////////////////////////////////

//         private void Awake()
//         {
//             m_timeCount = 0f;
//             m_material.SetFloat("_Enabled", m_useFogOfWar ? 1f : 0f);
//             Shader.SetGlobalFloat(m_idMaxUnits, UNITS_LIMIT);

//             m_arrPosUnits            = new Texture2D(UNITS_LIMIT, 1, TextureFormat.RGBAFloat, false, true);
//             m_arrVisionRadiusesUnits = new Texture2D(UNITS_LIMIT, 1, TextureFormat.RFloat, false, true);
//         }

//         private void Update()
//         {
//             if (m_timeCount > 0f)
//             {
//                 m_timeCount -= Time.deltaTime;
//                 return;
//             }

//             m_timeCount = TimeUpdate;
//             RecalculateUnitsVisibilityInFOW();
//         }


//         // -----------------------------------------------------------------------------
//         // FUNSTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////

//         // Tính toán lại hiệu ứng fog of war trên map chính.
//         private void RecalculateUnitsVisibilityInFOW()
//         {
//             var unitsPlayer = UnitManager.Instance.FunGetAllUnitActive(TypeController.Player);
//             for (int i = 0; i < unitsPlayer.Count; ++i)
//             {
//                 if (i > UNITS_LIMIT)
//                     break;
                
//                 // Chuyển đổi vị trí của đơn vị thành dữ liệu màu sắc
//                 var pos = unitsPlayer[i].transform.position;
//                 var posColor = new Color(pos.x/1024f, pos.y/1024f, pos.z/1024f, 1f);
//                 m_arrPosUnits.SetPixel(i, 0, posColor);

//                 // Chuyển đổi bán kính tầm nhìn thành dữ liệu màu sắc.
//                 m_arrVisionRadiusesUnits.SetPixel(i, 0, new Color(m_radiusUnit/512f, 0f, 0f, 0f));
//             }

//             // Cập nhật lại dữ liệu mới.
//             m_arrPosUnits.Apply();
//             m_arrVisionRadiusesUnits.Apply();

//             // Truyền dữ liệu vào shader để GPU xử lý dữ liệu.
//             Shader.SetGlobalFloat(m_idActualUnitsCount, unitsPlayer.Count);
//             Shader.SetGlobalTexture(m_idFOWPositionsTexture, m_arrPosUnits);
//             Shader.SetGlobalTexture(m_idFOWVisionRadiusesTexture, m_arrVisionRadiusesUnits);
//         }
//     }
// }