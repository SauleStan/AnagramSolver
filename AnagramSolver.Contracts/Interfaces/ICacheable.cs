using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface ICacheable
{
    Task CacheWordAsync(string word, IEnumerable<string> anagrams);
    Task<CachedWord> GetCachedWordAsync(string input);
}