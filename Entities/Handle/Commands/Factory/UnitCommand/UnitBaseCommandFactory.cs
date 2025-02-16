using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Một nhà máy tạo được dùng để tạo ra các lệnh Command dành cho các đơn vị loại cơ bản (unit base).
    ///     Lớp này xử lý việc tạo ra các lệnh điều khiển các hành động cơ bản của đơn vị như di chuyển, dừng, giữ vị trí, tuần tra và tấn công.
    /// </summary>
    public class UnitBaseCommandFactory : ICommandFacrory
    {
        /// <summary>
        /// Tạo một lệnh Command dựa trên kiểu hành động của unit Base.
        /// </summary>
        /// <param name="typeAction">Kiểu hành động cần tạo, thuộc kiểu <see cref="TypeRaceUnitBase"/>.</param>
        /// <returns>Trả về một đối tượng lệnh tương ứng hoặc null nếu không có lệnh nào được chỉ định.</returns>
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceUnitBase.Move           => new CommandUnitBaseMove(),
                TypeRaceUnitBase.Stop           => new CommandUnitBaseStop(),
                TypeRaceUnitBase.HoldPosition   => new CommandUnitBaseHoldPosition(),
                TypeRaceUnitBase.Patrol         => new CommandUnitBasePatrol(),
                TypeRaceUnitBase.Attack         => new CommandUnitBaseAttack(),
                _=> null
            };
        }
    }
}