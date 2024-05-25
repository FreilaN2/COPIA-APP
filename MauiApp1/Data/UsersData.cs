using Configurador_WPF.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore.Update;

namespace SpinningTrainer.Data
{
    class UsersData
    {        
        /// <summary>
        /// Valida los datos ingresados en el inicio de sesion  este es un cambio de prueba
        /// </summary>
        /// <param name="codUsuaIngresado">Codigo de usuario ingresado</param>
        /// <param name="contraIngresada">Clave ingresada</param>
        /// <returns>Devuelve un bool que dice es true si el inicio fue exitoso, un string como mensaje de error en caso de que lo haya y el tipo de usuario</returns>
        public static (bool, string, int) ValidarDatosInicioSesion(string codUsuaIngresado, string contraIngresada)
        {           
            using (SqlConnection connection = DataBaseConnection.OpenConnection())
            {                
                string query = $"SELECT CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', Contra)) AS Contra,\n" +
                                "       TipoUsuario,\n" +
                                "       FechaV\n" +
                                "FROM Usuarios\n" +
                                "WHERE CodUsua = @codUsua\n";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@codUsua", codUsuaIngresado);

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
                                    if (contraIngresada == contraBaseDatos) { return (true, "Inicio Exitoso", tipoUsuario); }
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
        public static string ValidaCodigoDeUsuarioParaCambioDeClave(string codUsua)
        {
            using(SqlConnection connection = DataBaseConnection.OpenConnection())
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
        public static string ValidaEmailParaRecuperacionDeUsuario(string email)
        {
            using (SqlConnection connection = DataBaseConnection.OpenConnection())
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
        /// <param name="codUsua">Código del usuario.</param>
        /// <param name="nuevaContra">Nueva contraseña.</param>
        /// <returns>Retorna 2 valores, el bool que indica si se actualizo o no exitosamente, y un string con un mensaje de error en caso de lo dé.</returns>
        public static (bool, string) ActulizaContraUsuario(string codUsua, string nuevaContra)
        {
            using (SqlConnection connection = DataBaseConnection.OpenConnection())
            {
                string query = "UPDATE Usuarios\n" +
                               "SET Contra = ENCRYPTBYPASSPHRASE('12345', @nuevaContra)\n" +
                               "WHERE CodUsua = @codUsua";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@nuevaContra", nuevaContra);

                        return (true, "");
                    }
                    catch (Exception ex)
                    {
                        return (false, ex.Message);
                    }
                }
            }
        }
    }
}
