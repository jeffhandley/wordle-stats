namespace WordleStats.Tests;

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
}
