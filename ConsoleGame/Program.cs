// See https://aka.ms/new-console-template for more information
using StatkiSilnik;
using StatkiSilnik.Players;

Console.WriteLine("Hello, World!");

//StatkiSilnik.Players.ComputerPlayer p = new StatkiSilnik.Players.ComputerPlayer();
//p.setUpShipsRandom();

ComputerPlayer p = new ComputerPlayer();
ComputerPlayer p2 = new ComputerPlayer();

p.printBoardText();
Console.WriteLine();
p.printMarkingBoardText();

Console.WriteLine();
p2.printBoardText();
Console.WriteLine();
p2.printMarkingBoardText();

//One shot
Coordinates c = p2.fire(new StatkiSilnik.Coordinates(1, 1));
p.checkShoot(c);


