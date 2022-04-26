namespace AnagramSolver.BusinessLogic;

public interface IAnagramController
{
    public HashSet<string> FindAnagrams(string inputWord);
}