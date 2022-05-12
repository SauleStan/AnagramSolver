namespace AnagramSolver.Contracts.Models;

public class Word
{
    public int Id { get; set; }
    public string? Name { get; set; }

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