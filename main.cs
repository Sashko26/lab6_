using System;
using System.Collections.Generic;

class GameAccount
{
   
    public string UserName { get; set; }
    public int CurrentRating { get; private set; }
    public int GamesCount { get; private set; }
    private List<Game> gamesHistory = new List<Game>();

    public GameAccount(string userName)
    {
        UserName = userName;
        CurrentRating = 1000; // Початковий рейтинг
        GamesCount = 0;
    }

    public void WinGame(string opponentName, int rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Рейтинг не може бути від'ємним.");
        }

        CurrentRating += rating;
        GamesCount++;
        Game game = new Game(opponentName, true, rating);
        gamesHistory.Add(game);
        Console.WriteLine($"Game {game.Id}: {UserName} won against {opponentName} with a rating of {rating}.");
    }

    public void LoseGame(string opponentName, int rating)
    {
        if (rating < 0)
        {
            throw new ArgumentException("Рейтинг не може бути від'ємним.");
        }

        CurrentRating -= rating;
        if (CurrentRating < 1)
        {
            CurrentRating = 1;
        }

        GamesCount++;
        Game game = new Game(opponentName, false, rating);
        gamesHistory.Add(game);
        Console.WriteLine($"Game {game.Id}: {UserName} lost against {opponentName} with a rating of {rating}.");
    }

    public void GetStats()
    {
        Console.WriteLine($"Statistics for player {UserName}:");
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine("|  Game ID  |  Opponent    |  Win/Lose           |  Rating   |");
        Console.WriteLine("-------------------------------------------------------------");
        foreach (var game in gamesHistory)
        {
            Console.WriteLine($"|  {game.Id,-9} |  {game.OpponentName,-12} |  {(game.IsWin ? "Win" : "Lose"),-16} |  {game.Rating,-8} |");
        }
        Console.WriteLine("-------------------------------------------------------------");
        Console.WriteLine($"Total rating: {CurrentRating}");
    }
}

class Game
{
    private static int nextGameId = 1;

    public int Id { get; }
    public string OpponentName { get; }
    public bool IsWin { get; }
    public int Rating { get; }

    public Game(string opponentName, bool isWin, int rating)
    {
        Id = nextGameId++;
        OpponentName = opponentName;
        IsWin = isWin;
        Rating = rating;
    }
}

class Program
{
    static void Main()
    {
        GameAccount Rozhko2006 = new GameAccount("Rozhko2006");
        GameAccount player2 = new GameAccount("Player2");

        Rozhko2006.WinGame("Player2", 20);
        Rozhko2006.LoseGame("COOLmAN", 15);
        player2.WinGame("KLAPAN", 10);
        player2.LoseGame("gUMANGI", 25);

        Rozhko2006.GetStats();
        Console.WriteLine();
        player2.GetStats();
    }
}
