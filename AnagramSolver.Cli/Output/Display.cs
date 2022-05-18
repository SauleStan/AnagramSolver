using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Output;

public class Display : IDisplay
{
    // public delegate void Print(string input);
    // private readonly Print _print;
    private readonly Action<string> _print;
    
    // public Display(Print printFunc)
    // {
    //     printFunc("word");
    // }
    // public Display(Print printFunc)
    // {
    //     _print = printFunc;
    // }

    public Display(Action<string> print)
    {
        _print = print;
    }
    public void FormattedPrint(Func<string, string> formatText, string input)
    {
        var formattedInput = formatText(input);
        _print(formattedInput);
    }
}