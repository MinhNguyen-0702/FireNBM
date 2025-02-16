using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    /// Lớp `BuildingSupplyBuildingsCommandFactory` chịu trách nhiệm tạo các lệnh dành cho các công trình cung cấp năng lực dân số trong trò chơi RTS.
    /// </summary>
    public class BuildingSupplyBuildingsCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu công trình cung cấp năng lực dân số.
        /// </summary>
        /// <param name="typeAction">Kiểu công trình cần tạo, thuộc kiểu <see cref="TypeRaceBuildingSupplyBuildings"/>.</param>
        /// <returns>Trả về một đối tượng lệnh tương ứng hoặc null nếu không có lệnh nào được chỉ định.</returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                _ => null
            };
        }
    }
}
