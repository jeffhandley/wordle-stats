namespace WordleStats;

public class SpotResult
{
    public char GuessLetter { get; }
    public SpotState State { get; }

    public SpotResult(char guessLetter, SpotState state)
    {
        this.GuessLetter = guessLetter;
        this.State = state;
    }

    public string Block
    {
        get
        {
            return State switch
            {
                SpotState.Correct => "🟩",
                SpotState.Present => "🟨",
                                _ => "⬛",
            };
        }
    }
}
