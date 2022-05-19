namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramResolver
{
    public Task<List<string>> FindAnagramsAsync(string inputWord);
}