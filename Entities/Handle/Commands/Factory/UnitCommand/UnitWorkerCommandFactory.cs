using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    /// Một nhà máy tạo được dùng để tạo ra lệnh Command dành cho unit loại Worker - Công nhân.
    /// </summary>
    public class UnitWorkerCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu hành động của unit Worker.
        /// </summary>
        /// <param name="typeAction">Kiểu hành động cần tạo, thuộc kiểu <see cref="TypeRaceUnitWorker"/>.</param>
        /// <returns>Trả về một đối tượng lệnh tương ứng hoặc null nếu không có lệnh nào được chỉ định.</returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceUnitWorker.Gather           => new CommandUnitWorkerGather(),
                TypeRaceUnitWorker.ReturnCargo      => new CommandUnitWorkerReturnCargo(),
                TypeRaceUnitWorker.BuildStructure   => new CommandUnitWorkerBuildStructure(),

                TypeRaceUnitWorker.Terrain_Repair   => new CommandUnitWorkerRepair(),
                _ => null
            };
        }
    }
}
 