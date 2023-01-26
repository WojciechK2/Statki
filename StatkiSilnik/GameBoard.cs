using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik
{
    public class GameBoard
    {
        public List<Field> Fields { get; set; }
        public int Width { get; set; }
        public GameBoard()
        {
            Width = 10;
            Fields = new List<Field>();
            for (int i=0; i < Width; i++)
            {
                for (int j=0; j < Width; j++)
                {
                    Fields.Add(new Field(i, j));
                }
            }
        }
        public Field getFieldByCoordinates(int x, int y)
        {
            return Fields.Find(el => (el.Coordinates.Row == x) && (el.Coordinates.Column == y));
        }
        public void setFieldByCoordinates(int x,int y,MarkedSpace type)
        {
            getFieldByCoordinates(x, y).MarkedSpace = type;
        }
        public void printBoardText()
        {
            for (int i = 0; i < Width; i++)
            {
                String line = "";
                for (int j = 0; j < Width; j++)
                {
                    Field f = getFieldByCoordinates(i, j);
                    if (f.MarkedSpace == MarkedSpace.Empty)
                    {
                        line += "O" + " ";
                    }
                    else if(f.MarkedSpace == MarkedSpace.Czteromasztowiec)
                    {
                        line += "4" + " ";
                    }
                    else if (f.MarkedSpace == MarkedSpace.Trojmasztowiec)
                    {
                        line += "3" + " ";
                    }
                    else if (f.MarkedSpace == MarkedSpace.Dwumasztowiec)
                    {
                        line += "2" + " ";
                    }
                    else if (f.MarkedSpace == MarkedSpace.Jednomasztowiec)
                    {
                        line += "1" + " ";
                    }
                    else if (f.MarkedSpace == MarkedSpace.Hit)
                    {
                        line += "!" + " ";
                    }
                    else
                    {
                        line += "M" + " ";
                    }
                }
                Console.WriteLine(line);
            }
        }
    }
}
