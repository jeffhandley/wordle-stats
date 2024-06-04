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
        var actual = result.GetPossibilityPattern();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("abcde", new string[] { "bwxyz" }, "(?=.*b)[^bwxyz][^wxyz][^wxyz][^wxyz][^wxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv" }, "[^bstuvwxyz]b[^stuvwxyz][^stuvwxyz][^stuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr" }, "(?=.*a)[^bpqrstuvwxyz]b[^apqrstuvwxyz][^pqrstuvwxyz][^pqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc" }, "(?=.*c)ab[^anopqrstuvwxyz][^nopqrstuvwxyz][^cnopqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc", "abcab" }, "abc[^abnopqrstuvwxyz][^abcnopqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc", "abcab", "abccd" }, "(?=.*d)abc[^abcnopqrstuvwxyz][^abcdnopqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc", "abcab", "abccd", "abcdm" }, "abcd[^abcdmnopqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc", "abcab", "abccd", "abcdm", "eabcd" }, "(?=.*e)abcd[^abcdmnopqrstuvwxyz]")]
    [InlineData("abcde", new string[] { "bwxyz", "sbtuv", "pbaqr", "abnoc", "abcab", "abccd", "abcdm", "eabcd", "abcde" }, "abcde")]
    public void PresentLetters(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.GetPossibilityPattern();

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("aaabb", new string[] { "bbbaa" }, "(?=.*a)(?=.*b)[^b][^b][^b][^a][^a]")]
    public void RepeatedPresentLetters(string answer, string[] guesses, string expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.GetPossibilityPattern();

        Assert.Equal(expected, actual);
    }
}
