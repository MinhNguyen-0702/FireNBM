using System;
using UnityEngine;

namespace FireNBM
{
    [AddComponentMenu("FireNBM/RaceRTS/Building/Building Data Comp")]
    public class BuildingDataComp : MonoBehaviour
    {
        /// <summary> 
        ///     Trạng thái hiện tại của đơn vị.</summary>
        public TypeState State;
        ///
        /// <summary>
        ///     Sức khỏe của đối tượng.</summary>
        public float Health;
        ///
        /// <summary>
        ///     Lượng sát thương mà nó gây ra cho mục tiêu.</summary>
        public float Attack;
        ///
        /// <summary>
        ///     Mức phòng thủ của nó.</summary>
        public float Defense;
        ///
        /// <summary>
        ///     Tốc độ đánh, nhanh hay chậm.</summary>
        public float AttackSpeed;
        ///
        /// <summary>
        ///     Phạm vi tấn công.</summary>
        public float RangeAttack;



        // ---------------------------------------------------------------------------------
        // METHOD PUBLIC
        // -------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Nhận dữ liệu từ người dùng rồi thiết lập dữ liệu cho chủ thể building.</summary>
        /// ------------------------------------------------------------------------------------
        public void FunSetData(BuildingDataSO data)
        {
            // this.State = data.State;
            // this.Attack = data.Attack;
            // this.Defense = data.Defense;
            // this.AttackSpeed = data.AttackSpeed;
            // this.RangeAttack = data.RangeAttack;
        }
    }
}