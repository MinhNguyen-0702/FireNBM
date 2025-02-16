using FireNBM.Pattern;
using System;

namespace FireNBM
{
    public class UnitListBuildingCommandFactory : ICommandFacrory
    {
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return typeAction switch
            {
                TypeRaceBuilding.Townhall            => new CommandListBuildingTownhall(),
                TypeRaceBuilding.SupplyBuildings     => new CommandListBuildingSupplyBuildings(),
                TypeRaceBuilding.ProductionBuildings => new CommandListBuildingProductionBuildings(),
                _=> null
            };
        }
    }
}