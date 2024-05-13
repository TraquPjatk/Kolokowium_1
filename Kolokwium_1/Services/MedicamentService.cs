﻿using System.Data.SqlClient;

namespace Services
{
    public class MedicamentService : IMedicamentService
    {
        private readonly IConfiguration _configuration;

        public MedicamentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void GetMedicine(int id)
        {
            var medicament = new Medicament();
            var presctiprionsList = new List<Prescription>();

            var connectionString = _configuration.GetConnectionString("Database");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                var sqlString =
                    "SELECT * FROM Medicament WHERE IdMedicament = @id"; 
             //  var sqlString =
               //     "UPDATE Medicament SET Description = 'newDesc' WHERE IdMedicament = 7;";

                using (var command = new SqlCommand(sqlString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }


        /*
        public void AddProduct(ProductWarehouse product)
        {
            var connectionString = _configuration.GetConnectionString("Database");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (!ProductExists(product.IdProduct, connection))
                {
                    throw new ArgumentException("Product with id: " + product.IdProduct + " doesn't exist.");
                }

                if (!WarehouseExists(product.IdWarehouse, connection))
                {
                    throw new ArgumentException("Warehouse with id: " + product.IdWarehouse + " doesn't exist.");
                }

                if (product.Amount <= 0)
                {
                    throw new ArgumentException("Product amount should be greater than 0!");
                }

                if (!OrderExists(product.IdProduct, connection))
                {
                    throw new ArgumentException("There is no order for product with id: " + product.IdProduct);
                }


                var sql =
                    "UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdProduct = @IdProduct AND FulfilledAt IS NULL";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
                    command.Parameters.AddWithValue("@IdProduct", product.IdProduct);

                    command.ExecuteNonQuery();
                }

                var price = CalculatePrice(product.IdProduct, product.Amount, connection);

                sql = "INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, Amount, CreatedAt, IdOrder, Price) " +
                      "VALUES (@IdProduct, @IdWarehouse, @Amount, @CreatedAt, @IdOrder, @Price)";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                    command.Parameters.AddWithValue("@IdWarehouse", product.IdWarehouse);
                    command.Parameters.AddWithValue("@Amount", product.Amount);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@IdOrder", 1);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        */

        private bool ProductExists(int productId, SqlConnection connection)
        {
            var sql = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdProduct", productId);
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        private bool WarehouseExists(int warehouseId, SqlConnection connection)
        {
            var sql = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdWarehouse", warehouseId);
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        private bool OrderExists(int productId, SqlConnection connection)
        {
            var sql = "SELECT COUNT(*) FROM [Order] WHERE IdProduct = @IdProduct";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdProduct", productId);
                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        private decimal CalculatePrice(int productId, int amount, SqlConnection connection)
        {
            var sql = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdProduct", productId);
                var price = (decimal)command.ExecuteScalar();

                return price * amount;
            }
        }
    }
}