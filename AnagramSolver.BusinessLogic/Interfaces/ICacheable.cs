using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface ICacheable
{
    Task<ActionResult> CacheWordAsync(string word, IEnumerable<string> anagrams);
    Task<CachedWord> GetCachedWordAsync(string input);
}