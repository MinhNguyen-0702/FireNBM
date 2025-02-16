using UnityEngine;
using System;

namespace FireNBM
{
    /// <summary>
    /// Lớp `BuildingTownhallStateFactory` chịu trách nhiệm tạo các trạng thái cho các công trình thu thập tài nguyên trong trò chơi RTS.
    /// </summary>
    public class BuildingTownhallStateFactory : IBuildingStateFactory
    {
        /// <summary>
        /// Tạo một trạng thái dựa trên kiểu trạng thái (typeState) và đối tượng sở hữu (owner).
        /// </summary>
        /// <param name="typeState">Kiểu trạng thái cần tạo, thuộc kiểu <see cref="TypeRaceBuildingResCollection"/>.</param>
        /// <param name="owner">GameObject sở hữu trạng thái này.</param>
        /// <returns>Trả về đối tượng trạng thái phù hợp, hoặc null nếu kiểu trạng thái không hợp lệ.</returns>
        public IBuildingState FunCreateState(Enum typeState, GameObject owner)
        {
            return typeState switch
            {
                TypeRaceBuildingTownhall.CreateUnitWorker => new StateBuildingCreateUnitWorker(owner),
                _ => null
            };
        }
    }
}
