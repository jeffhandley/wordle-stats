namespace WordleStats;

public class Game
{
    public static GuessResult GetGuessResult(string answer, string guess)
    {
        SpotState[] states = new SpotState[5];

        for (byte i = 0; i < 5; i++)
        {
            if (guess[i] == answer[i]) states[i] = SpotState.Correct;
        }

        bool[] answerLetterUsed = new bool[5];

        for (byte g = 0; g < 5; g++)
        {
            if (states[g] == SpotState.Correct) continue;

            for (byte a = 0; a < 5; a++)
            {
                if (states[a] == SpotState.Correct || answerLetterUsed[a]) continue;

                if (guess[g] == answer[a])
                {
                    states[g] = SpotState.Present;
                    answerLetterUsed[a] = true;
                    break;
                }
            }
        }

        SpotResult[] spots = states.Select((state, i) => new SpotResult(guess[i], state)).ToArray();

        return new GuessResult(spots);
    }
}
