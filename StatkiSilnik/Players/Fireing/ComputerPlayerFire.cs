using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players.Fireing
{
    public class ComputerPlayerFire : FireingStrategy
    {
        private Random rnd;
        public ComputerPlayerFire()
        {
            rnd = new Random();
        }
        public Coordinates fire(int range, MarkingBoard markingBoard)
        {
            bool notSet = true;
            int row, column;
            row = rnd.Next(range);
            column = rnd.Next(range);
            while (notSet)
            {
                Console.WriteLine("Trying at: " + row.ToString() + " " + column.ToString());
                //Not to fire on the previous fields
                MarkedSpace anticipatedPlaceMark = markingBoard.getFieldByCoordinates(row, column).MarkedSpace;
                Console.WriteLine(anticipatedPlaceMark);
                if (anticipatedPlaceMark == MarkedSpace.Empty)
                {
                    notSet = false;
                    break;
                }
                row = rnd.Next(markingBoard.Width);
                column = rnd.Next(markingBoard.Width);
            }
            Console.WriteLine("Shooting at: " + row.ToString() + " " + column.ToString());
            return new Coordinates(row, column);
        }
    }
}
