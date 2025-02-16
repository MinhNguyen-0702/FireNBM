using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một nhà máy được dùng để tạo trạng thái state building dựa trên loại enum đầu vào cho đối tượng.
    /// </summary>
    public interface IBuildingStateFactory
    {
        IBuildingState FunCreateState(Enum typeState, GameObject owner);
    }
}