using Configurador_WPF.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using SpinningTrainer.Data;
using SpinningTrainer.View;

namespace SpinningTrainer
{
    public partial class LoginView : ContentPage
    {        
        public LoginView()
        {
            InitializeComponent();            
        }
        
        private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
        {            
            string codUsuaIngresado = codUsuaIngresadoEntry.Text;
            string contraIngresada = contraIngresadaEntry.Text;

            var (inicioExitoso, mensaje, tipoUsuario) = UsersData.ValidarDatosInicioSesion(codUsuaIngresado, contraIngresada);

            if (inicioExitoso == true)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPageView)}");                
            }
            else
            {
                await DisplayAlert("Acceso Denegado",mensaje,"Aceptar");
            }
        }

        private async void tgrRecuperarContra_Tapped(object sender, TappedEventArgs e)
        {            
        }

        private async void tgrRecuperarUsua_Tapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new UserRecoveryView());
        }
    }
}
