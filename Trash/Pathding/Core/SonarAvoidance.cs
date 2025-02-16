// using System.Diagnostics;
// using System.Runtime.CompilerServices;
// using Unity.Collections;
// using Unity.Mathematics;
// using UnityEngine;

// namespace FireNBM.PathDing
// {
//     /// <summary>
//     ///     Hỗ trợ việc xây dựng hệ thống tránh chướng ngại vật trong một không gian cụ thể (cục bộ)
//     ///     và tìm hướng gần nhất.
//     /// </summary>
//     public class SonarAvoidance : MonoBehaviour
//     {





//         // ---------------------------------------------------------------------------------
//         // CONSTRUCTOR
//         // -----------
//         // /////////////////////////////////////////////////////////////////////////////////


//         /// <summary>
//         ///     Khởi tạo đối tượng bằng cách tạo bản sao độc lập với đối tượng gốc.
//         /// </summary>
//         /// 
//         /// <param name="other"     > Đối tượng cần được sao chép. </param>
//         /// <param name="allocator" > Loại bộ nhớ cần cấp phát.    </param>
//         /// --------------------------------------------------------------
//         public SonarAvoidance(in SonarAvoidance other, Allocator allocator = Allocator.Temp)
//         {
//         }

//         /// <summary>
//         ///     Khởi tạo đối tượng bằng cách sử dụng vị trí, hướng và bán kính.
//         /// </summary>
//         /// 
//         /// <param name="position"      > Vị trí của đối tượng cần sử dụng sonar. </param>
//         /// <param name="direction"     > Hướng mà sonar đang hướng tới (sử dụng trục x). </param>
//         /// <param name="up"            > Hướng lên trên của sonar (tính theo trục y). </param>
//         /// <param name="innerRadius"   > Bán kích tối thiểu mà sonar bắt đầu phát hiện chướng ngại vật, cũng để tính kích thước mà con đường nó đi qua. </param>
//         /// <param name="outerRadius"   > Bán kích tối đa mà sonar có thể phát hiện trướng ngại vật. </param>
//         /// <param name="speed"         > Tốc độ đi chuyển của sonar. </param>
//         /// <param name="allocator"     > Loại mà để cấp phát bộ nhớ, mặc định là 'Temp'.</param>
//         /// -------------------------------------------------------------------------------------
//         public SonarAvoidance(float3 position, float3 direction, float3 up, float3 innerRadius, 
//                               float outerRadius, float speed, Allocator allocator = Allocator.Temp)
//         {
//         }

//         /// <summary>
//         ///     Khởi tạo đối tượng bằng cách sử dụng vị trí, góc quay và bán kính.
//         /// </summary>
//         /// 
//         /// <param name="position"      > Vị trí của đối tượng cần sử dụng sonar.</param>
//         /// <param name="rotation"      > Góc quay của Sonar. </param>
//         /// <param name="innerRadius"   > Bán kính tối thiểu từ đó sonar sẽ theo dõi chướng ngại vật và cũng được sử dụng cho kích thước đường đi. </param>
//         /// <param name="outerRadius"   > Bán kính tối đa từ đó sonar sẽ theo dõi chướng ngại vật. </param>
//         /// <param name="speed"         > Tốc độ của sonar. </param>
//         /// <param name="allocator"     > Loại mà để cấp phát bộ nhớ, mặc định là 'Temp'. </param>
//         public SonarAvoidance(float3 position, quaternion rotation, float innerRadius, float outerRadius, 
//                               float speed, Allocator allocator = Allocator.Temp)
//         {
//         }

//         /// <summary>
//         ///     Khởi tạo đối tượng mà không có thuộc tính. Cần sử dụng hàm 'FunSet' để thiết lập 
//         ///     thuộc tính cho đối tượng sonar.
//         /// </summary>
//         /// 
//         /// <param name="allocator"> Loại bộ cấp phát bộ nhớ. </param>
//         public SonarAvoidance(Allocator allocator)
//         {
//         }





//         // ---------------------------------------------------------------------------------
//         // FUNSTION PUBLIC
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Thiết lập dữ liệu bằng các sử dụng vị trí, hướng và bán kính. </summary>
//         /// ----------------------------------------------------------------------------
//         public void FunSet(float3 position, float3 direction, float3 up, float innerRadiuse, float outerRadius, float speed)
//         {
//         }

//         /// <summary>
//         ///     Thiết lập dữ liệu bằng các sử dụng vị trí, góc quay và bán kính. </summary>
//         /// -------------------------------------------------------------------------------
//         public void FunSet(float3 position, quaternion rotation, float innerRadiuse, float outerRadius, float speed)
//         {
//         }

