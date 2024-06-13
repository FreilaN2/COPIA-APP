using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;

namespace SpinningTrainer.ViewModels
{
    public class SessionExerciseViewModel : ViewModelBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private ISessionExerciseRepository _sessionExerciseRepository;
        private ObservableCollection<ExerciseModel> _exercisesList;
        private ExerciseModel _selectedExercise;

        private ObservableCollection<SessionExerciseModel> _selectedExercisesList;
        private ObservableCollection<string> _handsPositions;

        private SessionModel _session;
        private int _idMovimiento;
        private int _posicionManos;
        private short _tipoEjercicio;
        private int _fase;
        private int _rpmMed;
        private int _rpmFin;
        private int _duracionSeg;
        private string _selectedHandsPosition;

        //private IEnumerable<ExerciseModel> _exercises

        public SessionModel Session
        {
            get => _session;
            set
            {
                _session = value;
                OnPropertyChanged(nameof(Session));                
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

        public ObservableCollection<ExerciseModel> ExercisesList
        {
            get => _exercisesList;
            set
            {
                _exercisesList = value;
                OnPropertyChanged(nameof(ExercisesList));
            }
        }

        public ExerciseModel SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                _selectedExercise = value;
                OnPropertyChanged(nameof(SelectedExercise));
                SelectExercise();
            }
        }

        public ObservableCollection<string> HandsPositions 
        {
            get => _handsPositions;
            set 
            {
                _handsPositions = value; 
                OnPropertyChanged(nameof(HandsPositions));
            }
        }

        public string SelectedHandsPosition 
        {
            get => _selectedHandsPosition; 
            set 
            {
                _selectedHandsPosition = value;
                OnPropertyChanged(nameof(SelectedHandsPosition));
            } 
        }

        public ObservableCollection<SessionExerciseModel> SessionExercises { get; }
        public ICommand AddSessionExerciseCommand { get; }        

        public SessionExerciseViewModel(SessionModel session, bool isEditing)
        {                        
            _sessionExerciseRepository = new SessionExerciseRepository();
            _exerciseRepository = new ExerciseRepository();
            
            SessionExercises = new ObservableCollection<SessionExerciseModel>();            
            ExercisesList = new ObservableCollection<ExerciseModel>();
            HandsPositions = new ObservableCollection<string>();

            LoadExercises();            
            Session = session;
            if (isEditing)
            {
                LoadSessionExercises();
            }

            AddSessionExerciseCommand = new ViewModelCommand(ExecuteAddSessionExerciseCommand, CanExecuteAddSessionExerciseCommand);                        
        }

        private bool CanExecuteAddSessionExerciseCommand(object obj)
        {
            return true;
            //return IDMovimiento > 0 &&
            //       PosicionManos > 0 &&
            //       TipoEjercicio > 0 &&
            //       Fase > 0 &&
            //       RPMMed >= 0 &&
            //       RPMFin >= 0 &&
            //       DuracionSeg > 0;
        }

        private void ExecuteAddSessionExerciseCommand(object obj)
        {
            SessionExerciseModel newSessionExercise = new SessionExerciseModel
            {
                IDSesion = this.Session.ID,
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

        private async void LoadSessionExercises()
        {
            if (Session.ID > 0)
            {
                try
                {
                    var exercises = _sessionExerciseRepository.GetAllBySessionID(Session.ID);
                    SessionExercises.Clear();
                    foreach (var exercise in exercises)
                    {
                        SessionExercises.Add(exercise);
                    }
                }
                catch (Exception ex)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    ToastDuration duration = ToastDuration.Long;
                    var toast = Toast.Make("Ocurrió un error al cargar los ejercicios de la sesión.", duration, 14);

                    await toast.Show(cancellationTokenSource.Token);
                }                
            }
        }

        private void LoadExercises()
        {
            IEnumerable<ExerciseModel> exercisesEnumerable = _exerciseRepository.GetAll();

            foreach (var item in exercisesEnumerable)
            {
                ExercisesList.Add(item);
            }
        }

        private void SelectExercise()
        {
            RPMMed = this.SelectedExercise.RPMMin;
            RPMFin = this.SelectedExercise.RPMMax;

            var arrayHandsPositions = this.SelectedExercise.PosicionesDeManos.Split(",");

            foreach (var item in arrayHandsPositions)
            {
                HandsPositions.Add(item);
            }            
        }

        public void EnableEdit(SessionExerciseModel sessionExercise)
        {
            this.SelectedExercise = _exerciseRepository.GetById(sessionExercise.ID);            
            this.RPMMed = sessionExercise.RPMMed;
            this.RPMFin = sessionExercise.RPMFin;
            this.SelectedHandsPosition = sessionExercise.PosicionManos.ToString();
        }
    }
}
