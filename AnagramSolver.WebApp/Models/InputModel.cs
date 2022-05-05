using System.ComponentModel.DataAnnotations;

namespace AnagramSolver.WebApp.Models;

public class InputModel
{
    [Required]
    public string Input { get; set; }
}