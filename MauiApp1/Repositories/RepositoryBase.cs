using Microsoft.Data.SqlClient;

namespace SpinningTrainer.Repositories
{
    public abstract class RepositoryBase
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
            catch (Exception ex)
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
                bool usuario, datoEmpresa, movimiento, sesion, movimientoSesion;

                usuario = ComprobarObjetosBaseDeDatos("Usuario");                
                datoEmpresa = ComprobarObjetosBaseDeDatos("DatosEmpresa");
                movimiento = ComprobarObjetosBaseDeDatos("Movimiento");
                sesion = ComprobarObjetosBaseDeDatos("Sesion");
                movimientoSesion = ComprobarObjetosBaseDeDatos("MovimientoSesion ");


                if (!usuario)
                {
                    CreaObjetosBaseDatos("Usuario");
                }
                if (!datoEmpresa)
                {
                    CreaObjetosBaseDatos("DatosEmpresa");
                }
                if (!movimiento)
                {
                    CreaObjetosBaseDatos("Movimiento");
                }
                if (!sesion)
                {
                    CreaObjetosBaseDatos("Sesion");
                }
                if (!movimientoSesion)
                {
                    CreaObjetosBaseDatos("MovimientoSesion");
                }

                comprobacionExitosa = true;
            }
            catch (Exception ex)
            {                
                comprobacionExitosa = false;
                Console.WriteLine(ex.Message);
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

                if (objeto == "Usuario")
                {
                    query = "CREATE TABLE Usuario (\n" +
                            "   ID INT IDENTITY(0,1) PRIMARY KEY NOT NULL ,\n" +
                            "   CodUsua VARCHAR(20),\n" +
                            "   Descrip VARCHAR(300) NOT NULL,\n" +
                            "   Contra VARBINARY(MAX) NOT NULL,\n" +
                            "   PIN VARBINARY(MAX),\n" +
                            "   Email VARCHAR(120) NOT NULL,\n" +
                            "   Telef VARCHAR(20) NULL,\n" +
                            "   FechaC DATETIME NOT NULL,\n" +
                            "   FechaR DATETIME NOT NULL,\n" +
                            "   FechaV DATETIME NOT NULL,\n" +
                            "   TipoUsuario SMALLINT NOT NULL,\n" +
                            ");\n" +                                                 
                            "INSERT INTO Usuario(CodUsua,Descrip,Contra,PIN,Email,Telef,FechaC,FechaR,FechaV,TipoUsuario)\n" +
                            "VALUES('SU', 'SUPER USUARIO', ENCRYPTBYPASSPHRASE('12345', '123456'),ENCRYPTBYPASSPHRASE('12345', '1234'), '', '', GETDATE(), '', '', 0)\n";
                }
                else if(objeto == "DatosEmpresa")
                {
                    query = "CREATE TABLE DatosEmpresa (\n" +
                            "   Rif VARCHAR(30) PRIMARY KEY NOT NULL,\n" +
                            "   Descrip VARCHAR(80) NOT NULL,\n" +
                            "   Direccion VARCHAR(160),\n" +
                            ");\n";
                }
                else if(objeto == "Movimiento")
                {
                    query = "CREATE TABLE Movimiento (\n" +
                            "   ID INT IDENTITY(0,1) PRIMARY KEY NOT NULL,\n" +
                            "   Descrip VARCHAR(60) NOT NULL,\n" +
                            "   ZonasDeEnergia VARCHAR(160) NOT NULL,\n" +
                            "   RPMMin INT NOT NULL,\n" +
                            "   RPMMax INT NOT NULL,\n" +
                            "   PosicionesDeManos VARCHAR(50) NOT NULL\n" +
                            ");\n";
                }
                else if(objeto == "Sesion")
                {
                    query = "CREATE TABLE Sesion (\n" +
                            "   ID INT IDENTITY(0,1) PRIMARY KEY NOT NULL,\n" +
                            "   IDEntrenador INT NOT NULL,\n" +
                            "   Descrip VARCHAR(120) NOT NULL,\n" +
                            "   FechaC DATETIME NOT NULL,\n" +
                            "   FechaI DATETIME NOT NULL,\n" +
                            "   Duracion INT NOT NULL,\n" +
                            "   EsPlantilla SMALLINT NOT NULL,\n" +
                            "   FOREIGN KEY (IDEntrenador) REFERENCES Usuario(ID) ON DELETE CASCADE -- Relación con la tabla Usuario\n" +
                            ");\n";
                }
                else if(objeto == "MovimientoSesion")
                {
                    query = "CREATE TABLE MovimientoSesion (\n" +
                            "   ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,\n" +
                            "   IDSesion INT NOT NULL,\n" +
                            "   IDMovimiento INT NOT NULL,\n" +
                            "   ZonaDeEnergia VARCHAR(160) NOT NULL,\n" +
                            "   PosicionManos INT NOT NULL,\n" +
                            "   RPMMed INT NOT NULL,\n" +
                            "   RPMFin INT NOT NULL,\n" +
                            "   DuracionMin INT NOT NULL,\n" +
                            "   FOREIGN KEY (IDSesion) REFERENCES Sesion(ID) ON DELETE CASCADE, -- Relación con la tabla Sesiones\n" +
                            "   FOREIGN KEY (IDMovimiento) REFERENCES Movimiento(ID) ON DELETE CASCADE, -- Relación con la tabla Movimientos\n" +
                            ");\n";
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
