// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//StatkiSilnik.Players.ComputerPlayer p = new StatkiSilnik.Players.ComputerPlayer();
//p.setUpShipsRandom();

for (int i = 0; i < 50; i++)
{
    Console.WriteLine("Index {0}", i);
    StatkiSilnik.Players.ComputerPlayer p = new StatkiSilnik.Players.ComputerPlayer();
    p.setUpShipsRandom();
    p.printBoardText();
    Console.WriteLine();
    //Console.WriteLine("Is valid board: {0}", p.validateBoard());
    //Check it later
}