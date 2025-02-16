namespace FireNBM
{
    public class ResourceMineralsHUD : ResourceTypeHUD
    {
        private void Start()
        {
            ResourceMineralsManager.Instance.FunSetResourceMineralsHUD(this);
        }
    }
}