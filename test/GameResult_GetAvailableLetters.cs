namespace WordleStats.Tests;

public class GameResult_GetAvailableLetters
{
    [Theory]
    [InlineData("abcde", new string[] { }, "abcdefghijklmnopqrstuvwxyz" )]
    [InlineData("abcde", new string[] { "zzzzz" }, "abcdefghijklmnopqrstuvwxy" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "abcdefghijklmnopqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa" }, "abcdefghijklmnopqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa", "afghi" }, "abcdejklmnopqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa", "afghi", "abjkl" }, "abcdemnopqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa", "afghi", "abjkl", "abcmn" }, "abcdeopqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa", "afghi", "abjkl", "abcmn", "abcdo" }, "abcdepqrst" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "aaaaa", "afghi", "abjkl", "abcmn", "abcdo", "abcde" }, "abcdepqrst" )]
    public void GetAvailableLetters(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetAvailableLetters());

        Assert.Equal(expected, actual);
    }
}
