using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class ResourceSupplyManager : Singleton<ResourceSupplyManager>
    {
        private ResourceSupplyHUD m_resourceSupplyHUD;
        public static ResourceSupplyManager Instace { get { return InstanceSingletonInScene; }}

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(
                typeof(MessageUpdateResourceSupplysHUD), OnUpdateResourceSupplysHUD);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(
                typeof(MessageUpdateResourceSupplysHUD), OnUpdateResourceSupplysHUD);
        }

        public void FunSetResourceSupplyHUD(ResourceSupplyHUD supplyHUD)
        {
            m_resourceSupplyHUD = supplyHUD;
        }


        private bool OnUpdateResourceSupplysHUD(IMessage message)
        {
            var messageResource = message as MessageUpdateResourceSupplysHUD;
            m_resourceSupplyHUD.FunUpdateResource(messageResource.CountResourceSupplys);
            return true;
        } 
    }
}