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
        cmd.CommandText = "SELECT * FROM Words";
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                _words.Add(new WordModel((int)dr["Id"], (string)dr["Name"]));
            }
        }
        dr.Close();
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
            cmd.CommandText = "SELECT * FROM Words WHERE Name LIKE @Filter";
            cmd.Parameters.AddWithValue("@Filter", filter);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    filteredWords.Add(new WordModel((int)dr["Id"], (string)dr["Name"]));
                }
            }
            dr.Close();
            
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