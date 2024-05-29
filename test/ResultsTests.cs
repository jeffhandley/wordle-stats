namespace WordleStats.Tests;

using System.Linq;

public class ResultsTests
{
    [Fact]
    public void GuessesAreAddedToGameState()
    {
        var answer = "abcde";
        var guesses = new[] { "aaaaa", "bbbbb", "ccccc" };

        var game = new GameState(answer, guesses);

        Assert.Equal(game.Guesses.Select(s => s.Guess), guesses);
    }

    [Fact]
    public void LettersCorrectAreRecorded()
    {
        var answer = "abcde";
        var guesses = new[] { "aaaaa", "bbbbb", "ccccc" };

        var game = new GameState(answer, guesses);

        Assert.Equal('a', game.Guesses[0].LettersCorrect[0]);
        Assert.Equal('b', game.Guesses[1].LettersCorrect[1]);
        Assert.Equal('c', game.Guesses[2].LettersCorrect[2]);
    }

    [Fact]
    public void LettersPresentAreRecorded()
    {
        var answer = "aabbc";
        var guesses = new[] { "bbcaa", "bcaab" };

        var game = new GameState(answer, guesses);

        Assert.Equal(2, game.Guesses[1].LettersPresent['a'].Count);
        Assert.Equal(2, game.Guesses[1].LettersPresent['b'].Count);
        Assert.Equal(1, game.Guesses[1].LettersPresent['c'].Count);
    }
}
