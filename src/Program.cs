using WordleStats;

string answer = WordList.AllWords[Random.Shared.Next(WordList.AllWords.Length)];
Console.WriteLine($"Answer: {answer}");

List<string> guesses = new();

while (guesses.Count < 6)
{
    Console.Write($"Guess {guesses.Count + 1}: ");
    string guess = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(guess)) break;

    guesses.Add(guess);

    GameResult result = Game.GetGameResult(answer, guesses.ToArray());
    string blocks = result.GuessResults[guesses.Count - 1].Blocks;
    string[] possibilities = result.GetPossibleWords();

    Console.WriteLine($"{blocks} {possibilities.Length}\n");
}
