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
        public Player Player { get => player; }
        public ComputerPlayer ComputerPlayer { get => computerPlayer; }
        public Game()
        {
            player = new ComputerPlayer();
            computerPlayer = new ComputerPlayer();
        }
        public void makeTurn()
        {
            Console.WriteLine("Player");
            Coordinates p = player.randomFire();
            MarkedSpace presult = computerPlayer.checkShoot(p);
            player.markOpponentShot(p,presult);

            Console.WriteLine("Computer Player");
            Coordinates cp = computerPlayer.randomFire();
            MarkedSpace cpresult = player.checkShoot(cp);
            computerPlayer.markOpponentShot(cp, cpresult);
        }
        public void GameLoop() 
        {
            player.printBoardText();
            Console.WriteLine();
            computerPlayer.printBoardText();

            while (!player.HasLost && !computerPlayer.HasLost)
            {
                makeTurn();
            }

            player.printBoardText();
            Console.WriteLine();
            player.printMarkingBoardText();
            Console.WriteLine();
            computerPlayer.printBoardText();
            Console.WriteLine();
            computerPlayer.printMarkingBoardText();

            if (player.HasLost)
            {
                Console.WriteLine("player lost");
            }

            if (computerPlayer.HasLost)
            {
                Console.WriteLine("computer player lost");
            }
        
        }
    }
}
