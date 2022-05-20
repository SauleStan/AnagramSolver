using System.Data;
using System.Data.SqlClient;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Database.Sql.Repositories;

public class WordDbRepository : IWordRepository
{
    private readonly SqlConnection _sqlConnection = new ();
    private readonly List<Word> _words = new ();

    public WordDbRepository()
    {
        _sqlConnection.ConnectionString = "Server=localhost;Database=AnagramDB;Trusted_Connection=True;";
    }
    public async Task<IEnumerable<Word>> GetWordsAsync()
    {
        await _sqlConnection.OpenAsync();
        var command = new SqlCommand();
        command.Connection = _sqlConnection;
        command.CommandType = CommandType.Text;
        command.CommandText = "SELECT Id, Name FROM Word";
        var dataReader = await command.ExecuteReaderAsync();
        if (dataReader.HasRows)
        {
            while (await dataReader.ReadAsync())
            {
                _words.Add(new Word((int)dataReader["Id"], (string)dataReader["Name"]));
            }
        }
        await dataReader.CloseAsync();
        await _sqlConnection.CloseAsync();
        return _words;
    }

    public async Task<bool> AddWordAsync(string word)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Word ([Name]) VALUES (@Word)";
            command.Parameters.AddWithValue("@Word", word);
            await command.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public Task EditWordAsync(string word, string editedWord)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWordAsync(string word)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddWordsAsync(IEnumerable<string> words)
    {
        try
        {
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Word (Name) VALUES (@Word)";
            foreach (var word in words)
            {
                command.Parameters.Clear();
                command.Parameters.Add(new  SqlParameter("@Word", word));
                await command.ExecuteNonQueryAsync();
            }

            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<IEnumerable<Word>> GetFilteredWordsAsync(string filter)
    {
        
        try
        {
            List<Word> filteredWords = new ();
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT Id, Name FROM Word WHERE Name LIKE @Filter";
            command.Parameters.AddWithValue("@Filter", filter);
            var dataReader = await command.ExecuteReaderAsync();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    filteredWords.Add(new Word((int)dataReader["Id"], (string)dataReader["Name"]));
                }
            }
            await dataReader.CloseAsync();
            return filteredWords;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
        
    }

    public async Task CacheWordAsync(string searchWord, IEnumerable<string> anagrams)
    {
        try
        {
            if (_words.Count == 0)
            {
                await GetWordsAsync();
            }
            var anagramModels = _words.FindAll(word => anagrams.Contains(word.Name));
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO CachedWord([InputWord],[AnagramWordId]) VALUES (@SearchedWord, @AnagramId)";
            foreach (var anagramModel in anagramModels)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@SearchedWord", searchWord);
                command.Parameters.AddWithValue("@AnagramId", anagramModel.Id);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<CachedWord> GetCachedWordAsync(string input)
    {
        try
        { 
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT cw.Id, cw.InputWord, w.Name " +
                                  "FROM CachedWord cw " +
                                  "INNER JOIN Word w " +
                                  "ON cw.AnagramWordId = w.Id " +
                                  "WHERE cw.InputWord = @Input";
            command.Parameters.AddWithValue("@Input", input);
            var dataReader = await command.ExecuteReaderAsync();
            var cachedWord = new CachedWord()
            {
                InputWord = string.Empty
            };

            if (dataReader.HasRows)
            {
                while (await dataReader.ReadAsync())
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
            await dataReader.CloseAsync();
            return cachedWord;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
        
    }

    public async Task<IEnumerable<SearchInfo>> GetAnagramSearchInfoAsync()
    {
        try
        {
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT si.Id, si.UserIp, si.ExecTime, si.SearchedWord, w.Name " +
                                  "FROM SearchInfo si " +
                                  "INNER JOIN Word w " +
                                  "ON si.AnagramId = w.Id";
            var dataReader = await command.ExecuteReaderAsync();

            var searchInfoList = new List<SearchInfo>();
            var newSearchInfo = new SearchInfo();
            if (dataReader.HasRows)
            {
                while (await dataReader.ReadAsync())
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
            await dataReader.CloseAsync();
            return searchInfoList;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task AddAnagramSearchInfoAsync(SearchInfo searchInfo)
    {
        try
        {
            if (_words.Count == 0)
            {
                await GetWordsAsync();
            }
            var anagramModels = _words.FindAll(word => searchInfo.Anagrams.Contains(word.Name));
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO SearchInfo ([UserIp],[ExecTime],[SearchedWord],[AnagramId]) " +
                                  "VALUES (@UserIp, @ExecTime, @SearchedWord, @AnagramId)";
            foreach (var anagramModel in anagramModels)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserIp", searchInfo.UserIp);
                command.Parameters.AddWithValue("@ExecTime", searchInfo.ExecTime);
                command.Parameters.AddWithValue("@SearchedWord", searchInfo.SearchedWord);
                command.Parameters.AddWithValue("@AnagramId", anagramModel.Id);
                await command.ExecuteNonQueryAsync();
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }

    public async Task<bool> ClearSearchInfoTableAsync(string tableName)
    {
        try
        { 
            await _sqlConnection.OpenAsync();
            var command = new SqlCommand();
            command.Connection = _sqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spClearTable";
            command.Parameters.Add(new SqlParameter("@TableName", tableName));
            await command.ExecuteNonQueryAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await _sqlConnection.CloseAsync();
        }
    }
}