namespace WordleStats.Tests;

public class GameResult_Letters
{
    [Theory]
    [InlineData("abcde", new string[] { }, "abcdefghijklmnopqrstuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz" }, "abcdefghijklmnopqrstuvwxy")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "abcdefghijklmnopqrst")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg" }, "abhijklmnopqrst")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk" }, "almnopqrst")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop" }, "aqrst")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst" }, "a")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda", "abcde" }, "")]
    public void Unknown(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLetters(LetterState.Unknown));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { }, "")]
    [InlineData("abcde", new string[] { "zzzzz" }, "z")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "uvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg" }, "fguvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk" }, "fghijkuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop" }, "fghijklmnopuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst" }, "fghijklmnopqrstuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba" }, "fghijklmnopqrstuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda" }, "fghijklmnopqrstuvwxyz")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda", "abcde" }, "fghijklmnopqrstuvwxyz")]
    public void Absent(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLetters(LetterState.Absent));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { }, "")]
    [InlineData("abcde", new string[] { "zzzzz" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg" }, "cde")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk" }, "bcde")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop" }, "bcde")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst" }, "bcde")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba" }, "abde")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda" }, "ae")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda", "abcde" }, "")]
    public void Present(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLetters(LetterState.Present));

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { }, "")]
    [InlineData("abcde", new string[] { "zzzzz" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst" }, "")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba" }, "c")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda" }, "bcd")]
    [InlineData("abcde", new string[] { "zzzzz", "uvwxy", "cdefg", "bhijk", "lmnop", "qqrst", "edcba", "ebcda", "abcde" }, "abcde")]
    public void Correct(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = string.Join("", result.GetLetters(LetterState.Correct));

        Assert.Equal(expected, actual);
    }
}
