using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lấy dữ liệu từ file cấu hình của người dùng liên quan đến đơn vị.
    /// </summary>
    [AddComponentMenu("FireNBM/RaceRTS/Unit/Unit Data Comp")]
    public class UnitDataComp : MonoBehaviour
    {
        /// <summary> 
        ///     Trạng thái hiện tại của đơn vị.</summary>
        public TypeState State;
        ///
        /// <summary> 
        ///     Lượng sát thương tấn công mà nó gây ra cho mục tiêu.</summary>
        public float Attack;
        ///
        /// <summary> 
        ///     Mức phòng thủ của đối tượng.</summary>
        public float Defense;
        //
        /// <summary> 
        ///     Tốc độ di chuyển của đối tượng.</summary>
        public float WalkSpeed;
        ///
        /// <summary> 
        ///     Tốc độ đánh, nhanh hay chậm.</summary>
        public float AttackSpeed;
        ///
        /// <summary> 
        ///     Phạm vi tấn công.</summary>
        public float RangeAttack;


        private Animator m_animatorUnit;            // Quản lý các animation cho unit.
        private TypeUnitAnimState m_preAnim;        // Lưu trữ kiểu anim trước đó của unit.
        private UnitFlyweightComp m_unitFlyweight;


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_animatorUnit = GetComponent<Animator>();
            DebugUtils.HandleErrorIfNullGetComponent<Animator, UnitDataComp>(m_animatorUnit, this, gameObject);
            m_preAnim = TypeUnitAnimState.None;
        }


        // ---------------------------------------------------------------------------------
        // METHOD PUBLIC
        // -------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Nhận dữ liệu từ người dùng rồi thiết lập dữ liệu cho chủ thể unit.</summary>
        /// --------------------------------------------------------------------------------
        public void FunSetData(UnitDataSO data)
        {
            // Dữ liệu dùng chung.
            m_unitFlyweight = GetComponent<UnitFlyweightComp>();
            DebugUtils.HandleErrorIfNullGetComponent<UnitFlyweightComp, UnitDataComp>(m_unitFlyweight, this, gameObject);

            // Dữ liệu dành riêng cho từng unit.
            this.State = data.State;
            this.Attack = data.Attack;
            this.Defense = data.Defense;
            this.WalkSpeed = data.WalkSpeed;
            this.AttackSpeed = data.AttackSpeed;
            this.RangeAttack = data.RangeAttack;

            var unitHealthComp = GetComponent<UnitHeathComp>();
            DebugUtils.HandleErrorIfNullGetComponent<UnitHeathComp, UnitDataComp>(unitHealthComp, this, gameObject);
            unitHealthComp.FunSetData(data.Health);
        }  

        /// <summary>
        ///     Thiết lập trạng thái cho unit. </summary>
        /// ---------------------------------------------
        public void FunSetAnimState(TypeUnitAnimState typeAnim)
        {
            // Lấy string anim tương ứng dựa trên enum được đưa vào. 
            var strAnim = m_unitFlyweight.FunGetStringAnim(typeAnim);
            if (strAnim == null)
            {
                DebugUtils.FunLog($"Error, Name: {typeAnim.ToString()} not found.");
                return;
            }

            // Thay đổi anim nếu đó là trạng thái mới.
            if (FunIsAnimStatePre(typeAnim) == false)
            {
                m_preAnim = typeAnim;
                m_animatorUnit.Play(strAnim);
            }
        }

        /// <summary>
        ///     Kiểm tra trạng thái trước đó có phải là trạng thái hiện tại ko.</summary>
        /// -----------------------------------------------------------------------------
        public bool FunIsAnimStatePre(TypeUnitAnimState state) => m_preAnim == state;
    }
}         