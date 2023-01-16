using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players
{
    public class ComputerPlayer : Player
    {
        private Random rnd = new Random();
        public ComputerPlayer() : base()
        {

        }
        public void setUpShipsRandom()
        {
            foreach (ShipBase ship in Ships)
            {
                while (true)
                {
                    int placeXstart = rnd.Next(GameBoard.Width);
                    int placeYstart = rnd.Next(GameBoard.Width);
                    int placeXend = placeXstart;
                    int placeYend = placeYstart;

                    int orientation = rnd.Next(1,101) % 2;

                    if (GameBoard.getFieldByCoordinates(placeXstart, placeYstart).MarkedSpace != MarkedSpace.Empty)
                    {
                        continue;
                    }
                    if(orientation == 0)
                    {
                       //Horizontal placement
                       placeXend = placeXstart + ship.Width - 1;
                    } else
                    {
                       //Vertical placement
                       placeYend = placeYstart + ship.Width - 1;
                    }

                    //Set of placement checks

                    //Check if doesn't goes over Board
                    if (placeXend >= GameBoard.Width || placeYend >= GameBoard.Width)
                    {
                        continue;
                    }
                    //Check exact cells
                    if (!checkExactCells(placeXstart, placeYstart, placeXend, placeYend))
                    {
                        continue;
                    }
                    //Check cells below
                    if (!checkCellsBelow(placeXstart - 1, placeXend +1, placeYend +1))
                    {
                        continue;
                    }
                    //Check cells above
                    if (!checkCellsAbove(placeXstart -1, placeYstart - 1, placeXend + 1))
                    {
                        continue;
                    }
                    //Check cells on left (vertical)
                    if (!checkCellsLeft(placeXend - 1, placeYstart - 1, placeYend + 1))
                    {
                        continue;
                    }
                    //Check cells on right (vertical)
                    if (!checkCellsRight(placeXend + 1, placeYstart - 1, placeYend + 1))
                    {
                        continue;
                    }

                    //Console.WriteLine("Finally");
                    //finally
                    //writting the cells to the GameBoard
                    setExactCells(placeXstart,placeYstart,placeXend,placeYend,ship.Width);
                    /*setCellsBelow(placeXstart - 1, placeXend + 1, placeYend + 1);
                    setCellsAbove(placeXstart - 1, placeYstart - 1, placeXend + 1);
                    setCellsLeft(placeXstart - 1, placeYstart - 1, placeYend + 1);
                    setCellsRight(placeXend + 1, placeYstart - 1, placeYend + 1);
                    */
                    break;
                }
            }
        }
        private bool checkExactCells(int startX, int startY, int endX, int endY)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    //Console.WriteLine(GameBoard.getFieldByCoordinates(i, j).MarkedSpace);
                    if (GameBoard.getFieldByCoordinates(i, j).MarkedSpace != MarkedSpace.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool checkCellsBelow(int startX, int endX, int endY)
        {
            if (endY >= (GameBoard.Width))
            {
                // Do not check if cells below are outside of board
                return true;
            }
            if (startX < 0)
            {
                //Correct loop starting points on edges
                startX = 0;
            }
            if (endX >= GameBoard.Width)
            {
                //Correct loop endong points on edges
                endX = GameBoard.Width - 1;
            }
            return checkExactCells(startX, endY, endX, endY);
        }

        private bool checkCellsAbove(int startX, int startY, int endX)
        {
            if (startY <= 0)
            {
                return true;
            }
            if (startX < 0)
            {
                //Correct loop starting points on edges
                startX = 0;
            }
            if (endX >= GameBoard.Width)
            {
                //Correct loop endong points on edges
                endX = GameBoard.Width - 1;
            }
            return checkExactCells(startX, startY, endX, startY);
        }
        private bool checkCellsLeft(int startX, int startY, int endY)
        {
            if (startX < 0)
            {
                return true;
            }
            if (endY >= GameBoard.Width)
            {
                endY = GameBoard.Width - 1;
            }
            if (startY < 0)
            {
                startY = 0;
            }
            return checkExactCells(startX, startY, startX, endY);
        }
        private bool checkCellsRight(int endX, int startY, int endY)
        {
            if (endX >= GameBoard.Width)
            {
                return true;
            }
            if (startY < 0)
            {
                startY = 0;
            }
            if (endY >= GameBoard.Width)
            {
                endY = GameBoard.Width - 1;
            }
            return checkExactCells(endX, startY, endX, endY);
        }
        private bool setExactCells(int startX, int startY, int endX, int endY, int shipWidth)
        {
            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    Console.WriteLine("Writting Fiels {0},{1}", i, j);
                    if (shipWidth == 4)
                    {
                        GameBoard.setFieldByCoordinates(i, j, MarkedSpace.Czteromasztowiec);
                    }
                    if (shipWidth == 3)
                    {
                        GameBoard.setFieldByCoordinates(i, j, MarkedSpace.Trojmasztowiec);
                    }
                    if (shipWidth == 2)
                    {
                        GameBoard.setFieldByCoordinates(i, j, MarkedSpace.Dwumasztowiec);
                    }
                    if (shipWidth == 1)
                    {
                        GameBoard.setFieldByCoordinates(i, j, MarkedSpace.Jednomasztowiec);
                    }
                }
            }
            return true;
        }
        public bool validateBoard()
        {
            for (int i = 0; i < GameBoard.Width; i++)
            {
                for (int j = 0; j < GameBoard.Width; j++)
                {
                    if (GameBoard.getFieldByCoordinates(i,j).MarkedSpace != MarkedSpace.Empty)
                    {
                        bool isValid = checkFields(i, j);
                        Console.WriteLine("Check {0},{1}", i, j);
                        if (isValid)
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool checkFields(int i, int j)
        {
            MarkedSpace myMark = GameBoard.getFieldByCoordinates(i, j).MarkedSpace;
            MarkedSpace upperLeft = GameBoard.getFieldByCoordinates(i - 1, j - 1).MarkedSpace;
            MarkedSpace upper = GameBoard.getFieldByCoordinates(i, j - 1).MarkedSpace;
            MarkedSpace upperRight = GameBoard.getFieldByCoordinates(i + 1 , j - 1).MarkedSpace;
            MarkedSpace Left = GameBoard.getFieldByCoordinates(i - 1, j).MarkedSpace;
            MarkedSpace Right = GameBoard.getFieldByCoordinates(i + 1, j).MarkedSpace;
            MarkedSpace downLeft = GameBoard.getFieldByCoordinates(i - 1, j + 1).MarkedSpace;
            MarkedSpace down = GameBoard.getFieldByCoordinates(i, j + 1).MarkedSpace;
            MarkedSpace downRight = GameBoard.getFieldByCoordinates( i + 1 , j + 1).MarkedSpace;
            if (upperLeft != MarkedSpace.Empty ||
                upperLeft != myMark)
            {
                return false;
            }
            if (upper != MarkedSpace.Empty ||
                upper != myMark)
            {
                return false;
            }
            if (upperRight != MarkedSpace.Empty ||
                upperRight != myMark)
            {
                return false;
            }
            if (Left != MarkedSpace.Empty ||
                Left != myMark)
            {
                return false;
            }
            if (Right != MarkedSpace.Empty ||
                Right != myMark)
            {
                return false;
            }
            if (downLeft != MarkedSpace.Empty ||
               downLeft != myMark)
            {
                return false;
            }
            if (down != MarkedSpace.Empty ||
                down != myMark)
            {
                return false;
            }
            if (downRight != MarkedSpace.Empty ||
                downRight != myMark)
            {
                return false;
            }
            return true;
        }
    }
}
