namespace WordleStats;

public class Game
{
    public static GuessResult GetGuessResult(string answer, string guess)
    {
        GuessSpotState[] states = new GuessSpotState[5];
        LetterResults<GuessLetterResult> letters = new();

        bool[] answerLetterUsed = new bool[5];

        for (byte i = 0; i < 5; i++)
        {
            if (guess[i] == answer[i])
            {
                states[i] = GuessSpotState.Correct;
                letters[guess[i]].Spots[i] = LetterState.Correct;
                answerLetterUsed[i] = true;
            }
        }

        for (byte g = 0; g < 5; g++)
        {
            if (states[g] == GuessSpotState.Correct) continue;

            for (byte a = 0; a < 5; a++)
            {
                if (!answerLetterUsed[a] && guess[g] == answer[a])
                {
                    states[g] = GuessSpotState.Present;
                    letters[guess[g]].Spots[g] = LetterState.Present;
                    answerLetterUsed[a] = true;
                    break;
                }
            }

            if (states[g] != GuessSpotState.Present)
            {
                for (byte i = 0; i < 5; i++)
                {
                    if (letters[guess[g]].Spots[i] == LetterState.Unknown)
                    {
                        letters[guess[g]].Spots[i] = LetterState.Absent;
                    }
                }
            }
        }

        GuessSpotResult[] spots = states.Select((state, i) => new GuessSpotResult(guess[i], state)).ToArray();

        return new GuessResult(guess, spots, letters);
    }

    public static GameResult GetGameResult(string answer, string[] guesses)
    {
        GuessResult[] guessResults = guesses.Select(guess => GetGuessResult(answer, guess)).ToArray();

        return new(answer, guessResults);
    }
}
