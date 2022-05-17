namespace AnagramSolver.Contracts.Models;

public class ActionResult
{
    public bool IsSuccessful { get; init; }
    public string? Error { get; init; }
}