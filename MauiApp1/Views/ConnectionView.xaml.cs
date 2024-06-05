using SpinningTrainer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using System;
using System.Data;
using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.IdentityModel.Tokens;
using System.Net.NetworkInformation;
using Microsoft.Maui;

namespace SpinningTrainer.Views;

public partial class ConnectionView : ContentPage
{
    //private Command openLoginViewCommand;

    public ConnectionView()
	{
		InitializeComponent();        
    }
    
    private async void btnVerificarConexion_ClickedAsync(object sender, EventArgs e)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Long;
        aiConexionVerificandose.IsRunning = true;
        await Task.Delay(2000);

        if (string.IsNullOrEmpty(entNombreServidor.Text) || string.IsNullOrEmpty(entNombreBaseDatos.Text) ||
           string.IsNullOrEmpty(entUsuario.Text) || string.IsNullOrEmpty(entClave.Text))
		{
            await DisplayAlert("Datos Incompletos", "Por favor complete la información de todos los campos.", "Aceptar");            
            aiConexionVerificandose.IsRunning = false;
        }
        else
        {            
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=" + entNombreServidor.Text + ";" +
                                                             "Initial Catalog=" + entNombreBaseDatos.Text + ";" +
                                                             "TrustServerCertificate=True;" +
                                                             "Persist Security Info=True;" +
                                                             "User Id=" + entUsuario.Text + ";" +
                                                             "Password=" + entClave.Text+";");                
                
                connection.Open();               

                if ((connection.State & ConnectionState.Open) > 0)
                {                                                           
                    var toast = Toast.Make("Conexión Exitosa", duration, 14);
                    await toast.Show(cancellationTokenSource.Token);

                    connection.Close();

                    entNombreServidor.IsEnabled = false;
                    entNombreBaseDatos.IsEnabled = false;
                    entUsuario.IsEnabled = false;
                    entClave.IsEnabled = false;

                    btnVerificarConexion.IsVisible = false;
                    btnGuardarConexion.IsVisible = true;

                    btnGuardarConexion.Focus();
                }
                else
                {
                    var toast = Toast.Make("Conexión Falló", duration, 14);
                    await toast.Show(cancellationTokenSource.Token);
                }
            }
            catch(Exception ex)
            {
                var toast = Toast.Make("Conexión Falló: "+ex.Message, duration, 14);
                await toast.Show(cancellationTokenSource.Token);
            }
            finally
            {
                await Task.Delay(2000);
                aiConexionVerificandose.IsRunning = false;
            }
        }
    }

    private async void btnGuardarConexion_Clicked(object sender, EventArgs e)
    {
        btnGuardarConexion.IsEnabled = false;        

        try
        {            
            string executablePath = AppDomain.CurrentDomain.BaseDirectory; // Obtiene la ruta del ejecutable
            string fileName = "Application.cfg"; // Nombre del archivo que deseas verificar o crear
            string filePath = System.IO.Path.Combine(executablePath, fileName);


            string connectionString = "Data Source=" + entNombreServidor.Text + ";" +
                                      "Initial Catalog=" + entNombreBaseDatos.Text + ";" +
                                      "TrustServerCertificate=True;" +
                                      "Persist Security Info=True;" +
                                      "User Id=" + entUsuario.Text + ";" +
                                      "Password=" + entClave.Text + ";";

            CryptographyData cryptography = new CryptographyData();

            string connectionStringEncriptada = cryptography.Encrypt(connectionString);

            File.WriteAllText(filePath, connectionStringEncriptada);

            RepositoryBase.TestConnection();

            if (RepositoryBase.CompruebaBaseDatos())
            {                
                await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
            }
            else
            {                
                entNombreServidor.IsEnabled = true;
                entNombreBaseDatos.IsEnabled = true;
                entUsuario.IsEnabled = true;
                entClave.IsEnabled = true;

                btnVerificarConexion.IsVisible = true;
                btnGuardarConexion.IsVisible = false;
                btnGuardarConexion.IsEnabled = true;
                await DisplayAlert("Error en Creación de Base de Datos", "Ha ocurrido un error al crear la base de datos, por favor verifique que la conexión es realizada de manera correcta y el usuario tenga permisos suficientes.", "Aceptar");
            }            
        }
        catch (Exception ex)
        {
            string mensajeError = ex.Message;
            entNombreServidor.IsEnabled = true;
            entNombreBaseDatos.IsEnabled = true;
            entUsuario.IsEnabled = true;
            entClave.IsEnabled = true;

            btnVerificarConexion.IsVisible = true;
            btnGuardarConexion.IsVisible = false;
            btnGuardarConexion.IsEnabled = true;
            throw;
        }        
    }
}