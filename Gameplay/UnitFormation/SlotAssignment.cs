using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     phân bổ vị trí cho các thành viên trong đội.
    /// </summary>
    public class SlotAssignment
    {
        /// <summary> 
        ///     Nhân vật sẽ được gán vị trí.</summary>
        public GameObject Character;

        /// <summary> 
        ///     Thứ tự của vị trí.</summary>
        public int SlotNumber;



        public SlotAssignment(GameObject character, int slotNumber)
            => (Character, SlotNumber) = (character, slotNumber);
    }
}