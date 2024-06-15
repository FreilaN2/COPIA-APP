using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public class ExerciseRepository : RepositoryBase, IExerciseRepository
    {

        public ExerciseModel Add(ExerciseModel exercise)
        {
            using (SqlConnection connection = OpenConnection())

            {
                string query = @"INSERT INTO Movimiento (Descrip, TipoMov, RPMMin, RPMMax, PosicionesDeManos)
                                 VALUES (@Descrip, @TipoMov, @RPMMin, @RPMMax, @PosicionesDeManos);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Descrip", exercise.Descrip);
                command.Parameters.AddWithValue("@TipoMov", exercise.TipoMov);
                command.Parameters.AddWithValue("@RPMMin", exercise.RPMMin);
                command.Parameters.AddWithValue("@RPMMax", exercise.RPMMax);
                command.Parameters.AddWithValue("@PosicionesDeManos", exercise.PosicionesDeManos);
                
                int id = Convert.ToInt32(command.ExecuteScalar());
                exercise.ID = id;
            }

            return exercise;
        }

        public ExerciseModel Update(ExerciseModel exercise)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"UPDATE Movimiento 
                                 SET Descrip = @Descrip, TipoMov = @TipoMov, RPMMin = @RPMMin, RPMMax = @RPMMax, PosicionesDeManos = @PosicionesDeManos
                                 WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", exercise.ID);
                command.Parameters.AddWithValue("@Descrip", exercise.Descrip);
                command.Parameters.AddWithValue("@TipoMov", exercise.TipoMov);
                command.Parameters.AddWithValue("@RPMMin", exercise.RPMMin);
                command.Parameters.AddWithValue("@RPMMax", exercise.RPMMax);
                command.Parameters.AddWithValue("@PosicionesDeManos", exercise.PosicionesDeManos);

                command.ExecuteNonQuery();
            }

            return exercise;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"DELETE FROM Movimiento WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                
                command.ExecuteNonQuery();
            }
        }

        public ExerciseModel GetById(int id)
        {
            ExerciseModel exercise = null;

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM Movimiento WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    exercise = new ExerciseModel
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

            return exercise;
        }

        public IEnumerable<ExerciseModel> GetAll()
        {
            List<ExerciseModel> exercises = new List<ExerciseModel>();

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM Movimiento";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExerciseModel exercise = new ExerciseModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Descrip = Convert.ToString(reader["Descrip"]),
                        TipoMov = Convert.ToInt16(reader["TipoMov"]),
                        RPMMin = Convert.ToInt32(reader["RPMMin"]),
                        RPMMax = Convert.ToInt32(reader["RPMMax"]),
                        PosicionesDeManos = Convert.ToString(reader["PosicionesDeManos"])
                    };

                    exercises.Add(exercise);
                }

                reader.Close();
            }

            return exercises;
        }
    }
}
