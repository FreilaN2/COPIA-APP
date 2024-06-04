using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System.Windows.Input;
using Microsoft.Maui.Media;

namespace SpinningTrainer.ViewModels
{
    class CompanyDataViewModel : ViewModelBase
    {
        private string _rif;
        private string _descrip;
        private string _direc;        
        private ImageSource _logo;
        private bool _editEnable = false;        

        private ICompanyDataRepository _companyDataRepository;

        public string Rif 
        {
            get => _rif;
            set 
            {
                _rif = value;
                OnPropertyChanged(nameof(Rif));
                ((ViewModelCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }
        public string Descrip 
        {
            get => _descrip;
            set 
            {
                _descrip = value; 
                OnPropertyChanged(nameof(Descrip));
                ((ViewModelCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }
        public string Direc
        { 
            get => _direc;
            set 
            {
                _direc = value; 
                OnPropertyChanged(nameof(Direc));
                ((ViewModelCommand)SaveDataCommand).RaiseCanExecuteChanged();
            }
        }
        public ImageSource Logo 
        {
            get => _logo; 
            set 
            {
                _logo = value; 
                OnPropertyChanged(nameof(Logo));
            }
        }
        public bool EditEnable
        {
            get => _editEnable;
            set
            {
                _editEnable = value;
                OnPropertyChanged(nameof(EditEnable));
            }
        }

        public ICommand EnableEditCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand SaveDataCommand { get; }
        public ICommand SearchImageCommand { get; }        

        public CompanyDataViewModel()
        {
            _companyDataRepository = new CompanyDataRepository();

            SaveDataCommand = new ViewModelCommand(ExecuteSaveDataCommand, CanExecuteSaveDataCommand);
            SearchImageCommand = new ViewModelCommand(ExecuteSearchImageCommand);
            EnableEditCommand = new ViewModelCommand(ExecuteEnableEditCommand);
            CancelEditCommand = new ViewModelCommand(ExecuteCancelEditCommand);
        }        

        private bool CanExecuteSaveDataCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(Rif) || string.IsNullOrWhiteSpace(Descrip) || string.IsNullOrWhiteSpace(Direc))
                return false;
            else
                return true;
        }

        private void ExecuteSaveDataCommand(object obj)
        {
            CompanyDataModel companyData = new CompanyDataModel();

            companyData.RIF = this.Rif;
            companyData.Descrip = this.Descrip;
            companyData.Direc = this.Direc;
            companyData.Logo = this.Logo;

            _companyDataRepository.SaveCompanyData(companyData);
        }

        private async void ExecuteSearchImageCommand(object obj)
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Selecciona una imagen"
                });

                if (result != null)
                {
                    // Open the file stream and return an ImageSource
                    var stream = await result.OpenReadAsync();
                    Logo = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error picking photo: {ex.Message}");
            }
        }
        
        private void ExecuteEnableEditCommand(object obj)
        {
            EditEnable = true;
        }

        private void ExecuteCancelEditCommand(object obj)
        {
            EditEnable = false;
        }
    }
}
