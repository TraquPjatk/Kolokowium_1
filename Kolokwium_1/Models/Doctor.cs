using System.ComponentModel.DataAnnotations;

public class Doctor
{
    [Required] public int IdDoctor { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public string Email { get; set; }
}