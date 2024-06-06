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
                string query = @"INSERT INTO MovimientosSesion (IDSesion, IDMovimiento, IDPosicionMano, TipoEjercicio, Fase, RPMMed, RPMFin, DuracionSeg)
                                 VALUES (@IDSesion, @IDMovimiento, @IDPosicionMano, @TipoEjercicio, @Fase, @RPMMed, @RPMFin, @DuracionSeg);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionExercise.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionExercise.IDMovimiento);
                command.Parameters.AddWithValue("@IDPosicionMano", sessionExercise.IDPosicionMano);
                command.Parameters.AddWithValue("@TipoEjercicio", sessionExercise.TipoEjercicio);
                command.Parameters.AddWithValue("@Fase", sessionExercise.Fase);
                command.Parameters.AddWithValue("@RPMMed", sessionExercise.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionExercise.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionExercise.DuracionSeg);

                connection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                sessionExercise.ID = id;
            }

            return sessionExercise;
        }

        public SessionExerciseModel Update(SessionExerciseModel sessionExercise)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"UPDATE MovimientosSesion 
                                 SET IDSesion = @IDSesion, IDMovimiento = @IDMovimiento, IDPosicionMano = @IDPosicionMano,
                                     TipoEjercicio = @TipoEjercicio, Fase = @Fase, RPMMed = @RPMMed, RPMFin = @RPMFin, DuracionSeg = @DuracionSeg
                                 WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", sessionExercise.ID);
                command.Parameters.AddWithValue("@IDSesion", sessionExercise.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionExercise.IDMovimiento);
                command.Parameters.AddWithValue("@IDPosicionMano", sessionExercise.IDPosicionMano);
                command.Parameters.AddWithValue("@TipoEjercicio", sessionExercise.TipoEjercicio);
                command.Parameters.AddWithValue("@Fase", sessionExercise.Fase);
                command.Parameters.AddWithValue("@RPMMed", sessionExercise.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionExercise.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionExercise.DuracionSeg);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return sessionExercise;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"DELETE FROM MovimientosSesion WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SessionExerciseModel> GetAll()
        {
            List<SessionExerciseModel> sessionExercises = new List<SessionExerciseModel>();

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM MovimientosSesion";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SessionExerciseModel sessionExercise = new SessionExerciseModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        IDSesion = Convert.ToInt32(reader["IDSesion"]),
                        IDMovimiento = Convert.ToInt32(reader["IDMovimiento"]),
                        IDPosicionMano = Convert.ToInt32(reader["IDPosicionMano"]),
                        TipoEjercicio = Convert.ToInt16(reader["TipoEjercicio"]),
                        Fase = Convert.ToInt32(reader["Fase"]),
                        RPMMed = Convert.ToInt32(reader["RPMMed"]),
                        RPMFin = Convert.ToInt32(reader["RPMFin"]),
                        DuracionSeg = Convert.ToInt32(reader["DuracionSeg"])
                    };

                    sessionExercises.Add(sessionExercise);
                }

                reader.Close();
            }

            return sessionExercises;
        }

        public SessionExerciseModel GetByID(int id)
        {
            SessionExerciseModel sessionExercise = null;

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM MovimientosSesion WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sessionExercise = new SessionExerciseModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDSesion = Convert.ToInt32(reader["IDSesion"]),
                            IDMovimiento = Convert.ToInt32(reader["IDMovimiento"]),
                            IDPosicionMano = Convert.ToInt32(reader["IDPosicionMano"]),
                            TipoEjercicio = Convert.ToInt16(reader["TipoEjercicio"]),
                            Fase = Convert.ToInt32(reader["Fase"]),
                            RPMMed = Convert.ToInt32(reader["RPMMed"]),
                            RPMFin = Convert.ToInt32(reader["RPMFin"]),
                            DuracionSeg = Convert.ToInt32(reader["DuracionSeg"])
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
                string query = @"SELECT * FROM MovimientosSesion WHERE IDSesion = @IDSesion";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionID);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SessionExerciseModel sessionExercise = new SessionExerciseModel
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            IDSesion = Convert.ToInt32(reader["IDSesion"]),
                            IDMovimiento = Convert.ToInt32(reader["IDMovimiento"]),
                            IDPosicionMano = Convert.ToInt32(reader["IDPosicionMano"]),
                            TipoEjercicio = Convert.ToInt16(reader["TipoEjercicio"]),
                            Fase = Convert.ToInt32(reader["Fase"]),
                            RPMMed = Convert.ToInt32(reader["RPMMed"]),
                            RPMFin = Convert.ToInt32(reader["RPMFin"]),
                            DuracionSeg = Convert.ToInt32(reader["DuracionSeg"])
                        };
                        sessionExercises.Add(sessionExercise);
                    }
                }
            }

            return sessionExercises;
        }


    }
}
