namespace WordleStats;

using System.Text.RegularExpressions;

public class GameResult
{
    public string Answer { get; }
    public bool IsWin => WinningGuess > 0;
    public byte WinningGuess { get; }
    public GuessResult[] GuessResults { get; }
    public GameSpotResult[] Spots { get; }
    public LetterResults<GameLetterResult> Letters { get; }

    public GameResult(string answer, GuessResult[] guessResults)
    {
        this.Answer = answer;
        this.GuessResults = guessResults;

        GameSpotResult[] spots = [ new(), new(), new(), new(), new() ];
        LetterResults<GameLetterResult> letters = new();

        for (byte guess = 0; guess < guessResults.Length; guess++)
        {
            HashSet<char> presentLetters = new();
            HashSet<char> absentLetters = new();

            for (byte spot = 0; spot < 5; spot++)
            {
                char letter = guessResults[guess].Spots[spot].GuessLetter;
                GuessLetterResult guessLetterResult = guessResults[guess].Letters[letter];

                letters[letter].GuessLetterResults.Add(guessLetterResult);
                GuessSpotState state = guessResults[guess].Spots[spot].State;

                switch (state)
                {
                    case GuessSpotState.Correct:
                        spots[spot].CorrectLetter = letter;
                        break;
                    case GuessSpotState.Present:
                        spots[spot].IncorrectLetters.Add(letter);
                        presentLetters.Add(letter);
                        break;
                    case GuessSpotState.Incorrect:
                        spots[spot].IncorrectLetters.Add(letter);
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
                    if (spots[spot].CorrectLetter != letter)
                    {
                        spots[spot].IncorrectLetters.Add(letter);
                    }
                }
            }

            if (guessResults[guess].Guess == answer)
            {
                this.WinningGuess = (byte)(guess + 1);
                break;
            }
        }

        this.Spots = spots;
        this.Letters = letters;
    }

    public char[] GetLetters(LetterState state) =>
        WordList.AllLetterChars.Where(l => Letters[l].State == state).ToArray();

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
