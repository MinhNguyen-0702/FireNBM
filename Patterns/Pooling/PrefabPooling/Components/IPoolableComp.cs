namespace FireNBM.Pattern
{
    /// <summary>
    ///     Thiết lập lại dữ liệu của thành phần.
    /// </summary>
    public interface IPoolableComp
    {
        /// <summary>
        ///     Được gọi khi cần lấy đối tượng.</summary>
        /// ---------------------------------------------
        void FunSpawned();

        /// <summary>
        ///     Được gọi khi thu hồi đối tượng.</summary>
        /// ---------------------------------------------
        void FunDespawned();
    }
}