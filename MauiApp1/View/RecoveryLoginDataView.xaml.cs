using SpinningTrainer.Repository;

namespace SpinningTrainer.View;

public partial class RecoveryLoginDataView : ContentPage
{
    private int recoveryType { get; set; }
    private string Email { get; set; }
    private string recoveryCode { get; set; }    
    private string username { get; set; }

    public RecoveryLoginDataView()
	{
		InitializeComponent();
	}

    private void pkTipoRecuperacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pkRecoveryType.SelectedIndex != -1)
        {
            recoveryType = pkRecoveryType.SelectedIndex;

            if (recoveryType == 0)
            {
                lblInfoRecoveryData.Text = "Correo electrónico";
                entRecoveryValue.Placeholder = "Correo electrónico de la cuenta";
            }
            else
            {
                lblInfoRecoveryData.Text = "Código de usuario";
                entRecoveryValue.Placeholder = "Código de usuario";                
            }

            vslRecoveryData.IsVisible = true;
        }
    }

    private void btnVerificarDatos_Clicked(object sender, EventArgs e)
    {
        //if(recoveryType == 0)
        //{
        //    string codUsua = UserRepository.ValidaEmailParaRecuperacionDeUsuario(entValorRecuperacion.Text);

        //    if (!string.IsNullOrEmpty(codUsua))
        //    {
        //        this.username = codUsua;

        //        MailViewModel mail = new MailViewModel();

        //        var (numeroAleatorio,mensajeError) = mail.SendMailRecoveryCode(entValorRecuperacion.Text);

        //        if (numeroAleatorio == "0")
        //        {
        //            DisplayAlert("Fallo en el envío del correo", mensajeError, "Aceptar");
        //        }
        //        else
        //        {
        //            recoveryCode = numeroAleatorio;
        //            this.Email = entValorRecuperacion.Text;

        //            vslTipoRecuperacion.IsVisible = false;
        //            vslDatoRecuperacion.IsVisible = false;

        //            vslVerificacionCodigo.IsVisible = true;
        //        }
        //    }
        //    else
        //    {
        //        DisplayAlert("Datos inválidos","El correo electrónico ingresado no es valido.", "Aceptar");
        //    }
        //}
        //else
        //{
        //    string email = UserRepository.ValidaCodigoDeUsuarioParaCambioDeClave(entValorRecuperacion.Text);

        //    if (!string.IsNullOrEmpty(email))
        //    {
        //        this.username = entValorRecuperacion.Text;

        //        MailViewModel mail = new MailViewModel();
        //        var (randomNumber, errorMessage) = mail.SendMailRecoveryCode(email);

        //        if (randomNumber == "0")
        //        {
        //            DisplayAlert("Datos inválidos", errorMessage, "Aceptar");
        //        }
        //        else
        //        {
        //            recoveryCode = randomNumber;
        //            this.Email = email;

        //            vslTipoRecuperacion.IsVisible = false;
        //            vslDatoRecuperacion.IsVisible = false;

        //            vslVerificacionCodigo.IsVisible = true;
        //        }
        //    }
        //    else
        //    {
        //        DisplayAlert("Datos inválidos", "El usuario ingresado no es valido.", "Aceptar");
        //    }
        //}
    }

    private async void btnVerificarCodigo_Clicked(object sender, EventArgs e)
    {
        //if(entConfirmationCode.Text == recoveryCode)
        //{
        //    if (recoveryType == 0)
        //    {
        //        MailViewModel mail = new MailViewModel();
        //        mail.SendMail(this.Email, "Recuperación de código usuario", "Su código de usuario es:" + username);
        //        await DisplayAlert("Proceso terminado!", "Se le ha enviado un correo electrónico en el cual se encuentra su código de usuario.", "Aceptar");
        //        await Navigation.PopAsync();
        //    }
        //    else if (recoveryType == 1)
        //    {
        //        vslVerifyCode.IsVisible = false;
        //        vslNewPassword.IsVisible = true;
        //    }
        //}
        //else
        //{
        //    await DisplayAlert("Datos inválidos", "El código ingresado no coincide con el enviado.", "Aceptar");
        //}
    }

    private void btnReenviarCodigo_Clicked(object sender, EventArgs e)
    {
        //MailViewModel mail = new MailViewModel();

        //var (randomNumber, errorMessage) = mail.SendMailRecoveryCode(entRecoveryValue.Text);

        //if (randomNumber == "0")
        //{
        //    DisplayAlert("Fallo en el envío del correo", errorMessage, "Aceptar");
        //}
        //else
        //{
        //    recoveryCode = randomNumber;
        //    this.Email = entRecoveryValue.Text;
        //}
    }

    private async void btnActualizarContra_Clicked(object sender, EventArgs e)
    {
        //if((entNuevaContra.Text == entVerificarNuevaContra.Text) && !(string.IsNullOrEmpty(entNuevaContra.Text) || string.IsNullOrEmpty(entVerificarNuevaContra.Text)))
        //{
        //    var(envioExitoso, mensajeError) = UserRepository.UpdatePassword(username, entNuevaContra.Text);

        //    if (envioExitoso)
        //    {                
        //        await DisplayAlert("Proceso terminado!", "Su contraseña se ha actualizado!", "Aceptar");
        //        await Navigation.PopAsync();
        //    }
        //    else
        //    {
        //        await DisplayAlert("Falle de Actualización", mensajeError, "Aceptar");
        //    }

        //}
        //else
        //{
        //    await DisplayAlert("Datos Inconsistentes", "Las contraseña ingresada no coincide con la verificación.", "Aceptar");
        //}
    }
}