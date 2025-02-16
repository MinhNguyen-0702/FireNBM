using UnityEditor;
using UnityEngine;

namespace FireNBM
{
    public static class MageDebugger
    {
        [MenuItem("FireNBM/Debug/Unit/Spawn Mage %m")]
        private static void SpawnWarrior()
        {
            // Debug.Log("Create unit Mage");
            UnitManager.Instance.FunSpawnUnit(TypeNameUnit.Mage, TypeRaceUnit.Worker, TypeRaceRTS.Terran);
        }
    }
}