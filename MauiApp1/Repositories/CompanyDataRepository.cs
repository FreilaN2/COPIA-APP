using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    class CompanyDataRepository : RepositoryBase, ICompanyDataRepository
    {
        public CompanyDataModel LoadCompanyData()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "SELECT TOP 1 RIF, Descrip, Direc, Logo FROM DatosEmpresa";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        CompanyDataModel companyData = new CompanyDataModel();

                        if (reader.Read())
                        {
                            companyData.RIF = reader["RIF"].ToString();
                            companyData.Descrip = reader["Descrip"].ToString();
                            companyData.Direc = reader["Direc"].ToString();

                            if (reader["Logo"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])reader["Logo"];
                                companyData.Logo = ImageSource.FromStream(() => new MemoryStream(imageData));
                            }
                        }

                        return companyData;
                    }
                }
            }
        }

        public async Task SaveCompanyDataAsync(CompanyDataModel companyData, byte[] imageData)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "TRUNCATE TABLE DatosEmpresa;\n" +
                               "INSERT INTO DatosEmpresa(RIF, Descrip, Direc" + (imageData != null ? ", Logo" : "") + ") \n" +
                               "VALUES (@rif, @descrip, @direc" + (imageData != null ? ", @logo" : "") + ");\n";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandTimeout = 0; // Aumenta el timeout del comando

                    cmd.Parameters.AddWithValue("@rif", companyData.RIF);
                    cmd.Parameters.AddWithValue("@descrip", companyData.Descrip);
                    cmd.Parameters.AddWithValue("@direc", companyData.Direc);

                    if (imageData != null)
                    {
                        cmd.Parameters.AddWithValue("@logo", imageData);
                    }

                    int retryCount = 3;
                    for (int i = 0; i < retryCount; i++)
                    {
                        try
                        {
                            await cmd.ExecuteNonQueryAsync();
                            break; // Salir del bucle si la ejecución es exitosa
                        }
                        catch (SqlException ex) when (ex.InnerException is IOException)
                        {
                            Console.WriteLine($"Error de red: {ex.Message}. Reintentando ({i + 1}/{retryCount})...");
                            if (i == retryCount - 1)
                            {
                                throw; // Re-lanzar la excepción si se ha alcanzado el número máximo de reintentos
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al ejecutar el comando SQL: {ex.Message}");
                            if (ex.InnerException != null)
                            {
                                Console.WriteLine($"Excepción interna: {ex.InnerException.Message}");
                            }
                            throw;
                        }
                    }
                }
            }
        }
    }
}
