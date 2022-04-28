using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileAccess : IWordRepository
{
    private readonly HashSet<string> _words = new ();

    public HashSet<string> GetWords()
    {
        try
        {
            var path = @"Resources\zodynas.txt";
            using (var sr = new StreamReader(path))
            {
                string? line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    var word = line.Split("\t")[0];
                    if (!_words.Contains(word))
                    {
                        _words.Add(word);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return _words;
    }
}