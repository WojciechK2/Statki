// See https://aka.ms/new-console-template for more information
using StatkiSilnik;
using StatkiSilnik.Players;

Console.WriteLine("Hello, World!");

//StatkiSilnik.Players.ComputerPlayer p = new StatkiSilnik.Players.ComputerPlayer();
//p.setUpShipsRandom();

ComputerPlayer p = new ComputerPlayer();
ComputerPlayer p2 = new ComputerPlayer();

//p.printBoardText();
//Console.WriteLine();
//p.printMarkingBoardText();

//Console.WriteLine();
//p2.printBoardText();
//Console.WriteLine();
//p2.printMarkingBoardText();

Game Game = new Game();
Game.GameLoop();

//Game.Player.printBoardText();
//Console.WriteLine();
//Game.Player.printMarkingBoardText();
//Console.WriteLine();
//Game.ComputerPlayer.printBoardText();
//Console.WriteLine();
//Game.ComputerPlayer.printMarkingBoardText();