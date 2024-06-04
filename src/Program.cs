using WordleStats;

Console.WriteLine("Enter answer or press enter to play a random wordle.");
string? answer = Console.ReadLine();

if (string.IsNullOrWhiteSpace(answer))
{
    answer = WordList.AllWords[Random.Shared.Next(WordList.AllWords.Length)];
}

List<string> guesses = new();

while (guesses.Count < 6)
{
    Console.Write($"Guess {guesses.Count + 1}: ");
    string? guess = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(guess)) break;

    guesses.Add(guess);

    GameResult result = Game.GetGameResult(answer, guesses.ToArray());
    string blocks = result.GuessResults[guesses.Count - 1].Blocks;
    string[] possibilities = result.GetPossibleWords();

    Console.WriteLine($"{GuessToBlockLetters(guess)}\n{blocks} {possibilities.Length}\n");

    if (result.IsWin)
    {
        Console.WriteLine($"WIN! Answer: {answer} - Score: {result.WinningGuess}");
        break;
    }
    else if (guesses.Count == 6)
    {
        Console.WriteLine($"LOSE! Answer: {answer}");
    }
}

static string GuessToBlockLetters(string guess)
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
