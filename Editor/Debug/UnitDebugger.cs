using UnityEditor;

namespace FireNBM
{
    // Kiểm tra xem các trình lắng nghe có hoạt động đúng không.
    public static class UnitDebugger
    {
        [MenuItem("FireNBM/Debug/Unit/Spawn Warrior %g")]
        private static void SpawnWarrior()
        {
            // MessageQueueManager.Instance.
            //     SendMessage(new BasicWarriorSpawnMessage());
        }
    }
}