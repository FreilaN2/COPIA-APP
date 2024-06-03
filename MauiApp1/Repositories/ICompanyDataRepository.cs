using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    interface ICompanyDataRepository
    {
        void SaveCompanyData(CompanyDataModel companyData);
    }
}
