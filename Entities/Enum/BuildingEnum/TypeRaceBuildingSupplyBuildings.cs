namespace FireNBM
{
    /// <summary>
    ///     <para> => Đây là nơi chứa các lệnh thuộc nhóm '<seealso cref="TypeRaceBuildingSupplyBuildings"/>'. </para>
    /// 
    ///     Các công trình này giúp tăng tổng số lượng đơn vị mà người chơi có thể điều khiển cùng một lúc (tối đa 200 supply).
    /// </summary>
    public enum TypeRaceBuildingSupplyBuildings
    {
        /// <summary>
        ///     Loại công trình chưa xác định hoặc không có công trình cụ thể.
        /// </summary>
        None,

        /// <summary>
        ///     Công trình cơ bản cung cấp năng lực dân số, thường là trại lính hoặc nhà ở.
        /// </summary>
        BasicHousing,

        /// <summary>
        ///     Công trình cao cấp giúp tăng năng lực dân số, như các trung tâm huấn luyện hoặc căn cứ.
        /// </summary>
        AdvancedHousing,

        /// <summary>
        ///     Công trình mở rộng năng lực dân số, cho phép tăng cường dân số và hỗ trợ cho các công trình lớn.
        /// </summary>
        PopulationExpansion,

        /// <summary>
        ///     Công trình đặc biệt hỗ trợ tăng năng lực dân số cho các đơn vị đặc biệt hoặc một chủng tộc riêng biệt.
        /// </summary>
        SpecialHousing
    }
}


// trung tâm: tạo worker, chỉ định vị trí xuất worker, 
// Worker: thu thập tài nguyên, xây công trình, 


// AI: Tuần tra giữa 2 điểm.

// gameplay: tiêu diệt đủ số quá vật trong một khoảng thời gian.

// Load sceen, audio, setting,