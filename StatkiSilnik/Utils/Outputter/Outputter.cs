using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils.Outputter
{
    public interface Outputter
    {
        void parseMessage(string Message);
    }
}
