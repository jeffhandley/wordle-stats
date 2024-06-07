using WordleStats;
using static System.Console;
using static Crayon.Output;

var cli = UtilityCli.CliArgs.Parse(args);
string? answer = cli.GetString();

if (string.IsNullOrWhiteSpace(answer))
{
    Console.WriteLine("Enter answer or press enter to play a random wordle.");
    answer = Console.ReadLine();
}

int answerIndex;

if (string.IsNullOrWhiteSpace(answer))
{
    answerIndex = Random.Shared.Next(WordList.AllWords.Length);
    answer = WordList.AllWords[answerIndex];
}
else if (int.TryParse(answer, out answerIndex))
{
    answer = WordList.AllWords[answerIndex];
}
else
{
    answerIndex = Array.IndexOf(WordList.AllWords, answer);
}

Console.WriteLine($"Answer Index: {answerIndex}");

List<string> guesses = new();

while (true)
{
    GameResult result = Game.GetGameResult(answer, guesses.ToArray());
    Action showBoard = () =>
    {
        foreach (GameGuessResult guessResult in result.GuessResults)
        {
            Write("           ");

            foreach (GuessSpotResult spotResult in guessResult.Spots)
            {
                var withColor = spotResult.State switch
                {
                    GuessSpotState.Correct => Bold().Bright.Green(),
                    GuessSpotState.Present => Bold().Bright.Yellow(),
                                         _ => Dim().White(),
                };

                Write(withColor.Text(AsBlockLetter(spotResult.GuessLetter)));
            }

            Console.WriteLine($" {guessResult.PossibleWords.Length}");
        }

    };

    if (result.IsWin || guesses.Count == 6)
    {
        showBoard();
        Console.WriteLine($"\n{(result.IsWin ? $"WIN! Score: {result.WinningGuess}" : "Try Again")}");

        break;
    }

    string[] possibilities = result.GetPossibleWords();
    string? guess;

    while (true)
    {
        showBoard();
        WriteLine();
        WriteLine($"           There are {Bold().Underline().Text(possibilities.Length.ToString())} possible words remaining");
        WriteLine($"Absent:    {Dim().White(AsBlockLetters(string.Join(" ", result.GetLetters(LetterState.Absent))))}");
        WriteLine($"Available: {Bold().Bright.White(AsBlockLetters(string.Join(" ", result.GetLettersAvailable())))}");
        WriteLine($"Present:   {Bold().Bright.Yellow(AsBlockLetters(string.Join(" ", result.GetLettersPresent())))}");
        WriteLine($"Correct:   {Bold().Bright.Green(AsBlockLetters(string.Join(" ", result.GetLettersCorrect())))}");
        WriteLine();

        Write($"Guess {guesses.Count + 1}:   ");
        guess = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(guess)) break;

        if (Array.IndexOf(WordList.AllWords, guess.ToLower()) == -1)
        {
            Console.WriteLine(Bold().Red($"Guess '{guess}' is not recognized as a word."));
        }
        else
        {
            guesses.Add(guess);
            break;
        }
    }

    if (string.IsNullOrWhiteSpace(guess)) break;
}

static string AsBlockLetter(char letter) => letter switch
{
    'a' => "🄰 ",
    'b' => "🄱 ",
    'c' => "🄲 ",
    'd' => "🄳 ",
    'e' => "🄴 ",
    'f' => "🄵 ",
    'g' => "🄶 ",
    'h' => "🄷 ",
    'i' => "🄸 ",
    'j' => "🄹 ",
    'k' => "🄺 ",
    'l' => "🄻 ",
    'm' => "🄼 ",
    'n' => "🄽 ",
    'o' => "🄾 ",
    'p' => "🄿 ",
    'q' => "🅀 ",
    'r' => "🅁 ",
    's' => "🅂 ",
    't' => "🅃 ",
    'u' => "🅄 ",
    'v' => "🅅 ",
    'w' => "🅆 ",
    'x' => "🅇 ",
    'y' => "🅈 ",
    'z' => "🅉 ",
    _   => letter.ToString()
};

static string AsBlockLetters(string letters) =>
    string.Join("", letters.ToArray().Select(AsBlockLetter));
