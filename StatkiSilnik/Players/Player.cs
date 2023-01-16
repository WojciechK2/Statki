using StatkiSilnik.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatkiSilnik.Players
{
    public class Player
    { 
        public GameBoard GameBoard { get; set; }
        public MarkingBoard MarkingBoard { get; set; }
        public List<ShipBase> Ships { get; set; }
        public bool HasLost
        {
            get { return Ships.All(x => x.isSunk); }
        }
        public Player()
        {
            Ships = new List<ShipBase>()
            {
                //1x4
                new Czteromasztowiec(),
                //2x3
                new Trojmasztowiec(),
                new Trojmasztowiec(),
                //3x2
                new Dwumasztowiec(),
                new Dwumasztowiec(),
                new Dwumasztowiec(),
                //4x1
                new Jednomasztowiec(),
                new Jednomasztowiec(),
                new Jednomasztowiec(),
                new Jednomasztowiec()
            };
            GameBoard = new GameBoard();
            MarkingBoard = new MarkingBoard();
        }

        public void printBoardText() 
        {
            GameBoard.printBoardText();
        }
        public void printMarkingBoardText()
        {
            MarkingBoard.printBoardText();
        }
    }
}
