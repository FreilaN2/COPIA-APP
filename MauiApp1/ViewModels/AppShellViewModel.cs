using SpinningTrainer.Views;
using System.Windows.Input;

namespace SpinningTrainer.ViewModels
{
    internal class AppShellViewModel : ViewModelBase
    {
        public ICommand LogoutCommand { get; }

        public AppShellViewModel()
        {
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnLogout()
        {
            // Aquí puedes realizar otras acciones como cerrar sesión antes de la navegación
            await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
        }
    }
}
