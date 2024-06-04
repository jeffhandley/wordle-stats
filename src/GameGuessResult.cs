namespace WordleStats;

public class GameGuessResult : GuessResult
{
    public string PossibilityPattern { get; }
    public string[] PossibleWords { get; }

    public GameGuessResult(GuessResult guessResult, string possibilityPattern, string[] possibleWords)
        : base(guessResult.Guess, guessResult.Spots, guessResult.Letters)
    {
        PossibilityPattern = possibilityPattern;
        PossibleWords = possibleWords;
    }
}
