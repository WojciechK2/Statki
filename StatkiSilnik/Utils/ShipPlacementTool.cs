using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils
{
    public class ShipPlacementTool
    {
        private GameBoard gb;
        private Random rnd;
        private BoardValidator boardValidator;

        public ShipPlacementTool()
        {
            boardValidator = new BoardValidator();
        }
        public GameBoard placeShipsAtRandom(List<ShipBase> Ships)
        {
            rnd = new Random();
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
                        if (!areExactCellsEmpty(placeXstart, placeYstart, placeXend, placeYend))
                        {
                            continue;
                        }
                        //Check cells below
                        if (!areCellsBelowEmpty(placeXend + 1, placeYstart - 1, placeYend + 1))
                        {
                            continue;
                        }
                        //Check cells above
                        if (!areCellsAboveEmpty(placeXstart - 1, placeYstart - 1, placeYend + 1))
                        {
                            continue;
                        }
                        //Check cells on left (vertical)
                        if (!areCellsLeftEmpty(placeXstart - 1, placeYstart - 1, placeXend + 1))
                        {
                            continue;
                        }
                        //Check cells on right (vertical)
                        if (!areCellsRightEmpty(placeXstart - 1, placeYend + 1, placeXend + 1))
                        {
                            continue;
                        }

                        setExactCells(placeXstart, placeYstart, placeXend, placeYend, ship.Width);

                        break;
                    }

                }

                //aditional check at the end of the generation loop
                bool validBoard = boardValidator.isBoardValid(gb);

                if (!validBoard)
                {
                    continue;
                } else
                {
                    break;
                }
            }
            return gb;
        }

        private bool areExactCellsEmpty(int startX, int startY, int endX, int endY)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    if (gb.getFieldByCoordinates(i, j).MarkedSpace != MarkedSpace.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool areCellsBelowEmpty(int startX, int startY, int endY)
        {
            if (startX >= (gb.Width))
            {
                // Do not check if cells below are outside of board
                return true;
            }
            if (startY < 0)
            {
                //Correct loop starting points on edges
                startY = 0;
            }
            if (endY >= gb.Width)
            {
                //Correct loop endong points on edges
                endY = gb.Width - 1;
            }
            return areExactCellsEmpty(startX, startY, startX, endY);
        }

        private bool areCellsAboveEmpty(int startX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (startY < 0)
            {
                //Correct loop starting points on edges
                startY = 0;
            }
            if (endY >= gb.Width)
            {
                //Correct loop endong points on edges
                endY = gb.Width - 1;
            }
            return areExactCellsEmpty(startX, startY, startX, endY);
        }
        private bool areCellsLeftEmpty(int startX, int startY, int endX)
        {
            if (startY < 0)
            {
                return true;
            }
            if (endX >= gb.Width)
            {
                endX = gb.Width - 1;
            }
            if (startX < 0)
            {
                startX = 0;
            }
            return areExactCellsEmpty(startX, startY, endX, startY);
        }
        private bool areCellsRightEmpty(int startX, int endY, int endX)
        {
            if (endY >= gb.Width)
            {
                return true;
            }
            if (startX < 0)
            {
                startX = 0;
            }
            if (endX >= gb.Width)
            {
                endX = gb.Width - 1;
            }
            return areExactCellsEmpty(startX, endY, endX, endY);
        }
        private void setExactCells(int startX, int startY, int endX, int endY, int shipWidth)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    //Console.WriteLine("Writting Fiels {0},{1}", i, j);
                    if (shipWidth == 4)
                    {
                        gb.setFieldByCoordinates(i, j, MarkedSpace.Czteromasztowiec);
                    }
                    if (shipWidth == 3)
                    {
                        gb.setFieldByCoordinates(i, j, MarkedSpace.Trojmasztowiec);
                    }
                    if (shipWidth == 2)
                    {
                        gb.setFieldByCoordinates(i, j, MarkedSpace.Dwumasztowiec);
                    }
                    if (shipWidth == 1)
                    {
                        gb.setFieldByCoordinates(i, j, MarkedSpace.Jednomasztowiec);
                    }
                }
            }
        }
    }   
}
