using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class WordFileAccess : IWordRepository
{
    private readonly HashSet<string> _words = new ();
    private readonly string _path;
    public WordFileAccess(string path)
    {
        _path = path;
    }
    public IEnumerable<string> GetWords()
    {
        try
        {
            using (var sr = new StreamReader(_path))
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

    public bool AddWord(string word)
    {
        try
        {
            File.AppendAllText(_path, word + Environment.NewLine);
            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
    }
}