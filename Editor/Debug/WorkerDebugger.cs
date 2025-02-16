using UnityEngine;
using UnityEditor;

namespace FireNBM
{
    // Kiểm tra xem các trình lắng nghe có hoạt động đúng không.
    public static class WorkerDebugger
    {
        [MenuItem("FireNBM/Debug/Unit/Spawn Worker %j")]
        private static void SpawnWarrior()
        {
            // MessagingSystem.Instance.
            //     FunTriggerMessage(new MessageBasicWorkerSpawn(Vector3.zero), false);
        }
    }
}