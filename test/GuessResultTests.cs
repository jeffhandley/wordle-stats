namespace WordleStats.Tests;

using System.Linq;

public class GuessResultTests
{
    [Theory]
    [InlineData("abcde", "fghij", "⬛⬛⬛⬛⬛")] // All incorrect
    [InlineData("abcde", "abcde", "🟩🟩🟩🟩🟩")] // All correct
    [InlineData("abcde", "bcdea", "🟨🟨🟨🟨🟨")] // All present, all unique
    [InlineData("aabcc", "ccaab", "🟨🟨🟨🟨🟨")] // All present, two pairs
    [InlineData("abcde", "ebadc", "🟨🟩🟨🟩🟨")] // Mixed present and correct, all unique
    [InlineData("abcde", "abced", "🟩🟩🟩🟨🟨")] // Mixed present and correct, all unique
    [InlineData("aabbc", "caabb", "🟨🟩🟨🟩🟨")] // Mixed present and correct, two pairs
    [InlineData("aabbc", "aacbb", "🟩🟩🟨🟩🟨")] // Mixed present and correct, two pairs
    [InlineData("abcde", "axcye", "🟩⬛🟩⬛🟩")] // Mixed correct and incorrect, all unique
    [InlineData("aabbc", "xabyc", "⬛🟩🟩⬛🟩")] // Mixed correct and incorrect, two pairs
    [InlineData("aabbc", "aabbx", "🟩🟩🟩🟩⬛")] // Mixed correct and incorrect, two pairs
    [InlineData("abcde", "edcxy", "🟨🟨🟩⬛⬛")] // Mixed correct, present, incorrect, all unique
    [InlineData("aaabb", "bbaxx", "🟨🟨🟩⬛⬛")] // Mixed correct, present, incorrect, repeats
    public void GuessResult_Blocks(string answer, string guess, string expected)
    {
        var result = Game.GetGuessResult(answer, guess);
        var actual = result.Blocks;

        Assert.Equal(expected, actual);
    }
}
