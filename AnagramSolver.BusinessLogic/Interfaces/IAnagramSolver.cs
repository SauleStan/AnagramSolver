namespace AnagramSolver.BusinessLogic.Interfaces;

public interface IAnagramSolver
{
    public HashSet<string> FindAnagrams(string inputWord);
}