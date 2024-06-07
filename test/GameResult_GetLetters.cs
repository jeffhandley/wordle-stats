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
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb" }, "b" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc" }, "c" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod" }, "d" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp", "abcde" }, "" )]
    public void GetLettersPresent(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLettersPresent());

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { }, "" )]
    [InlineData("abcde", new string[] { "zzzzz" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia" }, "" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb" }, "a" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc" }, "ab" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod" }, "abc" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp" }, "abcd" )]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "fghia", "ajklb", "abmnc", "abcod", "abcdp", "abcde" }, "abcde" )]
    public void GetLettersCorrect(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLettersCorrect());

        Assert.Equal(expected, actual);
    }
}
