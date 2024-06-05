using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public interface ISessionRepository
    {
        SessionModel Add(SessionModel session);
        SessionModel Update(SessionModel session);
        void Delete(string id);
        SessionModel GetByIDEntrenador(int IDEntrenador);
        IEnumerable<SessionModel> GetAll();
    }
}
