using UnityEngine;
using System.Collections.Generic;

namespace FireNBM
{
    [System.Serializable]
    public class AnimBuildingStateNamePair
    {
        public string AnimName;
        public TypeBuildingAnimState TypeAnimState;
    }

    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/BuildingRace/New_FlyweightBuilding")]
    public class BuildingFlyweightDataSO : ScriptableObject
    {
        public TypeNameBuilding NameBuilding;
        
        public TypeRaceRTS Race;
        public TypeRaceBuilding BuildingRace;

        [SerializeField] private List<AnimBuildingStateNamePair> m_listAnimeStates;
        private Dictionary<TypeBuildingAnimState, string> m_mapNameAnimState;

        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Khởi tạo dữ liệu cho flyweight. </summary>
        /// ----------------------------------------------
        public bool FunInitializeDataFlyweight()
        {
            m_mapNameAnimState = new Dictionary<TypeBuildingAnimState, string>();
            return SetAnimStateNames();
        }

        /// <summary>
        ///     Lấy tên chuỗi của Animation dựa trên type enum được đưa vào. </summary>
        /// ---------------------------------------------------------------------------
        public string FunGetStringAnim(TypeBuildingAnimState typeBuildingAnim)
        {
            return (m_mapNameAnimState.ContainsKey(typeBuildingAnim) == true)
                   ? m_mapNameAnimState[typeBuildingAnim] 
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

            foreach (AnimBuildingStateNamePair pair in m_listAnimeStates)
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