using System.ComponentModel.DataAnnotations;

public class Prescription
{
    [Required] public int IdPrescription { get; set; }
    [Required] public DateTime date { get; set; }
    [Required] public DateTime DueDate { get; set; }
    [Required] public int IdPatient { get; set; }
    [Required] public int IdDoctor { get; set; }
}