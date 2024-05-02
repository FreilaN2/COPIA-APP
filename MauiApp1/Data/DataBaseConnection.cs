using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Data.SqlClient;

namespace Configurador_WPF.Data
{
    internal class DataBaseConnection
    {
        //Cadena de conexión
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Carga la cadena de conexión del archivo almacenado en el dispositivo
        /// </summary>
        public static void LoadConnectionString()
        {
            try
            {
                string executablePath = AppDomain.CurrentDomain.BaseDirectory; // Obtiene la ruta del ejecutable
                string fileName = "Application.cfg"; //Nombre del archivo donde se almacena la connection string cifrada
                string filePath = Path.Combine(executablePath, fileName);

                string connectionStringEncriptada = File.ReadAllText(filePath);

                CryptographyData cryptography = new CryptographyData();
                ConnectionString = cryptography.Decrypt(connectionStringEncriptada);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Apertura la conexión a la base de datos SQL SERVER.
        /// </summary>
        /// <returns>Conexión aperturada</returns>
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Cierra la conexión a la base de datos SQL SERVER
        /// </summary>
        /// <param name="connection">Conexión a cerrar.</param>
        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Prueba la conexión hacia la base de datos SQL SERVER.
        /// </summary>
        /// <returns>True = conexión es exitosa, false = conexión fallida</returns>
        public static bool TestConnection()
        {
            try
            {
                LoadConnectionString();
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {                
                    connection.Open();
                    connection.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }            
        }

        /// <summary>
        /// Comprueba que los objetos de la base de datos esten creados, caso contrario, los crea
        /// </summary>
        /// <returns>Resultado de la comprobación</returns>
        public static bool CompruebaBaseDatos()
        {
            bool comprobacionExitosa = false;

            try
            {
                bool Usuarios;

                Usuarios = ComprobarObjetosBaseDeDatos("Usuarios");
                

                if (!Usuarios)
                {
                    CreaObjetosBaseDatos("Usuarios");
                }

                comprobacionExitosa = true;
            }
            catch (Exception ex)
            {                
                comprobacionExitosa = false;
            }

            return comprobacionExitosa;
        }

        /// <summary>
        /// Crea objetos en la base de datos
        /// </summary>
        /// <param name="objeto">Nombre del objeto a crear</param>
        private static void CreaObjetosBaseDatos(string objeto)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "";

                if (objeto == "Usuarios")
                {
                    query = "CREATE TABLE Usuarios(\n" +
                            "[ID] [int] IDENTITY(1,1) NOT NULL,\n" +
                            "[CodUsua] [varchar](20) NOT NULL,\n" +
                            "[Descrip] [varchar](300) NOT NULL,\n" +
                            "[Contra] [varbinary](max) NULL,\n" +
                            "[Email] [varchar](120) NULL,\n" +
                            "[Telef] [varchar](20) NULL,\n" +
                            "[FechaC] [datetime] NULL,\n" +
                            "[FechaR] [datetime] NULL,\n" +
                            "[FechaV] [datetime] NULL,\n" +
                            "[EsAdmin] [smallint] NULL,\n" +
                            "CONSTRAINT [PK_Usuarios_1] PRIMARY KEY CLUSTERED\n" +
                            "(\n" +
                            "   [ID] ASC\n" +
                            ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, \tOPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\n" +
                            ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]\n" +
                            "GO\n" +
                            "INSERT INTO Usuarios(CodUsua,Descrip,Contra,Email,Telef,FechaC,FechaR,FechaV,EsAdmin)\n" +
                            "VALUES('SU', 'SUPER USUARIO', ENCRYPTBYPASSPHRASE('12345', '123456'), '', '', GETDATE(), '', '', 1)\n" +
                            "GO\n";
                }
                
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Comprueba si el objeto existe en la base de datos.
        /// </summary>
        /// <param name="nombreObjeto">Nombre del objeto a comprobar</param>
        /// <returns>Booleano de si existe o no el objeto</returns>
        private static bool ComprobarObjetosBaseDeDatos(string nombreObjeto)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string resultado, query;
                bool existe;

                query = "SELECT * FROM  sysobjects WHERE  name = @nombreObjeto";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@nombreObjeto", nombreObjeto);

                    try
                    {
                        resultado = cmd.ExecuteScalar().ToString();
                    }
                    catch (NullReferenceException)
                    {
                        resultado = "";
                    }

                    if (resultado == "")
                    {
                        existe = false;
                    }
                    else
                    {
                        existe = true;
                    }
                    return existe;
                }
            }
        }
    }
}
