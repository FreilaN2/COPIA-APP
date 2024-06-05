using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;

namespace SpinningTrainer.ViewModels
{
    public class MovementViewModel : ViewModelBase
    {
        private readonly IMovementRepository _movementRepository;

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

        public ObservableCollection<MovementModel> Movements { get; }

        public ICommand AddMovementCommand { get; }

        public MovementViewModel()
        {
            _movementRepository = new MovementRepository();
            Movements = new ObservableCollection<MovementModel>();
            AddMovementCommand = new ViewModelCommand(ExecuteAddMovementCommand, CanExecuteAddMovementCommand);
        }

        private bool CanExecuteAddMovementCommand(object obj)
        {
            // Falta validacion
            return true;
        }

        private void ExecuteAddMovementCommand(object obj)
        {
            MovementModel newMovement = new MovementModel
            {
                Descrip = Descrip,
                TipoMov = TipoMov,
                RPMMin = RPMMin,
                RPMMax = RPMMax,
                PosicionesDeManos = PosicionesDeManos
            };

            MovementModel addedMovement = _movementRepository.Add(newMovement);
            Movements.Add(addedMovement);
        }
    }
}
