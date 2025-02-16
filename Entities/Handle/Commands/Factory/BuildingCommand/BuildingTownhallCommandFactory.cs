using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    /// Lớp `BuildingTownhallCommandFactory` chịu trách nhiệm tạo các lệnh dành cho các công trình thu thập tài nguyên trong trò chơi RTS.
    /// </summary>
    public class BuildingTownhallCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu hành động của công trình thu thập tài nguyên.
        /// </summary>
        /// <param name="typeAction">Kiểu hành động cần tạo, thuộc kiểu <see cref="TypeRaceBuildingTownhall"/>.</param>
        /// <returns>Trả về một đối tượng lệnh tương ứng hoặc null nếu không có lệnh nào được chỉ định.</returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceBuildingTownhall.CreateUnitWorker => new CommandBuildingCreateUnitWorker(),
                _ => null
            };
        }
    }
}
