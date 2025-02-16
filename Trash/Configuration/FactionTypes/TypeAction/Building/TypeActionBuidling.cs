// using System;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Các lệnh mà người chơi có thể thực hiện đối với công trình (building).
//     /// </summary>
//     [Flags]
//     [Serializable]
//     public enum TypeActionBuidling
//     {
//         /// <summary> Trạng thái mặc định.</summary>
//         None = 0,

//         /// <summary> Tạo đơn vị hoặc tài nguyên.</summary>
//         Produce = 1,

//         /// <summary> Nâng cấp tính năng hiện có, không thay đổi công trình/đơn vị.</summary>
//         Upgrade = 2, 

//         /// <summary> Tự sữa chữa hoặc phục hồi công trình.</summary>
//         Repair = 4,

//         /// <summary> Kích hoạt khả năng tấn công nếu công trình có vũ khí.</summary>
//         Attack = 8,

//         /// <summary> Bán hoặc phá hủy công trình.</summary>
//         Destroy = 16,

//         /// <summary> Thay đổi hoàn toàn dạng hoặc chức năng của công trình/đơn vị.</summary>
//         Morph = 32,

//         /// <summary> Kích hoạt khả năng đặc biệt.</summary>
//         SpecialAbility = 64,

//         AllAction = -1
//     }
// }