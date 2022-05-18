using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Output;

public class DisplayWithEvents : IDisplay
{
    public event EventHandler DisplayText;

    public void FormattedPrint(Func<string, string> formatText, string input)
    {
        var formattedInput = formatText(input);
        DisplayText?.Invoke(this, EventArgs.Empty);
    }
}