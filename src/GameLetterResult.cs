namespace WordleStats;

public class GameLetterResult
{
    public List<GuessLetterResult> GuessLetterResults { get; } = new();
    public LetterState State => GuessLetterResults.Any() ? GuessLetterResults.Max(g => g.State) : LetterState.Unknown;
}
