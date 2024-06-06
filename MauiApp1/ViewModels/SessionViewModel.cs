using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using SpinningTrainer.ViewModels;
using SpinningTrainer.Views;

namespace SpinningTrainer.ViewModel
{
    public class SessionViewModel : ViewModelBase
    {
        private ISessionRepository _sessionRepository;

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
            AddSessionCommand = new Command(ExecuteAddSessionCommand, CanExecuteAddSessionCommand);
            FechaI = DateTime.Now;
            TimeI = DateTime.Now;

            LoadSessions();
        }

        private void LoadSessions()
        {
            Sessions = new ObservableCollection<SessionModel>(_sessionRepository.GetAll());
        }

        private bool CanExecuteAddSessionCommand()
        {
            return !string.IsNullOrWhiteSpace(Descrip) && Duracion > 0;
        }

        private async void ExecuteAddSessionCommand()
        {
            var newSession = new SessionModel
            {
                IDEntrenador = 1, // Example value
                Descrip = Descrip,                
                FechaI = new DateTime(
                            FechaI.Year,
                            FechaI.Month,
                            FechaI.Day,
                            TimeI.Hour,
                            TimeI.Minute,
                            TimeI.Second
                                     ),
                Duracion = Duracion,
                EsPlantilla = 0 // Example value
            };

            var addedSession = _sessionRepository.Add(newSession);
            Sessions.Add(addedSession);

            await Application.Current.MainPage.Navigation.PushAsync(new NewSessionMovementsSelectionView());
        }
    }
}
