using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public SessionModel Add(SessionModel session)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"INSERT INTO Sesiones (IDEntrenador, Descrip, FechaC, FechaI, Duracion, EsPlantilla)
                                 VALUES (@IDEntrenador, @Descrip, @FechaC, @FechaI, @Duracion, @EsPlantilla);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDEntrenador", session.IDEntrenador);
                command.Parameters.AddWithValue("@Descrip", session.Descrip);
                command.Parameters.AddWithValue("@FechaC", session.FechaC);
                command.Parameters.AddWithValue("@FechaI", session.FechaI);
                command.Parameters.AddWithValue("@Duracion", session.Duracion);
                command.Parameters.AddWithValue("@EsPlantilla", session.EsPlantilla);


                connection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                session.ID = id;
            }

            return session;
        }

        public void Delete(string id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"DELETE FROM Sesiones WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SessionModel> GetAll()
        {
            return new List<SessionModel>();
        }

        public IEnumerable<SessionModel> GetAllByIDEntrenador(int IDEntrenador)
        {
            List<SessionModel> sessions = new List<SessionModel>();

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM Sesiones WHERE IDEntrenador = @IDEntrenador";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDEntrenador", IDEntrenador);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SessionModel session = new SessionModel
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            IDEntrenador = reader.GetInt32(reader.GetOrdinal("IDEntrenador")),
                            Descrip = reader.GetString(reader.GetOrdinal("Descrip")),
                            FechaC = reader.GetDateTime(reader.GetOrdinal("FechaC")),
                            FechaI = reader.GetDateTime(reader.GetOrdinal("FechaI")),
                            Duracion = reader.GetInt32(reader.GetOrdinal("Duracion")),
                            EsPlantilla = 0//reader.GetBoolean(reader.GetOrdinal("EsPlantilla"))
                        };
                        sessions.Add(session);
                    }
                }
            }

            return sessions;
        }

        public SessionModel GetByID(int id)
        {
            SessionModel session = null;

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT * FROM Sesiones WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        session = new SessionModel
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            IDEntrenador = reader.GetInt32(reader.GetOrdinal("IDEntrenador")),
                            Descrip = reader.GetString(reader.GetOrdinal("Descrip")),
                            FechaC = reader.GetDateTime(reader.GetOrdinal("FechaC")),
                            FechaI = reader.GetDateTime(reader.GetOrdinal("FechaI")),
                            Duracion = reader.GetInt32(reader.GetOrdinal("Duracion")),
                            EsPlantilla = 0//reader.GetBoolean(reader.GetOrdinal("EsPlantilla"))
                        };
                    }
                }
            }

            return session;
        }

        public SessionModel GetByIDEntrenador(int IDEntrenador)
        {
            SessionModel session = null;

            using (SqlConnection connection = OpenConnection())
            {
                string query = @"SELECT TOP 1 * FROM Sesiones WHERE IDEntrenador = @IDEntrenador";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDEntrenador", IDEntrenador);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        session = new SessionModel
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            IDEntrenador = reader.GetInt32(reader.GetOrdinal("IDEntrenador")),
                            Descrip = reader.GetString(reader.GetOrdinal("Descrip")),
                            FechaC = reader.GetDateTime(reader.GetOrdinal("FechaC")),
                            FechaI = reader.GetDateTime(reader.GetOrdinal("FechaI")),
                            Duracion = reader.GetInt32(reader.GetOrdinal("Duracion")),
                            EsPlantilla = 0//reader.GetBoolean(reader.GetOrdinal("EsPlantilla"))
                        };
                    }
                }
            }

            return session;
        }

        public SessionModel Update(SessionModel session)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = @"UPDATE Sesiones 
                         SET IDEntrenador = @IDEntrenador, 
                             Descrip = @Descrip, 
                             FechaC = @FechaC, 
                             FechaI = @FechaI, 
                             Duracion = @Duracion, 
                             EsPlantilla = @EsPlantilla
                         WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", session.ID);
                command.Parameters.AddWithValue("@IDEntrenador", session.IDEntrenador);
                command.Parameters.AddWithValue("@Descrip", session.Descrip);
                command.Parameters.AddWithValue("@FechaC", session.FechaC);
                command.Parameters.AddWithValue("@FechaI", session.FechaI);
                command.Parameters.AddWithValue("@Duracion", session.Duracion);
                command.Parameters.AddWithValue("@EsPlantilla", session.EsPlantilla);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // Handle the case where no rows were updated, if necessary
                    // For example, you could throw an exception or return null
                    return null;
                }
            }

            return session;
        }
    }
}
