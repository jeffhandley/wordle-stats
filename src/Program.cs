using System.Text.RegularExpressions;
using WordleStats;

var builder = WebApplication.CreateSlimBuilder(args);
var app = builder.Build();

var statsApi = app.MapGroup("/stats");

statsApi.MapGet("/", () => new {
    win1 = 0,
    win2 = 20,
    win3 = 150,
    win4 = 200,
    win5 = 30,
    win6 = 10,
    lose = 15
});

var possibilitiesApi = app.MapGroup("/possibilities");

possibilitiesApi.MapGet("/",
    (string answer, string[] guess) =>
    Possibilities.GetPossibilities(answer, guess)
);

var guessApi = app.MapGroup("/guess");

guessApi.MapGet("/",
    (string answer, string[] guess) =>
    {
        HashSet<string> patterns = new();

        (string Result, string[] Patterns, string[] Possibilities)[] results = guess.Select(g => (
            AsResultBlocks(Possibilities.GetGuessResult(answer, g, patterns)),
            patterns.ToArray(),
            Possibilities.GetPossibilities(patterns.ToArray())
        )).ToArray();

        return new {
            answer = answer,
            guesses = guess,
            results = results.Select(r => r.Result),
            patterns = results.Select(r => r.Patterns),
            possibilities = results.Select(r => r.Possibilities.Length)
        };
    }
);

static string AsResultBlocks(LetterResult[] letterResults)
{
    string[] blocks = letterResults.Select(r => r switch
    {
        LetterResult.Correct => "🟩",
        LetterResult.Present => "🟨",
        _ => "⬛",
    }).ToArray();

    return string.Join("", blocks);
}

app.Run();
