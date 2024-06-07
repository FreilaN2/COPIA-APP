using System;
using Microsoft.Maui.Controls;
using System.Data.SqlClient;

namespace SpinningTrainerTV
{
    public partial class PedirPIN : ContentPage
    {
        private const string connectionString = "Server=localhost;Database=gym_app;User Id=your_username;Password=your_password;"; // Conexión con la base de datos SQL Server
        private string codUsua;

        public PedirPIN(string codUsua)
        {
            InitializeComponent();
            this.codUsua = codUsua;
            lblUserName.Text = $"Usuario: {codUsua}";
            SetupEntryEvents();
        }

        private void SetupEntryEvents()
        {
            pin1.TextChanged += (s, e) => FocusNextEntry(pin1, e.NewTextValue);
            pin2.TextChanged += (s, e) => FocusNextEntry(pin2, e.NewTextValue);
            pin3.TextChanged += (s, e) => FocusNextEntry(pin3, e.NewTextValue);
            pin4.TextChanged += (s, e) => FocusNextEntry(pin4, e.NewTextValue);
        }

        private void FocusNextEntry(Entry currentEntry, string newTextValue)
        {
            if (!string.IsNullOrEmpty(newTextValue))
            {
                switch (currentEntry)
                {
                    case Entry _ when currentEntry == pin1:
                        pin2.Focus();
                        break;
                    case Entry _ when currentEntry == pin2:
                        pin3.Focus();
                        break;
                    case Entry _ when currentEntry == pin3:
                        pin4.Focus();
                        break;
                    case Entry _ when currentEntry == pin4:
                        ValidatePIN();
                        break;
                }
            }
        }

        private void OnPinEntryCompleted(object sender, EventArgs e)
        {
            Entry currentEntry = (Entry)sender;
            FocusNextEntry(currentEntry, currentEntry.Text);
        }

        private void OnPinEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry currentEntry = (Entry)sender;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                switch (currentEntry)
                {
                    case Entry _ when currentEntry == pin2:
                        pin1.Focus();
                        break;
                    case Entry _ when currentEntry == pin3:
                        pin2.Focus();
                        break;
                    case Entry _ when currentEntry == pin4:
                        pin3.Focus();
                        break;
                }
            }
        }

        private async void ValidatePIN()
        {
            /*string pin = $"{pin1.Text}{pin2.Text}{pin3.Text}{pin4.Text}";

            if (string.IsNullOrWhiteSpace(pin) || pin.Length != 4)
            {
                lblResultado.Text = "El PIN debe tener exactamente 4 dígitos.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT CodUsua FROM usuario WHERE CodUsua = @codUsua AND PIN = @pin";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@codUsua", codUsua);
                        cmd.Parameters.AddWithValue("@pin", pin);
                        var result = await cmd.ExecuteScalarAsync();

                        if (result != null)
                        {
                            lblResultado.Text = "PIN correcto.";
                            await Navigation.PushAsync(new WelcomePage(codUsua));
                        }
                        else
                        {
                            lblResultado.Text = "PIN incorrecto.";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                lblResultado.Text = $"Error de conexión a la base de datos: {ex.Message}";
            }
            catch (Exception ex)
            {
                lblResultado.Text = $"Error: {ex.Message}";
            }*/
        }
    }
}
