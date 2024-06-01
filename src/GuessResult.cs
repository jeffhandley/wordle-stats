namespace WordleStats;

using System.Linq;

public class GuessResult
{
    private string? _blocks;

    public SpotResult[] Spots { get; private set; }
    public LetterResults Letters { get; }

    public GuessResult(SpotResult[] spots, LetterResults letters)
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
