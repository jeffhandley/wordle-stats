using WordleStats;

Console.WriteLine("Enter answer or press enter to play a random wordle.");
string? answer = Console.ReadLine();
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

    if (guesses.Count > 0)
    {
        string[] possibilities = result.GetPossibleWords();
        GuessResult guessResult = result.GuessResults[guesses.Count - 1];

        Console.WriteLine($"{AsBlockLetters(guessResult.Guess)}\n{guessResult.Blocks} {possibilities.Length}\n");
    }

    if (result.IsWin || guesses.Count == 6)
    {
        Console.WriteLine($"\n{(result.IsWin ? $"WIN! Score: {result.WinningGuess}" : "Try Again")}");

        foreach (GuessResult guessResult in result.GuessResults)
        {
            Console.WriteLine(guessResult.Blocks);
        }

        Console.WriteLine(AsBlockLetters(answer));

        break;
    }

    Console.WriteLine($" - {AsBlockLetters(string.Join("-", result.GetAvailableLetters()))} - ");
    Console.Write($"Guess {guesses.Count + 1}: ");
    string? guess = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(guess)) break;
    guesses.Add(guess);
}

static string AsBlockLetters(string guess)
{
    return string.Join("", guess.ToArray().Select(letter => letter switch
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
    }));
}
