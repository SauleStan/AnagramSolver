using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class InMemData : IWordRepository
{
    private readonly HashSet<string> _words = new ();

    public InMemData()
    {
        _words.Add("alus");
        _words.Add("sula");
        _words.Add("alkis");
    }

    public HashSet<string> GetWords()
    {
        return _words;
    }
}