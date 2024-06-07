using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public interface ISessionRepository
    {
        SessionModel Add(SessionModel session);
        SessionModel Update(SessionModel session);
        void Delete(int id);

        IEnumerable<SessionModel> GetAllByIDEntrenador(int IDEntrenador);
        SessionModel GetByID(int id);
        
    }
}
