using StatkiSilnik.Players.Fireing;
using StatkiSilnik.Ships;
using StatkiSilnik.Utils;
using StatkiSilnik.Utils.ShipPlacementStrategy;
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
        protected Random rnd;
        public bool HasLost
        {
            get { return ShipList.All(x => x.isSunk); }
        }
        public bool isComputer;
        private ShipPlacementStrategy shipPlacementStrategy;
        private FireingStrategy fireingStrategy;
        public Player(bool isComputer)
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
            rnd = new Random();
            this.isComputer = isComputer;
            if (isComputer)
            {
                shipPlacementStrategy = new ComputerPlayerShipPlacementStrategy();
                fireingStrategy = new ComputerPlayerFire();
            } else
            {
                shipPlacementStrategy = new HumanConsolePlayerShipPlacementStrategy();
                fireingStrategy = new HumanConsolePlayerFire();
            }
            GameBoard = shipPlacementStrategy.placeShips(ShipList);
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

        public Coordinates fire()
        {
            return fireingStrategy.fire(GameBoard.Width, MarkingBoard);
        }
    }
}
