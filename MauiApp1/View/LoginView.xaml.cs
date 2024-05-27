using SpinningTrainer.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
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

            
        }

        private async void tgrRecuperarDatos_Tapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new RecoveryLoginDataView());
        }
    }
}
