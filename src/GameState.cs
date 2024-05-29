namespace WordleStats;

public enum LetterResult
{
    Absent,
    Present,
    Correct
}

public class GameState
{
    public string Answer { get; }

    public List<GuessState> Guesses { get; } = new();
    public List<LetterResult[]> Results { get; } = new();
    public List<string> Patterns { get; } = new();

    public GameState(string answer, string[] guesses)
    {
        Answer = answer.ToLower();

        foreach (string guess in guesses)
        {
            Guesses.Add(new GuessState(guess, this));
        }
    }
}

public class GuessState
{
    public string Guess { get; }
    public LetterResult[] Results { get; }
    public string[] Patterns { get; }

    public Dictionary<byte, char> LettersCorrect { get; } = new();
    public Dictionary<char, (byte Count, List<byte> IncorrectIndexes)> LettersPresent { get; } = new();
    public Dictionary<char, List<byte>> LettersAbsent { get; } = new();

    public GuessState(string guess, GameState game)
    {
        Guess = guess.ToLower();
        Results = new LetterResult[5];

        char[] answerLetters = game.Answer.ToArray();
        char[] guessLetters = guess.ToArray();

        for (byte i = 0; i < 5; i++)
        {
            if (guessLetters[i] == answerLetters[i])
            {
                LettersCorrect[i] = guess[i];
                Results[i] = LetterResult.Correct;

                answerLetters[i] = '-';
                guessLetters[i] = '-';
            }
        }

        for (byte gi = 0; gi < 5; gi++)
        {
            if (guessLetters[gi] == '-') continue;

            for (byte ai = 0; ai < 5; ai++)
            {
                if (answerLetters[ai] == '-') continue;

                if (answerLetters[ai] == guessLetters[gi])
                {
                    Results[gi] = LetterResult.Present;

                    if (!LettersPresent.TryGetValue(guessLetters[gi], out (byte Count, List<byte> IncorrectIndexes) present))
                    {
                        present.IncorrectIndexes = new();
                    }

                    present.Count = (byte)(present.Count + 1);
                    present.IncorrectIndexes.Add(gi);
                    LettersPresent[guessLetters[gi]] = present;

                    answerLetters[ai] = '-';
                    guessLetters[gi] = '-';
                }
            }

            if (Results[gi] == LetterResult.Absent)
            {
                if (!LettersAbsent.TryGetValue(guessLetters[gi], out List<byte> absent))
                {
                    absent = new();
                }

                absent.Add(gi);
                LettersAbsent[guessLetters[gi]] = absent;
            }
        }
    }
}
