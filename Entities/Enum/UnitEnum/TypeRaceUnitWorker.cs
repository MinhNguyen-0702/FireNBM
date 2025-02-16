namespace FireNBM
{
    /// <summary>
    ///     Các hành động mà đơn vị công nhân có thể thực hiện trong trò chơi RTS.
    ///     Enum này phân loại các hành động của đơn vị công nhân, bao gồm các hành động cơ bản và đặc biệt.
    /// </summary>
    public enum TypeRaceUnitWorker
    {
        // Default
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Loại hành động không xác định hoặc chưa được chỉ định.
        /// </summary>
        None,


        // Common
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thu thập tài nguyên từ các nguồn tài nguyên trong game.
        /// </summary>
        Gather, 

        /// <summary>
        ///     Trả lại tài nguyên đã thu thập cho kho hoặc công trình.
        /// </summary>
        ReturnCargo,  

        /// <summary>
        ///     Xây dựng công trình từ các tài nguyên có sẵn.
        /// </summary>
        BuildStructure,


        // Terrain
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        /// <summary>
        ///     Sửa chữa công trình hoặc đơn vị bị hư hỏng, giúp chúng hoạt động trở lại.
        /// </summary>
        Terrain_Repair, 


        // Protoss
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        // Các hành động đặc biệt cho Protoss có thể được thêm vào đây.


        // Zerg
        // ---------------------------------------------------------
        /////////////////////////////////////////////////////////////

        // Các hành động đặc biệt cho Zerg có thể được thêm vào đây.
    }
}
