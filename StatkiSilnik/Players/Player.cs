using StatkiSilnik.Ships;
using StatkiSilnik.Utils;
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
        protected BoardValidator boardValidator;
        protected ShipPlacementTool shipPlacementTool;
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
            boardValidator = new BoardValidator();
            shipPlacementTool = new ShipPlacementTool();
        }

        public void printBoardText() 
        {
            GameBoard.printBoardText();
        }
        public void printMarkingBoardText()
        {
            MarkingBoard.printBoardText();
        }

        public MarkedSpace checkShoot(Coordinates cords)
        {
            MarkedSpace shotPlace = GameBoard.getFieldByCoordinates(cords.Row, cords.Column).MarkedSpace;

            if(shotPlace == MarkedSpace.Empty)
            {
                return MarkedSpace.Miss;
            }

            //If it hit a ship
            ShipBase ship = Ships.First(x => x.Name.ToString() == shotPlace.ToString());
            Console.WriteLine(ship);

            ship.Hits++;

            return shotPlace;
        }

        public Coordinates fire(Coordinates cords)
        {
            return cords;
        }
    }
}
