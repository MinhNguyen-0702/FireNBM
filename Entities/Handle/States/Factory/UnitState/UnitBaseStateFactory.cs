using UnityEngine;
using System;

namespace FireNBM
{
    /// <summary>
    /// Lớp `UnitBaseStateFactory` chịu trách nhiệm tạo các trạng thái cơ bản cho đơn vị (unit) trong trò chơi.
    /// </summary>
    public class UnitBaseStateFactory : IUnitStateFactory
    {
        /// <summary>
        /// Tạo một trạng thái dựa trên kiểu trạng thái (typeState) và đối tượng sở hữu (owner).
        /// </summary>
        /// <param name="typeState">Kiểu trạng thái cần tạo, thuộc kiểu <see cref="TypeRaceUnitBase"/>.</param>
        /// <param name="owner">GameObject sở hữu trạng thái này.</param>
        /// <returns>Trả về đối tượng trạng thái phù hợp, hoặc null nếu kiểu trạng thái không hợp lệ.</returns>
        /// -------------------------------------------------------------
        public IUnitState FunCreateState(Enum typeState, GameObject owner)
        {
            return typeState switch
            {
                TypeRaceUnitBase.Move           => new StateUnitBaseMove(owner),
                TypeRaceUnitBase.Stop           => new StateUnitBaseStop(owner),
                TypeRaceUnitBase.HoldPosition   => new StateUnitBaseHoldPosition(owner),
                TypeRaceUnitBase.Patrol         => new StateUnitBasePatrol(owner),
                TypeRaceUnitBase.Attack         => new StateUnitBaseAttack(owner),
                
                _ => null
            };
        }
    }
}
