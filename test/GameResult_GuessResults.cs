namespace WordleStats.Tests;

public class GameResult_GuessResults
{
    // ⬛🟨🟩

    [Theory]
    [InlineData("abcde", new string[] { "aaaab" }, new string[] {
        "🟩⬛⬛⬛🟨"
    })]
    [InlineData("abcde", new string[] { "aaaab", "aaabc" }, new string[] {
        "🟩⬛⬛⬛🟨",
        "🟩⬛⬛🟨🟨"
    })]
    [InlineData("abcde", new string[] { "aaaab", "aaabc", "aabcd" }, new string[] {
        "🟩⬛⬛⬛🟨",
        "🟩⬛⬛🟨🟨",
        "🟩⬛🟨🟨🟨"
    })]
    [InlineData("abcde", new string[] { "aaaab", "aaabc", "aabcd", "abcde" }, new string[] {
        "🟩⬛⬛⬛🟨",
        "🟩⬛⬛🟨🟨",
        "🟩⬛🟨🟨🟨",
        "🟩🟩🟩🟩🟩"
    })]
    public void GuessResultsCaptured(string answer, string[] guesses, string[] expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.GuessResults.Select(g => g.Blocks).ToArray();

        Assert.Equal(expected, actual);
    }
}
