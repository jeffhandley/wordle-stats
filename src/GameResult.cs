namespace WordleStats;

using System.Text.RegularExpressions;

public class GameResult
{
    public string Answer { get; }
    public GameSpotResult[] Spots { get; }
    public LetterResults<GameLetterResult> Letters { get; }
    public GameGuessResult[] GuessResults { get; }

    public byte WinningGuess { get; }
    public bool IsWin => WinningGuess > 0;

    public GameResult(string answer, GuessResult[] guessResults)
    {
        Answer = answer;
        Spots = [ new(), new(), new(), new(), new() ];
        Letters = new();
        GuessResults = new GameGuessResult[guessResults.Length];

        for (byte guess = 0; guess < guessResults.Length; guess++)
        {
            HashSet<char> presentLetters = new();
            HashSet<char> absentLetters = new();

            for (byte spot = 0; spot < 5; spot++)
            {
                char letter = guessResults[guess].Spots[spot].GuessLetter;
                GuessLetterResult guessLetterResult = guessResults[guess].Letters[letter];

                Letters[letter].GuessLetterResults.Add(guessLetterResult);
                GuessSpotState state = guessResults[guess].Spots[spot].State;

                switch (state)
                {
                    case GuessSpotState.Correct:
                        Spots[spot].CorrectLetter = letter;
                        break;
                    case GuessSpotState.Present:
                        Spots[spot].IncorrectLetters.Add(letter);
                        presentLetters.Add(letter);
                        break;
                    case GuessSpotState.Incorrect:
                        Spots[spot].IncorrectLetters.Add(letter);
                        absentLetters.Add(letter);
                        break;
                }
            }

            // Letters marked as absent without another present spot are
            // known to be incorrect at all places where the letter wasn't
            // already marked as correct.
            foreach (char letter in absentLetters.Except(presentLetters))
            {
                for (byte spot = 0; spot < 5; spot++)
                {
                    if (Spots[spot].CorrectLetter != letter)
                    {
                        Spots[spot].IncorrectLetters.Add(letter);
                    }
                }
            }

            string possibilityPattern = GetPossibilityPattern();
            string[] possibleWords = GetPossibleWords();

            GuessResults[guess] = new GameGuessResult(guessResults[guess], possibilityPattern, possibleWords);

            if (guessResults[guess].Guess == answer)
            {
                WinningGuess = (byte)(guess + 1);
                break;
            }
        }
    }

    public char[] GetLetters(params LetterState[] states) =>
        WordList.AllLetterChars.Where(l => states.Contains(Letters[l].State)).ToArray();

    public char[] GetLettersAvailable() => GetLetters(LetterState.Unknown, LetterState.Present, LetterState.Correct);
    public char[] GetLettersKnown() => GetLetters(LetterState.Present, LetterState.Correct);

    public string GetPossibilityPattern()
    {
        // Use a character by character pattern for each spot
        // If all letters are possible, use '.'; if the correct
        // letter is known, use that letter only; otherwise, use
        // a negated list of characters for those that are known
        // to be incorrect at that spot.
        string correctLetters = string.Join("", Spots.Select(spot =>
            spot.AllLettersPossible ? "." :
            spot.CorrectLetter is not null ? spot.CorrectLetter.ToString() :
            $"[^{string.Join("", spot.IncorrectLetters)}]"
        ));

        // Use a look-ahead assertion for all letters that are
        // known to be present.
        string presentLetters = string.Join("",
            GetLetters(LetterState.Present).Select(l => $"(?=.*{l})")
        );

        return presentLetters + correctLetters;
    }

    public string[] GetPossibleWords()
    {
        Regex pattern = new(GetPossibilityPattern(), RegexOptions.Compiled);

        return WordList.AllWords.Where(word => pattern.IsMatch(word)).ToArray();
    }
}
