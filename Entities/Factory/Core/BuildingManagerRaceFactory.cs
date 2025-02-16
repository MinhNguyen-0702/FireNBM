using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public class PairObjectBuiding
    {
        public GameObject ObjectBuilding;
        public GameObject ObjectUnderConstruction;

        public PairObjectBuiding(GameObject building, GameObject underConstruction)
        {
            ObjectBuilding = building;
            ObjectUnderConstruction = underConstruction;
        }
    }

    /// <summary>
    ///     Lớp Factory chịu trách nhiệm quản lý việc tạo, khởi tạo dữ liệu, 
    ///     và huỷ các đối tượng công trình dựa trên loại chủng tộc.
    ///     Thực hiện giao diện <see cref="IBuildingRaceFactory"/>.
    /// </summary>
    public class BuildingManagerRaceFactory
    {
        private List<GameObject> m_listBuildingsActive;
        private Dictionary<TypeRaceBuilding, List<GameObject>> m_buildingRacesActiveMap;
        private Dictionary<TypeRaceBuilding, BuildingRaceTypeFactory> m_buildingFactory;

        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public BuildingManagerRaceFactory()
        {
            m_listBuildingsActive = new List<GameObject>();
            m_buildingRacesActiveMap = new Dictionary<TypeRaceBuilding, List<GameObject>>();
            m_buildingFactory = new Dictionary<TypeRaceBuilding, BuildingRaceTypeFactory>();
        }

        // --------------------------------------------------------------------------------------------
        // FUNCTION PROTECTED
        // ------------------
        // /////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Thêm một cặp key-value vào BuildingFactory.
        /// Phương thức này giúp các lớp con mở rộng dữ liệu một cách an toàn.
        /// </summary>
        /// <param name="typeBuilding">Loại công trình cần thêm.</param>
        /// <param name="factory">Factory tương ứng với loại công trình.</param>
        /// --------------------------------------------------------------------
        protected void AddBuildingFactoryEntry(TypeRaceBuilding typeBuilding, BuildingRaceTypeFactory factory)
        {
            if (m_buildingFactory.ContainsKey(typeBuilding))
            {
                Debug.LogWarning($"Factory cho loại công trình {typeBuilding} đã tồn tại, sẽ được ghi đè.");
            }
            m_buildingFactory[typeBuilding] = factory;
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Khởi tạo dữ liệu cho các factory công trình dựa trên danh sách dữ liệu chủng tộc.
        /// </summary>
        /// <param name="listBuildingRaceData">Danh sách chứa dữ liệu cho các công trình thuộc từng chủng tộc.</param>
        /// <returns>
        /// Trả về <c>true</c> nếu khởi tạo thành công cho tất cả dữ liệu; 
        /// <c>false</c> nếu không tìm thấy factory cần thiết cho một loại công trình.
        /// </returns>
        /// ----------------------------------------------------------------------------------
        public bool FunInitializeDataBuildingRace(List<BuildingRaceData> listBuildingRaceData)
        {
            foreach (var buildingData in listBuildingRaceData)
            {
                if (m_buildingFactory.TryGetValue(buildingData.ObjectRace, out BuildingRaceTypeFactory factory) == false)
                {
                    Debug.LogError($"Không tìm thấy factory cho loại công trình: {buildingData.ObjectRace}");
                    return false;
                }
                factory.FunInitializeData(buildingData.ListDataSO);
                m_buildingRacesActiveMap.Add(buildingData.ObjectRace, new List<GameObject>());
            }
            return true;
        }

        /// <summary>
        /// Tạo một đối tượng công trình thuộc loại được chỉ định.
        /// </summary>
        /// <param name="typeBuilding">Loại công trình cần tạo.</param>
        /// <returns>
        /// Trả về một <see cref="GameObject"/> đại diện cho công trình đã được tạo, 
        /// hoặc <c>null</c> nếu không tìm thấy factory cho loại được chỉ định.
        /// </returns>
        /// ------------------------------------------------------------------
        public PairObjectBuiding FunCreateBuildingRace(TypeNameBuilding name, TypeRaceBuilding typeBuilding)
        {
            if (m_buildingFactory.TryGetValue(typeBuilding, out BuildingRaceTypeFactory factory) == false)
            {
                Debug.LogWarning($"Không tìm thấy factory cho loại công trình: {typeBuilding}");
                return null;
            }

            var buildingSpawn = factory.FunCreateBuilding(name);
            m_listBuildingsActive.Add(buildingSpawn);
            m_buildingRacesActiveMap[typeBuilding].Add(buildingSpawn);
            
            return new PairObjectBuiding(buildingSpawn, factory.FunCreateUnderConstruction(name));
        }


        /// <summary>
        /// Huỷ một đối tượng công trình thuộc loại được chỉ định.
        /// </summary>
        /// <param name="typeBuilding">Loại công trình cần huỷ.</param>
        /// <param name="building">Đối tượng <see cref="GameObject"/> đại diện cho công trình cần huỷ.</param>
        /// <returns>
        /// Trả về <c>true</c> nếu huỷ công trình thành công; 
        /// <c>false</c> nếu không tìm thấy factory cho loại công trình hoặc huỷ thất bại.
        /// </returns>
        /// ---------------------------------------------------------------------------------
        public bool FunDisposeBuildingRace(GameObject building, TypeRaceBuilding typeBuilding)
        {
            if (m_buildingFactory.TryGetValue(typeBuilding, out BuildingRaceTypeFactory factory) == false)
            {
                Debug.LogWarning($"Không tìm thấy factory cho loại công trình: {typeBuilding}");
                return false;
            }
            m_listBuildingsActive.Remove(building);
            m_buildingRacesActiveMap[typeBuilding].Remove(building);

            return factory.FunDisposeBuilding(building);
        }

        public bool FunDisposeUnderConstructionRace(GameObject underConstruction, TypeRaceBuilding typeBuilding)
        {
            if (m_buildingFactory.TryGetValue(typeBuilding, out BuildingRaceTypeFactory factory) == false)
            {
                Debug.LogWarning($"Không tìm thấy factory cho loại công trình: {typeBuilding}");
                return false;
            }
            return factory.FunDisposeUnderConstruction(underConstruction);
        }
    }
}