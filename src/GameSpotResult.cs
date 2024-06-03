namespace WordleStats;

public class GameSpotResult
{
    private static char[] AllLetters = "abcdefghijklmnopqrstuvwxyz".ToArray();

    public char? CorrectLetter { get; set; }
    public HashSet<char> IncorrectLetters { get; } = new();

    public HashSet<char> PossibleLetters => new(
        CorrectLetter is not null ?
            [ CorrectLetter.Value ] :
            AllLetters.Except(IncorrectLetters)
    );
}
