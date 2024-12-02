using System.ComponentModel.DataAnnotations;

namespace ProbabilityApp.Model;

public class Probability
{
    [Required]
    [Range(0.0, 1.0, ErrorMessage = "{0} must be between 0 and 1")]
    public decimal probabilityA { get; set; }
    [Required]
    [Range(0.0, 1.0, ErrorMessage = "{0} must be between 0 and 1")]
    public decimal probabilityB { get; set; }



}