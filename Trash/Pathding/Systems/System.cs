// using UnityEngine;
// using UnityEngine.Profiling;

// namespace FireNBM.PathDing
// {
//     public abstract class System : MonoBehaviour
//     {
//         protected T[] FunQuery<T>() where T : MonoBehaviour
//         {
//             Profiler.BeginSample("FunQuety.FindObjectsOfType");

//             // Đây là thử nghiệm. Đợi sau có thể dùng cơ chế khác.
//             var behaviours = GameObject.FindObjectsOfType<T>();
//             Profiler.EndSample();
//             return behaviours;
//         }
//     }
// }