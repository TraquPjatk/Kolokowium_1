using System.Data.SqlClient;
using Kolokwium_1.Dtos;

namespace Services;

public class MedicamentService : IMedicamentService
{
    private readonly IConfiguration _configuration;

    public MedicamentService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MedicamentDto GetMedicine(int id)
    {
        var medicament = new MedicamentDto();
        var prescriptions = new List<PrescriptionDto>();

        var connectionString = _configuration.GetConnectionString("Database");
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            var sqlString = "SELECT IdMedicament, Name, Description, Type FROM Medicament WHERE IdMedicament = @id";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        medicament.IdMedicament = reader.GetInt32(0);
                        medicament.Name = reader.GetString(1);
                        medicament.Description = reader.GetString(2);
                        medicament.Type = reader.GetString(3);
                    }
                }
            }

            sqlString = @"
                    SELECT p.IdPrescription, p.Date, p.DueDate, p.IdPatient, p.IdDoctor 
                    FROM Prescription p
                    INNER JOIN Prescription_Medicament pm ON p.IdPrescription = pm.IdPrescription
                    WHERE pm.IdMedicament = @id
                    ORDER BY p.Date DESC";
            using (var command = new SqlCommand(sqlString, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var prescription = new PrescriptionDto
                        {
                            IdPrescription = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            DueDate = reader.GetDateTime(2),
                            IdPatient = reader.GetInt32(3),
                            IdDoctor = reader.GetInt32(4)
                        };
                        prescriptions.Add(prescription);
                    }
                }
            }

            medicament.Prescriptions = prescriptions;
            connection.Close();
        }

        return medicament;
    }
}