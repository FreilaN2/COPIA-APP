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
                lblInfoDatoAIngresar.Text = "Correo electrónico";
                entValorRecuperacion.Placeholder = "Correo electrónico de la cuenta";
            }
            else
            {
                lblInfoDatoAIngresar.Text = "Código de usuario";
                entValorRecuperacion.Placeholder = "Código de usuario";                
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
                    DisplayAlert("Datos inválidos", mensajeError, "Aceptar");
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
                DisplayAlert("Datos inválidos","El correo electonico ingresado no es valido.", "Aceptar");
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
                    DisplayAlert("Datos inválidos", mensajeError, "Aceptar");
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
                DisplayAlert("Datos inválidos", "El correo electonico ingresado no es valido.", "Aceptar");
            }
        }
    }

    private async void btnVerificarCodigo_Clicked(object sender, EventArgs e)
    {
        if(entCodigoRecuperacion.Text == CodigoRecuperacion)
        {
            if (TipoRecuperacion == 0)
            {
                EnviaCorreoViewModel.EnviarCorreo(this.Email, "Recuperación de código usuario", CodUsua);
                await DisplayAlert("Proceso terminado!", "Se le ha enviado un correo electrónico en el cual se encuentra su código de usuario.", "Aceptar");
                await Navigation.PopAsync();
            }
        }
        else
        {
            DisplayAlert("Datos inválidos", "El código ingresado no coincide con el enviado.", "Aceptar");
        }
    }
}