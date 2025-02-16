using UnityEngine;
using System;

namespace FireNBM
{
    /// <summary>
    /// Lớp `UnitWorkerStateFactory` chịu trách nhiệm tạo các trạng thái cho các đơn vị làm việc (worker) trong trò chơi.
    /// </summary>
    public class UnitWorkerStateFactory : IUnitStateFactory
    {
        /// <summary>
        /// Tạo một trạng thái dựa trên kiểu trạng thái (typeState) và đối tượng sở hữu (owner).
        /// </summary>
        /// <param name="typeState">Kiểu trạng thái cần tạo, thuộc kiểu <see cref="TypeRaceUnitWorker"/>.</param>
        /// <param name="owner">GameObject sở hữu trạng thái này.</param>
        /// <returns>Trả về đối tượng trạng thái phù hợp, hoặc null nếu kiểu trạng thái không được hỗ trợ.</returns>
        public IUnitState FunCreateState(Enum typeState, GameObject owner)
        {
            return typeState switch
            {
                TypeRaceUnitWorker.Gather           => new StateUnitWorkerGather(owner),
                TypeRaceUnitWorker.ReturnCargo      => null,
                TypeRaceUnitWorker.BuildStructure   => new StateUnitWorkerBuildStructure(owner),
                
                TypeRaceUnitWorker.Terrain_Repair   => null,

                _ => null
            };
        }
    }
}
