using System;

namespace FireNBM
{
    /// <summary>
    ///     Các trạng thái mà một chủ thể có thể có.
    /// </summary>
    public enum TypeUnitAnimState
    {
        None,
        Attack,
        Defense,
        Move,
        Idle,
        Collect,
        Death,
        Worker,
    }
}