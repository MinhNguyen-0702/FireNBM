using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    /// Lớp `BuildingProductionBuildingsCommandFactory` chịu trách nhiệm tạo các lệnh dành cho các công trình sản xuất quân đội trong trò chơi RTS.
    /// </summary>
    public class BuildingProductionBuildingsCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu hành động của công trình sản xuất quân đội.
        /// </summary>
        /// <param name="typeAction">Kiểu hành động cần tạo, thuộc kiểu <see cref="TypeRaceBuildingProductionBuildings"/>.</param>
        /// <returns>Trả về một đối tượng lệnh tương ứng hoặc null nếu không có lệnh nào được chỉ định.</returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceBuildingProductionBuildings.CreateUnitBase => new CommandBuildingCreateUnitBase(),
                _ => null
            };
        }
    }
}
