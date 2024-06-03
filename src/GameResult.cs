namespace WordleStats;

public class GameResult
{
    public GuessResult[] GuessResults { get; }
    public GameSpotResult[] Spots { get; }
    public LetterResults<GameLetterResult> Letters { get; }

    public GameResult(GuessResult[] guessResults)
    {
        this.GuessResults = guessResults;

        GameSpotResult[] spots = [ new(), new(), new(), new(), new() ];
        LetterResults<GameLetterResult> letters = new();

        for (byte guess = 0; guess < guessResults.Length; guess++)
        {
            for (byte spot = 0; spot < 5; spot++)
            {
                char letter = guessResults[guess].Spots[spot].GuessLetter;

                if (guessResults[guess].Spots[spot].State == GuessSpotState.Correct)
                {
                    spots[spot].CorrectLetter = letter;
                }
                else if (guessResults[guess].Spots[spot].State == GuessSpotState.Present)
                {
                    spots[spot].IncorrectLetters.Add(letter);
                }
                else
                {
                    for (byte i = 0; i < 5; i++)
                    {
                        if (spots[i].CorrectLetter != letter)
                        {
                            spots[i].IncorrectLetters.Add(letter);
                        }
                    }
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
