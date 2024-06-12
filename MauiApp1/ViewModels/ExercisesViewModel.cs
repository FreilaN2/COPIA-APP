using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System.Collections.ObjectModel;

namespace SpinningTrainer.ViewModels
{
    public class ExercisesViewModel : ViewModelBase
    {
        private ObservableCollection<ExerciseModel> _exerciseList;
        private ExerciseModel _exercisesSelected;
        
        public ObservableCollection<ExerciseModel> ExercisesList 
        { 
            get => _exerciseList;
            set 
            {
                _exerciseList = value; 
                OnPropertyChanged(nameof(ExercisesList));
            } 
        }

        public ExerciseModel ExerciseSelected
        {
            get => _exercisesSelected;
            set
            {
                _exercisesSelected = value;
                OnPropertyChanged(nameof(ExerciseSelected));
                SelectExercise?.Invoke(this, ExerciseSelected);
            }
        }

        private readonly IExerciseRepository _exerciseRepository;

        public event EventHandler<ExerciseModel> SelectExercise;

        public ExercisesViewModel()
        {
            _exerciseRepository = new ExerciseRepository();
            ExercisesList = new ObservableCollection<ExerciseModel>();
            LoadExercises();
        }

        private void LoadExercises()
        {
            IEnumerable<ExerciseModel> exercisesEnumerable = _exerciseRepository.GetAll();

            foreach (var item in exercisesEnumerable)
            {
                ExercisesList.Add(item);
            }
        }
    }
}
