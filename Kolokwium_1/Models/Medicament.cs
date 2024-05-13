using System.ComponentModel.DataAnnotations;

public class Medicament
{
    [Required] public int IdMedicament { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Desription { get; set; }
    [Required] public string Type { get; set; }
}