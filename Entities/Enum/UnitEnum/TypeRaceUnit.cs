namespace FireNBM
{
    /// <summary>
    ///     Các loại đơn vị cần có trong trò chơi RTS.
    ///     Enum này giúp phân loại các loại đơn vị trong game dựa trên chức năng và vai trò của chúng.
    /// </summary>
    public enum TypeRaceUnit
    {
        /// <summary>
        ///     Loại đơn vị không xác định hoặc chưa được chỉ định.
        /// </summary>
        None,  

        /// <summary>
        ///     Đơn vị cơ bản, chứa các lệnh và hành vi cơ bản của unit.
        /// </summary>
        Base,

        /// <summary>
        ///     Đại diện anh sách các công trình xây dựng. (sẽ liên kết với TypeRaceBuilding)
        /// </summary>
        ListBuilding,

        
        // -------------------------------------------------------------------------------
                         

        /// <summary>
        ///     Đơn vị công nhân, chuyên về thu thập tài nguyên hoặc xây dựng công trình.
        /// </summary>
        Worker,        

        /// <summary>
        ///     Đơn vị chiến đấu cơ bản, thường có sức mạnh và khả năng chiến đấu ở mức cơ bản.
        /// </summary>
        BasicCombat,   

        // /// <summary>
        // ///     Đơn vị chiến đấu cao cấp, với sức mạnh và kỹ năng vượt trội.
        // /// </summary>
        // Advanced,     

        // /// <summary>
        // ///     Đơn vị trên không, có khả năng di chuyển và chiến đấu trên không.
        // /// </summary>
        // Air,             

        // /// <summary>
        // ///     Đơn vị pháp sư, có khả năng triệu hồi phép thuật hoặc kỹ năng đặc biệt.
        // /// </summary>
        // Spellcaster,   

        // /// <summary>
        // ///     Đơn vị phòng thủ, chuyên dùng để bảo vệ khu vực hoặc công trình.
        // /// </summary>
        // Defensive,     

        // /// <summary>
        // ///     Đơn vị hỗ trợ, cung cấp sự giúp đỡ cho các đơn vị khác, như chữa trị hoặc tăng cường.
        // /// </summary>
        // Support,       

        // /// <summary>
        // ///     Đơn vị anh hùng, thường chỉ xuất hiện trong chế độ chiến dịch, có khả năng mạnh mẽ và thường không thể tái sinh.
        // /// </summary>
        // Hero,           


        /// <summary>
        ///     Nơi đại diện cho các lệnh hỗ trợ.
        /// </summary>
        Other,
    }
}
