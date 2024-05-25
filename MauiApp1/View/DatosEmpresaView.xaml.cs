using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Pages
{
    public partial class VerDataEmp : ContentPage
    {
        private string connectionString = "server=localhost;port=3307;database=gym_app;uid=root;password=";

        public ObservableCollection<Empresa> Empresas { get; set; }

        public VerDataEmp(List<Empresa> empresas)
        {
            InitializeComponent();
            Empresas = new ObservableCollection<Empresa>(empresas);
            CargarDatosEmpresa();
        }

        private async void CargarDatosEmpresa()
        {
            try
            {
                Empresas.Clear();

                string query = "SELECT Rif, Descrip, Direccion, Logo FROM DatosEmpresa";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var empresa = new Empresa
                                {
                                    Rif = reader.GetString(0),
                                    Descrip = reader.GetString(1),
                                    Direccion = reader.GetString(2),
                                    Logo = reader.IsDBNull(3) ? null : (byte[])reader["Logo"]
                                };

                                if (empresa.Logo != null)
                                {
                                    empresa.LogoImageSource = ImageSource.FromStream(() => new MemoryStream(empresa.Logo));
                                }

                                Empresas.Add(empresa);
                            }
                        }
                    }
                }

                if (Empresas.Count > 0)
                {
                    BindingContext = Empresas[0]; // Actualizar el contexto de enlace
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al cargar los datos: {ex.Message}", "OK");
            }
        }

        private async void EditarClicked(object sender, EventArgs e)
        {
            try
            {
                if (Empresas.Count == 0)
                {
                    // Si no hay empresas, abrir EditarEmpresa con campos vacíos para agregar nuevos datos
                    await Navigation.PushAsync(new EditarEmpresa(Empresas, string.Empty, string.Empty, string.Empty, null));
                }
                else
                {
                    // Si hay empresas, editar la primera empresa de la lista
                    var empresa = Empresas[0];
                    await Navigation.PushAsync(new EditarEmpresa(Empresas, empresa.Rif, empresa.Descrip, empresa.Direccion, empresa.Logo));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error al intentar editar la empresa: {ex.Message}", "OK");
            }
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            if (Empresas.Count == 0)
            {
                await DisplayAlert("Error", "No hay empresas disponibles para eliminar.", "OK");
                return;
            }

            var confirmar = await DisplayAlert("Eliminar", "¿Estás seguro de eliminar esta empresa?", "Sí", "No");
            if (confirmar)
            {
                try
                {
                    var empresa = Empresas[0]; // Suponiendo que solo estás trabajando con la primera empresa
                    if (empresa == null)
                    {
                        await DisplayAlert("Error", "No hay empresa seleccionada para eliminar.", "OK");
                        return;
                    }

                    string query = "DELETE FROM DatosEmpresa WHERE Rif = @Rif";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Rif", empresa.Rif);
                            int rowsAffected = await command.ExecuteNonQueryAsync();

                            if (rowsAffected > 0)
                            {
                                await DisplayAlert("Eliminar", "Empresa eliminada correctamente.", "OK");
                                Empresas.Remove(empresa); // Remover la empresa de la lista
                            }
                            else
                            {
                                await DisplayAlert("Eliminar", "No se pudo eliminar la empresa.", "OK");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Error al eliminar la empresa: {ex.Message}", "OK");
                }
            }
        }
    }

    public class Empresa
    {
        public string Rif { get; set; }
        public string Descrip { get; set; }
        public string Direccion { get; set; }
        public byte[] Logo { get; set; }
        public ImageSource LogoImageSource { get; set; }
    }
}
