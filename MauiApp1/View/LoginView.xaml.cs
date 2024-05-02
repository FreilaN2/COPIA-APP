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

            var (inicioExitoso, mensaje) = UsersData.ValidarDatosInicioSesion(codUsuaIngresado, contraIngresada);

            if (inicioExitoso == true)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPageView)}");

                //                await Navigation.PushAsync(new AppShell());
            }
            else
            {
                await DisplayAlert("Acceso Denegado",mensaje,"Aceptar");
            }
        }
    }
}
