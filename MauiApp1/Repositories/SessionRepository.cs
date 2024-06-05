using System.Collections.Generic;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        public SessionModel Add(SessionModel session)
        {
            return session;
        }

        public void Delete(string id)
        {
        }

        public IEnumerable<SessionModel> GetAll()
        {
            return new List<SessionModel>();
        }

        public SessionModel GetByIDEntrenador(int IDEntrenador)
        {
            return new SessionModel();
        }

        public SessionModel Update(SessionModel sessionModel)
        {
            return sessionModel;
        }
    }
}
