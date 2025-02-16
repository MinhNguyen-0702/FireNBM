using UnityEngine;
using System;

namespace FireNBM
{
    /// <summary>
    /// Lớp `UnitBasicCombatStateFactory` chịu trách nhiệm tạo các trạng thái chiến đấu cơ bản nâng cao 
    /// cho các đơn vị trong trò chơi RTS.
    /// </summary>
    public class UnitBasicCombatStateFactory : IUnitStateFactory
    {
        /// <summary>
        /// Tạo một trạng thái dựa trên kiểu trạng thái (typeState) và đối tượng sở hữu (owner).
        /// </summary>
        /// <param name="typeState">Kiểu trạng thái cần tạo, thuộc kiểu <see cref="TypeRaceUnitBasicCombat"/>.</param>
        /// <param name="owner">GameObject sở hữu trạng thái này.</param>
        /// <returns>Trả về đối tượng trạng thái phù hợp, hoặc null nếu kiểu trạng thái không được hỗ trợ.</returns>
        public IUnitState FunCreateState(Enum typeState, GameObject owner)
        {
            return typeState switch
            {
                TypeRaceUnitBasicCombat.HitAndRun               => null,
                TypeRaceUnitBasicCombat.FocusFire               => null,
                TypeRaceUnitBasicCombat.HoldFormation           => null,
                TypeRaceUnitBasicCombat.DetectAndAttack         => null,
                TypeRaceUnitBasicCombat.Cloak                   => null,
                TypeRaceUnitBasicCombat.Rescue                  => null,
                TypeRaceUnitBasicCombat.SpreadOut               => null,
                TypeRaceUnitBasicCombat.AttackAndMove           => null,
                TypeRaceUnitBasicCombat.CallReinforcements      => null,
                TypeRaceUnitBasicCombat.TargetPriority          => null,

                _ => null
            };
        }
    }
}
