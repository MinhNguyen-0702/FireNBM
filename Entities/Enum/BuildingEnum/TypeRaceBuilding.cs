namespace FireNBM
{
    /// <summary>
    ///     Enum đại diện cho các loại công trình trong game, phân loại theo chức năng của chúng.
    ///     Điều này giúp phân loại công trình dựa trên vai trò của chúng trong cơ sở hạ tầng của một chủng tộc.
    /// </summary>
    public enum TypeRaceBuilding
    {
        /// <summary>
        ///     Đại diện cho không có loại công trình hoặc loại công trình chưa xác định.
        /// </summary>
        None,

        /// <summary>
        ///     Tòa thị chính (Townhall/main building) là công trình quan trọng nhất. 
        ///     Đây là nơi công nhân mang tài nguyên thu thập được về, đồng thời cũng là nơi sản xuất công nhân. 
        ///     Ngoài ra, nó còn là điều kiện cần để xây dựng cơ sở sản xuất đầu tiên.
        /// </summary>
        Townhall,     
            
        /// <summary>
        ///     Trước khi có thể khai thác khí Vespene từ một Geyser (mạch khí), 
        ///     người chơi cần phải xây dựng một công trình khai thác trên đó.
        /// </summary>
        GasBuildings,                 

        /// <summary>
        ///     Các công trình này giúp tăng tổng số lượng đơn vị mà người chơi có thể điều khiển cùng một lúc (tối đa 200 supply).
        /// </summary>
        SupplyBuildings,    

        /// <summary>
        ///     Những công trình này được xây dựng để bảo vệ một khu vực nhất định. Mục đích chính của chúng là ngăn chặn hoặc ít
        ///     nhất là làm đối thủ nản lòng khi tấn công khu vực đó, nhưng chúng cũng có thể được sử dụng cùng với quân đội.
        /// </summary>
        StaticDefenseBuildings,

        /// <summary>
        ///     Những công trình này là nơi sản xuất phần lớn các đơn vị tấn công.
        /// </summary>
        ProductionBuildings,
        
        /// <summary>
        ///     Những công trình này mở ra công nghệ mới cho người chơi, có thể là các đơn vị, công trình, 
        ///     và/hoặc nâng cấp cho đơn vị/công trình. Hầu hết các công trình — ngay cả các công trình Sản 
        ///     Xuất và Tòa Thị Chính — đều thuộc loại này.
        /// </summary>
        TechnologyBuildings,

        /// <summary>
        ///     Đây là những công trình được người tạo bản đồ đặt vào. Chúng đôi khi được đặt với mục đích trang trí, 
        ///     hoặc có một mục đích cụ thể trong một trận đấu theo chế độ Use Map Settings. Trong các bản đồ cạnh tranh, 
        ///     hầu hết các công trình trung lập là Destructible Rocks (Đá có thể phá hủy), cung cấp một con đường mới 
        ///     cho các đơn vị di chuyển, hoặc để chặn các mỏ tài nguyên High Yield, v.v. Một số bản đồ có các Supply Depots trung lập, 
        ///     thường để ngăn việc xây dựng tường chặn lối vào ramp của đối thủ.
        /// </summary>
        NeutralBuildings,

        /// <summary>
        ///     Nơi đại diện cho các lệnh hỗ trợ.
        /// </summary>
        Other,        
    }
}
