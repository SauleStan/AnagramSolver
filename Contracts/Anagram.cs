namespace Contracts;

public class Anagram
{
    public string Name { get; }
    public string Crib { get; }

    public Anagram(string name)
    {
        Name = name;
        Crib = SortNameAlphabetically();
    }
    
    /// <summary>
    /// This function sorts name prop in alphabetical order.
    /// Knowing the crib of the word allows to check for anagrams. 
    /// </summary>
    /// <returns> String in alphabetical order.</returns>
    private string SortNameAlphabetically()
    {
        char[] chars = Name.ToCharArray();
        Array.Sort(chars);
        return new string(chars);
    }
}