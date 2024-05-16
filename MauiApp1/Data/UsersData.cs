using Configurador_WPF.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SpinningTrainer.Data
{
    class UsersData
    {
        /*public static ObservableCollection<Usuarios> CargarUsuarios()
        {
            ObservableCollection<Usuarios> infoUsuarios = new ObservableCollection<Usuarios>();

            using(SqlConnection connection = DataBaseConnection.OpenConnection())
            {
                string query = "SELECT Id,\n" +
                               "       CodUsua,\n" +
                               "       Descrip,\n" +
                               "       DECRYPTBYPASSPHRASE('12345',Contra) AS Contra,\n" +
                               "       DECRYPTBYPASSPHRASE('12345',PIN) AS PIN\n" +
                               "       Email,\n" +
                               "       Telef,\n" + 
                               "       FechaC,\n" +
                               "       FechaR,\n" +
                               "       FechaV,\n" +
                               "       TipoUsuario\n" +
                               "FROM Usuarios\n" + 
                               "WHERE TipoUsuario != 0\n";
                using(SqlCommand cmd = new SqlCommand(query,connection))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        While(reader.Read())
                        {
                            int id = int.Parse(reader["Id"].ToString());
                            string codUsua = reader["CodUsua"].ToString();
                            string descrip = reader["Descrip"].ToString();
                            string contra = reader["Contra"].ToString();
                            string pin = reader["PIN"].ToString();
                            string email = reader["Email"].ToString();
                            string telef  = reader["Telef"].ToString();
                            DateTime FechaC = DateTime.parse(reader["FechaC"].ToString());
                            DateTime FechaC = DateTime.parse(reader["FechaC"].ToString());
                            

                            infoUsuarios.Add(new Usuarios 
                            { 
                                Id=1, 
                                CodUsua="Vellito", 
                                Descrip="Diego Estevez", 
                                Contra="LoMaisimo", 
                                PIN="12345678", 
                                Email="diegoestevz73@gmail.com",
                                Telef="0424-5712363",
                                FechaC=DateTime.Now,
                                FechaR=DateTime.Now,
                                FechaV=DateTime.Now,
                                TipoUsuario=1 
                            });
                        }
                    }
                }
            }
        }*/

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

                                if(fechaV > DateTime.Now)
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
    }
}
