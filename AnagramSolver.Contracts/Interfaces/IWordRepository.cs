using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository
{
    IEnumerable<WordModel> GetWords();
    IEnumerable<WordModel> GetFilteredWords(string filter);
    bool AddWord(string word);
    bool AddWords(IEnumerable<string> words);
}