namespace WordleStats.Tests;

public class GuessResult_Spots
{
    private const GuessSpotState Incorrect  = GuessSpotState.Incorrect;
    private const GuessSpotState Present = GuessSpotState.Present;
    private const GuessSpotState Correct = GuessSpotState.Correct;

    [Theory]
    [InlineData("abcde", "vwxyz", new GuessSpotState[] { Incorrect, Incorrect, Incorrect, Incorrect, Incorrect })] // All incorrect, all unique
    [InlineData("aabbc", "vwxyz", new GuessSpotState[] { Incorrect, Incorrect, Incorrect, Incorrect, Incorrect })] // All incorrect, two pairs
    [InlineData("abcde", "abcde", new GuessSpotState[] { Correct, Correct, Correct, Correct, Correct })] // All correct, all unique
    [InlineData("aabbc", "aabbc", new GuessSpotState[] { Correct, Correct, Correct, Correct, Correct })] // All correct, two pairs
    [InlineData("abcde", "bcdea", new GuessSpotState[] { Present, Present, Present, Present, Present })] // All present, all unique
    [InlineData("aabcc", "ccaab", new GuessSpotState[] { Present, Present, Present, Present, Present })] // All present, two pairs
    [InlineData("abcde", "ebadc", new GuessSpotState[] { Present, Correct, Present, Correct, Present })] // Mixed present and correct, all unique
    [InlineData("abcde", "abced", new GuessSpotState[] { Correct, Correct, Correct, Present, Present })] // Mixed present and correct, all unique
    [InlineData("aabbc", "caabb", new GuessSpotState[] { Present, Correct, Present, Correct, Present })] // Mixed present and correct, two pairs
    [InlineData("aabbc", "aacbb", new GuessSpotState[] { Correct, Correct, Present, Correct, Present })] // Mixed present and correct, two pairs
    [InlineData("abcde", "axcye", new GuessSpotState[] { Correct, Incorrect, Correct, Incorrect, Correct })] // Mixed correct and incorrect, all unique
    [InlineData("aabbc", "xabyc", new GuessSpotState[] { Incorrect, Correct, Correct, Incorrect, Correct })] // Mixed correct and incorrect, two pairs
    [InlineData("aabbc", "aabbx", new GuessSpotState[] { Correct, Correct, Correct, Correct, Incorrect })] // Mixed correct and incorrect, two pairs
    [InlineData("abcde", "edcxy", new GuessSpotState[] { Present, Present, Correct, Incorrect, Incorrect })] // Mixed correct, present, incorrect, all unique
    [InlineData("aaabb", "bbaxx", new GuessSpotState[] { Present, Present, Correct, Incorrect, Incorrect })] // Mixed correct, present, incorrect, repeats
    public void GuessResult_Spots_Tests(string answer, string guess, GuessSpotState[] expected)
    {
        var result = Game.GetGuessResult(answer, guess);
        var actual = result.Spots.Select(s => s.State);

        Assert.Equal(expected, actual);
    }
}
