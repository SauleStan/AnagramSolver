using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IWordService : IWord, IFilterable, ICacheable, ISearchInfo, IClearTable
{
}