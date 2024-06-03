using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpinningTrainer.ViewModels
{
    class CompanyDataViewModel : ViewModelBase
    {
        private string _rif;
        private string _descrip;
        private string _direc;
        private Image _logo;

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
        public Image Logo 
        {
            get => _logo; 
            set 
            {
                _logo = value; 
                OnPropertyChanged(nameof(Logo));
            }
        }

        public ICommand SaveDataCommand;

        public CompanyDataViewModel()
        {
            _companyDataRepository = new CompanyDataRepository();

            SaveDataCommand = new ViewModelCommand(ExecuteSaveDataCommand, CanExecuteSaveDataCommand);
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
    }
}
