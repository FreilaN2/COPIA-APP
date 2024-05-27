using SpinningTrainer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpinningTrainer.ViewModel
{
    internal class RecoveryLoginDataViewModel : ViewModelBase
    {
        private int _recoveryType;
        private string _email;
        private string _username;
        private string _recoveryCode;
        private string _recoveryData;
        private string _errorMessage;
        private bool _recoveryCodeValidate;

        private IUserRepository userRepository;

        public int RecoveryType 
        { 
            get => _recoveryType; 
            set 
            { 
                _recoveryType = value; 
                OnPropertyChanged(nameof(RecoveryType));
            }
        }

        public string Email 
        { 
            get => _email; 
            set 
            { 
                _email = value; 
                OnPropertyChanged(nameof(Email));
            }
        }

        public string RecoveryCode 
        {
            get => _recoveryCode; 
            set 
            {
                _recoveryCode = value; 
                OnPropertyChanged(nameof(RecoveryCode));
            }
        }

        public string RecoveryData 
        {
            get => _recoveryData; 
            set 
            {
                _recoveryData = value; 
                OnPropertyChanged(nameof(RecoveryData));
            }
        }        

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _recoveryData = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public string Username 
        {
            get => _username; 
            set 
            {
                _username = value; 
                OnPropertyChanged(nameof(Username));
            } 
        }

        public ICommand VerifyRecoveryData;

        public RecoveryLoginDataViewModel()
        {
            VerifyRecoveryData = new ViewModelCommand(ExecuteVerifyRecoveryDataCommand, CanExecuteVerifyRecoveryDataCommand);
        }

        private bool CanExecuteVerifyRecoveryDataCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(RecoveryData))
                return false;
            else
                return true;
        }

        private void ExecuteVerifyRecoveryDataCommand(object obj)
        {            
        }
    }
}
