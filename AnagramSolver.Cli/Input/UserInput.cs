using AnagramSolver.Cli.Interfaces;

namespace AnagramSolver.Cli.Input;

public class UserInput : IUserInput
{
    private readonly int _minInputLength;
    
    public UserInput(int minInputLength)
    {
        _minInputLength = minInputLength;
    }

    public string GetUserInput()
    {
        string? inputWord;
        var validInput = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine("Your input: ");
            inputWord = Console.ReadLine();
            if (inputWord is null)
            {
                Console.WriteLine("Input cannot be null");
                continue;
            }
            if (inputWord.Length < _minInputLength)
            {
                Console.WriteLine($"Minimal input length: {_minInputLength}");
            }
            else
            {
                validInput = true;
            }
        } while (!validInput);

        return inputWord!;
    }
}