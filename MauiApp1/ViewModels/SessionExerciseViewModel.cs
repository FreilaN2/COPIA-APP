using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microcharts;
using SkiaSharp;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;

namespace SpinningTrainer.ViewModels
{
    public class SessionExerciseViewModel : ViewModelBase
    {
        private readonly IExerciseRepository _exerciseRepository;
        private ISessionExerciseRepository _sessionExerciseRepository;

        private SessionModel _session;
        private ExerciseModel _selectedExercise;
        private SessionExerciseModel _selectedSessionExercise;
        private ImageSource _selectedHandsPositionImage;

        private ObservableCollection<SessionExerciseModel> _selectedExercisesList;
        private ObservableCollection<ExerciseModel> _exercisesList;
        private ObservableCollection<string> _energyZoneList;
        private ObservableCollection<string> _handsPositions;        

        private int _idMovimiento;
        private int _posicionManos;        
        private int _rpmMed;
        private int _rpmFin;
        private int _duracionMin;
        private short _tipoEjercicio;
        private string _selectedHandsPosition;
        private string _selectedEnergyZone;
        private bool _editionExerciseEnable;

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
        public int DuracionMin
        {
            get => _duracionMin;
            set
            {
                _duracionMin = value;
                OnPropertyChanged(nameof(DuracionMin));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }        
        public ExerciseModel SelectedExercise
        {
            get => _selectedExercise;
            set
            {
                if(_selectedExercise != value) 
                {
                    _selectedExercise = value;
                    OnPropertyChanged(nameof(SelectedExercise));
                    SelectExercise();
                    ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
                }
            }
        }
        public string SelectedHandsPosition 
        {
            get => _selectedHandsPosition; 
            set 
            {
                _selectedHandsPosition = value;
                OnPropertyChanged(nameof(SelectedHandsPosition));
                UpdateSelectedHandsPositionImage();
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            } 
        }
        public string SelectedEnergyZone 
        {
            get => _selectedEnergyZone;
            set 
            {
                _selectedEnergyZone = value;
                OnPropertyChanged(nameof(SelectedEnergyZone));
                ((ViewModelCommand)AddSessionExerciseCommand).RaiseCanExecuteChanged();
            }
        }
        public ImageSource SelectedHandsPositionImage
        {
            get => _selectedHandsPositionImage; 
            set 
            {
                _selectedHandsPositionImage = value;
                OnPropertyChanged(nameof(SelectedHandsPositionImage));
            }
        }
        public ObservableCollection<SessionExerciseModel> SelectedExercisesList
        {
            get => _selectedExercisesList;
            set
            {
                _selectedExercisesList = value;
                OnPropertyChanged(nameof(SelectedExercisesList));
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
        public ObservableCollection<string> EnergyZoneList
        {
            get => _energyZoneList;
            set
            {
                _energyZoneList = value;
                OnPropertyChanged(nameof(EnergyZoneList));
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
        public SessionExerciseModel SelectedSessionExercise 
        { 
            get => _selectedSessionExercise; 
            set 
            {
                _selectedSessionExercise = value;
                OnPropertyChanged(nameof(SelectedSessionExercise));
            }
        }

        public bool EditionExerciseEnable 
        {
            get => _editionExerciseEnable; 
            set 
            {
                _editionExerciseEnable = value; 
                OnPropertyChanged(nameof(EditionExerciseEnable));  
            }
        }

        public ICommand AddSessionExerciseCommand { get; }        
        public ICommand RemoveSessionExerciseCommand { get; }

        public SessionExerciseViewModel(SessionModel session, bool isEditing)
        {                        
            _sessionExerciseRepository = new SessionExerciseRepository();
            _exerciseRepository = new ExerciseRepository();

            SelectedExercisesList = new ObservableCollection<SessionExerciseModel>();
            ExercisesList = new ObservableCollection<ExerciseModel>();
            HandsPositions = new ObservableCollection<string>();
            EnergyZoneList = new ObservableCollection<string>();            

            LoadExercises();            
            Session = session;
            if (isEditing)
            {
                LoadSessionExercises();
            }

            if (EditionExerciseEnable)
            {

            }

            AddSessionExerciseCommand = new ViewModelCommand(ExecuteAddSessionExerciseCommand, CanExecuteAddSessionExerciseCommand);
            RemoveSessionExerciseCommand = new ViewModelCommand(ExecuteRemoveSessionExerciseCommand);
        }
        
        private bool CanExecuteAddSessionExerciseCommand(object obj)
        {
            if (SelectedExercise == null || SelectedHandsPosition == null || DuracionMin == 0 ||
                RPMFin == 0 || RPMMed == 0 || string.IsNullOrEmpty(SelectedEnergyZone))
                return false;
            else
                return true;
        }

        private void ExecuteAddSessionExerciseCommand(object obj)
        {
            SessionExerciseModel newSessionExercise = new SessionExerciseModel
            {
                IDSesion = this.Session.ID,
                IDMovimiento = this.SelectedExercise.ID,
                DescripMov = this.SelectedExercise.Descrip,
                PosicionManos = SelectedHandsPosition,
                ZonaDeEnergia = SelectedEnergyZone,
                RPMMed = RPMMed,
                RPMFin = RPMFin,
                DuracionMin = DuracionMin
            };

            SelectedExercisesList.Add(newSessionExercise);
            ClearSelection();
        }

        private async void ExecuteRemoveSessionExerciseCommand(object obj)
        {
            SelectedExercisesList.Remove(SelectedSessionExercise);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void LoadSessionExercises()
        {
            if (Session.ID > 0)
            {
                try
                {
                    var exercises = _sessionExerciseRepository.GetAllBySessionID(Session.ID);
                    SelectedExercisesList.Clear();
                    foreach (var exercise in exercises)
                    {
                        SelectedExercisesList.Add(exercise);
                    }
                }
                catch (Exception ex)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    ToastDuration duration = ToastDuration.Long;
                    var toast = Toast.Make("Ocurrió un error al cargar los Ejercicios de la Sesión.", duration, 14);

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
            try
            {                
                RPMMed = this.SelectedExercise.RPMMin;
                RPMFin = this.SelectedExercise.RPMMax;

                var arrayHandsPositions = this.SelectedExercise.PosicionesDeManos.Split(",");

                HandsPositions.Clear();
                foreach (var item in arrayHandsPositions)
                {
                    HandsPositions.Add(item);
                }

                var arrayEnergyZone = this.SelectedExercise.ZonasDeEnergia.Split(",");

                EnergyZoneList.Clear();
                foreach (var item in arrayEnergyZone)
                {
                    EnergyZoneList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        private void UpdateSelectedHandsPositionImage()
        {
            if (!string.IsNullOrEmpty(SelectedHandsPosition))
            {
                string resourceID = $"SpinningTrainer.Resources.Images.HandsPositions.hp1.jpeg";
                SelectedHandsPositionImage = ImageSource.FromResource(resourceID, typeof(SessionExerciseViewModel).GetTypeInfo().Assembly);
            }
        }

        public void EnableEdit(SessionExerciseModel sessionExercise)
        {
            var exercise = _exerciseRepository.GetById(sessionExercise.IDMovimiento);

            this.EditionExerciseEnable = true;
            this.SelectedSessionExercise = sessionExercise;
            this.SelectedExercise = ExercisesList.FirstOrDefault(item => item.Descrip == exercise.Descrip);
            this.SelectedHandsPosition = sessionExercise.PosicionManos.ToString();
            this.SelectedEnergyZone = sessionExercise.ZonaDeEnergia;            
            this.RPMMed = sessionExercise.RPMMed;
            this.RPMFin = sessionExercise.RPMFin;
            this.DuracionMin = sessionExercise.DuracionMin;
        }

        public void ClearSelection()
        {            
            this.RPMFin = 0;
            this.RPMMed = 0;            
            this.DuracionMin = 0;
            this.SelectedHandsPosition = null;
            this.SelectedEnergyZone = null;
            this.SelectedExercise = null;
            this.SelectedSessionExercise = null;
            this.EditionExerciseEnable = false;
        }
    }
}
