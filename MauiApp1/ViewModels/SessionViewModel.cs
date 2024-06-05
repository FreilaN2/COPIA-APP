using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using Microsoft.Maui.Controls;

namespace SpinningTrainer.ViewModel
{
    public class SessionViewModel : ViewModelBase
    {
        private ISessionRepository _sessionRepository;

        private string _descrip;
        private DateTime _fechaC;
        private DateTime _fechaI;
        private TimeSpan _duracion;

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

        public DateTime FechaC
        {
            get => _fechaC;
            set
            {
                _fechaC = value;
                OnPropertyChanged(nameof(FechaC));
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

        public TimeSpan Duracion
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

            LoadSessions();
        }

        private void LoadSessions()
        {
            Sessions = new ObservableCollection<SessionModel>(_sessionRepository.GetAll());
        }

        private bool CanExecuteAddSessionCommand()
        {
            return !string.IsNullOrWhiteSpace(Descrip) &&
                   FechaC != default &&
                   FechaI != default &&
                   Duracion != default;
        }

        private void ExecuteAddSessionCommand()
        {
            var newSession = new SessionModel
            {
                IDEntrenador = 1, // Example value
                Descrip = Descrip,
                FechaC = FechaC,
                FechaI = FechaI,
                Duracion = Duracion,
                EsPlantilla = 0 // Example value
            };

            var addedSession = _sessionRepository.Add(newSession);
            Sessions.Add(addedSession);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
