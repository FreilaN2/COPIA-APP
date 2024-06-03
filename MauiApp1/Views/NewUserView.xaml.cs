namespace SpinningTrainer.Views
{
    public partial class NewUserView : ContentPage
    {    
        public NewUserView()
        {
            InitializeComponent();
        }

        // Boton para guardar los datos en la Base de Datos
        private void Guardar_Clicked(object sender, EventArgs e)
        {
            if (CamposCompletos())
            {
                if (ValidarContrase�as())
                {
                    if (CrearUsuario())
                    {
                        DisplayAlert("�xito", "Datos guardados exitosamente", "Aceptar");
                    }
                    else
                    {
                        DisplayAlert("Error", "No se pudieron guardar los datos", "Aceptar");
                    }
                }
                else
                {
                    DisplayAlert("Error", "Las contrase�as no coinciden", "Aceptar");
                }
            }
            else
            {
                DisplayAlert("Error", "Por favor, complete todos los campos", "Aceptar");
            }
        }

        private bool CamposCompletos()
        {
            return !string.IsNullOrWhiteSpace(UsuarioEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ContrasenaEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ConfirmarContrasenaEntry.Text) &&
                   !string.IsNullOrWhiteSpace(NombreEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ApellidoEntry.Text) &&
                   !string.IsNullOrWhiteSpace(EmailEntry.Text);
        }

        private bool ValidarContrase�as()
        {
            string contrasena = ContrasenaEntry.Text;
            string confirmarContrasena = ConfirmarContrasenaEntry.Text;

            return contrasena == confirmarContrasena;
        }

        private bool CrearUsuario()
        {
            return false;
        }
    }
}