using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lớp này chịu trách nhiệm tạo ra các lệnh (commands) cho các hành động chiến đấu cơ bản nâng cao của đơn vị.
    ///     Nó thực hiện việc tạo các lệnh dựa trên kiểu hành động được chỉ định, giúp quản lý và điều khiển các hành động chiến đấu trong trò chơi.
    /// </summary>
    public class UnitBasicCombatCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu hành động chiến đấu cơ bản của đơn vị.
        /// </summary>
        /// <param name="typeAction">Kiểu hành động cần tạo, thuộc kiểu <see cref="TypeRaceUnitBasicCombat"/>.</param>
        /// <returns>
        /// Trả về đối tượng lệnh tương ứng với hành động chiến đấu cơ bản, hoặc <c>null</c> nếu không có hành động nào được chỉ định.
        /// </returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceUnitBasicCombat.HitAndRun           => null,
                TypeRaceUnitBasicCombat.FocusFire           => null,
                TypeRaceUnitBasicCombat.HoldFormation       => null,
                TypeRaceUnitBasicCombat.DetectAndAttack     => null,
                TypeRaceUnitBasicCombat.Cloak               => null,
                TypeRaceUnitBasicCombat.Rescue              => null,
                TypeRaceUnitBasicCombat.SpreadOut           => null,
                TypeRaceUnitBasicCombat.AttackAndMove       => null,
                TypeRaceUnitBasicCombat.CallReinforcements  => null,
                TypeRaceUnitBasicCombat.TargetPriority      => null,

                // Trường hợp mặc định: nếu không có hành động nào được chỉ định, trả về null.
                _ => null
            };
        }
    }
}
