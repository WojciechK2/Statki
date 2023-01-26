using StatkiSilnik.Ships;
using StatkiSilnik.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatkiSilnik.Players
{
    public class Player
    { 
        public GameBoard GameBoard { get; set; }
        public MarkingBoard MarkingBoard { get; set; }
        public List<ShipBase> ShipList { get; set; }
        protected BoardValidator boardValidator;
        protected ShipPlacementTool shipPlacementTool;
        protected Random rnd;
        public bool HasLost
        {
            get { return ShipList.All(x => x.isSunk); }
        }
        public Player()
        {
            ShipList = new List<ShipBase>()
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
            rnd = new Random();
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

            //If it hit a ship, not the greatest
            ShipBase ship = ShipList.First(x => (x.Name.ToString() == shotPlace.ToString() && x.isSunk == false));
            
            ship.Hits++;
            Console.WriteLine("Targetted ship:" + ship.Name);
            Console.WriteLine("How many hits:" + ship.Hits);
            Console.WriteLine("Was sunk:" + ship.isSunk);

            return MarkedSpace.Hit;
        }

        public void markOpponentShot(Coordinates cords, MarkedSpace mPlace)
        {
            MarkingBoard.setFieldByCoordinates(cords.Row, cords.Column, mPlace);
        }

        public Coordinates randomFire()
        {
            bool notSet = true;
            int row,column;
            row = rnd.Next(GameBoard.Width);
            column = rnd.Next(GameBoard.Width);
            while (notSet)
            {
                Console.WriteLine("Trying at: " + row.ToString() + " " + column.ToString());
                //Not to fire on the previous fields
                MarkedSpace anticipatedPlaceMark = MarkingBoard.getFieldByCoordinates(row, column).MarkedSpace;
                Console.WriteLine(anticipatedPlaceMark);
                if (anticipatedPlaceMark == MarkedSpace.Empty)
                {
                    notSet = false;
                    break;
                }
                row = rnd.Next(GameBoard.Width);
                column = rnd.Next(GameBoard.Width);
            }
            Console.WriteLine("Shooting at: " + row.ToString() + " " + column.ToString());
            return new Coordinates(row,column);
        }

        public Coordinates fire(Coordinates cords)
        {
            bool notSet = true;
            while (notSet)
            {
                //Not to fire on the previous fields
                MarkedSpace anticipatedPlaceMark = MarkingBoard.getFieldByCoordinates(cords.Row, cords.Column).MarkedSpace;
                Console.WriteLine(anticipatedPlaceMark);
                if (anticipatedPlaceMark == MarkedSpace.Empty)
                {
                    notSet = false;
                }
            }
            Console.WriteLine("Shooting at: " + cords.Row.ToString() + " " + cords.Column.ToString());
            return cords;
        }
    }
}
