namespace WordleStats;

using System.Text.RegularExpressions;

internal partial class Possibilities
{

    public static string[] GetPossibilities(string answer, Span<string> guesses)
    {
        if (guesses.Length == 0)
        {
            return WordList;
        }

        return new string[] {};
    }

    public static string[] GetPossibilities(string[] patterns)
    {
        if (patterns.Length == 0)
        {
            return WordList;
        }

        if (patterns.Contains("spell"))
        {
            foreach (string pattern in patterns)
            {
                System.Console.WriteLine($"{pattern}: {new Regex(pattern).IsMatch("spell")}");
            }
        }

        return WordList.Where(word => patterns.All(pattern => new Regex(pattern).IsMatch(word))).ToArray();
    }

    public static LetterResult[] GetGuessResult(string answer, string guess, HashSet<string> patterns)
    {
        char[] answerLetters = answer.ToArray();

        LetterResult[] results = new LetterResult[5];
        Dictionary<char, (byte Count, bool Present)> letterMatches = new();

        Action<byte, byte> setLetterMatch = (byte guessIndex, byte answerIndex) => {
            results[guessIndex] = (guessIndex == answerIndex) ? LetterResult.Correct : LetterResult.Present;

            (byte count, bool present) = letterMatches[answerLetters[answerIndex]];
            count = (byte)(count + 1);
            present = present || (guessIndex != answerIndex);
            letterMatches[answerLetters[answerIndex]] = (count, present);

            answerLetters[answerIndex] = '-';
        };

        string correctPattern = "";

        for (byte g = 0; g < 5; g++)
        {
            letterMatches[guess[g]] = (0, false);

            if (guess[g] == answerLetters[g])
            {
                setLetterMatch(g, g);
                correctPattern += guess[g];
            }
            else
            {
                correctPattern += $"[^{guess[g]}]";
            }
        }

        bool hasLettersPresent = false;

        for (byte g = 0; g < 5; g++)
        {
            for (byte a = 0; a < 5; a++)
            {
                if (guess[g] == answerLetters[a])
                {
                    System.Console.WriteLine($"Letter '{guess[g]}' is present (position {a}).");
                    setLetterMatch(g, a);
                    hasLettersPresent = true;

                    break;
                }
            }
        }

        if (hasLettersPresent)
        {
            patterns.Add(correctPattern);
        }

        List<char> absentLetters = new();

        foreach (KeyValuePair<char, (byte Count, bool Present)> match in letterMatches)
        {
            if (match.Value.Count > 0)
            {
                if (match.Value.Present)
                {
                    patterns.Add(string.Join(".*", Enumerable.Repeat(match.Key, match.Value.Count)));
                }
            }
            else
            {
                absentLetters.Add(match.Key);
            }
        }

        if (absentLetters.Count > 0)
        {
            patterns.Add($"[^{string.Join("", absentLetters)}]{{5}}");
        }

        return results;
    }
}
