using UnityEngine;

namespace FireNBM
{
    [AddComponentMenu("FireNBM/RaceRTS/Building/Building Flyweight Comp")]
    public class BuildingFlyweightComp : MonoBehaviour
    {
        private BuildingFlyweightDataSO m_data;


        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        public void FunSetDataFlyweight(BuildingFlyweightDataSO data)
        {
            m_data = data;
            m_data.FunInitializeDataFlyweight();
        }

        public TypeRaceRTS FunGetRaceRTS() => m_data.Race;
        public TypeNameBuilding FunGetNameBuilding() => m_data.NameBuilding;
        public TypeRaceBuilding FunGetRaceBuilding() => m_data.BuildingRace;

        /// <summary>
        ///     Lấy tên chuỗi của Animation dựa trên type enum được đưa vào. </summary>
        /// ---------------------------------------------------------------------------
        public string FunGetStringAnim(TypeBuildingAnimState typeBuildingAnim)
        {
            return m_data.FunGetStringAnim(typeBuildingAnim);
        }
    }
}