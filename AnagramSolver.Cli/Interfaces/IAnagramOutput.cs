namespace AnagramSolver.Cli.Interfaces;

public interface IAnagramOutput
{ 
    public Task<List<string>> AnagramOutput(string userInput);
}