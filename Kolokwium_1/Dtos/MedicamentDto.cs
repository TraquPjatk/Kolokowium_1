namespace Kolokwium_1.Dtos;

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; } = new List<PrescriptionDto>();
}