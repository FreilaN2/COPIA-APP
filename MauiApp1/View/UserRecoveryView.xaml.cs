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

            vslDatoRecuperacion.IsVisible = true;
        }
    }

    private void btnVerificarDatos_Clicked(object sender, EventArgs e)
    {
        if(TipoRecuperacion == 0)
        {
            string codUsua = UsersData.ValidaEmailParaRecuperacionDeUsuario(entValorRecuperacion.Text);

            if (!string.IsNullOrEmpty(codUsua))
            {
                this.CodUsua = codUsua;

                var (numeroAleatorio,mensajeError) = EnviaCorreoViewModel.EnviarCorreoCodigoRecuperacion(entValorRecuperacion.Text);

                if (numeroAleatorio == "0")
                {
                    DisplayAlert("Fallo en el env�o del correo", mensajeError, "Aceptar");
                }
                else
                {
                    CodigoRecuperacion = numeroAleatorio;
                    this.Email = entValorRecuperacion.Text;

                    vslTipoRecuperacion.IsVisible = false;
                    vslDatoRecuperacion.IsVisible = false;

                    vslVerificacionCodigo.IsVisible = true;
                }
            }
            else
            {
                DisplayAlert("Datos inv�lidos","El correo electr�nico ingresado no es valido.", "Aceptar");
            }
        }
        else
        {
            string email = UsersData.ValidaCodigoDeUsuarioParaCambioDeClave(entValorRecuperacion.Text);

            if (!string.IsNullOrEmpty(email))
            {
                this.CodUsua = entValorRecuperacion.Text;
                var (numeroAleatorio, mensajeError) = EnviaCorreoViewModel.EnviarCorreoCodigoRecuperacion(email);

                if (numeroAleatorio == "0")
                {
                    DisplayAlert("Datos inv�lidos", mensajeError, "Aceptar");
                }
                else
                {
                    CodigoRecuperacion = numeroAleatorio;
                    this.Email = email;

                    vslTipoRecuperacion.IsVisible = false;
                    vslDatoRecuperacion.IsVisible = false;

                    vslVerificacionCodigo.IsVisible = true;
                }
            }
            else
            {
                DisplayAlert("Datos inv�lidos", "El usuario ingresado no es valido.", "Aceptar");
            }
        }
    }

    private async void btnVerificarCodigo_Clicked(object sender, EventArgs e)
    {
        if(entCodigoRecuperacion.Text == CodigoRecuperacion)
        {
            if (TipoRecuperacion == 0)
            {
                EnviaCorreoViewModel.EnviarCorreo(this.Email, "Recuperaci�n de c�digo usuario", "Su c�digo de usuario es:" + CodUsua);
                await DisplayAlert("Proceso terminado!", "Se le ha enviado un correo electr�nico en el cual se encuentra su c�digo de usuario.", "Aceptar");
                await Navigation.PopAsync();
            }
            else if (TipoRecuperacion == 1)
            {
                vslVerificacionCodigo.IsVisible = false;
                vslNuevaContra.IsVisible = true;
            }
        }
        else
        {
            await DisplayAlert("Datos inv�lidos", "El c�digo ingresado no coincide con el enviado.", "Aceptar");
        }
    }

    private void btnReenviarCodigo_Clicked(object sender, EventArgs e)
    {
        var (numeroAleatorio, mensajeError) = EnviaCorreoViewModel.EnviarCorreoCodigoRecuperacion(Email);

        if (numeroAleatorio == "0")
        {
            DisplayAlert("Fallo en el env�o del correo", mensajeError, "Aceptar");
        }
        else
        {
            CodigoRecuperacion = numeroAleatorio;
            this.Email = entValorRecuperacion.Text;
        }
    }

    private async void btnActualizarContra_Clicked(object sender, EventArgs e)
    {
        if((entNuevaContra.Text == entVerificarNuevaContra.Text) && !(string.IsNullOrEmpty(entNuevaContra.Text) || string.IsNullOrEmpty(entVerificarNuevaContra.Text)))
        {
            var(envioExitoso, mensajeError) = UsersData.ActulizaContraUsuario(CodUsua, entNuevaContra.Text);

            if (envioExitoso)
            {                
                await DisplayAlert("Proceso terminado!", "Su contrase�a se ha actualizado!", "Aceptar");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Falle de Actualizaci�n", mensajeError, "Aceptar");
            }

        }
        else
        {
            await DisplayAlert("Datos Inconsistentes", "Las contrase�a ingresada no coincide con la verificaci�n.", "Aceptar");
        }
    }
}