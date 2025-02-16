using System.Collections;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class StartGameTimeAttack : MonoBehaviour
    {
        [SerializeField] private GameObject m_objStart;
        private Vector3 m_posStart;

        private void Awake()
        {
            m_posStart = m_objStart.transform.position;
            Destroy(m_objStart);
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageStartGame), OnStartGame);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageStartGame), OnStartGame);
        }


        private bool OnStartGame(IMessage message)
        {
            var buildingRTS = FactorySystem.Instance.RaceFactory.
                            FunGetBuildingRace(TypeNameBuilding.CommandCenter, TypeRaceBuilding.Townhall, TypeRaceRTS.Terran);

            var underConstructor = buildingRTS.ObjectUnderConstruction;
            underConstructor.SetActive(false);

            var building = buildingRTS.ObjectBuilding;
            var size = building.GetComponent<BoxCollider>().size;
            building.transform.position = new Vector3(m_posStart.x, -size.y, m_posStart.z);

            StartCoroutine(HandleEffectBuilding(building));
            return true;
        }

        private IEnumerator HandleEffectBuilding(GameObject building)
        {
            while (Vector3.Distance(building.transform.position, m_posStart) >= 0.25f)
            {
                building.transform.Translate(Vector3.up * 5f * Time.deltaTime);

                if (Vector3.Distance(building.transform.position, m_posStart) <= 0.25f)
                {
                    building.transform.position = m_posStart;
                    yield break;
                }
                yield return null;
            }
        }
    }
}