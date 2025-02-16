using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Mẫu phòng thủ đội hình được bố trí thành hình tròn.
    /// </summary>
    public class DefensiveCriclePattern : IFormationPattern
    {
        // Bán kính của nhân vật, giúp xác định khoảng cách giữa các slot trong hình tròn.
        private float m_characterRadius;
        // Số lượng slot có trong đội hình.
        private float m_sizeAllSlot;


        // --------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////////////////////

        public DefensiveCriclePattern()
        {
            m_sizeAllSlot = 0f;
            m_characterRadius = 1.5f;
        }


        // --------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // //////////////////////////////////////////////////////////////////////////

        public Location FunGetDriftOffset(Dictionary<GameObject, SlotAssignment> slots)
        {
            // Lấy số lượng slot có trong đội hình.
            m_sizeAllSlot = slots.Count;

            // Lưu trữ thông tin về độ lêch 
            Location resultDrift = new Location();
            foreach (var slot in slots.Values.ToList())
            {
                // Lấy thông tin vị trí của slot.
                Location locationSlot = FunGetSlotLocation(slot.SlotNumber);

                // Tính tổng tất cả các vị trí và hướng của slot.
                resultDrift.Position += locationSlot.Position;
                // resultDrift.Orientation += locationSlot.Orientation;
            }

            // Tính toán độ lệch.
            resultDrift.Position /= m_sizeAllSlot;
            // resultDrift.Orientation /= countSlot;

            return resultDrift;
        }


        public Location FunGetSlotLocation(int slotIndex)
        {
            // Đặt các slot xung quanh hình tròn dựa trên vị trí của nó.
            // Bằng cách tìm góc quay (radian) cho mối slot.
            float angleAroundCircle = ((float)slotIndex / m_sizeAllSlot) * (2 * Mathf.PI);

            // Tính bán kính của hình tròn dựa trên bán kính của nhân vật 
            // và góc giữa hai nhân vật (với 2 nhân vật đứng xát nhau).
            float radius = m_characterRadius;
            if (m_sizeAllSlot > 1)
                radius = m_characterRadius / Mathf.Sin(Mathf.PI / m_sizeAllSlot);  


            // Lưu trữ thông tin về vị trí và hướng của slot trong đội hình.
            Location locationSlot = new Location();

            // Thiết lập vị trí 
            locationSlot.Position.x = radius * Mathf.Cos(angleAroundCircle);
            locationSlot.Position.z = radius * Mathf.Sin(angleAroundCircle);
    
            // Thiết lập hướng.
            // Với đội hình là hình tròn, hướng sẽ quay ra ngoài.
            // locationSlot.Orientation = 

            return locationSlot;
        }

        /// <summary>
        ///     Có thêm vị trí mới vào đội hình được hay ko. </summary>
        /// ------------------------------------------------------------
        public bool FunSupportSlots(int slotCount) => true;
    }
}