using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;

namespace SpinningTrainer.ViewModels
{
    public class SessionExerciseViewModel : ViewModelBase
    {
        private  ISessionExerciseRepository _sessionExerciseRepository;

        private int _idSesion;
        private int _idMovimiento;
        private int _idPosicionMano;
        private short _tipoEjercicio;
        private int _fase;
        private int _rpmMed;
        private int _rpmFin;
        private int _duracionSeg;

        public int IDSesion
        {
            get => _idSesion;
            set
            {
                _idSesion = value;
                OnPropertyChanged(nameof(IDSesion));
            }
        }

        public int IDMovimiento
        {
            get => _idMovimiento;
            set
            {
                _idMovimiento = value;
                OnPropertyChanged(nameof(IDMovimiento));
            }
        }

        public int IDPosicionMano
        {
            get => _idPosicionMano;
            set
            {
                _idPosicionMano = value;
                OnPropertyChanged(nameof(IDPosicionMano));
            }
        }

        public short TipoEjercicio
        {
            get => _tipoEjercicio;
            set
            {
                _tipoEjercicio = value;
                OnPropertyChanged(nameof(TipoEjercicio));
            }
        }

        public int Fase
        {
            get => _fase;
            set
            {
                _fase = value;
                OnPropertyChanged(nameof(Fase));
            }
        }

        public int RPMMed
        {
            get => _rpmMed;
            set
            {
                _rpmMed = value;
                OnPropertyChanged(nameof(RPMMed));
            }
        }

        public int RPMFin
        {
            get => _rpmFin;
            set
            {
                _rpmFin = value;
                OnPropertyChanged(nameof(RPMFin));
            }
        }

        public int DuracionSeg
        {
            get => _duracionSeg;
            set
            {
                _duracionSeg = value;
                OnPropertyChanged(nameof(DuracionSeg  ));
            }
        }

        public ObservableCollection<SessionExerciseModel> SessionExercises { get; }

        public ICommand AddSessionExerciseCommand { get; }

        /*public SessionMovementViewModel()
        {
            _sessionMovementRepository = new SessionMovementRepository();
            SessionMovements = new ObservableCollection<SessionMovementModel>();
            AddSessionMovementCommand = new ViewModelCommand(ExecuteAddSessionMovementCommand, CanExecuteAddSessionMovementCommand);
        }*/

        private bool CanExecuteAddSessionExerciseCommand(object obj)
        {
            return true;
        }

        private void ExecuteAddSessionExerciseCommand(object obj)
        {
            SessionExerciseModel newSessionMovement = new SessionExerciseModel
            {
                IDSesion = IDSesion,
                IDMovimiento = IDMovimiento,
                IDPosicionMano = IDPosicionMano,
                TipoEjercicio = TipoEjercicio,
                Fase = Fase,
                RPMMed = RPMMed,
                RPMFin = RPMFin,
                DuracionSeg = DuracionSeg
            };

            //SessionMovementModel addedSessionMovement = _sessionMovementRepository.Add(newSessionMovement);
            //SessionMovements.Add(addedSessionMovement);
        }
    }
}
