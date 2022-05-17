namespace AnagramSolver.Contracts.Models;

public class Word
{
    public int Id { get; init; }
    public string? Name { get; init; }

    public Word(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Word(string name)
    {
        Name = name;
    }

    public Word()
    {
        
    }
}