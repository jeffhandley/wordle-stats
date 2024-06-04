namespace WordleStats;

public class GameLetterResult
{
    public List<GuessLetterResult> GuessLetterResults { get; } = new();
    public LetterState State => GuessLetterResults.Any() ? GuessLetterResults.Max(g => g.State) : LetterState.Unknown;

    public byte MinCount => (byte)GuessLetterResults.Select(g => g.Spots.Count(s => s == LetterState.Present || s == LetterState.Correct)).Max();
    public byte MaxCount => (byte)GuessLetterResults.Select(g => g.Spots.Count(s => s != LetterState.Absent)).Max();

    public Range CountRange => new(MinCount, MaxCount);
}
