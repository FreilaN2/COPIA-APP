using Configurador_WPF.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Data
{
    class UsersData
    {
        public static (bool, string) ValidarDatosInicioSesion(string codUsuaIngresado, string contraIngresada)
        {           
            using (SqlConnection connection = DataBaseConnection.OpenConnection())
            {
                
                string query = $"SELECT CONVERT(varchar,DECRYPTBYPASSPHRASE('12345', Contra)) AS Contra,\n" +
                                "       EsAdmin,\n" +
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
                                string esAdmin = reader["EsAdmin"].ToString();
                                DateTime fechaV = DateTime.Parse(reader["FechaV"].ToString());

                                if(fechaV > DateTime.Now)
                                {
                                    if (contraIngresada == contraBaseDatos) { return (true, "Inicio Exitoso"); }
                                    else { return (false, "Contraseña incorrecta."); }
                                }
                                else { return (false, "Membresía vencida."); }
                            }
                            else { return (false, "Usuario invalido."); }
                        }
                    }
                    catch(Exception ex)
                    {
                        return (false, ex.Message);
                    }
                }
            }
        }                   
    }
}
