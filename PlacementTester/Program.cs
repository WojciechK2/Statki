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
for (int i = 0; i < 100000; i++)
{
    //Console.WriteLine("Index {0}", i);
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
            if (!checkExactCells(placeXstart, placeYstart, placeXend, placeYend))
            {
                continue;
            }
            //Check cells below
            if (!checkCellsBelow(placeXend + 1, placeYstart - 1, placeYend + 1))
            {
                continue;
            }
            //Check cells above
            if (!checkCellsAbove(placeXstart - 1, placeYstart - 1, placeYend + 1))
            {
                continue;
            }
            //Check cells on left (vertical)
            if (!checkCellsLeft(placeXstart - 1, placeYstart - 1, placeXend + 1))
            {
                continue;
            }
            //Check cells on right (vertical)
            if (!checkCellsRight(placeXstart - 1, placeYend + 1, placeXend + 1))
            {
                continue;
            }

            setExactCells(placeXstart, placeYstart, placeXend, placeYend, ship.Width);

            break;
        }
        
    }
    
    if (!isBoardValid(gb))
    {
        Console.WriteLine("Validity: {0}", isBoardValid(gb));
        gb.printBoardText();
        break;
    }
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
        isDownRightNeighbourValid(9 + 1, 9 + 1, myMark, gb))
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
    i = i - 1;
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

bool checkExactCells(int startX, int startY, int endX, int endY)
{
    for (int i = startX; i <= endX; i++)
    {
        for (int j = startY; j <= endY; j++)
        {
            //Console.WriteLine("writting to: {0}, {1}", i, j);
            //gb.getFieldByCoordinates(i, j).MarkedSpace = MarkedSpace.Hit;
            if (gb.getFieldByCoordinates(i, j).MarkedSpace != MarkedSpace.Empty)
            {
                return false;
            }
        }
    }
    return true;
}
bool checkCellsBelow(int startX, int startY, int endY)
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
    return checkExactCells(startX, startY, startX, endY);
}

bool checkCellsAbove(int startX, int startY, int endY)
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
    return checkExactCells(startX, startY, startX, endY);
}
bool checkCellsLeft(int startX, int startY, int endX)
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
    return checkExactCells(startX, startY, endX, startY);
}
bool checkCellsRight(int startX, int endY, int endX)
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
    return checkExactCells(startX, endY, endX, endY);
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


//OLD CODE
/*
void checkExactCells(int startX, int startY, int endX, int endY)
{
    for (int i = startX; i <= endX; i++)
    {
        for (int j = startY; j <= endY; j++)
        {
            //Console.WriteLine("writting to: {0}, {1}", i, j);
            gb.getFieldByCoordinates(i, j).MarkedSpace = MarkedSpace.Hit;
        }
    }
}
void checkCellsBelow(int startX, int startY, int endY)
{
    if (startX >= (gb.Width))
    {
        // Do not check if cells below are outside of board
        return;
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
    checkExactCells(startX, startY, startX, endY);
}

void checkCellsAbove(int startX, int startY, int endY)
{
    if (startX < 0)
    {
        return;
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
    checkExactCells(startX, startY, startX, endY);
}
void checkCellsLeft(int startX, int startY, int endX)
{
    if (startY < 0)
    {
        return;
    }
    if (endX >= gb.Width)
    {
        endX = gb.Width - 1;
    }
    if (startX < 0)
    {
        startX = 0;
    }
    checkExactCells(startX, startY, endX, startY);
}
void checkCellsRight(int startX, int endY, int endX)
{
    if (endY >= gb.Width)
    {
        return;
    }
    if (startX < 0)
    {
        startX = 0;
    }
    if (endX >= gb.Width)
    {
        endX = gb.Width - 1;
    }
    checkExactCells(startX, endY, endX, endY);
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
}*/
/*    for (int sSize = 5; sSize <= 5; sSize++)
{
    Console.WriteLine("Testing ship {0}", sSize);
    for (int o = 0; o <= 1; o++)
    {
        for (int i = 0; i <= 10; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                Console.WriteLine("Testing {0},{1}", i, j);
                int placeXstart = i;//rnd.Next(gb.Width);
                int placeYstart = j;//rnd.Next(gb.Width);
                int placeXend = placeXstart;
                int placeYend = placeYstart;
                if (o == 0)
                {
                    placeYend = placeYstart + sSize-1;
                }
                else
                {
                    placeXend = placeXstart + sSize-1;
                }
                int orientation = 1;//rnd.Next(1, 101) % 2;
                if (placeXend >= gb.Width || placeYend >= gb.Width)
                {
                    continue;
                }

                gb = new GameBoard();
                setExactCells(placeXstart, placeYstart, placeXend, placeYend, sSize);
                //gb.printBoardText();
                //chack cells below
                checkCellsBelow(placeXend + 1, placeYstart - 1, placeYend + 1);
                gb.printBoardText();
                Console.WriteLine();
                gb = new GameBoard();
                setExactCells(placeXstart, placeYstart, placeXend, placeYend, sSize);
                //gb.printBoardText();
                //Check cells above
                checkCellsAbove(placeXstart - 1, placeYstart - 1, placeYend + 1);
                gb.printBoardText();
                Console.WriteLine();
                gb = new GameBoard();
                setExactCells(placeXstart, placeYstart, placeXend, placeYend, sSize);
                //gb.printBoardText();
                //Check cells on left (vertical)
                checkCellsLeft(placeXstart - 1, placeYstart - 1, placeXend + 1);
                gb.printBoardText();
                Console.WriteLine();
                gb = new GameBoard();
                setExactCells(placeXstart, placeYstart, placeXend, placeYend, sSize);
                //gb.printBoardText();
                //Check cells on right (vertical)
                checkCellsRight(placeXstart - 1, placeYend + 1, placeXend + 1);
                gb.printBoardText();
                Console.WriteLine();
            }
        }
    }
}*/
