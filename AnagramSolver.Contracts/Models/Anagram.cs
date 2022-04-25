namespace AnagramSolver.Contracts.Models;

public class Anagram : IEquatable<string>
{
    public string Name { get; }
    public string Crib { get; }

    public Anagram(string name)
    {
        Name = name;
        Crib = SortWordAlphabetically(Name);
    }
    
    private string SortWordAlphabetically(string word)
    {
        char[] chars = word.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }

    
    public bool Equals(Anagram other)
    {
        return Crib == other.Crib;
    }

    public bool Equals(string? other)
    {
        return Name == other;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Anagram)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Crib);
    }
}