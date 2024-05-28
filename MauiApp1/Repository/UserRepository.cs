using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Update;
using SpinningTrainer.Model;
using System.Data;

namespace SpinningTrainer.Repository
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

                                if(fechaV > DateTime.Now || tipoUsuario == 0)
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

        public void Delete(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
