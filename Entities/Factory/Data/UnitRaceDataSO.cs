using UnityEngine;

namespace FireNBM
{
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/UnitRace/New_UnitRaceData")]
    public class UnitRaceDataSO : ScriptableObject
    {
        public int SizePool;                        // Dùng để sản sinh một nhóm đối tượng 
        public GameObject Prefab;                   // Mẫu đối tượng dùng để sản sinh.
        
        public UnitDataSO Data;                     // Chứa dữ liệu cần có.
        public ActionUnitDataSO ActionData;         // Chứa các hành động mà người chơi có thể điều khiển.
        public UnitFlyweightDataSO FlyweightData;   // Chứa dữ liệu dùng chung.
    }
}  