namespace AnagramSolver.Contracts.Interfaces;

public interface IWordEditable
{
    Task EditWordAsync(string wordToEdit, string editedWord);
}