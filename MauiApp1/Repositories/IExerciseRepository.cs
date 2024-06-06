using SpinningTrainer.Models;
using System.Collections.Generic;


namespace SpinningTrainer.Repositories
{
    public interface IExerciseRepository
    {
        ExerciseModel Add(ExerciseModel exercise);
        ExerciseModel Update(ExerciseModel exercise);
        void Delete(int id);
        ExerciseModel GetById(int id);
       IEnumerable<ExerciseModel> GetAll();
    }
}
