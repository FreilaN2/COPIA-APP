using Microsoft.Maui.Controls;

namespace SpinningTrainerTV
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage(string username)
        {
            InitializeComponent();
            lblNombreUsuario.Text = username;
        }
    }
}
