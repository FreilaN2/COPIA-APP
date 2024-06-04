using Microsoft.Data.SqlClient;
using Microsoft.Maui.Devices.Sensors;
using SpinningTrainer.Models;
using System;

namespace SpinningTrainer.Repositories
{
    class CompanyDataRepository : RepositoryBase, ICompanyDataRepository
    {
        public CompanyDataModel LoadCompanyData()
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "SELECT TOP 1 RIF, Descrip, Direc, Logo FROM DatosEmpresa\n";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        CompanyDataModel companyData = new CompanyDataModel();                        

                        if (reader.Read())
                        {
                            companyData.RIF = reader["RIF"].ToString();
                            companyData.Descrip = reader["Descrip"].ToString();
                            companyData.Direc = reader["Direc"].ToString();
                            byte[] imageData = (byte[])reader["Logo"];

                            companyData.Logo = ImageSource.FromStream(() => new MemoryStream(imageData));
                        }

                        return companyData;
                    }                                        
                }                
            }
        }

        public async void SaveCompanyData(CompanyDataModel companyData)
        {
            using (SqlConnection connection = OpenConnection())
            {
                byte[] imageData;

                // Asegúrate de que el ImageSource es un StreamImageSource y obtiene su Stream de manera segura.
                if (companyData.Logo is StreamImageSource streamImageSource)
                {
                    using (var stream = await streamImageSource.Stream(CancellationToken.None))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await stream.CopyToAsync(ms);
                            imageData = ms.ToArray();
                        }
                    }
                }
                else
                {
                    imageData = new byte[0]; // Maneja el caso donde Logo es null o no es un StreamImageSource
                }

                string query = "TRUNCATE TABLE DatosEmpresa;" +
                               "INSERT INTO DatosEmpresa(RIF, Descrip, Direc, Logo) " +
                               "VALUES (@rif, @descrip, @direc, @logo);";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@rif", companyData.RIF);
                    cmd.Parameters.AddWithValue("@descrip", companyData.Descrip);
                    cmd.Parameters.AddWithValue("@direc", companyData.Direc);
                    cmd.Parameters.AddWithValue("@logo", imageData);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
