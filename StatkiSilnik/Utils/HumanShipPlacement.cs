using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Utils
{
    public class HumanShipPlacement
    {
        private GameBoard gb;
        private BoardValidator boardValidator;
        public HumanShipPlacement()
        {
            boardValidator = new BoardValidator();
        }   
        
    }
}
