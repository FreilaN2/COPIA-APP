using Microsoft.Data.SqlClient;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System;

namespace Pages
{
    public partial class MainPage : ContentPage
    {
        // Conexion con la Base de Datos
        private string connectionString = "server=localhost;port=3306;database=gym_app;uid=root;password=";

        public MainPage()
        {
            InitializeComponent();
        }

        // Boton para guardar los datos en la Base de Datos
        private async void Guardar_Clicked(object sender, EventArgs e)
        {
            if (CamposCompletos())
            {
                if (ValidarContraseñas())
                {
                    if (CrearUsuario())
                    {
                        await DisplayAlert("Éxito", "Datos guardados exitosamente", "Aceptar");
                        await Navigation.PushAsync(new UserListPage()); // Redirigir o recargar UserListPage
                    }
                    else
                    {
                        await DisplayAlert("Error", "No se pudieron guardar los datos", "Aceptar");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Las contraseñas no coinciden", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "Aceptar");
            }
        }


        private bool CamposCompletos()
        {
            return !string.IsNullOrWhiteSpace(UsuarioEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ContrasenaEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ConfirmarContrasenaEntry.Text) &&
                   !string.IsNullOrWhiteSpace(PINEntry.Text) &&
                   !string.IsNullOrWhiteSpace(NombreEntry.Text) &&
                   !string.IsNullOrWhiteSpace(ApellidoEntry.Text) &&
                   !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
                   !string.IsNullOrWhiteSpace(TelefonoEntry.Text);
        }

        private bool ValidarContraseñas()
        {
            string contrasena = ContrasenaEntry.Text;
            string confirmarContrasena = ConfirmarContrasenaEntry.Text;

            return contrasena == confirmarContrasena;
        }

        private bool CrearUsuario()
        {
            string nombreUsuario = UsuarioEntry.Text;
            string nombre = NombreEntry.Text;
            string apellido = ApellidoEntry.Text;
            string contrasena = ContrasenaEntry.Text;
            string PIN = PINEntry.Text; // Aquí no es necesario convertir el PIN
            string email = EmailEntry.Text;
            string telefono = TelefonoEntry.Text;

            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Calcular la fecha de recarga (fecha actual)
            DateTime fechaRecarga = fechaActual;

            // Calcular la fecha de vencimiento (un mes después de la fecha actual)
            DateTime fechaVencimiento = fechaActual.AddMonths(1);

            // Calcular el tipo de usuario
            int tipoUsuario = 0;
            if (SuperUsuarioCheckBox.IsChecked)
            {
                tipoUsuario = 0;
            }
            else if (AdministradorCheckBox.IsChecked)
            {
                tipoUsuario = 1;
            }
            else if (EntrenadorCheckBox.IsChecked)
            {
                tipoUsuario = 2;
            }

            // Insertar datos a la tabla Usuarios
            string query = "INSERT INTO Usuario (CodUsua, Contra, PIN, Descrip, Email, Telef, FechaC, FechaR, FechaV, TipoUsuario) " +
                           "VALUES (@CodUsua, @Contra, @PIN, @Descrip, @Email, @Telef, @FechaC, @FechaR, @FechaV, @TipoUsuario)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Variables que almacenan los datos de la Base de Datos
                        command.Parameters.AddWithValue("@CodUsua", nombreUsuario);
                        command.Parameters.AddWithValue("@Contra", contrasena);
                        command.Parameters.AddWithValue("@PIN", PIN); // Aquí no es necesario convertir el PIN
                        command.Parameters.AddWithValue("@Descrip", $"{nombre} {apellido}");
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Telef", telefono);
                        command.Parameters.AddWithValue("@FechaC", fechaActual);
                        command.Parameters.AddWithValue("@FechaR", fechaRecarga);
                        command.Parameters.AddWithValue("@FechaV", fechaVencimiento);
                        command.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
