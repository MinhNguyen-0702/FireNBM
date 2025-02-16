namespace FireNBM
{
    /// <summary>
    ///     <para> => Đây là nơi chứa các lệnh thuộc nhóm '<seealso cref="TypeRaceBuildingTownhall"/>'. </para>
    /// 
    ///     Tòa thị chính (Townhall/main building) là công trình quan trọng nhất. 
    ///     Đây là nơi công nhân mang tài nguyên thu thập được về, đồng thời cũng là nơi sản xuất công nhân. 
    ///     Ngoài ra, nó còn là điều kiện cần để xây dựng cơ sở sản xuất đầu tiên.
    /// </summary>
    public enum TypeRaceBuildingTownhall
    {
        /// <summary>
        ///     Không có hành động nào được chỉ định.
        /// </summary>
        None,

        Free,

        CreateUnitWorker
    }
}
