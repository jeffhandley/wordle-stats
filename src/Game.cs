namespace WordleStats;

public class Game
{
    public static GuessResult GetGuessResult(string answer, string guess)
    {
        SpotState[] states = new SpotState[5];
        LetterResults letters = new();

        bool[] answerLetterUsed = new bool[5];

        for (byte i = 0; i < 5; i++)
        {
            if (guess[i] == answer[i])
            {
                states[i] = SpotState.Correct;
                letters[guess[i]][i] = LetterState.Correct;
                answerLetterUsed[i] = true;
            }
        }

        for (byte g = 0; g < 5; g++)
        {
            if (states[g] == SpotState.Correct) continue;

            for (byte a = 0; a < 5; a++)
            {
                if (!answerLetterUsed[a] && guess[g] == answer[a])
                {
                    states[g] = SpotState.Present;
                    letters[guess[g]][g] = LetterState.Present;
                    answerLetterUsed[a] = true;
                    break;
                }
            }

            if (states[g] != SpotState.Present)
            {
                for (byte i = 0; i < 5; i++)
                {
                    if (letters[guess[g]][i] == LetterState.Unknown)
                    {
                        letters[guess[g]][i] = LetterState.Absent;
                    }
                }
            }
        }

        SpotResult[] spots = states.Select((state, i) => new SpotResult(guess[i], state)).ToArray();

        return new GuessResult(spots, letters);
    }
}
