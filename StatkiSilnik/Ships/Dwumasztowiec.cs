using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Ships
{
    public class Dwumasztowiec : ShipBase
    {
        public Dwumasztowiec()
        {
            Name = "Dwumasztowiec";
            Width = 2;
            MarkedSpace = MarkedSpace.Ship;
        }
    }
}
