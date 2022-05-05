using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileAccess : IWordRepository
{
    private readonly List<string> _words = new ();

    public IEnumerable<string> GetWords(string path)
    {
        try
        {
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

    public void AddWord(string word, string path)
    {
        try
        {

            File.AppendAllText(path, word + Environment.NewLine);
        }
        catch (FileNotFoundException e)
        {
            throw;
        }
    }
}