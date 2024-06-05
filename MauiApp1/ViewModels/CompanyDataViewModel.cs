using SpinningTrainer.Models;
using SpinningTrainer.Repositories;
using System.Windows.Input;
using Microsoft.Maui.Media;
using Microsoft.Maui.Controls;

namespace SpinningTrainer.ViewModels
{
    class CompanyDataViewModel : ViewModelBase
    {
        private string _rif;
        private string _descrip;
        private string _direc;
        private ImageSource _logo;
        private byte[] _logoBytes;
            
        private bool _editEnable;

        private readonly ICompanyDataRepository _companyDataRepository;

        public CompanyDataViewModel()
        {
            _companyDataRepository = new CompanyDataRepository();

            SaveDataCommand = new Command(async () => await ExecuteSaveDataCommand(), CanExecuteSaveDataCommand);
            SearchImageCommand = new Command(async () => await ExecuteSearchImageCommand());
            EnableEditCommand = new Command(ExecuteEnableEditCommand);
            CancelEditCommand = new Command(ExecuteCancelEditCommand);
        }

        public string Rif
        {
            get => _rif;
            set
            {
                _rif = value;
                OnPropertyChanged(nameof(Rif));
                ((Command)SaveDataCommand).ChangeCanExecute();
            }
        }

        public string Descrip
        {
            get => _descrip;
            set
            {
                _descrip = value;
                OnPropertyChanged(nameof(Descrip));
                ((Command)SaveDataCommand).ChangeCanExecute();
            }
        }

        public string Direc
        {
            get => _direc;
            set
            {
                _direc = value;
                OnPropertyChanged(nameof(Direc));
                ((Command)SaveDataCommand).ChangeCanExecute();
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

        public byte[] LogoBytes
        {
            get => _logoBytes;
            set
            {
                _logoBytes = value;
                OnPropertyChanged(nameof(LogoBytes));
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

        private bool CanExecuteSaveDataCommand()
        {
            return !string.IsNullOrWhiteSpace(Rif) && !string.IsNullOrWhiteSpace(Descrip) && !string.IsNullOrWhiteSpace(Direc);
        }

        private async Task ExecuteSaveDataCommand()
        {
            try
            {
                var companyData = new CompanyDataModel
                {
                    RIF = Rif,
                    Descrip = Descrip,
                    Direc = Direc
                };

                await _companyDataRepository.SaveCompanyDataAsync(companyData, LogoBytes);
                Console.WriteLine("Datos de la empresa guardados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar los datos de la empresa: {ex.Message}");
            }
        }

        private async Task ExecuteSearchImageCommand()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Selecciona una imagen"
                });

                if (result != null)
                {
                    using (var stream = await result.OpenReadAsync())
                    {
                        // Verifica las propiedades del stream
                        if (!stream.CanRead)
                        {
                            throw new Exception("El stream no es legible.");
                        }

                        // Verifica el tamaño del archivo de imagen
                        var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();
                        if (imageBytes.Length > 4000)
                        {
                            throw new Exception("La imagen seleccionada es demasiado grande. Por favor, elige una imagen más pequeña.");
                        }

                        // Si el tamaño es aceptable, procesa la imagen
                        LogoBytes = imageBytes;
                        stream.Position = 0; // Reset the stream position
                        Logo = ImageSource.FromStream(() => new MemoryStream(LogoBytes));
                    }
                }
            }
            catch (Exception ex)
            {
                // Maneja las excepciones
                Console.WriteLine($"Error al seleccionar la foto: {ex.Message}");
            }
        }

        private void ExecuteEnableEditCommand()
        {
            EditEnable = true;
        }

        private void ExecuteCancelEditCommand()
        {
            EditEnable = false;
        }

        private async Task<byte[]> ConvertImageSourceToByteArrayAsync(ImageSource imageSource)
        {
            if (imageSource is StreamImageSource streamImageSource)
            {
                using (var stream = await streamImageSource.Stream(CancellationToken.None))
                    {
                    if (stream == null)
                    {
                        throw new Exception("El stream de la imagen es nulo.");
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        return memoryStream.ToArray();
                    }
                }
            }
            else
            {
                throw new Exception("El origen de la imagen no es un StreamImageSource.");
            }
        }
    }
}
