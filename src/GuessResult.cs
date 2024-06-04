namespace WordleStats;

public class GuessResult
{
    private string? _blocks;

    public string Guess { get; }
    public GuessSpotResult[] Spots { get; }
    public LetterResults<GuessLetterResult> Letters { get; }

    public GuessResult(string guess, GuessSpotResult[] spots, LetterResults<GuessLetterResult> letters)
    {
        this.Guess = guess;
        this.Spots = spots;
        this.Letters = letters;
    }

    public string Blocks
    {
        get
        {
            if (_blocks is null)
            {
                _blocks = string.Join("", this.Spots.Select(s => s.Block));
            }

            return _blocks;
        }
    }

}
