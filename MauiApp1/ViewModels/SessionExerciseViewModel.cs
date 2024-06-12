using System.Collections.ObjectModel;
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
        private int _posicionManos;
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
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int IDMovimiento
        {
            get => _idMovimiento;
            set
            {
                _idMovimiento = value;
                OnPropertyChanged(nameof(IDMovimiento));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int PosicionManos
        {
            get => _posicionManos;
            set
            {
                _posicionManos = value;
                OnPropertyChanged(nameof(PosicionManos));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public short TipoEjercicio
        {
            get => _tipoEjercicio;
            set
            {
                _tipoEjercicio = value;
                OnPropertyChanged(nameof(TipoEjercicio));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int Fase
        {
            get => _fase;
            set
            {
                _fase = value;
                OnPropertyChanged(nameof(Fase));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int RPMMed
        {
            get => _rpmMed;
            set
            {
                _rpmMed = value;
                OnPropertyChanged(nameof(RPMMed));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int RPMFin
        {
            get => _rpmFin;
            set
            {
                _rpmFin = value;
                OnPropertyChanged(nameof(RPMFin));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public int DuracionSeg
        {
            get => _duracionSeg;
            set
            {
                _duracionSeg = value;
                OnPropertyChanged(nameof(DuracionSeg));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<SessionExerciseModel> SessionExercises { get; }

        public ICommand AddSessionExerciseCommand { get; }

        public SessionExerciseViewModel(int idSession)
        {
            AddSessionExerciseCommand = new ViewModelCommand(ExecuteAddSessionExerciseCommand, CanExecuteAddSessionExerciseCommand);
            IDSesion = idSession;
            _sessionExerciseRepository = new SessionExerciseRepository();
            SessionExercises = new ObservableCollection<SessionExerciseModel>();
            LoadSessionExercises();            
        }

        private bool CanExecuteAddSessionExerciseCommand(object obj)
        {
  
            return IDSesion > 0 &&
                   IDMovimiento > 0 &&
                   PosicionManos > 0 &&
                   TipoEjercicio > 0 &&
                   Fase > 0 &&
                   RPMMed >= 0 &&
                   RPMFin >= 0 &&
                   DuracionSeg > 0;
        }

        private void ExecuteAddSessionExerciseCommand(object obj)
        {
            SessionExerciseModel newSessionExercise = new SessionExerciseModel
            {
                IDSesion = this.IDSesion,
                IDMovimiento = IDMovimiento,
                PosicionManos = PosicionManos,
                TipoEjercicio = TipoEjercicio,
                Fase = Fase,
                RPMMed = RPMMed,
                RPMFin = RPMFin,
                DuracionSeg = DuracionSeg
            };

            SessionExerciseModel addedSessionMovement = _sessionExerciseRepository.Add(newSessionExercise);
        }

        private void LoadSessionExercises()
        {
            if (IDSesion > 0)
            {
                try
                {
                    var exercises = _sessionExerciseRepository.GetAllBySessionID(IDSesion);
                    SessionExercises.Clear();
                    foreach (var exercise in exercises)
                    {
                        SessionExercises.Add(exercise);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }                
            }
        }
    }
}
