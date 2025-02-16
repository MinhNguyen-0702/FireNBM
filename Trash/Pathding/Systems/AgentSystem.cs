// using Unity.Mathematics;
// using UnityEngine;

// namespace FireNBM.PathDing
// {
//     public class AgentSystem : System
//     {
//         [Range(1, 10)]
//         [SerializeField] private float m_sonarRadius = 6f;          // Bán kích cảm biến dùng để phát hiện các vật thể ở gần.
//         [SerializeField] private bool m_is3D = true;                // Xác định môi trường 2D hay 3D.
//         [SerializeField] private bool m_tightFormation = false;     // Đội hình có di chuyển chặt chẽ với nhau hay không.
//         [SerializeField] private bool m_changeTransform = true;     // Tác nhân có di chuyển không.
//         [SerializeField] private float m_agentAcceleration = 8f;    // Điều chỉnh tốc độ tăng tốc của tác nhân.

//         // ---------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // /////////////////////////////////////////////////////////////////////////////////

//         // Đc gọi định kỳ để cập nhật trạng thái và vị trí của các tác nhân.
//         private void FixedUpdate()
//         {
//             var agents = FunQuery<Agent>();
//             if (agents.Length == 0)
//                 return;
            
//             foreach (var agent in agents)
//             {
//                 // Tính lực tác động khi agent di chuyển.
//                 if (agent.IsStopped == false && agent.Speed > 0f)
//                 {
//                     float3 impusle = 0f;
//                     float3 desiredDirection = math.normalizesafe(agent.Destination - agent.Position);
//                     if (agent.Avoid == true)
//                         impusle += GetAvoid(agent, agents, desiredDirection);
//                     else 
//                         impusle += desiredDirection * agent.Speed;

//                     var velocity = math.lerp(agent.Velocity, impusle, Time.deltaTime * m_agentAcceleration);
//                     agent.Velocity = velocity;
//                 }
//                 else 
//                     agent.Velocity = 0f;

//                 // Điều chỉnh vị trí và xoay hướng.
//                 if (m_changeTransform == true && math.lengthsq(agent.Velocity) != 0f)
//                 {
//                     var offset = agent.Velocity * Time.deltaTime;  // s = v.t

//                     // Tránh vượt quá đích đến.
//                     var distance = math.distance(agent.Destination, agent.Position);
//                     offset = ForceLength(offset, distance);

//                     agent.transform.position += (Vector3)offset;

//                     if (m_is3D == true)
//                     {
//                         var direction = math.normalizesafe(agent.Velocity);
//                         float angle = math.atan2(direction.z, -direction.x);
//                         var rotation = quaternion.RotateY(angle);
//                         rotation = quaternion.LookRotation(math.normalizesafe(new float3(agent.Velocity.x, 0f, agent.Velocity.z)), new float3(0f, 1f, 0f));
//                         agent.transform.rotation = math.slerp(transform.rotation, rotation, math.saturate(Time.deltaTime * agent.TurnSpeed));
//                     }
//                 }
//             }
//         }


//         // ---------------------------------------------------------------------------------
//         // FUNSTION HELPER
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Tính toán hướng di chuyển mới để tránh va chạm với các agent khác trong vùng lân cận. </summary>
//         private float3 GetAvoid(Agent agent, Agent[] nearByAgents, float3 desiredDirection)
//         {
//             // Thoát nếu không có hướng cần đi.
//             if (math.lengthsq(desiredDirection) == 0f)
//                 return float3.zero;

//             // Tạo cảm biến sonar
//             var sonarRadius = math.min(m_sonarRadius, math.distance(agent.Destination, agent.Position));
//             var up = m_is3D ? new float3(0f, 1f, 0f) : new float3(0f, 0f, -1f);      // (-1f = Là hướng lên - hướng ra khỏi màn hình)
//             var sonar = new SonarAvoidance(agent.Position, desiredDirection, up, agent.Radius, sonarRadius, math.length(agent.Velocity));

//             // Tính toán vị trí dự kiến mà agent sẽ đạt được sau một khung hình.
//             float2 predictedPosition = agent.Position.xz + (math.length(agent.Velocity) * Time.deltaTime * desiredDirection.xz);
//             var agentCircle = new Circle(predictedPosition, agent.Radius);

//             // Tác nhân có bị chặn hay không.
//             bool desiredDirectionOccluded = false;

//             // Lặp qua các tác nhân lân cận để kiểm tra va chạm và xử lý logic tránh xa.
//             foreach (var nearbyAgent in nearByAgents)
//             {   
//                 // Bỏ qua chính nó.
//                 if (nearbyAgent == agent)
//                     continue;

//                 // Thêm các agent vào hệ thống sonar như một vật cản.
//                 sonar.FunInsertObstacle(nearbyAgent.Position, nearbyAgent.Velocity, nearbyAgent.Radius);

//                 // Kiểm tra xem hướng di chuyển dự kiến của agent có va chạm với một tác nhân lân cận không
//                 // khi đang di chuyển chặt chẽ.
//                 if (m_tightFormation == true &&
//                     desiredDirectionOccluded == false && 
//                     Circle.FunCollide(agentCircle, new Circle(nearbyAgent.Position.xz, nearbyAgent.Radius)) == true)
//                 {
//                     desiredDirectionOccluded = true;
//                 }
//             }

//             // sonar.FunInsertObstacle(math.normalizesafe(-agent.Velocity), math.radians(so))

//             bool success = sonar.FunFindClosesDirection(out float3 newDirection);

//             sonar.FunDispose();

//             return newDirection * agent.Speed;
//         }

//         /// <summary>
//         ///     Giới hạn độ dài của một vector để đảm bảo tác nhân không vượt qua đích. </summary>
//         private float3 ForceLength(float3 value, float length)
//         {
//             return new float3();
//         }
//     }
// }
 
// // Packages\com.projectdawn.localavoidance