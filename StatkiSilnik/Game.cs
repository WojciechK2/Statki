using StatkiSilnik.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik
{
    public class Game
    {
        private Player player;
        private ComputerPlayer computerPlayer;
        public Player Player { get; set; }
        public ComputerPlayer ComputerPlayer { get; set; }
        public Game()
        {
            player = new Player();
            computerPlayer = new ComputerPlayer();
        }
        public void makeTurn()
        {

        }
        public void GameLoop() { }
    }
}
