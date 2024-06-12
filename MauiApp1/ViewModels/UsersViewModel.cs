using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SpinningTrainer.Views;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Threading.Tasks;

namespace SpinningTrainer.ViewModels
{
    public class UsersViewModel : ViewModelBase
    {
        private string _newCodUsua;
        private string _newDescrip;
        private string _newContra;
        private string _newConfirmContra;
        private string _newPIN;
        private string _newEmail;
        private string _newTelef;        
        private DateTime _newFechaR = DateTime.Now;
        private DateTime _newFechaV = DateTime.Now;
        private int _newTipoUsuario = 2;
        private bool _editionEnable;
        private bool _codExists;

        private UserModel _selectedUser;
        
        public ObservableCollection<UserModel> Users { get; set; }
        private readonly IUserRepository _userRepository;

        public string NewCodUsua
        {
            get => _newCodUsua;
            set
            {
                _newCodUsua = value;
                OnPropertyChanged(nameof(NewCodUsua));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
                VerifyCodUsuaExists();
            }
        }
        public string NewDescrip 
        {
            get => _newDescrip;
            set 
            {
                _newDescrip = value;
                OnPropertyChanged(nameof(NewDescrip));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public string NewContra 
        {
            get => _newContra;
            set 
            {
                _newContra = value; 
                OnPropertyChanged(nameof(NewContra));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public string NewConfirmContra
        {
            get => _newConfirmContra;
            set
            {
                _newConfirmContra = value;
                OnPropertyChanged(nameof(NewConfirmContra));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public string NewPIN 
        {
            get => _newPIN;
            set 
            {
                _newPIN = value;
                OnPropertyChanged(nameof(NewPIN));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public string NewEmail 
        {
            get => _newEmail; 
            set 
            {
                _newEmail = value;
                OnPropertyChanged(nameof(NewEmail));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public string NewTelef 
        {
            get => _newTelef;
            set 
            {
                _newTelef = value;
                OnPropertyChanged(nameof(NewTelef));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public DateTime NewFechaR 
        {
            get => _newFechaR; 
            set 
            {
                _newFechaR = value; 
                OnPropertyChanged(nameof(NewFechaR));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }
        public DateTime NewFechaV 
        {
            get => _newFechaV;
            set 
            {
                _newFechaV = value; 
                OnPropertyChanged(nameof(NewFechaV));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            }
        }        
        public int NewTipoUsuario 
        {
            get => _newTipoUsuario; 
            set 
            {
                _newTipoUsuario = value;
                OnPropertyChanged(nameof(NewTipoUsuario));
                ((ViewModelCommand)SaveUserCommand).RaiseCanExecuteChanged();
            } 
        }
        public bool EditionEnable
        {
            get => _editionEnable;
            set
            {
                _editionEnable = value;
                OnPropertyChanged(nameof(EditionEnable));
            }
        }
        public UserModel SelectedUser 
        {
            get => _selectedUser; 
            set 
            {
                _selectedUser = value; 
                OnPropertyChanged(nameof(SelectedUser));
            }
        }
        public bool CodExists 
        {
            get => _codExists; 
            set 
            {
                _codExists = value; 
                OnPropertyChanged(nameof(CodExists));
            }
        }

        public ICommand SaveUserCommand { get; }
        public ICommand IncrementMembershipCommand { get; }
        public ICommand DeleteUserCommand { get; }     
        public ICommand RefreshUserListCommand { get; }

        public UsersViewModel()
        {
            _userRepository = new UserRepository();
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleteUserCommand);
            SaveUserCommand = new ViewModelCommand(ExecuteSaveUserCommand, CanExecuteSaveUserCommand);
            IncrementMembershipCommand = new ViewModelCommand(ExecuteIncrementMembershipCommand);
            RefreshUserListCommand = new ViewModelCommand(ExecuteRefreshUserListCommand);

            LoadUsers();
        }

        private bool CanExecuteSaveUserCommand(object obj)
        {

            if (!string.IsNullOrEmpty(NewCodUsua) && !string.IsNullOrEmpty(NewDescrip) && !string.IsNullOrEmpty(NewContra) && !string.IsNullOrEmpty(NewConfirmContra))
            {
                if (string.IsNullOrEmpty(NewPIN) && NewTipoUsuario == 2)
                    return false;
                else if (CodExists || (NewContra != NewConfirmContra))
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
            
        }

        private async void ExecuteSaveUserCommand(object obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;

            UserModel user = new UserModel()
            {
                Id = SelectedUser.Id, CodUsua = NewCodUsua, Descrip = NewDescrip, Contra = NewContra, Email = NewEmail, PIN = NewPIN, Telef = NewTelef, TipoUsuario = NewTipoUsuario
            };

            bool operationCompleted;

            if (EditionEnable)
                operationCompleted = _userRepository.Update(user);
            else
                operationCompleted = _userRepository.Add(user);

            if (operationCompleted)
            {
                var toast = Toast.Make("Operación completada.", duration, 14);
                await toast.Show(cancellationTokenSource.Token);
                await App.Current.MainPage.Navigation.PopAsync();
            }

            RefreshUserListCommand.Execute(null);
        }

        private async void ExecuteIncrementMembershipCommand(object obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;
            var updateSuccessfully = _userRepository.IncrementMembership(SelectedUser.Id);

            if (updateSuccessfully)
            {                
                var toast = Toast.Make("Incremento exitoso.", duration, 14);
                await toast.Show(cancellationTokenSource.Token);

                NewFechaR = DateTime.Now;
                NewFechaV = NewFechaV.AddMonths(1);
            }
            else
            {
                var toast = Toast.Make("Fallo al incrementar membresia.", duration, 14);
                await toast.Show(cancellationTokenSource.Token);
            }
        }      

        private async void ExecuteDeleteUserCommand(object obj)
        {
            try
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                ToastDuration duration = ToastDuration.Long;

                var memmembershipAvailability = _userRepository.VerifyMembershipValidity(SelectedUser.Id);

                if (memmembershipAvailability)
                {
                    bool confirm = await Application.Current.MainPage.DisplayAlert("Membresía Activa", $"El usuario {SelectedUser.CodUsua} tiene una membresía activa. ¿Deseas eliminar este usuario?", "Sí", "No");
                    if (confirm)
                    {
                        string result = await Application.Current.MainPage.DisplayPromptAsync("Código de Confirmación", "Ingresa el código del usuario a eliminar:");
                        if (result == SelectedUser.CodUsua)
                        {
                            var deleteSuccessfully = _userRepository.Delete(SelectedUser.Id);
                            if (!deleteSuccessfully)
                            {
                                await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo eliminar de la base de datos.", "OK");
                            }
                            else
                            {
                                var toast = Toast.Make("Eliminación exitosa.", duration, 14);
                                await toast.Show(cancellationTokenSource.Token);
                                await Application.Current.MainPage.Navigation.PopAsync();

                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Código de confirmación incorrecto.", "OK");
                        }
                    }
                }
                else
                {
                    bool confirm = await Application.Current.MainPage.DisplayAlert("Eliminar Usuario", $"¿Seguro que desea eliminar al usuario {SelectedUser.CodUsua}?", "Sí", "No");
                    if (confirm)
                    {
                        string result = await Application.Current.MainPage.DisplayPromptAsync("Código de Confirmación", "Ingresa el código del usuario a eliminar:");
                        if (result == SelectedUser.CodUsua)
                        {
                            var deleteSuccessfully = _userRepository.Delete(SelectedUser.Id);
                            if (!deleteSuccessfully)
                            {
                                await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo eliminar de la base de datos.", "OK");
                            }
                            else
                            {
                                var toast = Toast.Make("Eliminación exitosa.", duration, 14);
                                await toast.Show(cancellationTokenSource.Token);
                                await Application.Current.MainPage.Navigation.PopAsync();
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Código de confirmación incorrecto.", "OK");
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo verificar la membresía del usuario: " + ex.Message, "OK");
            }                       
        }

        private void ExecuteRefreshUserListCommand(object obj)
        {
            Users.Clear();
            LoadUsers();
        }

        private void VerifyCodUsuaExists()
        {
            if (EditionEnable && SelectedUser != null && NewCodUsua == SelectedUser.CodUsua)
            {
                CodExists = false;
            }
            else
            {
                CodExists = Users.Any(c => c.CodUsua == NewCodUsua);
            }
        }

        private void LoadUsers()
        {
            Users = _userRepository.GetAll();
        }

        public void Clean()
        {
            NewCodUsua = string.Empty;
            NewDescrip = string.Empty;
            NewTelef = string.Empty;
            NewEmail = string.Empty;
            NewContra = string.Empty;
            NewConfirmContra = string.Empty;
            NewFechaR = DateTime.Now;
            NewFechaV = DateTime.Now.AddMonths(1);
            NewTipoUsuario = 2;
            NewPIN = string.Empty;
            EditionEnable = false;
        }

        public void Edit(UserModel selectedUser)
        {
            NewCodUsua = selectedUser.CodUsua;
            NewDescrip = selectedUser.Descrip;
            NewContra = selectedUser.Contra;
            NewConfirmContra = selectedUser.Contra;
            NewPIN = selectedUser.PIN;
            NewEmail = selectedUser.Email;
            NewTelef = selectedUser.Telef;
            NewFechaR = selectedUser.FechaR;
            NewFechaV = selectedUser.FechaV;
            NewTipoUsuario = selectedUser.TipoUsuario;
            SelectedUser = selectedUser;
            EditionEnable = true;
        }
    }
}
