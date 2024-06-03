using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    class CompanyDataRepository : RepositoryBase, ICompanyDataRepository
    {
        public void SaveCompanyData(CompanyDataModel companyData)
        {
            using (SqlConnection connection = OpenConnection())
            {
                string query = "TRUNCATE TABLE DatosEmpresa\n" +
                               "INSERT INTO DatosEmpresa(RIF, Descrip, Direc, Logo)\n" +
                               "VALUES (@rif, @descrip, @direc, @logo)\n";
                
                using (SqlCommand cmd = new SqlCommand(query,connection))
                {
                    cmd.Parameters.AddWithValue("@rif", companyData.Rif);
                    cmd.Parameters.AddWithValue("@descrip", companyData.Descrip);
                    cmd.Parameters.AddWithValue("@direc", companyData.Direc);
                    cmd.Parameters.AddWithValue("@logo", companyData.Logo);

                    cmd.ExecuteNonQuery();
                }                
            }            
        }
    }
}
