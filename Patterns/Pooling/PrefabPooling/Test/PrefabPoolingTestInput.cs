using System.Collections.Generic;
using UnityEngine;

namespace FireNBM.Pattern
{
    public class PrefabPoolingTestInput : MonoBehaviour
    {
        [SerializeField] private GameObject m_golemPrefab;
        [SerializeField] private GameObject m_warriorPrefab;

        private List<GameObject> m_golems = new List<GameObject>();
        private List<GameObject> m_warriors = new List<GameObject>();

        
        private void Start()
        {
            PrefabPoolingSystem.FunPrespawn(m_golemPrefab, 10);
            PrefabPoolingSystem.FunPrespawn(m_warriorPrefab, 20);
        }

        private void Update()
        {
            // For Spawn.
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SpawnObject(m_golemPrefab, m_golems);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                SpawnObject(m_warriorPrefab, m_warriors);

            // For Despawn.
            if (Input.GetKeyDown(KeyCode.Q))
                DespawnRandomObjet(m_golems);

            if (Input.GetKeyDown(KeyCode.W))
                DespawnRandomObjet(m_warriors);
        }

        private void SpawnObject(GameObject prefab, List<GameObject> list)
        {
            GameObject obj = PrefabPoolingSystem.
                FunSpawn(prefab, 5.0f * Random.insideUnitSphere, Quaternion.identity);
            
            // LỖI SAI NGỚ NGẪN: thêm đối tượng prefab vào danh sách, mà ko thêm đối tượng được
            //                   sinh ra từ pool, gây lỗi.
            list.Add(obj);
        }

        private void DespawnRandomObjet(List<GameObject> list)
        {
            if (list.Count == 0) return;    // Nothing to despawn.

            int i = Random.Range(0, list.Count);
            PrefabPoolingSystem.FunDespawn(list[i]);
            list.RemoveAt(i);
        }
    }
}