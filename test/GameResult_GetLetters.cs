namespace WordleStats.Tests;

public class GameResult_GetLetters
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
    public void GetLettersAvailable(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLettersAvailable());

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { }, "" )]
    [InlineData("abcde", new string[] { "zzzzz" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia" }, "a" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb" }, "ab" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc" }, "abc" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod" }, "abcd" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp" }, "abcd" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp", "abcde" }, "abcde" )]
    public void GetLettersKnown(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLettersKnown());

        Assert.Equal(expected, actual);
    }
}
