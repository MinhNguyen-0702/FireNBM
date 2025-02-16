using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một lớp nhận dữ liệu đầu vào từ người dùng cho tháp của họ.
    /// </summary>
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/BuildingRace/New_DataBuilding")]
    public class BuildingDataSO : ScriptableObject
    {
        /// <summary>
        ///     Lượng sát thương mà nó gây ra cho mục tiêu.</summary>
        public float Attack;
        /// <summary>
        ///     Phạm vi tấn công.</summary>
        public float RangeAttack;
        /// <summary>   
        ///     Mức phòng thủ của nó.</summary>
        public float Defense;
        /// <summary>
        ///     Tốc độ đánh, nhanh hay chậm.</summary>
        public float AttackSpeed;
    }
} 