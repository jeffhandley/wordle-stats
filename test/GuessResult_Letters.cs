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
    [InlineData("abcde", "abbbb", 'b', new[] { Unknown, Correct, Absent, Absent, Absent })]
    [InlineData("abcde", "abccc", 'c', new[] { Unknown, Unknown, Correct, Absent, Absent })]
    [InlineData("abcde", "abcdd", 'd', new[] { Unknown, Unknown, Unknown, Correct, Absent })]
    [InlineData("abcde", "abcde", 'e', new[] { Unknown, Unknown, Unknown, Unknown, Correct })]
    [InlineData("aabbc", "ababc", 'a', new[] { Correct, Unknown, Present, Unknown, Unknown })]
    [InlineData("aabbb", "bbaaa", 'a', new[] { Unknown, Unknown, Present, Present, Absent })]
    [InlineData("aabbb", "bbaaa", 'b', new[] { Present, Present, Unknown, Unknown, Unknown })]
    [InlineData("aabbb", "aaabb", 'a', new[] { Correct, Correct, Absent, Unknown, Unknown })]
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
}
