namespace WordleStats.Tests;

using System.Linq;

public class GameResult_Spots
{
    public static IEnumerable<object[]> CorrectLetters_Data() =>
    [
        [
            "abcde", new string[] { },
            new char?[] { null, null, null, null, null }
        ],
        [
            "abcde", new[] { "aaaaa" },
            new char?[] { 'a', null, null, null, null }
        ],
        [
            "abcde", new[] { "aaaaa", "abbbb" },
            new char?[] { 'a', 'b', null, null, null }
        ],
        [
            "abcde", new[] { "aaaaa", "abbbb", "abccc" },
            new char?[] { 'a', 'b', 'c', null, null }
        ],
        [
            "abcde", new[] { "aaaaa", "abbbb", "abccc", "abcdd" },
            new char?[] { 'a', 'b', 'c', 'd', null }
        ],
        [
            "abcde", new[] { "aaaaa", "abbbb", "abccc", "abcdd", "abcde" },
            new char?[] { 'a', 'b', 'c', 'd', 'e'}
        ],
        [
            "aaaaa", new[] { "edcba" },
            new char?[] { null, null, null, null, 'a' }
        ],
        [
            "aaaaa", new[] { "edcba", "edcaa" },
            new char?[] { null, null, null, 'a', 'a' }
        ],
        [
            "aaaaa", new[] { "edcba", "edcaa", "edaaa" },
            new char?[] { null, null, 'a', 'a', 'a' }
        ],
        [
            "aaaaa", new[] { "edcba", "edcaa", "edaaa", "eaaaa" },
            new char?[] { null, 'a', 'a', 'a', 'a' }
        ],
        [
            "aaaaa", new[] { "edcba", "edcaa", "edaaa", "eaaaa", "aaaaa" },
            new char?[] { 'a', 'a', 'a', 'a', 'a' }
        ]
    ];

