using System.Data;
using System.Data.SqlClient;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.DataAccess;

public class WordDbRepository : IWordRepository
{
    private readonly SqlConnection _cn = new ();
    private readonly List<WordModel> _words = new ();

    public WordDbRepository()
    {
        _cn.ConnectionString = "Server=localhost;Database=AnagramDB;Trusted_Connection=True;";
    }
    public IEnumerable<WordModel> GetWords()
    {
        _cn.Open();
        SqlCommand command = new SqlCommand();
        command.Connection = _cn;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT Id, Name FROM Word";
        SqlDataReader dataReader = command.ExecuteReader();
        if (dataReader.HasRows)
        {
            while (dataReader.Read())
            {
                _words.Add(new WordModel((int)dataReader["Id"], (string)dataReader["Name"]));
            }
        }
        dataReader.Close();
        _cn.Close();
        return _words;
    }

    public bool AddWord(string word)
    {
        try
        {
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [Word] ([Name]) VALUES (@Word)";
            command.Parameters.AddWithValue("@Word", word);
            command.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _cn.Close();
        }
    }

    public bool AddWords(IEnumerable<string> words)
    {
        try
        {
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [Word] (Name) VALUES (@Word)";
            foreach (var word in words)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new  SqlParameter("@Word", word));
                command.ExecuteNonQuery();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _cn.Open();
        }
    }

    public IEnumerable<WordModel> GetFilteredWords(string filter)
    {
        
        try
        {
            List<WordModel> filteredWords = new ();
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Name FROM [Word] WHERE Name LIKE @Filter";
            command.Parameters.AddWithValue("@Filter", filter);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    filteredWords.Add(new WordModel((int)dataReader["Id"], (string)dataReader["Name"]));
                }
            }
            dataReader.Close();
            return filteredWords;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _cn.Close();
        }
        
    }

    public bool CacheWord(string searchWord, IEnumerable<string> anagrams)
    {
        try
        {
            if (_words.Count == 0)
            {
                GetWords();
            }
            var anagramModels = _words.FindAll(word => anagrams.Contains(word.Name));
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO [CachedWord]([InputWord],[AnagramWordId]) VALUES (@SearchedWord, @AnagramId)";
            foreach (var anagramModel in anagramModels)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SearchedWord", searchWord);
                command.Parameters.AddWithValue("@AnagramId", anagramModel.Id);
                command.ExecuteNonQuery();
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _cn.Close();
        }
    }

    public IEnumerable<CachedWord> GetCachedWords()
    {
        try
        { 
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT cw.Id, cw.InputWord, w.Name " +
                                  "FROM [CachedWord] cw " +
                                  "INNER JOIN [Word] w " +
                                  "ON cw.AnagramWordId = w.Id";
            SqlDataReader dataReader = command.ExecuteReader();
            
            List<CachedWord> cachedWords = new();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var inputWord = (string)dataReader["InputWord"];
                    if (!cachedWords.Any(cached => cached.InputWord.Equals(inputWord)))
                    {
                        var cachedWord = new CachedWord()
                        {
                            Id = (int) dataReader["Id"],
                            InputWord = inputWord
                        };
                        cachedWord.Anagrams.Add((string) dataReader["Name"]);
                        cachedWords.Add(cachedWord);

                    }
                    else
                    {
                        var existingCachedWord = cachedWords.First(cached => cached.InputWord.Equals(inputWord));
                        if (existingCachedWord.Anagrams.Contains((string)dataReader["Name"]))
                        {
                            continue;
                        }
                        existingCachedWord.Anagrams.Add((string) dataReader["Name"]);
                    }
                }
            }
            dataReader.Close();
            return cachedWords;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _cn.Close();
        }
        
    }

    public IEnumerable<SearchInfo> GetAnagramSearchInfo()
    {
        try
        {
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT si.Id, si.UserIp, si.ExecTime, si.SearchedWord, w.Name " +
                                  "FROM SearchInfo si " +
                                  "INNER JOIN Word w " +
                                  "ON si.AnagramId = w.Id";
            SqlDataReader dataReader = command.ExecuteReader();

            var searchInfoList = new List<SearchInfo>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var searchedWord = (string)dataReader["SearchedWord"];
                    if (!searchInfoList.Any(searchInfo => searchInfo.SearchedWord.Equals(searchedWord)))
                    {
                        var newSearchInfo = new SearchInfo()
                        {
                            Id = (int)dataReader["Id"],
                            UserIp = (string) dataReader["UserIp"],
                            ExecTime = (TimeSpan) dataReader["ExecTime"],
                            SearchedWord = (string) dataReader["SearchedWord"],
                        };
                        newSearchInfo.Anagrams.Add((string) dataReader["Name"]);
                        searchInfoList.Add(newSearchInfo);
                    }
                    else
                    {
                        var existingCachedWord = searchInfoList.First(searchInfo => searchInfo.SearchedWord.Equals(searchedWord));
                        if (existingCachedWord.Anagrams.Contains((string)dataReader["Name"]))
                        {
                            continue;
                        }
                        existingCachedWord.Anagrams.Add((string) dataReader["Name"]);
                    }
                }
            }
            dataReader.Close();
            return searchInfoList;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _cn.Close();
        }
    }

    public bool AddAnagramSearchInfo(SearchInfo searchInfo)
    {
        try
        {
            if (_words.Count == 0)
            {
                GetWords();
            }
            var anagramModels = _words.FindAll(word => searchInfo.Anagrams.Contains(word.Name));
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO SearchInfo ([UserIp],[ExecTime],[SearchedWord],[AnagramId]) VALUES (@UserIp, @ExecTime, @SearchedWord, @AnagramId)";
            foreach (var anagramModel in anagramModels)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserIp", searchInfo.UserIp);
                command.Parameters.AddWithValue("@ExecTime", searchInfo.ExecTime);
                command.Parameters.AddWithValue("@SearchedWord", searchInfo.SearchedWord);
                command.Parameters.AddWithValue("@AnagramId", anagramModel.Id);
                command.ExecuteNonQuery();
            }

            return true;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            _cn.Close();
        }
    }
}