using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;
using System.Data;

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

        public async Task SaveCompanyDataAsync(CompanyDataModel companyData, byte[] imageBytes)
        {
            try
            {
                string query = "TRUNCATE TABLE DatosEmpresa;\n" +
                                   "INSERT INTO DatosEmpresa(RIF, Descrip, Direc" + (imageBytes != null ? ", Logo" : "") + ") \n" +
                                   "VALUES (@rif, @descrip, @direc" + (imageBytes != null ? ", @logo" : "") + ");\n";

                using (SqlConnection connection = OpenConnection())
                {                    
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@rif", companyData.RIF);
                        cmd.Parameters.AddWithValue("@descrip", companyData.Descrip);
                        cmd.Parameters.AddWithValue("@direc", companyData.Direc);

                        if (imageBytes != null)
                        {
                            cmd.Parameters.Add("@logo", SqlDbType.VarBinary).Value = (object)imageBytes ?? DBNull.Value;
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }            
        }
    }
}
