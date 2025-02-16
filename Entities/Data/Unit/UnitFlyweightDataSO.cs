using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FireNBM
{
    [System.Serializable]
    public class AnimUnitStateNamePair
    {
        public string AnimName;
        public TypeUnitAnimState TypeAnimState;
    }

    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/UnitRace/New_FlyweightUnit")]
    public class UnitFlyweightDataSO : ScriptableObject
    {
        public Sprite Avartar;

        [Space(7)]
        public TypeNameUnit NameUnit;
        public TypeRaceRTS Race;
        public TypeRaceUnit UnitRace;

        [Space(7)]
        public string Info;

        [Space(7)]
        [SerializeField] private List<AnimUnitStateNamePair> m_listAnimeStates = new List<AnimUnitStateNamePair>();
        private Dictionary<TypeUnitAnimState, string> m_mapNameAnimState;

        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Khởi tạo dữ liệu cho flyweight. </summary>
        /// ----------------------------------------------
        public bool FunInitializeDataFlyweight()
        {
            m_mapNameAnimState = new Dictionary<TypeUnitAnimState, string>();
            return SetAnimStateNames();
        }

        /// <summary>
        ///     Lấy tên chuỗi của Animation dựa trên type enum được đưa vào. </summary>
        /// ---------------------------------------------------------------------------
        public string FunGetStringAnim(TypeUnitAnimState typeUnitAnim)
        {
            return (m_mapNameAnimState.ContainsKey(typeUnitAnim) == true)
                   ? m_mapNameAnimState[typeUnitAnim] 
                   : null;
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Thiết lập các trạng thái hoạt ảnh mà unit có thông qua dữ liệu từ người dùng.
        // -----------------------------------------------------------------------------
        private bool SetAnimStateNames()
        {
            if (m_listAnimeStates == null)
                return false;

            foreach (AnimUnitStateNamePair pair in m_listAnimeStates)
            {
                if (pair == null)
                    continue;

                if (m_mapNameAnimState.ContainsKey(pair.TypeAnimState) == false)
                    m_mapNameAnimState.Add(pair.TypeAnimState, pair.AnimName);
            }
            return true;
        }
    }
}