using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     interface cho các hành động cần được xử lý dựa trên enum.
    /// </summary>
    public interface IActionHandler<T>
    {
        T FunCreate(Enum TypeAction, GameObject owner = null);
    }

    /// <summary>
    ///     Một lớp trợ giúp chứa thông tin cần trả về của 'FormationPatternHandler'
    /// </summary>
    public class FormationActionResult
    {
        public bool IsRepeat;
        public IFormationPattern Pattern;

        public FormationActionResult(IFormationPattern pattern, bool isRepeat)
        {
            IsRepeat = isRepeat;
            Pattern = pattern;
        }
    }

    /// <summary>
    ///     Xử lý lệnh enum liên quan đế mẫu đội hình.
    /// </summary>
    public class FormationPatternHandler : IActionHandler<FormationActionResult>
    {
        private Dictionary<Enum, Func<FormationActionResult>> m_formationHandlers;


        // -----------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////////

        public FormationPatternHandler()
        {
            m_formationHandlers = new Dictionary<Enum, Func<FormationActionResult>>
            {
                // For: TypeActionUnitBase
                { TypeRaceUnitBase.Move,         () => new FormationActionResult(new DefensiveCriclePattern(), false) }, 
                { TypeRaceUnitBase.HoldPosition, () => new FormationActionResult(new DefensiveCriclePattern(), false) }, 
                { TypeRaceUnitBase.Patrol,       () => new FormationActionResult(new DefensiveCriclePattern(), true)  }, 
                { TypeRaceUnitBase.Attack,       () => new FormationActionResult(new DefensiveCriclePattern(), false) }, 
            };
        }


        // -------------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////////////////

        public FormationActionResult FunCreate(Enum typeAction, GameObject owner = null)
        {
            return m_formationHandlers.ContainsKey(typeAction)  // Kiểm tra xem có chứa khóa là typeAction trong map ko.
                   ? m_formationHandlers[typeAction]()          // Có thì gọi hàm lamda để tạo ra đối tượng mới. '()' là ký hiệu gọi hàm.
                   : null;                                      // Nếu ko có.
        }
    }
}