using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    interface ICompanyDataRepository
    {
        Task SaveCompanyDataAsync(CompanyDataModel companyData, byte[] imageData);
        CompanyDataModel LoadCompanyData();
    }
}
