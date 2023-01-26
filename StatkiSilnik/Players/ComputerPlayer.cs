using StatkiSilnik.Ships;
using StatkiSilnik.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer() : base()
        {
            shipPlacementTool = new ShipPlacementTool();
            GameBoard = shipPlacementTool.placeShipsAtRandom(ShipList,rnd);
        }
    }
}
