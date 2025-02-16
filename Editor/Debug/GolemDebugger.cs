using UnityEditor;

namespace FireNBM
{
    public static class GolemDebugger 
    {
        [MenuItem("FireNBM/Debug/Unit/Spawn Golem %h")]
        private static void SpawnGolem()
        {
            var golemSpawn = FactorySystem.Instance.RaceFactory.
                FunCreateUnitRace(TypeNameUnit.Golem, TypeRaceUnit.BasicCombat, TypeRaceRTS.Zerg);

            golemSpawn.transform.position = new UnityEngine.Vector3(10f, 0f, 0f);
        }
    }
}