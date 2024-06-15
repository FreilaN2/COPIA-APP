using System.Collections.ObjectModel;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using SpinningTrainer.ViewModels;
using SpinningTrainer.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Runtime.CompilerServices;


namespace SpinningTrainer.ViewModel
{
    public class SessionViewModel : ViewModelBase
    {
        private ISessionRepository _sessionRepository;
        private IUserRepository _userRepository;

        private string _descrip;
        private DateTime _selectedCreationDate;
        private DateTime _fechaI;
        private DateTime _timeI;
        private int _duracion;
        private bool _isEditing;
        private int _editingSessionID;

        private string _searchTerm;

        public string searchTerm
        {
            get => _searchTerm;
            set
            {
                _descrip = value;
                OnPropertyChanged(nameof(_searchTerm));
            }
        }

        public string Descrip
        {
            get => _descrip;
            set
            {
                _descrip = value;
                OnPropertyChanged(nameof(Descrip));
                ((Command)AddSessionCommand).ChangeCanExecute();
            }
        }

        public DateTime FechaI
        {
            get => _fechaI;
            set
            {
                _fechaI = value;
                OnPropertyChanged(nameof(FechaI));
                ((Command)AddSessionCommand).ChangeCanExecute();
            }
        }

        public DateTime TimeI
        {
            get => _timeI;
            set
            {
                _timeI = value;
                OnPropertyChanged(nameof(FechaI));
            }
        }

        public int Duracion
        {
            get => _duracion;
            set
            {
                _duracion = value;
                OnPropertyChanged(nameof(Duracion));
                ((Command)AddSessionCommand).ChangeCanExecute();
            }
        }

        public ObservableCollection<SessionModel> Sessions { get; set; }

        // Commands
        public ICommand AddSessionCommand { get; }
        public ICommand GetSessionsByTitleCommand { get; }
        public ICommand GetSessionsByCreationDateCommand { get; }
        public ICommand DeleteSessionCommand { get; }

        public SessionViewModel(bool isEditing, int editingSessionID)
        {
            _sessionRepository = new SessionRepository();
            _userRepository = new UserRepository();

            AddSessionCommand = new Command(ExecuteAddSessionCommand, CanExecuteAddSessionCommand);
            GetSessionsByTitleCommand = new Command(ExecuteGetSessionsByTitleCommand, CanExecuteGetSessionsByTitleCommand);
            GetSessionsByCreationDateCommand = new Command(ExecuteGetSessionsByCreationDateCommand, CanExecuteGetSessionsByCreationDateCommand);
            DeleteSessionCommand = new Command(ExecuteDeleteSessionCommand, CanExecuteDeleteSessionCommand);

            Sessions = new ObservableCollection<SessionModel>();
            
            FechaI = DateTime.Now;
            TimeI = DateTime.Now;
            _isEditing = isEditing;
            _editingSessionID = editingSessionID;
            
        }

        // CanExecute
        private bool CanExecuteAddSessionCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(Descrip) && Duracion > 0;
        }

        private bool CanExecuteGetSessionsByTitleCommand()
        {
            return !string.IsNullOrWhiteSpace(searchTerm);
        }

        private bool CanExecuteGetSessionsByCreationDateCommand()
        {
            return _selectedCreationDate is DateTime;
        }

        private bool CanExecuteDeleteSessionCommand()
        {
            return _editingSessionID > 0;
        }

        // Execute Commands
        private async void ExecuteAddSessionCommand(object obj)
        {
            try 
            {
                var currentUser = _userRepository.GetCurrentUser();
                
                var session = new SessionModel
                {
                    IDEntrenador = currentUser.Id,
                    Descrip = Descrip,
                    FechaC = DateTime.Now,
                    FechaI = new DateTime(
                                FechaI.Year,
                                FechaI.Month,
                                FechaI.Day,
                                TimeI.Hour,
                                TimeI.Minute,
                                TimeI.Second
                                         ),
                    Duracion = Duracion,
                    EsPlantilla = 0,
                };

                if (_isEditing)
                    session.ID = _editingSessionID;

                await Application.Current.MainPage.Navigation.PushAsync(new SessionExercisesListView(session, _isEditing));                

            }
            catch(Exception ex) {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;                
                var toast = Toast.Make("Ocurrió un error al crear la Sesión.", duration, 14);

                await toast.Show(cancellationTokenSource.Token);
            }
        }

        private async void ExecuteGetSessionsByTitleCommand()
        {
            try {

               // Sessions = await _sessionRepository.GetSessionsByTitle(_searchTerm);
            
            }
            catch (Exception ex) {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;
                var toast = Toast.Make("Ocurrió un error al intentar encontrar las Sesiones con ese término.", duration, 14);

                await toast.Show(cancellationTokenSource.Token);

            }
        }

        private async void ExecuteGetSessionsByCreationDateCommand()
        {
            try
            {

                //Sessions = await _sessionRepository.GetSessionsByCreationDate(_selectedCreationDate);

            }
            catch (Exception ex)
            {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;
                var toast = Toast.Make("Ocurrió un error al intentar encontrar las Sesiones con esa fecha de creación.", duration, 14);

                await toast.Show(cancellationTokenSource.Token);

            }
        }

        private async void ExecuteDeleteSessionCommand()
        {
            try {

                _sessionRepository.Delete(_editingSessionID);
            
            }
            catch(Exception ex)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;
                var toast = Toast.Make("Ocurrió un error al intentar eliminar la Sesión especificada.", duration, 14);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }
}
