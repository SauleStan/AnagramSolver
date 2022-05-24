using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces;

public interface IWordRepository : IWordCrud, IFilterable, ICacheable, ISearchInfo, IClearTable
{
}