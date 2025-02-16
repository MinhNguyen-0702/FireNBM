namespace FireNBM
{
    /// <summary>
    ///     Các hành động chiến đấu cơ bản nâng cao mà đơn vị có thể thực hiện trong trò chơi RTS.
    ///     Enum này phân loại các hành động liên quan đến chiến đấu hoặc chiến thuật trong trận đấu.
    /// </summary>
    public enum TypeRaceUnitBasicCombat
    {
        // Default
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Loại hành động không xác định hoặc chưa được chỉ định.
        /// </summary>
        None,


        // Advanced Combat
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Tấn công và rút lui ngay sau khi gây sát thương, thường dùng để quấy rối đối thủ.
        /// </summary>
        HitAndRun,

        /// <summary>
        ///     Giữ vị trí và tập trung hỏa lực vào một mục tiêu cụ thể.
        /// </summary>
        FocusFire,

        /// <summary>
        ///     Di chuyển đến vị trí chỉ định trong khi giữ đội hình hiện tại.
        /// </summary>
        HoldFormation,

        /// <summary>
        ///     Phát hiện và tấn công đơn vị tàng hình hoặc ngụy trang.
        /// </summary>
        DetectAndAttack,

        /// <summary>
        ///     Tạm thời ẩn đơn vị để tránh bị tấn công.
        /// </summary>
        Cloak,

        /// <summary>
        ///     Giải cứu đồng minh hoặc đơn vị bị bao vây.
        /// </summary>
        Rescue,

        /// <summary>
        ///     Phân tán đội hình để giảm thiểu sát thương diện rộng.
        /// </summary>
        SpreadOut,

        /// <summary>
        ///     Kết hợp tấn công và di chuyển đồng thời để duy trì áp lực.
        /// </summary>
        AttackAndMove,

        /// <summary>
        ///     Gọi viện trợ hoặc yêu cầu hỗ trợ từ các đơn vị khác.
        /// </summary>
        CallReinforcements,

        /// <summary>
        ///     Tấn công ưu tiên vào các đơn vị hỗ trợ hoặc mục tiêu quan trọng của đối phương.
        /// </summary>
        TargetPriority,
    }
}
