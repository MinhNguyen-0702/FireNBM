using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý lệnh enum liên quan đến việc tạo trạng thái cho unit.
    /// </summary>
    public static class UnitStateHandler
    {
        static private Dictionary<Type, IUnitStateFactory> m_unitStateHandlers;

        // -------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////

        static UnitStateHandler()
        {
            m_unitStateHandlers = new Dictionary<Type, IUnitStateFactory>
            {
                { typeof(TypeRaceUnitBase),         new UnitBaseStateFactory() },
                { typeof(TypeRaceUnitWorker),       new UnitWorkerStateFactory() },
                { typeof(TypeRaceUnitBasicCombat),  new UnitBasicCombatStateFactory() },
            };
        }

        // --------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////

        static public IUnitState FunCreate(Enum typeAction, GameObject unit)
        {
            var type = typeAction.GetType();
            return m_unitStateHandlers.ContainsKey(type) 
                   ? m_unitStateHandlers[type].FunCreateState(typeAction, unit)
                   : null;
        }
    }
}