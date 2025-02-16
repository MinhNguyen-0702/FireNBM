namespace FireNBM
{
    /// <summary>
    ///     Các hành động cơ bản mà đơn vị có thể thực hiện trong trò chơi RTS.
    ///     Enum này phân loại các hành động cơ bản của đơn vị, bao gồm di chuyển, dừng lại, phòng thủ, tuần tra và tấn công.
    /// </summary>
    public enum TypeRaceUnitBase
    {
        // Default
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Loại hành động không xác định hoặc chưa được chỉ định.
        /// </summary>
        None,

        Free,


        // Common
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thực hiện di chuyển đến vị trí mới hoặc theo một lộ trình đã chỉ định.
        /// </summary>
        Move,  

        /// <summary>
        ///     Dừng tất cả các hành động hiện tại của đơn vị và giữ nguyên vị trí.
        /// </summary>
        Stop,  

        /// <summary>
        ///     Đơn vị giữ vị trí hiện tại và thực hiện hành động phòng thủ nếu có mối đe dọa.
        /// </summary>
        HoldPosition,  

        /// <summary>
        ///     Đơn vị di chuyển qua một khu vực theo một lộ trình tuần tra đã chỉ định.
        /// </summary>
        Patrol, 

        /// <summary>
        ///     Đơn vị tấn công mục tiêu hoặc đối tượng được chỉ định.
        /// </summary>
        Attack,  
    }
}
