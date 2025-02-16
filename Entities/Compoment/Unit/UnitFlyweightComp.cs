using UnityEngine;
using UnityEngine.UI;

namespace FireNBM
{
    [AddComponentMenu("FireNBM/RaceRTS/Unit/Unit Flyweight Comp")]
    public class UnitFlyweightComp : MonoBehaviour
    {
        private UnitFlyweightDataSO m_data;

        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        public void FunSetDataFlyweight(UnitFlyweightDataSO data)
        {
            m_data = data;
            if (m_data.FunInitializeDataFlyweight() == false)
            {
                DebugUtils.FunLog("Thiết lập dữ liệu Flyweight thất bại.");
            }
        }

        public Sprite FunGetAvartar() => m_data.Avartar;
        public TypeNameUnit FunGetNameUnit() => m_data.NameUnit;
        public TypeRaceRTS FunGetRaceRTS() => m_data.Race;
        public TypeRaceUnit FunGetRaceUnit() => m_data.UnitRace;
        public string FunGetInfo() => m_data.Info;

        /// <summary>
        ///     Lấy tên chuỗi của Animation dựa trên type enum được đưa vào. </summary>
        /// ---------------------------------------------------------------------------
        public string FunGetStringAnim(TypeUnitAnimState typeUnitAnim)
        {
            return m_data.FunGetStringAnim(typeUnitAnim);
        }
    }
}