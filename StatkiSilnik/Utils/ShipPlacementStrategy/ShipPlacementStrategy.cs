using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils.ShipPlacementStrategy
{
    public interface ShipPlacementStrategy
    {
        GameBoard placeShips(List<ShipBase> Ships);
    }
}
