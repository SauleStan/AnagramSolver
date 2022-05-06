using System.Data;
using System.Data.SqlClient;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Contracts.DataAccess;

public class WordDbAccess : IWordRepository
{
    private readonly SqlConnection _cn = new ();
    private readonly HashSet<string> _words = new ();
    
    public WordDbAccess()
    {
        _cn.ConnectionString = "Server=localhost;Database=AnagramDB;Trusted_Connection=True;";
    }
    public IEnumerable<string> GetWords()
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
                _words.Add((string)dr["Name"]);
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
}