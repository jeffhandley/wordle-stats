namespace WordleStats;

using System.Linq;

public class GuessResult
{
    private string? _blocks;

    public GuessSpotResult[] Spots { get; }
    public LetterResults<GuessLetterResult> Letters { get; }

    public GuessResult(GuessSpotResult[] spots, LetterResults<GuessLetterResult> letters)
    {
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
