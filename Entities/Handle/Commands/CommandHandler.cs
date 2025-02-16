using System;
using System.Collections.Generic;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý lệnh enum liên quan đến việc tạo lệnh thực thi cho nút button action.
    /// </summary>
    static class CommandHandler
    {
        static private Dictionary<Type, ICommandFacrory> m_commandHandlers;


        // -------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////

        static CommandHandler()
        {
            m_commandHandlers = new Dictionary<Type, ICommandFacrory>
            {
                // For Handle Unit
                { typeof(TypeRaceUnitBase),         new UnitBaseCommandFactory() },
                { typeof(TypeRaceUnitWorker),       new UnitWorkerCommandFactory() },
                { typeof(TypeRaceUnitBasicCombat),  new UnitBasicCombatCommandFactory() },
                { typeof(TypeRaceBuilding),         new UnitListBuildingCommandFactory() },

                // For Handle Building
                { typeof(TypeRaceBuildingTownhall),             new BuildingTownhallCommandFactory() },
                { typeof(TypeRaceBuildingSupplyBuildings),      new BuildingSupplyBuildingsCommandFactory() },
                { typeof(TypeRaceBuildingProductionBuildings),  new BuildingProductionBuildingsCommandFactory() },

                // For Handle UnderConstruction

                // For Other.
                { typeof(TypeOther), new OtherCommandFactory() }
            };
        }


        // -------------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////////////////

        static public ICommand FunCreate(Enum typeAction, GameObject owner = null)
        {
            var type = typeAction.GetType();
            return m_commandHandlers.ContainsKey(type) 
                   ? m_commandHandlers[type].FunCreateCommand(typeAction)
                   : null;
        }
    }
}