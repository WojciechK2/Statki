using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players.Fireing
{
    public class HumanConsolePlayerFire : FireingStrategy
    {
        public Coordinates fire(int range, MarkingBoard markingBoard)
        {
            Console.WriteLine("Provide row value");
            string row = Console.ReadLine();
            Console.WriteLine("Provide column value");
            string column = Console.ReadLine();

            return new Coordinates(int.Parse(row), int.Parse(column));
        }
    }
}
