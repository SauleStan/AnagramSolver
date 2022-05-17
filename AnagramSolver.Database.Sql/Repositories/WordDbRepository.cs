using System.Data;
using System.Data.SqlClient;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Database.Sql.Repositories;

public class WordDbRepository : IWordRepository
{
    private readonly SqlConnection _cn = new ();
    private readonly List<Word> _words = new ();

    public WordDbRepository()
    {
        _cn.ConnectionString = "Server=localhost;Database=AnagramDB;Trusted_Connection=True;";
    }
    public IEnumerable<Word> GetWords()
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
                _words.Add(new Word((int)dataReader["Id"], (string)dataReader["Name"]));
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
            command.CommandText = "INSERT INTO Word ([Name]) VALUES (@Word)";
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

    public void EditWord(string word, string editedWord)
    {
        throw new NotImplementedException();
    }

    public void DeleteWord(string word)
    {
        throw new NotImplementedException();
    }

    public bool AddWords(IEnumerable<string> words)
    {
        try
        {
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Word (Name) VALUES (@Word)";
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

    public IEnumerable<Word> GetFilteredWords(string filter)
    {
        
        try
        {
            List<Word> filteredWords = new ();
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Name FROM Word WHERE Name LIKE @Filter";
            command.Parameters.AddWithValue("@Filter", filter);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    filteredWords.Add(new Word((int)dataReader["Id"], (string)dataReader["Name"]));
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

    public void CacheWord(string searchWord, IEnumerable<string> anagrams)
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
            command.CommandText = "INSERT INTO CachedWord([InputWord],[AnagramWordId]) VALUES (@SearchedWord, @AnagramId)";
            foreach (var anagramModel in anagramModels)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SearchedWord", searchWord);
                command.Parameters.AddWithValue("@AnagramId", anagramModel.Id);
                command.ExecuteNonQuery();
            }
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

    public CachedWord GetCachedWord(string input)
    {
        try
        { 
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT cw.Id, cw.InputWord, w.Name " +
                                  "FROM CachedWord cw " +
                                  "INNER JOIN Word w " +
                                  "ON cw.AnagramWordId = w.Id " +
                                  "WHERE cw.InputWord = @Input";
            command.Parameters.AddWithValue("@Input", input);
            SqlDataReader dataReader = command.ExecuteReader();
            var cachedWord = new CachedWord()
            {
                InputWord = String.Empty
            };

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var inputWord = (string)dataReader["InputWord"];
                    if (!cachedWord.InputWord.Equals(inputWord))
                    {
                        cachedWord.InputWord = inputWord;
                        cachedWord.Anagrams.Add((string) dataReader["Name"]);
                    }
                    else
                    {
                        if (cachedWord.Anagrams.Contains((string)dataReader["Name"]))
                        {
                            continue;
                        }
                        cachedWord.Anagrams.Add((string) dataReader["Name"]);
                    }
                }
            }
            dataReader.Close();
            return cachedWord;
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
            var newSearchInfo = new SearchInfo();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var searchedWordExecTime = (TimeSpan)dataReader["ExecTime"];
                    
                    if (searchInfoList.Count != 0 && searchedWordExecTime.Equals(searchInfoList.Last().ExecTime))
                    {
                        newSearchInfo.Anagrams.Add((string) dataReader["Name"]);
                    }
                    else
                    {
                        newSearchInfo = new SearchInfo
                        {
                            Id = (int)dataReader["Id"],
                            UserIp = (string)dataReader["UserIp"],
                            ExecTime = (TimeSpan)dataReader["ExecTime"],
                            SearchedWord = (string)dataReader["SearchedWord"]
                        };
                        newSearchInfo.Anagrams.Add((string) dataReader["Name"]);
                        searchInfoList.Add(newSearchInfo);
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

    public bool ClearSearchInfoTable(string tableName)
    {
        try
        { 
            _cn.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = _cn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spClearTable";
            command.Parameters.Add(new SqlParameter("@TableName", tableName));
            command.ExecuteNonQuery();
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