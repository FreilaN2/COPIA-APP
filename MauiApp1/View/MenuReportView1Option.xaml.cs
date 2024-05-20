using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Microsoft.Maui.Controls;

namespace YourNamespace
{
    public partial class ConfirmationPage : ContentPage
    {
        public ConfirmationPage()
        {
            InitializeComponent();
        }

        private async void OnYesButtonClicked(object sender, EventArgs e)
        {
            // Acción a realizar cuando se presiona "Sí"
            await DisplayAlert("Confirmación", "Has seleccionado Sí", "OK");
        }

        private async void OnNoButtonClicked(object sender, EventArgs e)
        {
            // Volver a la página anterior
            await Navigation.PopAsync();
        }
    }
}
