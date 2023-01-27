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
        private Player player1;
        private Player player2;
        public Player Player1 { get => player1; }
        public Player Player2 { get => player2; }

        //TODO update constructor later
        public Game()
        {
            player1 = new Player(true);
            player2 = new Player(true);
        }

        //TODO uncommment this later
        public void makeTurn()
        {
            Console.WriteLine("Player");
            Coordinates p = player1.fire();
            MarkedSpace presult = player2.checkShoot(p);
            player1.markOpponentShot(p,presult);

            Console.WriteLine("Computer Player");
            Coordinates cp = player2.fire();
            MarkedSpace cpresult = player1.checkShoot(cp);
            player2.markOpponentShot(cp, cpresult);
        }
        public void GameLoop() 
        {
            player1.printBoardText();
            Console.WriteLine();
            player2.printBoardText();

            while (!player1.HasLost && !player2.HasLost)
            {
                makeTurn();
            }

            player1.printBoardText();
            Console.WriteLine();
            player1.printMarkingBoardText();
            Console.WriteLine();
            player2.printBoardText();
            Console.WriteLine();
            player2.printMarkingBoardText();

            if (player1.HasLost)
            {
                Console.WriteLine("player lost");
            }

            if (player2.HasLost)
            {
                Console.WriteLine("computer player lost");
            }
        
        }
    }
}
