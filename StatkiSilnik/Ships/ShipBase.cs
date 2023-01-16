using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Ships
{
    public abstract class ShipBase
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Hits { get; set; }
        public MarkedSpace MarkedSpace { get; set; }
        public bool isSunk
        {
            get
            {
                return Hits >= Width;
            }
        }
    }
}
