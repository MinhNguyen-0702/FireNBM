using UnityEngine;

namespace FireNBM
{
    public class ActionInfoButtonBuyHUD : MonoBehaviour
    {
        [SerializeField] int m_countMinerals;
        [SerializeField] int m_countVespeneGas;
        [SerializeField] int m_countSupplys;

        public int FunGetCountMinerals() => m_countMinerals;
        public int FunGetCountVespeneGas() => m_countVespeneGas;
        public int FunGetCountSupplys() => m_countSupplys;
    }
}