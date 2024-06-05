using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SpinningTrainer.Models;
using System.Configuration;

namespace SpinningTrainer.Repositories
{
    public class SessionMovementRepository : RepositoryBase, ISessionMovementRepository
    {

        public SessionMovementModel Add(SessionMovementModel sessionMovement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO MovimientosSesion (IDSesion, IDMovimiento, IDPosicionMano, TipoEjercicio, Fase, RPMMed, RPMFin, DuracionSeg)
                                 VALUES (@IDSesion, @IDMovimiento, @IDPosicionMano, @TipoEjercicio, @Fase, @RPMMed, @RPMFin, @DuracionSeg);
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionMovement.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionMovement.IDMovimiento);
                command.Parameters.AddWithValue("@IDPosicionMano", sessionMovement.IDPosicionMano);
                command.Parameters.AddWithValue("@TipoEjercicio", sessionMovement.TipoEjercicio);
                command.Parameters.AddWithValue("@Fase", sessionMovement.Fase);
                command.Parameters.AddWithValue("@RPMMed", sessionMovement.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionMovement.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionMovement.DuracionSeg);

                connection.Open();
                int id = Convert.ToInt32(command.ExecuteScalar());
                sessionMovement.ID = id;
            }

            return sessionMovement;
        }

        public SessionMovementModel Update(SessionMovementModel sessionMovement)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE MovimientosSesion 
                                 SET IDSesion = @IDSesion, IDMovimiento = @IDMovimiento, IDPosicionMano = @IDPosicionMano,
                                     TipoEjercicio = @TipoEjercicio, Fase = @Fase, RPMMed = @RPMMed, RPMFin = @RPMFin, DuracionSeg = @DuracionSeg
                                 WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", sessionMovement.ID);
                command.Parameters.AddWithValue("@IDSesion", sessionMovement.IDSesion);
                command.Parameters.AddWithValue("@IDMovimiento", sessionMovement.IDMovimiento);
                command.Parameters.AddWithValue("@IDPosicionMano", sessionMovement.IDPosicionMano);
                command.Parameters.AddWithValue("@TipoEjercicio", sessionMovement.TipoEjercicio);
                command.Parameters.AddWithValue("@Fase", sessionMovement.Fase);
                command.Parameters.AddWithValue("@RPMMed", sessionMovement.RPMMed);
                command.Parameters.AddWithValue("@RPMFin", sessionMovement.RPMFin);
                command.Parameters.AddWithValue("@DuracionSeg", sessionMovement.DuracionSeg);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return sessionMovement;
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM MovimientosSesion WHERE ID = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<SessionMovementModel> GetBySessionID(int sessionID)
        {
            List<SessionMovementModel> sessionMovements = new List<SessionMovementModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM MovimientosSesion WHERE IDSesion = @IDSesion";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IDSesion", sessionID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SessionMovementModel sessionMovement = new SessionMovementModel
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

                    sessionMovements.Add(sessionMovement);
                }

                reader.Close();
            }

            return sessionMovements;
        }

        public IEnumerable<SessionMovementModel> GetAll()
        {
            List<SessionMovementModel> sessionMovements = new List<SessionMovementModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM MovimientosSesion";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SessionMovementModel sessionMovement = new SessionMovementModel
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

                    sessionMovements.Add(sessionMovement);
                }

                reader.Close();
            }

            return sessionMovements;
        }
    }
}
