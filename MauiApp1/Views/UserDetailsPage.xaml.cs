using Microsoft.Data.SqlClient;
using SpinningTrainer.Models;

namespace SpinningTrainer.Views
{
    public partial class UserDetailsView : ContentPage
    {
        private string connectionString = "Server=DIEGOES;Database=TambocaPruebas;User Id=SA;Password=180903;";

        public UserDetailsView(UserModel user)
        {
            InitializeComponent();
            LoadUserData(user.Id);
        }

        public UserDetailsView()
        {
            InitializeComponent();            
        }

        private async void LoadUserData(int userId)
        {
           
        }

        private async void btnEditar_Clicked(object sender, EventArgs e)
        {
            await GuardarCambios();
        }

        private async Task GuardarCambios()
        {
            UserModel user = (UserModel)BindingContext;


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
                        command.Parameters.AddWithValue("@ID", user.Id);

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
