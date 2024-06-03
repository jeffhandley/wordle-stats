namespace WordleStats.Tests;

public class GameResult_GetPossibilityPatterns
{
    [Theory]
    [InlineData("abcde", new string[] { }, ".....")]
    [InlineData("abcde", new string[] { "abcde" }, "abcde")]
    [InlineData("aaaaa", new string[] { "aaaaa" }, "aaaaa")]
    [InlineData("abcde", new string[] { "aaaaa" }, "a[^a][^a][^a][^a]")]
    [InlineData("abcde", new string[] { "aaaaa", "abbbb" }, "ab[^ab][^ab][^ab]")]
    [InlineData("abcde", new string[] { "aaaaa", "abbbb", "abccc" }, "abc[^abc][^abc]")]
    [InlineData("abcde", new string[] { "aaaaa", "abbbb", "abccc", "abcdd" }, "abcd[^abcd]")]
    [InlineData("abcde", new string[] { "aaaaa", "abbbb", "abccc", "abcdd", "abcde" }, "abcde")]
    public void CorrectIncorrectLetters(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.GetPossibilityPatterns().FirstOrDefault();

        Assert.Equal(expected, actual);
    }
}
