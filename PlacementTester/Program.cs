// See https://aka.ms/new-console-template for more information
using StatkiSilnik;
using StatkiSilnik.Ships;
using System.Diagnostics;
using System.Reflection.Metadata;

Random rnd = new Random();
GameBoard gb = new GameBoard();
List<ShipBase> Ships = new List<ShipBase>()
{
    //1x4
    new Czteromasztowiec(),
    //2x3
    new Trojmasztowiec(),
    new Trojmasztowiec(),
    //3x2
    new Dwumasztowiec(),
    new Dwumasztowiec(),
    new Dwumasztowiec(),
    //4x1
    new Jednomasztowiec(),
    new Jednomasztowiec(),
    new Jednomasztowiec(),
    new Jednomasztowiec()
};

/* MAIN TEST */
for (int i = 0; i < 1000000; i++)
{
    if (i % 1000 == 0)
    {
        Console.WriteLine("Index {0}", i);
    }
    gb = new GameBoard();
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

    bool validBoard = isBoardValid(gb);
    
    if (!validBoard)
    {
        Console.WriteLine("Validity: {0}", validBoard);
        gb.printBoardText();
        break;
    }
    //gb.printBoardText();
}

bool isBoardValid(GameBoard g)
{
    for(int i=0; i< g.Width; i++)
    {
        for(int j=0; j<g.Width; j++)
        {
            MarkedSpace m = g.getFieldByCoordinates(i, j).MarkedSpace;
            if (m != MarkedSpace.Empty)
            {
                if(!areNeighboursValid(i, j, m,g))
                {
                    return false;
                }
            }
        }
    }
    return true;
}

bool areNeighboursValid(int i, int j, MarkedSpace m, GameBoard g)
{
    MarkedSpace myMark = g.getFieldByCoordinates(i, j).MarkedSpace;

    if(isUpperLeftNeighbourValid(i, j, myMark, gb) ||
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

bool isUpperLeftNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i - 1;
    j = j - 1;
    if(i < 0 || j < 0)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isUpperNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i - 1;
    if (i < 0)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isUpperRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i - 1;
    j = j + 1;
    if (i < 0 || j >= g.Width)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isLeftNeighbourValid(int i, int j,MarkedSpace m, GameBoard g)
{
    j = j - 1;
    if (j < 0)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    j = j + 1;
    if (j >= g.Width)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isDownLeftNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i + 1;
    j = j - 1;
    if (i >= g.Width || j < 0)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isDownNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i + 1;
    if (i >= g.Width)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool isDownRightNeighbourValid(int i, int j, MarkedSpace m, GameBoard g)
{
    i = i + 1;
    j = j + 1;
    if (i >= g.Width || j >= g.Width)
    {
        return true;
    }
    MarkedSpace mark = g.getFieldByCoordinates(i, j).MarkedSpace;
    if (mark == MarkedSpace.Empty || mark == m)
    {
        return true;
    }
    return false;
}

bool areExactCellsEmpty(int startX, int startY, int endX, int endY)
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
bool areCellsBelowEmpty(int startX, int startY, int endY)
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

bool areCellsAboveEmpty(int startX, int startY, int endY)
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
bool areCellsLeftEmpty(int startX, int startY, int endX)
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
bool areCellsRightEmpty(int startX, int endY, int endX)
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
void setExactCells(int startX, int startY, int endX, int endY, int shipWidth)
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