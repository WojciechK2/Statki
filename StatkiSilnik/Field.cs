using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik
{
    public class Field
    {
        public MarkedSpace MarkedSpace { get; set; }
        public Coordinates Coordinates { get; set; }

        public Field(int row, int column)
        {
            Coordinates = new Coordinates(row, column);
            MarkedSpace = MarkedSpace.Empty;
        }
    }
}
