namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService
{
    public HashSet<string> GetWords();
    public IEnumerable<string> GetFilteredWords(string filter);
    bool AddWord(string word);
}