using System.Collections;
using UnityEngine;

namespace FireNBM
{
    public class BuildingControllerComp : MonoBehaviour
    {
        public void FunStartCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public Vector3 FunGetPosOwner()
        {
            return gameObject.transform.position;
        }

        public Vector3 FunGetUnitSpawnLocation(float radius)
        {
            Vector3 posCurrent = FunGetPosOwner();
            Vector3 size = gameObject.GetComponent<BoxCollider>().size;

            float radiusOwner = (size.x + size.z) / 2f + radius;
            Vector2 randomCircle = Random.insideUnitCircle * radius;

            return new Vector3(posCurrent.x + randomCircle.x, posCurrent.y, posCurrent.z + randomCircle.y);
        }
    }
}