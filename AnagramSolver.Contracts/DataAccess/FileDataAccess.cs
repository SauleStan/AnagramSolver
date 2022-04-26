using System.Text;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class FileDataAccess : IWordRepository
{
    private readonly HashSet<string> _words = new ();

    public HashSet<string> GetWords()
    {
        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (var sr = new StreamReader("zodynas.txt"))
            {
                string? line;
                // Read and display lines from the file until the end of
                // the file is reached.
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
            // Let the user know what went wrong.
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        return _words;
    }
}