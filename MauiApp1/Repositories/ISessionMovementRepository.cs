using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    interface ISessionMovementRepository
    {
        SessionMovementModel Add(SessionMovementModel sessionMovement);
        SessionMovementModel Update(SessionMovementModel sessionMovement);
        void Delete(int id);
        IEnumerable<SessionMovementModel> GetBySessionID(int sessionID);
        IEnumerable<SessionMovementModel> GetAll();
    }
}
