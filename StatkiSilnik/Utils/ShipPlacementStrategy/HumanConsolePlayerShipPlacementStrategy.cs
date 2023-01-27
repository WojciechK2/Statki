using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils.ShipPlacementStrategy
{
    public class HumanConsolePlayerShipPlacementStrategy : ShipPlacementStrategy
    {
        private GameBoard gb;
        private BoardValidator boardValidator;
        private Random rnd;
        private ShipPlacementTool placementTool;

        public HumanConsolePlayerShipPlacementStrategy()
        {
            boardValidator = new BoardValidator();
            rnd = new Random();
            placementTool = new ShipPlacementTool();
        }
        public GameBoard placeShips(List<ShipBase> Ships)
        {
            gb = new GameBoard();
            while (true)
            {
                foreach (ShipBase ship in Ships)
                {
                    while (true)
                    {
                        Console.WriteLine("Choose orientation for ship: " + ship.Name + "// 0 - vertical, 1 - horizontal");
                        string orientation_str = Console.ReadLine();
                        int orientation = int.Parse(orientation_str);

                        Console.WriteLine("Choose row for ship: " + ship.Name + "// range: 0-9");
                        string placeXstart_str = Console.ReadLine();
                        int placeXstart = int.Parse(placeXstart_str);

                        Console.WriteLine("Choose column for ship: " + ship.Name + "// range: 0-9");
                        string placeYstart_str = Console.ReadLine();
                        int placeYstart = int.Parse(placeYstart_str);

                        int placeXend = placeXstart;
                        int placeYend = placeYstart;

                        

                        if (gb.getFieldByCoordinates(placeXstart, placeYstart).MarkedSpace != MarkedSpace.Empty)
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        if (orientation == 0)
                        {
                            //Vertical placement
                            placeXend = placeXstart + ship.Width - 1;
                        }
                        else
                        {
                            //Horizontal placement
                            placeYend = placeYstart + ship.Width - 1;
                        }

                        //Set of placement checks
                        //Check if doesn't goes over Board
                        if (placeXend >= gb.Width || placeYend >= gb.Width)
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        //Check exact cells
                        if (!placementTool.areExactCellsEmpty(placeXstart, placeYstart, placeXend, placeYend, gb))
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        //Check cells below
                        if (!placementTool.areCellsBelowEmpty(placeXend + 1, placeYstart - 1, placeYend + 1, gb))
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        //Check cells above
                        if (!placementTool.areCellsAboveEmpty(placeXstart - 1, placeYstart - 1, placeYend + 1, gb))
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        //Check cells on left (vertical)
                        if (!placementTool.areCellsLeftEmpty(placeXstart - 1, placeYstart - 1, placeXend + 1, gb))
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }
                        //Check cells on right (vertical)
                        if (!placementTool.areCellsRightEmpty(placeXstart - 1, placeYend + 1, placeXend + 1, gb))
                        {
                            Console.WriteLine("Invalid Placement, try again");
                            continue;
                        }

                        placementTool.setExactCells(placeXstart, placeYstart, placeXend, placeYend, ship.Width, gb);
                        gb.printBoardText();
                        break;
                    }

                }

                //aditional check at the end of the generation loop
                bool validBoard = boardValidator.isBoardValid(gb);

                if (!validBoard)
                {
                    Console.WriteLine("Invalid Board, try again from the start");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return gb;
        }
    }
}
