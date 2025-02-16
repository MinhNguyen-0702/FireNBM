using UnityEngine;

namespace FireNBM
{
    public class UnitHeathComp : ObjectTypeBaseHealthComp
    {
        protected override void Awake()
        {
            base.Awake();
            FunSetActiveHealth(false);
        }

        protected override void Update()
        {
            base.Update();

            // Test.
            if (Input.GetKeyDown(KeyCode.L) == true)
            {
                FunSetHealth(10);
            }
        }

        // Khi máu cạn.
        protected override void Death()
        {
            FunSetActiveHealth(false);
            GetComponent<UnitDeathComp>().FunOnTriggerUnitDeath();
        }
    }
}