namespace WordleStats;

using System.Linq;

public class LetterResult
{
    public LetterState[] Spots { get; } = new LetterState[5];

    public LetterState this[byte spot]
    {
        get => Spots[spot];
        set => Spots[spot] = value;
    }

    public LetterState State => Spots.Max();
}
