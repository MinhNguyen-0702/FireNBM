using UnityEngine;

namespace FireNBM.Pattern
{
    public class ResetPooledRigidbodyComponent : MonoBehaviour, IPoolableComp
    {
        [SerializeField] Rigidbody m_body;

        public void FunSpawned() {}

        public void FunDespawned()
        {
            if (m_body == null)
            {
                m_body = GetComponent<Rigidbody>();
                if (m_body == null) return; // no Rigidbody!
            }
            m_body.velocity = Vector3.zero;
            m_body.angularVelocity = Vector3.zero;
        }
    }
}