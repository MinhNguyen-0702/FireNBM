using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một lớp nhận dữ liệu đầu vào từ người dùng cho đơn vị chiến đấu của họ.
    /// </summary>
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/UnitRace/New_DataUnit")]
    public class UnitDataSO : ScriptableObject
    {
        /// <summary>
        ///     Trạng thái hiện tại của đơn vị.</summary>
        public TypeState State;

        /// <summary>
        ///     Lượng sát thương mà nó gây ra cho mục tiêu.</summary>
        public float Attack;

        /// <summary>
        ///     Mức phòng thủ của nó.</summary>
        public float Defense;

        /// <summary>
        ///     Tốc độ di chuyển của đối tượng.</summary>
        public float WalkSpeed;

        /// <summary>
        ///     Tốc độ đánh, nhanh hay chậm.</summary>
        public float AttackSpeed;

        /// <summary>
        ///     Phạm vi tấn công.</summary>
        public float RangeAttack;

        /// <summary>
        ///     Sức khỏe của đối tượng.</summary>
        public float Health;
    }
} 