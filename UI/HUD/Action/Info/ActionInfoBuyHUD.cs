using TMPro;
using UnityEngine;

namespace FireNBM
{
    public class ActionInfoBuyHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_mineralsText;
        [SerializeField] private TextMeshProUGUI m_vespeneGasText;
        [SerializeField] private TextMeshProUGUI m_supplysText;

        public void FunSetResourceMinerals(int minerals) => m_mineralsText.text = minerals.ToString();
        public void FunSetResourceVespeneGas(int vespeneGas) => m_vespeneGasText.text = vespeneGas.ToString();
        public void FunSetResourceSupplys(int supplys) => m_supplysText.text = supplys.ToString();
    }
}