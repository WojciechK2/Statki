using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;

namespace StatkiSilnik.Utils.ShipPlacementStrategy
{
    public class ComputerPlayerShipPlacementStrategy : ShipPlacementStrategy
    {
        private GameBoard gb;
        private BoardValidator boardValidator;
        private Random rnd;
        private ShipPlacementTool placementTool;

        public ComputerPlayerShipPlacementStrategy()
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
                        int placeXstart = rnd.Next(gb.Width);
                        int placeYstart = rnd.Next(gb.Width);
                        int placeXend = placeXstart;
                        int placeYend = placeYstart;

                        int orientation = rnd.Next(1, 101) % 2;

                        if (gb.getFieldByCoordinates(placeXstart, placeYstart).MarkedSpace != MarkedSpace.Empty)
                        {
                            continue;
                        }
                        if (orientation == 0)
                        {
                            //Horizontal placement
                            placeXend = placeXstart + ship.Width - 1;
                        }
                        else
                        {
                            //Vertical placement
                            placeYend = placeYstart + ship.Width - 1;
                        }

                        //Set of placement checks
                        //Check if doesn't goes over Board
                        if (placeXend >= gb.Width || placeYend >= gb.Width)
                        {
                            continue;
                        }
                        //Check exact cells
                        if (!placementTool.areExactCellsEmpty(placeXstart, placeYstart, placeXend, placeYend, gb))
                        {
                            continue;
                        }
                        //Check cells below
                        if (!placementTool.areCellsBelowEmpty(placeXend + 1, placeYstart - 1, placeYend + 1, gb))
                        {
                            continue;
                        }
                        //Check cells above
                        if (!placementTool.areCellsAboveEmpty(placeXstart - 1, placeYstart - 1, placeYend + 1, gb))
                        {
                            continue;
                        }
                        //Check cells on left (vertical)
                        if (!placementTool.areCellsLeftEmpty(placeXstart - 1, placeYstart - 1, placeXend + 1, gb))
                        {
                            continue;
                        }
                        //Check cells on right (vertical)
                        if (!placementTool.areCellsRightEmpty(placeXstart - 1, placeYend + 1, placeXend + 1, gb))
                        {
                            continue;
                        }

                        placementTool.setExactCells(placeXstart, placeYstart, placeXend, placeYend, ship.Width, gb);

                        break;
                    }

                }

                //aditional check at the end of the generation loop
                bool validBoard = boardValidator.isBoardValid(gb);

                if (!validBoard)
                {
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
