using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using SpinningTrainer.Model;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Pages
{
    public partial class UsersDetailsView : ContentPage
    {
        private string connectionString = "server=localhost;port=3307;database=gym_app;uid=root;password=";

        public UsersDetailsView(UserModel userModel)
        {
            //InitializeComponent();
            LoadUserData(userModel.Id);
        }

        public class Usuario
        {
            public int ID { get; set; }
            public string CodUsua { get; set; }
            public string Descrip { get; set; }
            public byte[] Contra { get; set; }
            public byte[] PIN { get; set; }
            public string Email { get; set; }
            public string Telef { get; set; }
            public DateTime FechaC { get; set; }
            public DateTime FechaR { get; set; }
            public DateTime FechaV { get; set; }
            public int TipoUsuario { get; set; }
            public bool IsSelected { get; set; } // Nueva propiedad para selección
        }

        private async void LoadUserData(int userId)
        {
            //try
            //{
            //    using (MySqlConnection connection = new MySqlConnection(connectionString))
            //    {
            //        await connection.OpenAsync();
            //        string query = "SELECT * FROM Usuario WHERE ID = @ID";
            //        using (MySqlCommand command = new MySqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("@ID", userId);
            //            using (MySqlDataReader reader = command.ExecuteReader())
            //            {
            //                if (reader.Read())
            //                {
            //                    Usuario user = new Usuario
            //                    {
            //                        ID = reader.GetInt32("ID"),
            //                        CodUsua = reader.GetString("CodUsua"),
            //                        Descrip = reader.IsDBNull("Descrip") ? string.Empty : reader.GetString("Descrip"),
            //                        Contra = reader.IsDBNull("Contra") ? null : (byte[])reader["Contra"],
            //                        PIN = reader.IsDBNull("PIN") ? null : (byte[])reader["PIN"],
            //                        Email = reader.IsDBNull("Email") ? string.Empty : reader.GetString("Email"),
            //                        Telef = reader.IsDBNull("Telef") ? string.Empty : reader.GetString("Telef"),
            //                        FechaC = reader.GetDateTime("FechaC"),
            //                        FechaR = reader.GetDateTime("FechaR"),
            //                        FechaV = reader.GetDateTime("FechaV"),
            //                        TipoUsuario = reader.GetInt32("TipoUsuario"),
            //                        IsSelected = false // Nueva propiedad para selección
            //                    };

            //                    BindingContext = user;
            //                }
            //                else
            //                {
            //                    await DisplayAlert("Error", "Usuario no encontrado.", "OK");
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error: " + ex.Message);
            //    await DisplayAlert("Error", "No se pudo cargar los datos del usuario: " + ex.Message, "OK");
            //}
        }

        private async void Editar_Clicked(object sender, EventArgs e)
        {
            // Guardar los cambios en la base de datos
            await GuardarCambios();
        }

        private async void Eliminar_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Eliminar Usuario", "¿Estás seguro de que deseas eliminar este usuario?", "Sí", "No");
            if (confirm)
            {
                await EliminarUsuario();
            }
        }

        private async Task GuardarCambios()
        {
            //Usuario user = (Usuario)BindingContext;

            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    try
            //    {
            //        await connection.OpenAsync();
            //        string query = @"UPDATE Usuario 
            //                         SET CodUsua = @CodUsua,
            //                             Descrip = @Descrip,
            //                             Contra = @Contra,
            //                             PIN = @PIN,
            //                             Email = @Email,
            //                             Telef = @Telef,
            //                             FechaC = @FechaC,
            //                             FechaR = @FechaR,
            //                             FechaV = @FechaV,
            //                             TipoUsuario = @TipoUsuario
            //                         WHERE ID = @ID"
            //        ;

            //        using (MySqlCommand command = new MySqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("@CodUsua", user.CodUsua);
            //            command.Parameters.AddWithValue("@Descrip", user.Descrip);
            //            command.Parameters.AddWithValue("@Contra", user.Contra);
            //            command.Parameters.AddWithValue("@PIN", user.PIN);
            //            command.Parameters.AddWithValue("@Email", user.Email);
            //            command.Parameters.AddWithValue("@Telef", user.Telef);
            //            command.Parameters.AddWithValue("@FechaC", user.FechaC);
            //            command.Parameters.AddWithValue("@FechaR", user.FechaR);
            //            command.Parameters.AddWithValue("@FechaV", user.FechaV);
            //            command.Parameters.AddWithValue("@TipoUsuario", user.TipoUsuario);
            //            command.Parameters.AddWithValue("@ID", user.ID);

            //            await command.ExecuteNonQueryAsync();
            //            await DisplayAlert("Editar Usuario", "Los cambios se han guardado exitosamente.", "OK");
            //            await Navigation.PushAsync(new UserListPage()); // Redirigir o recargar UserListPage
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error: " + ex.Message);
            //        await DisplayAlert("Error", "No se pudieron guardar los cambios: " + ex.Message, "OK");
            //    }
            //}
        }

        private async Task EliminarUsuario()
        {
            //Usuario user = (Usuario)BindingContext;

            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    try
            //    {
            //        await connection.OpenAsync();
            //        string query = "DELETE FROM Usuario WHERE ID = @ID";

            //        using (MySqlCommand command = new MySqlCommand(query, connection))
            //        {
            //            command.Parameters.AddWithValue("@ID", user.ID);
            //            await command.ExecuteNonQueryAsync();
            //            await DisplayAlert("Eliminar Usuario", "El usuario ha sido eliminado.", "OK");
            //            await Navigation.PushAsync(new UserListPage()); // Redirigir o recargar UserListPage
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error: " + ex.Message);
            //        await DisplayAlert("Error", "No se pudo eliminar el usuario: " + ex.Message, "OK");
            //    }
            //}
        }
    }
}
