using Microsoft.Data.SqlClient;
using Microsoft.Maui.Controls;
using System;
using System.Data.SqlClient;  
using System.Threading.Tasks;

namespace SpinningTrainer.Views
{
    public partial class UserDetailsView : ContentPage
    {
        private string connectionString = "Server=DIEGOES;Database=TambocaPruebas;User Id=SA;Password=180903;";

        public UserDetailsView(UserViewModel userViewModel)
        {
            InitializeComponent();
            LoadUserData(userViewModel.User.ID);
        }

        public class Usuario
        {
            public int ID { get; set; }
            public string CodUsua { get; set; }
            public string Descrip { get; set; }
            public byte[] Contra { get; set; }
            public string PIN { get; set; }
            public string Email { get; set; }
            public string Telef { get; set; }
            public DateTime FechaC { get; set; }
            public DateTime FechaR { get; set; }
            public DateTime FechaV { get; set; }
            public int TipoUsuario { get; set; }
            public bool IsSelected { get; set; }
        }

        private async void LoadUserData(int userId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))  
                {
                    await connection.OpenAsync();
                    string query = "SELECT * FROM Usuario WHERE ID = @ID";
                    using (SqlCommand command = new SqlCommand(query, connection))  
                    {
                        command.Parameters.AddWithValue("@ID", userId);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())  
                        {
                            if (await reader.ReadAsync())
                            {
                                Usuario user = new Usuario
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    CodUsua = reader.GetString(reader.GetOrdinal("CodUsua")),
                                    Descrip = reader.IsDBNull(reader.GetOrdinal("Descrip")) ? string.Empty : reader.GetString(reader.GetOrdinal("Descrip")),
                                    Contra = reader.IsDBNull(reader.GetOrdinal("Contra")) ? null : (byte[])reader["Contra"],
                                    PIN = reader.IsDBNull(reader.GetOrdinal("PIN")) ? null : reader.GetString(reader.GetOrdinal("PIN")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                    Telef = reader.IsDBNull(reader.GetOrdinal("Telef")) ? string.Empty : reader.GetString(reader.GetOrdinal("Telef")),
                                    FechaC = reader.IsDBNull(reader.GetOrdinal("FechaC")) ? default : reader.GetDateTime(reader.GetOrdinal("FechaC")),
                                    FechaR = reader.IsDBNull(reader.GetOrdinal("FechaR")) ? default : reader.GetDateTime(reader.GetOrdinal("FechaR")),
                                    FechaV = reader.IsDBNull(reader.GetOrdinal("FechaV")) ? default : reader.GetDateTime(reader.GetOrdinal("FechaV")),
                                    TipoUsuario = reader.GetInt32(reader.GetOrdinal("TipoUsuario")),
                                    IsSelected = false
                                };


                                if (!string.IsNullOrEmpty(user.PIN) && user.PIN.Length != 4)
                                {
                                    user.PIN = string.Empty;
                                }

                                BindingContext = user;


                                pkTipoUsuario.SelectedIndex = user.TipoUsuario;
                            }
                            else
                            {
                                await DisplayAlert("Error", "Usuario no encontrado.", "OK");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                await DisplayAlert("Error", "No se pudo cargar los datos del usuario: " + ex.Message, "OK");
            }
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            await GuardarCambios();
        }

        private async Task GuardarCambios()
        {
            Usuario user = (Usuario)BindingContext;


            if (!string.IsNullOrEmpty(user.PIN) && user.PIN.Length != 4)
            {
                await DisplayAlert("Error", "El PIN debe tener 4 dígitos.", "OK");
                return;
            }


            if (entContra.Text != entConfirmarContra.Text)
            {
                await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                return;
            }


            user.TipoUsuario = pkTipoUsuario.SelectedIndex;

            using (SqlConnection connection = new SqlConnection(connectionString))  
            {
                try
                {
                    await connection.OpenAsync();
                    string query = @"UPDATE Usuario 
                                     SET CodUsua = @CodUsua,
                                         Descrip = @Descrip,
                                         Contra = @Contra,
                                         PIN = @PIN,
                                         Email = @Email,
                                         Telef = @Telef,
                                         FechaC = @FechaC,
                                         FechaR = @FechaR,
                                         FechaV = @FechaV,
                                         TipoUsuario = @TipoUsuario
                                     WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        command.Parameters.AddWithValue("@CodUsua", user.CodUsua);
                        command.Parameters.AddWithValue("@Descrip", user.Descrip);
                        command.Parameters.AddWithValue("@Contra", user.Contra);
                        command.Parameters.AddWithValue("@PIN", user.PIN);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Telef", user.Telef);
                        command.Parameters.AddWithValue("@FechaC", user.FechaC);
                        command.Parameters.AddWithValue("@FechaR", user.FechaR);
                        command.Parameters.AddWithValue("@FechaV", user.FechaV);
                        command.Parameters.AddWithValue("@TipoUsuario", user.TipoUsuario);
                        command.Parameters.AddWithValue("@ID", user.ID);

                        await command.ExecuteNonQueryAsync();
                        await DisplayAlert("Editar Usuario", "Los cambios se han guardado exitosamente.", "OK");
                        await Navigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    await DisplayAlert("Error", "No se pudieron guardar los cambios: " + ex.Message, "OK");
                }
            }
        }
    }
}
