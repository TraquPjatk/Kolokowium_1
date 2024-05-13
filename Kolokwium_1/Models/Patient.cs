using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Required] public int IdPatient { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }
    [Required] public DateTime Date { get; set; }

}