namespace FireNBM
{
    public class ResourceSupplyHUD : ResourceTypeHUD
    {
        private int m_maxResourceSupply;

        private void Awake()
        {
            m_maxResourceSupply = 15; // Test
            ResourceSupplyManager.Instace.FunSetResourceSupplyHUD(this);
        }

        public override void FunUpdateResource(int resource)
        {
            if (m_currentResource >= m_maxResourceSupply)
            {
                DebugUtils.FunLog("Max Supply.");
                return;
            }
            
            m_currentResource += resource;
            if (m_currentResource < 0)
            {
                FunResetResource();
                return;
            }
            m_text.text = $"{m_currentResource.ToString()}/{m_maxResourceSupply.ToString()}";
        }
    }
}