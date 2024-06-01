namespace WordleStats.Tests;

public class GuessResult_Letters
{
    private const LetterState Unknown = LetterState.Unknown;
    private const LetterState Absent  = LetterState.Absent;
    private const LetterState Present = LetterState.Present;
    private const LetterState Correct = LetterState.Correct;

    [Theory]
    [InlineData("abcde", "xxxxx", 'x', new[] { Absent, Absent, Absent, Absent, Absent })]
    [InlineData("abcde", "aaaaa", 'a', new[] { Correct, Absent, Absent, Absent, Absent })]
    [InlineData("abcde", "abbbb", 'b', new[] { Absent, Correct, Absent, Absent, Absent })]
    [InlineData("abcde", "abccc", 'c', new[] { Absent, Absent, Correct, Absent, Absent })]
    [InlineData("abcde", "abcdd", 'd', new[] { Absent, Absent, Absent, Correct, Absent })]
    [InlineData("abcde", "abcde", 'e', new[] { Unknown, Unknown, Unknown, Unknown, Correct })]
    [InlineData("aabbc", "ababc", 'a', new[] { Correct, Unknown, Present, Unknown, Unknown })]
    [InlineData("aabbb", "bbaaa", 'a', new[] { Absent, Absent, Present, Present, Absent })]
    [InlineData("aabbb", "bbaaa", 'b', new[] { Present, Present, Unknown, Unknown, Unknown })]
    [InlineData("aabbb", "aaabb", 'a', new[] { Correct, Correct, Absent, Absent, Absent })]
    [InlineData("aabbb", "aaabb", 'b', new[] { Unknown, Unknown, Unknown, Correct, Correct })]
    [InlineData("aaaaa", "aaaaa", 'a', new[] { Correct, Correct, Correct, Correct, Correct })]
    public void GuessResult_Letters_Spots(string answer, string guess, char letter, LetterState[] expected)
    {
        var result = Game.GetGuessResult(answer, guess);
        var actual = result.Letters[letter].Spots;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", "vwxyz", 'a', Unknown)]
    [InlineData("abcde", "vwxyz", 'm', Unknown)]
    [InlineData("abcde", "lmnop", 'l', Absent)]
    public void GuessResult_Letters_State(string answer, string guess, char letter, LetterState expected)
    {
        var result = Game.GetGuessResult(answer, guess);
        var actual = result.Letters[letter].State;

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", "xxxxx", 'a', 0, 5)]
    [InlineData("abcde", "xxxxa", 'a', 1, 5)]
    [InlineData("abcde", "xxxaa", 'a', 1, 1)]
    [InlineData("aabbb", "xxxxa", 'a', 1, 5)]
    [InlineData("aabbb", "xxxaa", 'a', 2, 5)]
    [InlineData("aabbb", "xxaaa", 'a', 2, 2)]
    [InlineData("aaabb", "xxxxa", 'a', 1, 5)]
    [InlineData("aaabb", "xxxaa", 'a', 2, 5)]
    [InlineData("aaabb", "xxaaa", 'a', 3, 5)]
    [InlineData("aaabb", "xaaaa", 'a', 3, 3)]
    [InlineData("abcde", "xxxxx", 'x', 0, 0)]
    [InlineData("abcde", "aaaaa", 'a', 1, 1)]
    [InlineData("abcde", "abbbb", 'b', 1, 1)]
    [InlineData("abcde", "abccc", 'c', 1, 1)]
    [InlineData("abcde", "abcdd", 'd', 1, 1)]
    [InlineData("abcde", "abcde", 'e', 1, 5)]
    [InlineData("aabbc", "ababc", 'a', 2, 5)]
    [InlineData("aabbb", "bbaaa", 'a', 2, 2)]
    [InlineData("aabbb", "bbaaa", 'b', 2, 5)]
    [InlineData("aabbb", "aaabb", 'a', 2, 2)]
    [InlineData("aabbb", "aaabb", 'b', 2, 5)]
    [InlineData("aaaaa", "aaaaa", 'a', 5, 5)]
    public void GuessResult_Letters_Counts(string answer, string guess, char letter, byte minExpected, byte maxExpected)
    {
        var result = Game.GetGuessResult(answer, guess);
        var actual = result.Letters[letter].CountRange;
        var expected = new Range(minExpected, maxExpected);

        Assert.Equal(expected, actual);
    }
}
