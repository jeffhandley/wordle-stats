namespace WordleStats;

public class LetterResults<T> where T : new()
{
    private Dictionary<char, T> _results = new();

    public T this[char letter]
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

    public char[] Guessed => _results.Keys.ToArray();
}
