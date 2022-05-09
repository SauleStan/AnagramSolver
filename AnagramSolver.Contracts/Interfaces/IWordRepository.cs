using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<WordModel> GetWords();
    public IEnumerable<WordModel> GetFilteredWords(string filter);
    bool AddWord(string word);
}