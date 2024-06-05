using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace SpinningTrainer.Views
{
    public partial class UserListView : ContentPage
    {
        private string connectionString = "Server=DIEGOES;Database=TambocaPruebas;User Id=SA;Password=180903;";

        private ObservableCollection<Usuario> usuarios;

        public UserListView()
        {
            InitializeComponent();
            usuarios = new ObservableCollection<Usuario>();
            ltvUserListView.ItemsSource = usuarios;
            CargarUsuarios();
        }

        private async void CargarUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            string query = "SELECT ID, CodUsua, FechaV FROM Usuario";

            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))  
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync()) 
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));
                                string codUsua = reader.GetString(reader.GetOrdinal("CodUsua"));
                                DateTime fechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaV")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("FechaV"));

                                usuarios.Add(new Usuario
                                {
                                    ID = id,
                                    CodUsua = codUsua,
                                    FechaVencimiento = fechaVencimiento,
                                    IsSelected = false
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    await DisplayAlert("Error", "No se pudieron cargar los usuarios: " + ex.Message, "OK");
                }
            }

            ltvUserListView.ItemsSource = usuarios;
        }

        private async void btnAgregarUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnEliminarUsers_Clicked(object sender, EventArgs e)
        {
            var usuariosSeleccionados = ((List<Usuario>)ltvUserListView.ItemsSource).Where(u => u.IsSelected).ToList();

            if (usuariosSeleccionados.Count == 0)
            {
                await DisplayAlert("Eliminar Usuario", "Por favor, selecciona un usuario para eliminar.", "OK");
                return;
            }

            if (usuariosSeleccionados.Count > 1)
            {
                await DisplayAlert("Eliminar Usuario", "Solo puedes seleccionar un usuario a la vez para eliminar.", "OK");
                return;
            }

            var usuarioSeleccionado = usuariosSeleccionados.First();

            if (usuarioSeleccionado.FechaVencimiento < DateTime.Now)
            {

                string result = await DisplayPromptAsync("Código de Confirmación", "Ingresa el código de confirmación:");
                if (result == "1234")
                {
                    await EliminarUsuario(usuarioSeleccionado.ID);
                }
                else
                {
                    await DisplayAlert("Error", "Código de confirmación incorrecto.", "OK");
                }
            }
            else
            {

                await VerificarYEliminarUsuario(usuarioSeleccionado);
            }


            CargarUsuarios();
        }

        private async Task VerificarYEliminarUsuario(Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))  
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "SELECT FechaV FROM Usuario WHERE ID = @ID";
                    DateTime fechaVencimiento;

                    using (SqlCommand command = new SqlCommand(query, connection)) 
                    {
                        command.Parameters.AddWithValue("@ID", usuario.ID);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())  
                        {
                            if (await reader.ReadAsync())
                            {
                                fechaVencimiento = reader.GetDateTime(reader.GetOrdinal("FechaV"));
                            }
                            else
                            {
                                await DisplayAlert("Error", "Usuario no encontrado.", "OK");
                                return;
                            }
                        }
                    }

                    if (fechaVencimiento < DateTime.Now)
                    {
                        bool confirm = await DisplayAlert("Eliminar Usuario", "La membresía ha vencido. ¿Deseas eliminar este usuario?", "Sí", "No");
                        if (confirm)
                        {
                            await EliminarUsuario(usuario.ID);
                        }
                    }
                    else
                    {
                        bool confirm = await DisplayAlert("Membresía Activa", "La membresía está activa. ¿Deseas eliminar este usuario?", "Sí", "No");
                        if (confirm)
                        {
                            string result = await DisplayPromptAsync("Código de Confirmación", "Ingresa el código de confirmación:");
                            if (result == "1234")
                            {
                                await EliminarUsuario(usuario.ID);
                            }
                            else
                            {
                                await DisplayAlert("Error", "Código de confirmación incorrecto.", "OK");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    await DisplayAlert("Error", "No se pudo verificar la membresía del usuario: " + ex.Message, "OK");
                }
            }
        }

        private async Task EliminarUsuario(int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) 
            {
                try
                {
                    await connection.OpenAsync();
                    string query = "DELETE FROM Usuario WHERE ID = @ID";

                    using (SqlCommand command = new SqlCommand(query, connection))  
                    {
                        command.Parameters.AddWithValue("@ID", userId);
                        await command.ExecuteNonQueryAsync();
                        await DisplayAlert("Eliminar Usuario", "El usuario ha sido eliminado.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    await DisplayAlert("Error", "No se pudo eliminar el usuario: " + ex.Message, "OK");
                }
            }
        }

        private async void ltvUserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            var selectedUser = (Usuario)e.Item;

            await Navigation.PushAsync(new UserDetailsView(new UserViewModel(selectedUser)));

            ((ListView)sender).SelectedItem = null;
        }
    }

    public class Usuario
    {
        public int ID { get; set; }
        public string CodUsua { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool IsSelected { get; set; }
    }

    public class UserViewModel
    {
        public Usuario User { get; set; }

        public UserViewModel(Usuario user)
        {
            User = user;
        }
    }
}
