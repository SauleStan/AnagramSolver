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
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = _cn;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = "SELECT Id, Name FROM Words";
        SqlDataReader dataReader = cmd.ExecuteReader();
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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Words ([Name]) VALUES (@Word)";
            cmd.Parameters.AddWithValue("@Word", word);
            cmd.ExecuteNonQuery();
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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Words ([Name]) VALUES (@Word)";
            foreach (var word in words)
            {
                cmd.Parameters.AddWithValue("@Word", word);
                cmd.ExecuteNonQuery();
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
            HashSet<WordModel> filteredWords = new ();
            filter = filter.Insert(0, "%");
            filter += "%";
            _cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Id, Name FROM Words WHERE Name LIKE @Filter";
            cmd.Parameters.AddWithValue("@Filter", filter);
            SqlDataReader dataReader = cmd.ExecuteReader();
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
}