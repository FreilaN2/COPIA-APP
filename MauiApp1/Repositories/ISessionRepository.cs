using SpinningTrainer.Models;
using System.Collections.ObjectModel;

namespace SpinningTrainer.Repositories
{
    public interface ISessionRepository
    {
        SessionModel Add(SessionModel session);
        void Update(SessionModel session);
        void Delete(int id);

        ObservableCollection<SessionModel> GetAllByIDEntrenador(int IDEntrenador);
        SessionModel GetByID(int id);

        ObservableCollection<SessionModel> GetSessionsByTitle(string searchTerm);

        ObservableCollection<SessionModel> GetSessionsByCreationDate(DateTime fechaC);  

    }
}
