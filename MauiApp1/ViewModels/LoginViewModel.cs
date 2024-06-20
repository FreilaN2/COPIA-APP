using SpinningTrainer.Repositories;
using SpinningTrainer.Views;
using System.Windows.Input;

namespace SpinningTrainer.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _loginSuccessful;
        private IUserRepository userRepository;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                ((ViewModelCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((ViewModelCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool InicioExitoso
        {
            get => _loginSuccessful;
            set
            {
                _loginSuccessful = value;
                OnPropertyChanged(nameof(InicioExitoso));
            }
        }

        public ICommand LoginCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            if (String.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                return false;
            else
                return true;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            var (inicioExitoso, mensaje, tipoUsuario) = userRepository.AuthenticateUser(Username, Password);

            if (inicioExitoso)
            {
                var appShell = (AppShell)Application.Current.MainPage;
                appShell.SetUserType(tipoUsuario);

                var currentUser = userRepository.GetByUserName(Username);
                userRepository.SetCurrentUser(currentUser);

                Username = "";
                Password = "";

                // Navegación relativa desde la página actual
                if (tipoUsuario == 0) // Super Usuario
                    await Shell.Current.GoToAsync($"///SuperUserMenuView");
                else if (tipoUsuario == 1) // Administrador
                    await Shell.Current.GoToAsync($"///AdminMenuView");
                else if (tipoUsuario == 2) // Entrenador
                    await Shell.Current.GoToAsync($"///TrainerMenuView");
            }
            else
            {
                ErrorMessage = "* " + mensaje + " *";
            }
        }
    }
}