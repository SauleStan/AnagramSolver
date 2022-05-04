namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramResolver
{
    public HashSet<string> FindAnagrams(string inputWord);
}