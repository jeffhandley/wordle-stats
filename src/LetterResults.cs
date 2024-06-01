namespace WordleStats;

public class LetterResults
{
    private Dictionary<char, LetterResult> _results = new();

    public LetterResult this[char letter]
    {
        get
        {
            if (!_results.ContainsKey(letter))
            {
                _results[letter] = new();
            }

            return _results[letter];
        }
    }
}
