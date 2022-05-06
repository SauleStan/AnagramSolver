namespace AnagramSolver.Contracts.Models;

public class WordModel
{
    public int Id { get; }
    public string Name { get; }

    public WordModel(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public WordModel(string name)
    {
        Name = name;
    }
}