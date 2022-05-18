namespace AnagramSolver.Cli.Interfaces;

public interface IDisplay
{
    void FormattedPrint(Func<string, string> formatText, string input);
}