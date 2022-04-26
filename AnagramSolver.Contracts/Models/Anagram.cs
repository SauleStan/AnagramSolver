namespace AnagramSolver.Contracts.Models;

public class Anagram
{
    public string Name { get; }
    public string Crib { get; }

    public Anagram(string name, string crib)
    {
        Name = name;
        Crib = crib;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Crib);
    }
}