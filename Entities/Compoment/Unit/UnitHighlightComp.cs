using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Thành phần đánh dấu các unit được chọn.
    /// </summary>
    [AddComponentMenu("FireNBM/RaceRTS/Unit/Unit Highlight Comp")]
    public class UnitHighlightComp : ObjectTypeBaseHighlightComp
    {
        [SerializeField] private Color m_highlightColor;
        [SerializeField] private Color m_selectedColor;
        [SerializeField] private Color m_checkColor;

        private GameObject m_selector;
        private ParticleSystem m_particleSystem;
        private ParticleSystemRenderer m_renderer;

 
        // ------------------------------------------------------------------
        // API UNITY 
        // ---------
        /////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            // Lấy thành phần lựa chọn trong đối tượng cha.
            m_selector = gameObject.transform.GetChild(0)?.gameObject;
            if (m_selector == null)
            {
                Debug.LogWarning("In HighlightUnitComponent, Could not find obj children selector");
                return;
            }

            m_particleSystem = m_selector.GetComponent<ParticleSystem>();
            DebugUtils.HandleErrorIfNullGetComponent<ParticleSystem, UnitHighlightComp>(m_particleSystem, this, gameObject);

            m_renderer = m_particleSystem.GetComponent<ParticleSystemRenderer>();
            DebugUtils.HandleErrorIfNullGetComponent<ParticleSystemRenderer, UnitHighlightComp>(m_renderer, this, gameObject);

            // Ẩn đi trạng thái được chọn cho đối tượng cha.
            FunDisableSelectedState();
        }


        // ------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thiết lập màu sắc highlight cho chủ nhân khi người dùng di chuột đến.</summary>
        /// ----------------------------------------------------------------------------------
        public override void FunHighlightColor()
        {
            SetActiveSelector(true);
            SetColor(m_highlightColor);
        }

        /// <summary>
        ///     Thiết lập màu sắc selector cho chủ nhân khi người dùng nhấn chuột.</summary>
        /// --------------------------------------------------------------------------------
        public override void FunSelectedColor()
        {
            SetActiveSelector(true);
            SetColor(m_selectedColor);  
        }

        /// <summary>
        ///     Tắt trạng thái được chọn cho đối tượng cha. </summary>
        /// ----------------------------------------------------------
        public override void FunDisableSelectedState() => SetActiveSelector(false);

        public override void FunCheckColor()
        {
            SetActiveSelector(true);
            SetColor(m_checkColor);  
        }


        // ------------------------------------------------------------------------------
        // METHODS HEPLER
        // --------------
        /////////////////////////////////////////////////////////////////////////////////

        private void SetActiveSelector(bool active) => m_selector.SetActive(active);
        private void SetColor(Color color) => m_renderer.material.SetColor("_EmissionColor", color);
    }
}