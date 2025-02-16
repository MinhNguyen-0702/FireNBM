using UnityEngine;

namespace FireNBM
{
    [CreateAssetMenu(menuName = "FireNBM/New Color Selection")]
    public class ColorSelectionData : ScriptableObject
    {
        public Color HighLight;
        public Color Selected;
    }
}