    [Theory]
    [MemberData(nameof(CorrectLetters_Data))]
    public void CorrectLetters(string answer, string[] guesses, char?[] expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.Spots.Select(s => s.CorrectLetter).ToArray();

        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> IncorrectLetters_Data =>
    [
        [
            "abcde", new string[] { },
            new string[] { "", "", "", "", "" }
        ],
        [
            "abcde", new[] { "fghij" },
            new string[] { "fghij", "fghij", "fghij", "fghij", "fghij" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn" },
            new string[] { "fghijklmn", "fghijklmn", "fghijklmn", "fghijklmn", "fghijklmn" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq" },
            new string[] { "fghijklmnopq", "fghijklmnaopq", "fghijklmnaopq", "fghijklmnaopq", "fghijklmnaopq" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst" },
            new string[] { "fghijklmnopqrst", "fghijklmnaopqrst", "fghijklmnaopqrst", "fghijklmnaopqrst", "fghijklmnaopqrst" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv" },
            new string[] { "fghijklmnopqrstbuv", "fghijklmnaopqrstuv", "fghijklmnaopqrstbuv", "fghijklmnaopqrstbuv", "fghijklmnaopqrstbuv" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx" },
            new string[] { "fghijklmnopqrstbuvwx", "fghijklmnaopqrstuvwx", "fghijklmnaopqrstbuvwx", "fghijklmnaopqrstbuvwx", "fghijklmnaopqrstbuvwx" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx", "abccy" },
            new string[] { "fghijklmnopqrstbuvwxcy", "fghijklmnaopqrstuvwxcy", "fghijklmnaopqrstbuvwxy", "fghijklmnaopqrstbuvwxcy", "fghijklmnaopqrstbuvwxcy" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx", "abccy", "abcdz" },
            new string[] { "fghijklmnopqrstbuvwxcyz", "fghijklmnaopqrstuvwxcyz", "fghijklmnaopqrstbuvwxyz", "fghijklmnaopqrstbuvwxcyz", "fghijklmnaopqrstbuvwxcyz" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx", "abccy", "abcdz", "abcdd" },
            new string[] { "fghijklmnopqrstbuvwxcyzd", "fghijklmnaopqrstuvwxcyzd", "fghijklmnaopqrstbuvwxyzd", "fghijklmnaopqrstbuvwxcyz", "fghijklmnaopqrstbuvwxcyzd" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx", "abccy", "abcdz", "abcdd", "abcde" },
            new string[] { "fghijklmnopqrstbuvwxcyzd", "fghijklmnaopqrstuvwxcyzd", "fghijklmnaopqrstbuvwxyzd", "fghijklmnaopqrstbuvwxcyz", "fghijklmnaopqrstbuvwxcyzd" }
        ],
        [
            "abcde", new[] { "fghij", "aklmn", "aaopq", "abrst", "abbuv", "abcwx", "abccy", "abcdz", "abcdd", "abcde", "eeeee" },
            new string[] { "fghijklmnopqrstbuvwxcyzde", "fghijklmnaopqrstuvwxcyzde", "fghijklmnaopqrstbuvwxyzde", "fghijklmnaopqrstbuvwxcyze", "fghijklmnaopqrstbuvwxcyzd" }
        ]
    ];

    [Theory]
    [MemberData(nameof(IncorrectLetters_Data))]
    public void IncorrectLetters(string answer, string[] guesses, string[] expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.Spots.Select(s => string.Join("", s.IncorrectLetters.ToArray())).ToArray();

        Assert.Equal(expected[0], actual[0]);
        Assert.Equal(expected[1], actual[1]);
        Assert.Equal(expected[2], actual[2]);
        Assert.Equal(expected[3], actual[3]);
        Assert.Equal(expected[4], actual[4]);
        // Assert.Equal(expected, actual);
    }

    public static string All = "abcdefghijklmnopqrstuvwxyz";
    public static string Except(string except) => string.Join("", All.Except(except.ToArray()));

    public static IEnumerable<object[]> PossibleLetters_Data =>
    [
        [
            "abcde", new string[] { },
            new string[] { All, All, All, All, All }
        ],
        [
            "abcde", new string[] { "aaaaa" },
            new string[] { "a", Except("a"), Except("a"), Except("a"), Except("a")}
        ],
        [
            "abcde", new string[] { "aaaaa", "bbbbb" },
            new string[] { "a", "b", Except("ab"), Except("ab"), Except("ab")}
        ],
        [
            "abcde", new string[] { "aaaaa", "bbbbb", "ccccc" },
            new string[] { "a", "b", "c", Except("abc"), Except("abc")}
        ],
        [
            "abcde", new string[] { "aaaaa", "bbbbb", "ccccc", "ddddd" },
            new string[] { "a", "b", "c", "d", Except("abcd")}
        ],
        [
            "abcde", new string[] { "aaaaa", "bbbbb", "ccccc", "ddddd", "eeeee" },
            new string[] { "a", "b", "c", "d", "e" }
        ],
        [
            "abcde", new string[] { "zyxwv" },
            new string[] {
                "abcdefghijklmnopqrstu",
                "abcdefghijklmnopqrstu",
                "abcdefghijklmnopqrstu",
                "abcdefghijklmnopqrstu",
                "abcdefghijklmnopqrstu",
            }
        ],
        [
            "abcde", new string[] { "zyxwv", "utsrq" },
            new string[] {
                "abcdefghijklmnop",
                "abcdefghijklmnop",
                "abcdefghijklmnop",
                "abcdefghijklmnop",
                "abcdefghijklmnop",
            }
        ],
        [
            "abcde", new string[] { "zyxwv", "utsrq", "ponml" },
            new string[] {
                "abcdefghijk",
                "abcdefghijk",
                "abcdefghijk",
                "abcdefghijk",
                "abcdefghijk",
            }
        ],
        [
            "abcde", new string[] { "zyxwv", "utsrq", "ponml", "kjihg" },
            new string[] {
                "abcdef",
                "abcdef",
                "abcdef",
                "abcdef",
                "abcdef",
            }
        ],
        [
            "abcde", new string[] { "zyxwv", "utsrq", "ponml", "kjihg", "fedcb" },
            new string[] {
                "abcde",
                "abcd",
                "abce",
                "abde",
                "acde",
            }
        ]
    ];

    [Theory]
    [MemberData(nameof(PossibleLetters_Data))]
    public void PossibleLetters(string answer, string[] guesses, string[] expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.Spots.Select(s => s.PossibleLetters.ToArray()).ToArray();

        Assert.Equal(expected.Select(e => e.ToArray()), actual);
    }
}
