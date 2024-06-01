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

    public byte MinCount => (byte)Spots.Count(s => s == LetterState.Present || s == LetterState.Correct);
    public byte MaxCount => (byte)Spots.Count(s => s != LetterState.Absent);

    public Range CountRange => new(MinCount, MaxCount);
}
