using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils
{
    public class BoardValidator
    {
        public bool isBoardValid(GameBoard gb)
        {
            for (int i = 0; i < gb.Width; i++)
            {
                for (int j = 0; j < gb.Width; j++)
                {
                    MarkedSpace m = gb.getFieldByCoordinates(i, j).MarkedSpace;
                    if (m != MarkedSpace.Empty)
                    {
                        if (!areNeighboursValid(i, j, m, gb))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private bool areNeighboursValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            MarkedSpace myMark = gb.getFieldByCoordinates(i, j).MarkedSpace;

            if (isUpperLeftNeighbourValid(i, j, myMark, gb) ||
                isUpperNeighbourValid(i, j, myMark, gb) ||
                isUpperRightNeighbourValid(i, j, myMark, gb) ||
                isLeftNeighbourValid(i, j, myMark, gb) ||
                isRightNeighbourValid(i, j, myMark, gb) ||
                isDownLeftNeighbourValid(i, j, myMark, gb) ||
                isDownNeighbourValid(i, j, myMark, gb) ||
                isDownRightNeighbourValid(i, j, myMark, gb))
            {
                return true;
            }
            return false;
        }

        private bool isUpperLeftNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i - 1;
            j = j - 1;
            if (i < 0 || j < 0)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isUpperNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i - 1;
            if (i < 0)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isUpperRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i - 1;
            j = j + 1;
            if (i < 0 || j >= gb.Width)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isLeftNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            j = j - 1;
            if (j < 0)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            j = j + 1;
            if (j >= gb.Width)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isDownLeftNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i + 1;
            j = j - 1;
            if (i >= gb.Width || j < 0)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isDownNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i + 1;
            if (i >= gb.Width)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }

        private bool isDownRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard gb)
        {
            i = i + 1;
            j = j + 1;
            if (i >= gb.Width || j >= gb.Width)
            {
                return true;
            }
            MarkedSpace mark = gb.getFieldByCoordinates(i, j).MarkedSpace;
            if (mark == MarkedSpace.Empty || mark == m)
            {
                return true;
            }
            return false;
        }
    }
}
