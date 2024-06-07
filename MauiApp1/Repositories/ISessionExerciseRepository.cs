using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpinningTrainer.Models;

namespace SpinningTrainer.Repositories
{
    interface ISessionExerciseRepository
    {
        SessionExerciseModel Add(SessionExerciseModel sessionExercise);
        SessionExerciseModel Update(SessionExerciseModel sessionExercise);
        void Delete(int id);

        IEnumerable<SessionExerciseModel> GetAllBySessionID(int sessionID);
        
        SessionExerciseModel GetByID(int id);
    }
}
