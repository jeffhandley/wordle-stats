namespace WordleStats;

public class GameSpotResult
{
    public char? CorrectLetter { get; set; }
    public HashSet<char> IncorrectLetters { get; } = new();
    public HashSet<char> PossibleLetters { get; } = new();
}
