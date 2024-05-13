using System.ComponentModel.DataAnnotations;

public class Prescription_Medicament
{
    [Required] public int IdMedicament { get; set; }
    [Required] public int IdPrescription { get; set; }
    [Required] public int Dose { get; set; }
    [Required] public string Details { get; set; }
}