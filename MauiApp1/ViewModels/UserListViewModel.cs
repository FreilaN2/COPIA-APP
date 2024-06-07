using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SpinningTrainer.Views;

namespace SpinningTrainer.ViewModels
{
    internal class UserListViewModel : ViewModelBase
    {
        private UserModel _selectedUser;
        
        public ObservableCollection<UserModel> Users { get; set; }
        private readonly IUserRepository _userRepository;

        public UserModel SelectedUser 
        {
            get => _selectedUser; 
            set 
            {
                _selectedUser = value; 
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ICommand EditUserCommand { get; }
        public ICommand CreateUserCommand { get; }

        public UserListViewModel()
        {
            _userRepository = new UserRepository();           
            EditUserCommand = new ViewModelCommand(ExecuteEditUserCommandAsync);
            CreateUserCommand = new ViewModelCommand(ExecuteCreateUserCommandAsync);
            LoadUsers();
        }

        private async void ExecuteEditUserCommandAsync(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new UserDetailsView(SelectedUser));
        }

        private async void ExecuteCreateUserCommandAsync(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new UserDetailsView());
        }

        /*private async void ExecuteDeleteUserCommand(object obj)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    var memmembershipAvailability = _userRepository.VerifyMembershipValidity

                    if (fechaVencimiento < DateTime.Now)
                    {
                        bool confirm = await DisplayAlert("Eliminar Usuario", "La membresía ha vencido. ¿Deseas eliminar este usuario?", "Sí", "No");
                        if (confirm)
                        {
                            await EliminarUsuario(usuario.ID);
                        }
                    }
                    else
                    {
                        bool confirm = await DisplayAlert("Membresía Activa", "La membresía está activa. ¿Deseas eliminar este usuario?", "Sí", "No");
                        if (confirm)
                        {
                            string result = await DisplayPromptAsync("Código de Confirmación", "Ingresa el código de confirmación:");
                            if (result == "1234")
                            {
                                await EliminarUsuario(usuario.ID);
                            }
                            else
                            {
                                await DisplayAlert("Error", "Código de confirmación incorrecto.", "OK");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    await DisplayAlert("Error", "No se pudo verificar la membresía del usuario: " + ex.Message, "OK");
                }
            }

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;            

            var deleteSuccessfully = _userRepository.Delete(SelectedUser.Id);
            
            if (deleteSuccessfully)
            {
                var toast = Toast.Make("Eliminación exitosa.", duration, 14);
                await toast.Show(cancellationTokenSource.Token);
                LoadUsers();
            }
            else
            {
                var toast = Toast.Make("Fallo al eliminar.", duration, 14);
                await toast.Show(cancellationTokenSource.Token);                
            }
        }*/

        private void LoadUsers()
        {
            Users = _userRepository.GetAll();
        }
    }
}
