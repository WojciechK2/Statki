using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players.Fireing
{
    public interface FireingStrategy
    {
        Coordinates fire(int range, MarkingBoard markingBoard);
    }
}
