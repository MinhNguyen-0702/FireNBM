// using Unity.Mathematics;
// using UnityEngine;

// namespace FireNBM.PathDing
// {
//     /// <summary>
//     ///     Đại diện cho một tác nhân trong Unity.  
//     /// </summary>
//     public class Agent : MonoBehaviour
//     {
//         /// <summary>
//         ///     Vị trí đích mà đối tượng agent đang cố gắng di chuyển đến. </summary>
//         public float3 Destination;

//         /// <summary>
//         ///     Vận tốc hiện tại của agent. </summary>
//         public float3 Velocity;

//         /// <summary>
//         ///     Tốc độ di chuyển của agen. </summary>
//         public float Speed;

//         /// <summary>
//         ///     Tốc độ quay của agent, giúp xác định 
//         ///     khả năng quay của nó để hướng tới mục tiêu. </summary>
//         public float TurnSpeed;

//         /// <summary>
//         ///     Bán kích của agent, có thể tính toán va 
//         ///     chạm hoặc tính khoảng cách an toàn giữa các tác nhân. </summary>
//         public float Radius;

//         /// <summary>
//         ///     Khoảng cách dừng lại, dùng để xác định khi nào Agent nên dừng gần đích. </summary>
//         public float StopDistance;

//         /// <summary>
//         ///     Cờ xác định liệu Agent có nên tránh va chạm hay không. </summary>
//         public bool Avoid;

//         /// <summary>
//         ///     Trả về vị trí hiện tại của Agent trên thế giới. </summary>
//         public float3 Position => transform.position;

//         /// <summary>
//         ///     Kiểm tra xem tác nhân đã đến mục tiêu chưa. </summary>
//         public bool IsStopped => math.distancesq(Destination, Position) <= (float)math.pow(StopDistance + 0.01f, 2);

        
//         /// <summary>
//         ///     Lấy dữ liệu của Agent. </summary>
//         /// -------------------------------------
//         public AgentData FunGetData()
//         {
//             return new AgentData
//             {
//                 Destination = this.Destination,
//                 Speed = this.Speed,
//                 TurnSpeed = this.TurnSpeed,
//                 Radius = this.Radius,
//                 StopDistance = this.StopDistance,
//                 Avoid = this.Avoid,
//                 Acceleration = 8
//             };
//         }
//     }


//     /// <summary>
//     ///     Là một cấu trúc dùng để sao chép và lưu thông tin của agent.
//     /// </summary>
//     public struct AgentData
//     {
//         public float3 Destination;
//         public float Speed;
//         public float TurnSpeed;
//         public float Radius;
//         public float StopDistance;
//         public bool Avoid;
//         public float Acceleration;

//         public bool FunIsStopped(float3 position)
//         {
//             return  math.distancesq(Destination, position) <= (float)math.pow(StopDistance + 0.01f, 2);;
//         }
//     }
// }