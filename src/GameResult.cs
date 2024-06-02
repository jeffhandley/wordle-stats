namespace WordleStats;

public class GameResult
{
    public GuessResult[] GuessResults { get; }
    public GameSpotResult[] Spots { get; }
    public LetterResults<GameLetterResult> Letters { get; }

    public GameResult(GuessResult[] guessResults)
    {
        this.GuessResults = guessResults;

        GameSpotResult[] spots = new GameSpotResult[5];
        LetterResults<GameLetterResult> letters = new();

        for (byte guess = 0; guess < guessResults.Length; guess++)
        {
            for (byte spot = 0; spot < 5; spot++)
            {
                char letter = guessResults[guess].Spots[spot].GuessLetter;

                if (guess == 0) spots[spot] = new GameSpotResult();

                if (guessResults[guess].Spots[spot].State == GuessSpotState.Correct)
                {
                    spots[spot].CorrectLetter = letter;
                }
                else
                {
                    spots[spot].IncorrectLetters.Add(letter);
                }
            }

            foreach (char letter in guessResults[guess].Letters.Guessed)
            {
                GameLetterResult result = letters[letter];
                result.GuessLetterResults.Add(guessResults[guess].Letters[letter]);
            }
        }

        this.Spots = spots;
        this.Letters = letters;
    }
}
