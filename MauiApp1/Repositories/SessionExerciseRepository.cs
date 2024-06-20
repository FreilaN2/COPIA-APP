using SpinningTrainer.Models;
using Microsoft.Data.SqlClient;

namespace SpinningTrainer.Repositories
{
    public class SessionExerciseRepository : RepositoryBase, ISessionExerciseRepository
    {

        public SessionExerciseModel Add(SessionExerciseModel sessionExercise)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"INSERT INTO MovimientoSesion (IDSesion, IDMovimiento, ZonaDeEnergia, PosicionManos, RPMMed, RPMFin, DuracionSeg)
                                 VALUES (@IDSesion, @IDMovimiento, @ZonaDeEnergia, @PosicionManos, @RPMMed, @RPMFin, @DuracionSeg);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionExercise.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionExercise.IDMovimiento);
                command.Parameters.AddWithValue("@ZonaDeEnergia", sessionExercise.ZonaDeEnergia);
                command.Parameters.AddWithValue("@PosicionManos", sessionExercise.PosicionManos);
                command.Parameters.AddWithValue("@RPMMed", sessionExercise.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionExercise.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionExercise.DuracionMin);

                int id = Convert.ToInt32(command.ExecuteScalar());
                sessionExercise.ID = id;
            }

            return sessionExercise;
        }

        public SessionExerciseModel Update(SessionExerciseModel sessionExercise)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"UPDATE MovimientoSesion 
                                 SET IDSesion = @IDSesion, IDMovimiento = @IDMovimiento, ZonaDeEnergia = @ZonaDeEnergia,
                                 PosicionManos = @PosicionManos";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", sessionExercise.ID);
                command.Parameters.AddWithValue("@IDSesion", sessionExercise.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionExercise.IDMovimiento);
                command.Parameters.AddWithValue("@ZonaDeEnergia", sessionExercise.ZonaDeEnergia);
                command.Parameters.AddWithValue("@PosicionManos", sessionExercise.PosicionManos);
                command.Parameters.AddWithValue("@RPMMed", sessionExercise.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionExercise.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionExercise.DuracionMin);
                
                command.ExecuteNonQuery();
            }

            return sessionExercise;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"DELETE FROM MovimientoSesion WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                
                command.ExecuteNonQuery();
            }
        }

        public SessionExerciseModel GetByID(int id)
        {
            SessionExerciseModel sessionExercise = null;

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM MovimientoSesion WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sessionExercise = new SessionExerciseModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDSesion = Convert.ToInt32(reader["IDSesion"]),
                            IDMovimiento = Convert.ToInt32(reader["IDMovimiento"]),
                            ZonaDeEnergia = Convert.ToString(reader["ZonaDeEnergia"]),
                            PosicionManos = Convert.ToString(reader["PosicionManos"]),
                            RPMMed = Convert.ToInt32(reader["RPMMed"]),
                            RPMFin = Convert.ToInt32(reader["RPMFin"]),
                            DuracionMin = Convert.ToInt32(reader["DuracionSeg"])
                        };
                    }
                }
            }

            return sessionExercise;
        }
        
        public IEnumerable<SessionExerciseModel> GetAllBySessionID(int sessionID)
        {
            List<SessionExerciseModel> sessionExercises = new List<SessionExerciseModel>();

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM MovimientoSesion WHERE IDSesion = @IDSesion";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionID);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SessionExerciseModel sessionExercise = new SessionExerciseModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDSesion = Convert.ToInt32(reader["IDSesion"]),
                            IDMovimiento = Convert.ToInt32(reader["IDMovimiento"]),
                            ZonaDeEnergia = Convert.ToString(reader["ZonaDeEnergia"]),
                            PosicionManos = Convert.ToString(reader["PosicionManos"]),
                            RPMMed = Convert.ToInt32(reader["RPMMed"]),
                            RPMFin = Convert.ToInt32(reader["RPMFin"]),
                            DuracionMin = Convert.ToInt32(reader["DuracionSeg"])
                        };
                        sessionExercises.Add(sessionExercise);
                    }
                }
            }

            return sessionExercises;
        }


    }
}
