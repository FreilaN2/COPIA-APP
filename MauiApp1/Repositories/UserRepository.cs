using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public class UserRepository: RepositoryBase, IUserRepository
    {        
        /// <summary>
        /// Valida los datos ingresados en el inicio de sesión
        /// </summary>
        /// <param name="codUsuaIngresado">Código de usuario ingresado</param>
        /// <param name="contraIngresada">Clave ingresada</param>
        /// <returns>Devuelve un bool que dice es true si el inicio fue exitoso, un string como mensaje de error en caso de que lo haya y el tipo de usuario</returns>
        public (bool, string, int) AuthenticateUser(string username, string password)
        {           
            using (SqlConnection connection = OpenConnection())
            {                
                string query = $"SELECT CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', Contra)) AS Contra,\n" +
                                "       TipoUsuario,\n" +
                                "       FechaV\n" +
                                "FROM Usuarios\n" +
                                "WHERE CodUsua = @codUsua\n";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@codUsua", username);

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string contraBaseDatos = reader["Contra"].ToString();
                                int tipoUsuario = int.Parse(reader["TipoUsuario"].ToString());
                                DateTime fechaV = DateTime.Parse(reader["FechaV"].ToString());

                                if(fechaV > DateTime.Now || tipoUsuario == 0 || tipoUsuario == 1)
                                {
                                    if (password == contraBaseDatos) { return (true, "Inicio Exitoso", tipoUsuario); }
                                    else { return (false, "Contraseña incorrecta.", tipoUsuario); }
                                }
                                else { return (false, "Membresía vencida.", tipoUsuario); }
                            }
                            else { return (false, "Usuario invalido.", 0); }
                        }
                    }
                    catch(Exception ex)
                    {
                        return (false, ex.Message,0);
                    }
                }
            }
        }
        
        /// <summary>
        /// Valida si el codigo de usuario existe.
        /// </summary>
        /// <param name="codUsua">Codigo de usuario</param>
        /// <returns>Email del usuario.</returns>
        public string ValidateUsernameforPasswordChange(string codUsua)
        {
            using(SqlConnection connection = RepositoryBase.OpenConnection())
            {
                string query = "SELECT ISNULL(Email,'') FROM Usuarios WHERE CodUsua = @codUsua";

                using (SqlCommand cmd = new SqlCommand(query,connection))
                {                    
                    try
                    {
                        cmd.Parameters.AddWithValue("@codUsua", codUsua);
                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {                        
                        Console.WriteLine(ex.Message);
                        return "";
                    }
                }
            }
        }

        /// <summary>
        /// Valida si el email del usuario existe.
        /// </summary>
        /// <param name="email">Email del usuario</param>
        /// <returns>Devuelve si el email existe o no.</returns>
        public string ValidateUserEmalforUsernameRecovery(string email)
        {
            using (SqlConnection connection = RepositoryBase.OpenConnection())
            {
                string query = "SELECT CodUsua FROM Usuarios WHERE Email = @email";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        return cmd.ExecuteScalar().ToString();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return "";
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza la contraseña del usuario.
        /// </summary>
        /// <param name="username">Código del usuario.</param>
        /// <param name="newPassword">Nueva contraseña.</param>
        /// <returns>Retorna 2 valores, el bool que indica si se actualizo o no exitosamente, y un string con un mensaje de error en caso de lo dé.</returns>
        public  (bool, string) UpdatePassword(string username, string newPassword)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "DECLARE @NewPassworLocal varchar(60) = @newPassword\n" +
                               "UPDATE Usuarios\n" +
                               "SET Contra = ENCRYPTBYPASSPHRASE('12345', @NewPassworLocal)\n" +
                               "WHERE CodUsua = @username";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@newPassword", newPassword);
                        cmd.ExecuteNonQuery();

                        return (true, "");
                    }
                    catch (Exception ex)
                    {
                        return (false, ex.Message);
                    }
                }
            }
        }

        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public void Update(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "DELTE FROM Usuarios WHERE ID=@id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al borrar usuario de la base de datos: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public UserModel GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Este método busca y retorna una lista de todos los usuarios en la base de datos.
        /// </summary>
        /// <returns>IEnumerable con la lista de los usuarios.</returns>
        public ObservableCollection<UserModel> GetAll()
        {
            ObservableCollection<UserModel> users = new ObservableCollection<UserModel>();
            try
            {
                using (SqlConnection connection = OpenConnection())
                {
                    string query = "SELECT ID,\n" +
                                   "       CodUsua,\n" +
                                   "       Descrip,\n" +
                                   "       ISNULL(CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', Contra)),'') AS Contra,\n" +
                                   "       ISNULL(CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', PIN)),'') AS PIN,\n" +
                                   "       ISNULL(Email,'') AS Email,\n" +
                                   "       ISNULL(Telef,'') AS Telef,\n" +
                                   "       FechaC,\n" +
                                   "       FechaR,\n" +
                                   "       FechaV,\n" +
                                   "       TipoUsuario\n" +
                                   "FROM Usuarios\n";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string codUsua = reader.GetString(1);
                                string descrip = reader.GetString(2);
                                string contra = reader.GetString(3);
                                string pin = reader.GetString(4);
                                string email = reader.GetString(5);
                                string telef = reader.GetString(6);
                                DateTime fechaC = reader.GetDateTime(7);
                                DateTime fechaR = reader.GetDateTime(8);
                                DateTime fechaV = reader.GetDateTime(9);
                                int tipoUsuario = reader.GetInt16(10);

                                UserModel userModel = new UserModel()
                                {
                                    Id = id,
                                    CodUsua = codUsua,
                                    Descrip = descrip,
                                    Contra = contra,
                                    PIN = pin,
                                    Email = email,
                                    Telef = telef,
                                    FechaC = fechaC,
                                    FechaR = fechaR,
                                    FechaV = fechaV,
                                    TipoUsuario = tipoUsuario
                                };

                                users.Add(userModel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return users;
        }

        public bool VerifyMembershipValidity(int id)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "SELECT FechaV FROM Usuarios WHERE ID = @id";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    DateTime fechaV = DateTime.Parse(cmd.ExecuteScalar().ToString());
                    if (fechaV > DateTime.Now)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}
