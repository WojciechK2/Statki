using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Ships
{
    public class Trojmasztowiec : ShipBase
    {
        public Trojmasztowiec()
        {
            Name = "Trojmasztowiec";
            Width = 3;
            MarkedSpace = MarkedSpace.Ship;
        }
    }
}
