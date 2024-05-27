using SpinningTrainer.Repository;
using SpinningTrainer.Model;
using System.Windows.Input;

namespace SpinningTrainer.ViewModel
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

            if (inicioExitoso == true)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPageView)}");
            }
            else
            {
                ErrorMessage = "* "+mensaje+" *";
            }
        }
    }
}
