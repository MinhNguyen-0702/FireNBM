using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Quản lý việc tạo đối tượng unit dựa trên tên và loại công việc của nó theo chủng tộc.
    /// </summary>
    public class UnitManagerRaceFactory
    {
        private List<GameObject> m_listUnitsActive;
        private Dictionary<TypeRaceUnit, List<GameObject>> m_unitRacesActiveMap;
        private Dictionary<TypeRaceUnit, UnitRaceTypeFactory> m_unitFactoryMap;

        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public UnitManagerRaceFactory()
        {
            m_listUnitsActive = new List<GameObject>();
            m_unitRacesActiveMap = new Dictionary<TypeRaceUnit, List<GameObject>>();
            m_unitFactoryMap = new Dictionary<TypeRaceUnit, UnitRaceTypeFactory>();
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PROTECTED
        // ------------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm một cặp key-value vào UnitFactory.
        ///     Phương thức này giúp các lớp con mở rộng dữ liệu một cách an toàn.
        /// </summary>
        /// 
        ///     <param name="typeUnit">Loại công trình cần thêm.</param>
        ///     <param name="factory">Factory tương ứng với loại công trình.</param>
        /// --------------------------------------------------------------------
        protected void AddUnitFactoryEntry(TypeRaceUnit typeUnit, UnitRaceTypeFactory factory)
        {
            if (m_unitFactoryMap.ContainsKey(typeUnit) == true)
            {
                Debug.LogWarning($"Factory cho loại đơn vị {typeUnit} đã tồn tại, sẽ được ghi đè.");
            }
            m_unitFactoryMap[typeUnit] = factory;
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Khởi tạo dữ liệu cho các factory đơn vị dựa trên danh sách dữ liệu chủng tộc.
        /// </summary>
        /// <param name="listUnitRaceData"> Danh sách chứa dữ liệu cho các đơn vị thuộc từng chủng tộc.</param>
        /// <returns>
        ///     Trả về <c>true</c> nếu khởi tạo thành công cho tất cả dữ liệu; 
        ///     <c>false</c> nếu không tìm thấy factory cần thiết cho một loại đơn vị.
        /// </returns>
        /// ---------------------------------------------------------------------
        public bool FunInitializeDataUnitRace(List<UnitRaceData> listUnitRaceData)
        {
            foreach (var unitData in listUnitRaceData)
            {
                if (m_unitFactoryMap.TryGetValue(unitData.ObjectRace, out UnitRaceTypeFactory factory) == false)
                {
                    Debug.Log($"Không tìm thấy factory cho loại đơn vị: {unitData.ObjectRace}");
                    return false;
                }
                factory.FunInitializeData(unitData.ListDataSO);
                m_unitRacesActiveMap.Add(unitData.ObjectRace, new List<GameObject>());
            }
            return true;
        }  
        
        /// <summary>
        ///     Tạo một đơn vị thuộc loại được chỉ định.
        /// </summary>
        /// 
        /// <param name="typeUnit"> Loại đơn vị cần được tạo. </param>
        /// 
        /// <returns>
        ///     Trả về một <see cref="GameObject"/> đại diện cho đơn vị đã được tạo, 
        ///     hoặc <c>null</c> nếu không tìm thấy factory cho loại được chỉ định.
        /// </returns>
        /// -----------------------------------------------------
        public GameObject FunCreateUnitRace(TypeNameUnit name, TypeRaceUnit typeUnit)
        {
            if (m_unitFactoryMap.TryGetValue(typeUnit, out UnitRaceTypeFactory factory) == false)
            {
                Debug.LogWarning($"Không tìm thấy factory cho loại đon vị: {typeUnit}");
                return null;
            }

            var unitSpawn = factory.FunCreateUnit(name);
            m_listUnitsActive.Add(unitSpawn);
            m_unitRacesActiveMap[typeUnit].Add(unitSpawn);

            return unitSpawn;
        }

        /// <summary>
        ///     Hủy một đơn vị thuộc loại được chỉ định.
        /// </summary>
        /// 
        /// <param name="typeUnit">Loại đơn vị cần hủy.</param>
        /// <param name="unit">Đối tượng <see cref="GameObject"/> đại diện cho đơn vị cần huỷ.</param>
        /// 
        /// <returns>
        ///     Trả về <c>true</c> nếu huỷ đơn vị thành công; 
        ///     <c>false</c> nếu không tìm thấy factory cho loại công trình hoặc huỷ thất bại.
        /// </returns>
        /// -----------------------------------------------------------------
        public bool FunDisposeUnitRace(GameObject unit, TypeRaceUnit typeUnit)
        {
            if (m_unitFactoryMap.TryGetValue(typeUnit, out UnitRaceTypeFactory factory) == false)
            {
                Debug.LogWarning($"Không tìm thấy factory cho loại đơn vị: {typeUnit}");
                return false;
            }
            m_listUnitsActive.Remove(unit);
            m_unitRacesActiveMap[typeUnit].Remove(unit);

            return factory.FunDisposeUnit(unit);
        }

        /// <summary>
        ///     Lấy tất cả các unit đang hoạt động. </summary>
        /// --------------------------------------------------
        public List<GameObject> FunGetListUnitActive() => m_listUnitsActive;

        /// <summary>
        ///     Lấy danh sách các unit dựa theo loại công việc có trong scene. </summary>
        /// ----------------------------------------------------------------------------- 
        public List<GameObject> FunGetListUnitRaceActive(TypeRaceUnit raceunit)
        {
            if (m_unitRacesActiveMap.TryGetValue(raceunit, out var listUnit) == false)
            {
                listUnit = new List<GameObject>();
            }
            return listUnit;
        }
    }
}