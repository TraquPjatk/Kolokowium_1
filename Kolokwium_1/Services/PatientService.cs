using System.Data.SqlClient;

namespace Services
{
    public class PatientService : IPatientService
    {
        private readonly IConfiguration _configuration;

        public PatientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DeletePatient(int id)
        {
            var connectionString = _configuration.GetConnectionString("Database");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var sqlString = "DELETE FROM Prescription_Medicament WHERE IdPrescription IN (SELECT IdPrescription FROM Prescription WHERE IdPatient = @id)";
                        using (var command = new SqlCommand(sqlString, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }

                        sqlString = "DELETE FROM Prescription WHERE IdPatient = @id";
                        using (var command = new SqlCommand(sqlString, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }

                        sqlString = "DELETE FROM Patient WHERE IdPatient = @id";
                        using (var command = new SqlCommand(sqlString, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                connection.Close();
            }
        }
    }
}
