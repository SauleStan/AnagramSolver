using System.ComponentModel.DataAnnotations;

namespace AnagramSolver.WebApp.Models;

public class AnagramModel
{
    [Required]
    public string Input { get; set; }
}