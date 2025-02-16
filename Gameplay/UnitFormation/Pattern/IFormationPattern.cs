using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Là một lớp giao diện chưa các phương thức cơ bản để các lớp con triên khai.</summary>
    public interface IFormationPattern
    {
        /// <summary>
        ///     Tính toán độ lệch (vị trí trung bình) cho các nhân 
        ///     vật trong đội hình dựa trên số slot có trong đội.
        ///     Drift đảm bảo điểm neo vẫn ở trung tâm đội hình.</summary>
        /// --------------------------------------------------------------
        public Location FunGetDriftOffset(Dictionary<GameObject, SlotAssignment> slots);

        /// <summary>
        ///     Tính toàn và trả về vị trí cụ thể của một 
        ///     slot nhất định dựa trên chỉ số của nó.</summary>
        /// ----------------------------------------------------
        public Location FunGetSlotLocation(int slotNumber);

        /// <summary>
        ///     Kiểm tra xem mẫu đội hình có thể hỗ trợ 
        ///     một số lượng  slot nhất định hay ko.</summary>
        /// -------------------------------------------------- 
        public bool FunSupportSlots(int slotCount);
    }
}