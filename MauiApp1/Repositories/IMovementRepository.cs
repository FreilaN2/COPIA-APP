using SpinningTrainer.Models;
using System.Collections.Generic;


namespace SpinningTrainer.Repositories
{
    public interface IMovementRepository
    {
        MovementModel Add(MovementModel movement);
        MovementModel Update(MovementModel movement);
        void Delete(int id);
        MovementModel GetById(int id);
       IEnumerable<MovementModel> GetAll();
    }
}
