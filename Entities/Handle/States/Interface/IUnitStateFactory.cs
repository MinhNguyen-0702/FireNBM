using System;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một nhà máy được dùng để tạo trạng thái state unit dựa trên loại enum đầu vào cho đối tượng.
    /// </summary>
    public interface IUnitStateFactory
    {
        IUnitState FunCreateState(Enum typeState, GameObject owner);
    }
}