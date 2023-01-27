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
        public Game(bool isPlayer1Computer, bool isPlayer2Computer)
        {
            player1 = new Player(isPlayer1Computer);
            player2 = new Player(isPlayer2Computer);
        }
        public void makeTurn()
        {
            Console.WriteLine("Player1");
            Coordinates p = player1.fire();
            MarkedSpace presult = player2.checkShoot(p);
            player1.markOpponentShot(p,presult);

            if (!player1.isComputer)
            {
                player1.GameBoard.printBoardText();
                Console.WriteLine();
                player1.MarkingBoard.printBoardText();
                Console.WriteLine();
            }

            if (player2.HasLost)
            {
                return;
            }

            Console.WriteLine("Player2");
            Coordinates cp = player2.fire();
            MarkedSpace cpresult = player1.checkShoot(cp);
            player2.markOpponentShot(cp, cpresult);

            if (!player2.isComputer)
            {
                player2.GameBoard.printBoardText();
                Console.WriteLine();
                player2.MarkingBoard.printBoardText();
                Console.WriteLine();
            }
        }
        public void GameLoop() 
        {
            //DEBUG
            //player1.printBoardText();
            //Console.WriteLine();
            //player2.printBoardText();

            while (!player1.HasLost && !player2.HasLost)
            {
                makeTurn();
            }

            //DEBUG
            player1.printBoardText();
            Console.WriteLine();
            player1.printMarkingBoardText();
            Console.WriteLine();
            player2.printBoardText();
            Console.WriteLine();
            player2.printMarkingBoardText();

            //Needs to be rewritten to something more meaningful in UI 
            if (player1.HasLost)
            {
                Console.WriteLine("player1 lost");
            }

            if (player2.HasLost)
            {
                Console.WriteLine("player2 lost");
            }
        
        }
    }
}
