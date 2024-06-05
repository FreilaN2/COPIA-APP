using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public class MovementRepository : RepositoryBase, IMovementRepository
    {

        public MovementModel Add(MovementModel movement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Movimientos (Descrip, TipoMov, RPMMin, RPMMax, PosicionesDeManos)
                                 VALUES (@Descrip, @TipoMov, @RPMMin, @RPMMax, @PosicionesDeManos);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Descrip", movement.Descrip);
                command.Parameters.AddWithValue("@TipoMov", movement.TipoMov);
                command.Parameters.AddWithValue("@RPMMin", movement.RPMMin);
                command.Parameters.AddWithValue("@RPMMax", movement.RPMMax);
                command.Parameters.AddWithValue("@PosicionesDeManos", movement.PosicionesDeManos);

                connection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                movement.ID = id;
            }

            return movement;
        }

        public MovementModel Update(MovementModel movement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE Movimientos 
                                 SET Descrip = @Descrip, TipoMov = @TipoMov, RPMMin = @RPMMin, RPMMax = @RPMMax, PosicionesDeManos = @PosicionesDeManos
                                 WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", movement.ID);
                command.Parameters.AddWithValue("@Descrip", movement.Descrip);
                command.Parameters.AddWithValue("@TipoMov", movement.TipoMov);
                command.Parameters.AddWithValue("@RPMMin", movement.RPMMin);
                command.Parameters.AddWithValue("@RPMMax", movement.RPMMax);
                command.Parameters.AddWithValue("@PosicionesDeManos", movement.PosicionesDeManos);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return movement;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM Movimientos WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public MovementModel GetById(int id)
        {
            MovementModel movement = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Movimientos WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    movement = new MovementModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Descrip = Convert.ToString(reader["Descrip"]),
                        TipoMov = Convert.ToInt16(reader["TipoMov"]),
                        RPMMin = Convert.ToInt32(reader["RPMMin"]),
                        RPMMax = Convert.ToInt32(reader["RPMMax"]),
                        PosicionesDeManos = Convert.ToString(reader["PosicionesDeManos"])
                    };
                }

                reader.Close();
            }

            return movement;
        }

        public IEnumerable<MovementModel> GetAll()
        {
            List<MovementModel> movimientos = new List<MovementModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM Movimientos";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    MovementModel movimiento = new MovementModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Descrip = Convert.ToString(reader["Descrip"]),
                        TipoMov = Convert.ToInt16(reader["TipoMov"]),
                        RPMMin = Convert.ToInt32(reader["RPMMin"]),
                        RPMMax = Convert.ToInt32(reader["RPMMax"]),
                        PosicionesDeManos = Convert.ToString(reader["PosicionesDeManos"])
                    };

                    movimientos.Add(movimiento);
                }

                reader.Close();
            }

            return movimientos;
        }
    }
}
