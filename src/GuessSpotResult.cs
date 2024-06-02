namespace WordleStats;

public class GuessSpotResult
{
    public char GuessLetter { get; }
    public GuessSpotState State { get; }

    public GuessSpotResult(char guessLetter, GuessSpotState state)
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
                GuessSpotState.Correct => "🟩",
                GuessSpotState.Present => "🟨",
                                     _ => "⬛",
            };
        }
    }
}
