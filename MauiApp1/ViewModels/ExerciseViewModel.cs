using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;

namespace SpinningTrainer.ViewModels
{
    public class ExerciseViewModel : ViewModelBase
    {
        private readonly IExerciseRepository _exerciseRepository;

        private string _descrip;
        private short _tipoMov;
        private int _rpmMin;
        private int _rpmMax;
        private string _posicionesDeManos;

        public string Descrip
        {
            get => _descrip;
            set
            {
                _descrip = value;
                OnPropertyChanged(nameof(_descrip));
            }
        }

        public short TipoMov
        {
            get => _tipoMov;
            set
            {
                _tipoMov = value;
                OnPropertyChanged(nameof(_tipoMov));
            }
        }

        public int RPMMin
        {
            get => _rpmMin;
            set
            {
                _rpmMin = value;
                OnPropertyChanged(nameof(_rpmMin));
            }
        }

        public int RPMMax
        {
            get => _rpmMax;
            set
            {
                _rpmMax = value;
                OnPropertyChanged(nameof(_rpmMax));
            }
        }

        public string PosicionesDeManos
        {
            get => _posicionesDeManos;
            set
            {
                _posicionesDeManos = value;
                OnPropertyChanged(nameof(_posicionesDeManos));
            }
        }

        public ObservableCollection<ExerciseModel> Exercises { get; }

        public ICommand AddExerciseCommand { get; }

        public ExerciseViewModel()
        {

            _exerciseRepository = new ExerciseRepository();
            Exercises = new ObservableCollection<ExerciseModel>();
            AddExerciseCommand = new ViewModelCommand(ExecuteAddExerciseCommand, CanExecuteAddExerciseCommand);
        }

        private bool CanExecuteAddExerciseCommand(object obj)
        {
            // Falta validacion
            return true;
        }

        private void ExecuteAddExerciseCommand(object obj)
        {
            ExerciseModel newExercise = new ExerciseModel
            {
                Descrip = Descrip,
                TipoMov = TipoMov,
                RPMMin = RPMMin,
                RPMMax = RPMMax,
                PosicionesDeManos = PosicionesDeManos
            };

            ExerciseModel addedExercise = _exerciseRepository.Add(newExercise);
            Exercises.Add(addedExercise);
        }
    }
}
