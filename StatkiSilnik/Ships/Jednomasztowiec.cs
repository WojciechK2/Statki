using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Ships
{
    public class Jednomasztowiec : ShipBase
    {
        public Jednomasztowiec()
        {
            Name = "Jednomasztowiec";
            Width = 1;
            MarkedSpace = MarkedSpace.Ship;
        }
    }
}
