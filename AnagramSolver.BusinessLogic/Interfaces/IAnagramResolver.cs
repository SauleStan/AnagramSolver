namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramResolver
{
    public List<string> FindAnagrams(string inputWord);
}