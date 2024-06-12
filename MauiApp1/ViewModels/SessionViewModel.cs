using System.Collections.ObjectModel;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using SpinningTrainer.ViewModels;
using SpinningTrainer.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;


namespace SpinningTrainer.ViewModel
{
    public class SessionViewModel : ViewModelBase
    {
        private ISessionRepository _sessionRepository;
        private IUserRepository _userRepository;

        private string _descrip;
        private DateTime _fechaI;
        private DateTime _timeI;
        private int _duracion;

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

        public ICommand AddSessionCommand { get; }        

        public SessionViewModel()
        {
            _sessionRepository = new SessionRepository();
            _userRepository = new UserRepository();
            AddSessionCommand = new Command(ExecuteAddSessionCommand, CanExecuteAddSessionCommand);
            Sessions = new ObservableCollection<SessionModel>();
            FechaI = DateTime.Now;
            TimeI = DateTime.Now;

            //LoadSessions();
        }

        /*private void LoadSessions()
        {
            Sessions = new ObservableCollection<SessionModel>(_sessionRepository.GetAll());
        }*/

        private bool CanExecuteAddSessionCommand(object obj)
        {
            return !string.IsNullOrWhiteSpace(Descrip) && Duracion > 0;
        }

        private async void ExecuteAddSessionCommand(object obj)
        {
            try 
            {
                var currentUser = _userRepository.GetCurrentUser();

                var newSession = new SessionModel
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

                var addedSession = _sessionRepository.Add(newSession);
                Sessions.Add(addedSession);

                if (addedSession != null)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new NewSessionMovementsSelectionView(addedSession.ID));
                }

            }
            catch(Exception ex) {

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;                
                var toast = Toast.Make("Ocurrió un error al crear la sesión", duration, 14);

                await toast.Show(cancellationTokenSource.Token);
            }
        }
    }
}