//         /// <summary>
//         ///     Loại bỏ tất cả trướng ngại vật được đưa vào. </summary>
//         /// -----------------------------------------------------------
//         public void FunClear()
//         {
//         }

//         /// <summary>
//         ///     Dọn dẹp tài nguyên. </summary>
//         /// ---------------------------------- 
//         public void FunDispose()
//         {
//         }


//         /// <summary>
//         ///     Kiểm tra xem có thể thêm một trướng ngại vật vào 
//         ///     sonar dựa trên hướng và bán kính của chướng ngại vật. </summary>
//         /// --------------------------------------------------------------------
//         public bool FunInsertObstacle(float3 direction, float radius)
//         {
//             return true;
//         }

//         /// <summary>
//         ///     Kiểm tra xem có thể thêm một trướng ngại vật dạng hình cầu vào sonar hay không. </summary>
//         /// ----------------------------------------------------------------------------------------------
//         public bool FunInsertObstacle(float3 position, float3 velocity, float radius)
//         {
//             return true;
//         }

//         /// <summary>
//         ///     Kiểm tra xem có thể thêm một trướng ngại vật dạng đường thẳng vào sonar hay không. </summary>
//         /// -------------------------------------------------------------------------------------------------
//         public bool FunInsertObstacle(float3 startPos, float3 endPos)
//         {
//             return true;
//         }

//         /// <summary>
//         ///     Kiểm tra xem có thể tìm hướng gần nhất mà không bị chướng ngại vật cản trở từ vị trí của sonar hay ko. </summary>
//         /// ---------------------------------------------------------------------------------------------------------------------
//         public bool FunFindClosesDirection(out float3 closestDirection)
//         {
//             closestDirection = float3.zero;
//             return true;
//         }







//         // ---------------------------------------------------------------------------------
//         // FUNSTION HELPER
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Tìm góc gần nhất có thể từ bên trái của cây. </summary>
//         private bool FindClosestAngleLeft(out float angle)
//         {
//             angle = 0f;
//             return true;
//         }

//         /// <summary>
//         ///     Tìm góc gần nhất có thể từ bên phải của cây. </summary>
//         private bool FindClosestAngleRight(out float angle)
//         {
//             angle = 0f;
//             return true;
//         }

//         /// <summary>
//         ///     Tạo nút mới và trả về tham chiếu đến nút đó. </summary>
//         private SonarNodeHandle CreateNode(Line line)
//         {
//             return null;
//         }

//         private void InsertObstacleHelper(Line range)
//         {
//         }

//         private void InsertObstacleHelper(SonarNodeHandle handle, Line line)
//         {
//         }

        



//         // ---------------------------------------------------------------------------------
//         // FUNSTION INLINE
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Chuyển một vector 3D vào hệ tọa độ cục bộ của sonar. </summary>
//         /// ------------------------------------------------------------------
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         float2 ToLocalSpace(float3 value) => float2.zero;

//         /// <summary>
//         ///     Chuyển một vector 2D từ hệ tọa độ cục bộ sang hệ tọa độ thế giới. </summary>
//         /// --------------------------------------------------------------------------------
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         float2 ToWorldSpace(float3 value) => float2.zero;

//         /// <summary>
//         ///     Chuyển một góc (theo radian) thành vector hướng 2D trong hệ cục bộ. </summary>
//         /// ---------------------------------------------------------------------------------- 
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         static float2 AngleToDirectionLS(float angle) => float2.zero;

//         /// <summary>
//         ///     Chuyển đổi một vector 2D trong hệ cục bộ sang một góc (theo radian).</summary>
//         /// ----------------------------------------------------------------------------------
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         static float DirectionLSToAngle(float2 direction) => 0f;


//         /// <summary>
//         ///     Tính góc ký hiệu (signed angle) giữa hai vector 2D. 
//         ///     Cho biết chiều quay cùng (+) hay ngược chiều kim đông hồ (-). </summary>
//         /// ----------------------------------------------------------------------------
//         [MethodImpl(MethodImplOptions.AggressiveInlining)]
//         static float SingnedAngle(float2 a, float2 b) => 0f;

        



//         // ---------------------------------------------------------------------------------
//         // FUNSTION STATIC
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
//         internal void CheckIsCreated()
//         {
//         }

//         [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
//         private void CheckHandle(SonarNodeHandle handle)
//         {

//         }

//         [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
//         internal static void CheckArguments(quaternion rotation, float innerRadius, float outerRadius)
//         {
//         }
//     }
// }