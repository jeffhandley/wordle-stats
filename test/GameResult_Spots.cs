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

    public static IEnumerable<object[]> IncorrectLetters_Data =>
    [
        [
            "abcde", new string[] { },
            new char[][] { new char[0], new char[0], new char[0], new char[0], new char[0] }
        ],
        [
            "abcde", new[] { "afghi" },
            new char[][] { new char[0], ['f'], ['g'], ['h'], ['i'] }
        ],
        [
            "abcde", new[] { "afghi", "abjkl" },
            new char[][] { new char[0], ['f'], ['g','j'], ['h','k'], ['i','l'] }
        ],
        [
            "abcde", new[] { "afghi", "abjkl", "abcmn" },
            new char[][] { new char[0], ['f'], ['g','j'], ['h','k','m'], ['i','l','n'] }
        ],
        [
            "abcde", new[] { "afghi", "abjkl", "abcmn", "abcdo" },
            new char[][] { new char[0], ['f'], ['g','j'], ['h','k','m'], ['i','l','n','o'] }
        ],
        [
            "abcde", new[] { "afghi", "abjkl", "abcmn", "abcdo", "abcde" },
            new char[][] { new char[0], ['f'], ['g','j'], ['h','k','m'], ['i','l','n','o'] }
        ],
        [
            "aaaaa", new[] { "edcba" },
            new char[][] { ['e'], ['d'], ['c'], ['b'], new char[0] }
        ],
        [
            "aaaaa", new[] { "edcba", "edcax" },
            new char[][] { ['e'], ['d'], ['c'], ['b'], ['x'] }
        ],
        [
            "aaaaa", new[] { "edcba", "edcax", "edaxx" },
            new char[][] { ['e'], ['d'], ['c'], ['b','x'], ['x'] }
        ],
        [
            "aaaaa", new[] { "edcba", "edcax", "edaxx", "eaxxx" },
            new char[][] { ['e'], ['d'], ['c','x'], ['b','x'], ['x'] }
        ],
        [
            "aaaaa", new[] { "edcba", "edcax", "edaxx", "eaxxx", "axxxx" },
            new char[][] { ['e'], ['d','x'], ['c','x'], ['b','x'], ['x'] }
        ],
        [
            "aaaaa", new[] { "edcba", "edcax", "edaxx", "eaxxx", "axxxx", "xxxxx" },
            new char[][] { ['e','x'], ['d','x'], ['c','x'], ['b','x'], ['x'] }
        ]
    ];

    [Theory]
    [MemberData(nameof(IncorrectLetters_Data))]
    public void IncorrectLetters(string answer, string[] guesses, char[][] expected)
    {
        var result = Game.GetGameResult(answer, guesses);
        var actual = result.Spots.Select(s => s.IncorrectLetters.ToArray()).ToArray();

        Assert.Equal(expected, actual);
    }
}
