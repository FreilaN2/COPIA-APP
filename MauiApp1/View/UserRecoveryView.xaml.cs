using SpinningTrainer.Data;
using SpinningTrainer.ViewModel;

namespace SpinningTrainer.View;

public partial class UserRecoveryView : ContentPage
{
    private int TipoRecuperacion { get; set; }
    private string Email { get; set; }
    private string CodigoRecuperacion { get; set; }    
    private string CodUsua { get; set; }

    public UserRecoveryView()
	{
		InitializeComponent();
	}

    private void pkTipoRecuperacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pkTipoRecuperacion.SelectedIndex != -1)
        {
            TipoRecuperacion = pkTipoRecuperacion.SelectedIndex;

            if (TipoRecuperacion == 0)
            {
                lblInfoDatoAIngresar.Text = "Correo electr�nico";
                entValorRecuperacion.Placeholder = "Correo electr�nico de la cuenta";
            }
            else
            {
                lblInfoDatoAIngresar.Text = "C�digo de usuario";
                entValorRecuperacion.Placeholder = "C�digo de usuario";                
            }

            lblInfoDatoAIngresar.IsVisible = true;
            frmValorRecuperacion.IsVisible = true;
            btnVerificarDatos.IsVisible = true;
        }
    }

    private void btnVerificarDatos_Clicked(object sender, EventArgs e)
    {
        if(TipoRecuperacion == 0)
        {
            if (!string.IsNullOrEmpty(UsersData.ValidaEmailParaCambioDeUsuario(entValorRecuperacion.Text)))
            {
                var (numeroAleatorio,mensajeError) = EnviaCorreoViewModel.EnviarCorreoCodigoRecuperacion(entValorRecuperacion.Text);

                if (numeroAleatorio == "0")
                {
                    DisplayAlert("Datos inv�lidos", mensajeError, "Aceptar");
                }
                else
                {
                    CodigoRecuperacion = numeroAleatorio;
                    this.Email = entValorRecuperacion.Text;

                    lblTipoRecuperacion.IsVisible = false;
                    frmTipoRecuperacion.IsVisible = false;
                    lblInfoDatoAIngresar.IsVisible = false;
                    frmValorRecuperacion.IsVisible = false;
                    btnVerificarDatos.IsVisible = false;

                    lblCodigoConfirmacion.IsVisible = true;
                    frmCodigoRecuperacion.IsVisible = true;
                    btnVerificarCodigo.IsVisible = true;
                }
            }
            else
            {
                DisplayAlert("Datos inv�lidos","El correo electonico ingresado no es valido.", "Aceptar");
            }
        }
        else
        {
            string email = UsersData.ValidaCodigoDeUsuarioParaCambioDeClave(entValorRecuperacion.Text);

            if (!string.IsNullOrEmpty(email))
            {
                var (numeroAleatorio, mensajeError) = EnviaCorreoViewModel.EnviarCorreoCodigoRecuperacion(entValorRecuperacion.Text);

                if (numeroAleatorio == "0")
                {
                    DisplayAlert("Datos inv�lidos", mensajeError, "Aceptar");
                }
                else
                {
                    CodigoRecuperacion = numeroAleatorio;
                    this.Email = email;

                    lblTipoRecuperacion.IsVisible = false;
                    frmTipoRecuperacion.IsVisible = false;
                    lblInfoDatoAIngresar.IsVisible = false;
                    frmValorRecuperacion.IsVisible = false;
                    btnVerificarDatos.IsVisible = false;

                    lblCodigoConfirmacion.IsVisible = true;
                    frmCodigoRecuperacion.IsVisible = true;
                    btnVerificarCodigo.IsVisible = true;
                }
            }
            else
            {
                DisplayAlert("Datos inv�lidos", "El correo electonico ingresado no es valido.", "Aceptar");
            }
        }
    }

    private async void btnVerificarCodigo_Clicked(object sender, EventArgs e)
    {
        if(entCodigoRecuperacion.Text == CodigoRecuperacion)
        {
            if (TipoRecuperacion == 0)
            {
                EnviaCorreoViewModel.EnviarCorreo(this.Email, "Recuperaci�n de c�digo usuario", CodUsua);
                await DisplayAlert("Proceso terminado!", "Se le ha enviado un correo electr�nico en el cual se encuentra su c�digo de usuario.", "Aceptar");
                await Navigation.PopAsync();
            }
        }
        else
        {
            DisplayAlert("Datos inv�lidos", "El c�digo ingresado no coincide con el enviado.", "Aceptar");
        }
    }
}