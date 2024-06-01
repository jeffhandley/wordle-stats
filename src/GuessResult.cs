namespace WordleStats;

using System.Linq;

public class GuessResult
{
    private string? _blocks;
    public SpotResult[] Spots { get; private set; }

    public GuessResult(SpotResult[] spots)
    {
        this.Spots = spots;
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
