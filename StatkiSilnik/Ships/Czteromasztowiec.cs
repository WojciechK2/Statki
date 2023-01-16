using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Ships
{
    public class Czteromasztowiec : ShipBase
    {
        public Czteromasztowiec()
        {
            Name = "Czteromasztowiec";
            Width = 4;
            MarkedSpace = MarkedSpace.Ship;
        }
    }
}
