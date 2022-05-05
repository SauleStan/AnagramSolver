using System.ComponentModel.DataAnnotations;

namespace AnagramSolver.WebApp.Models;

public class CreateWordModel
{
    [Required]
    public string Word { get; set; }
}