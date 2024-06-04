namespace WordleStats;

public class GameSpotResult
{
    public char? CorrectLetter { get; set; }
    public SortedSet<char> IncorrectLetters { get; } = new();

    public bool AllLettersPossible => CorrectLetter is null && !IncorrectLetters.Any();

    public char[] PossibleLetters =>  CorrectLetter is not null ?
        [ CorrectLetter.Value ] :
        WordList.AllLetterChars.Except(IncorrectLetters).ToArray();
}
