using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Microsoft.Maui.Controls;

namespace YourNamespace
{
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        private async void OnYesButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página de confirmación
            await Navigation.PushAsync(new ConfirmationPage());
        }

        private async void OnNoButtonClicked(object sender, EventArgs e)
        {
            // Volver a la página anterior
            await Navigation.PopAsync();
        }
    }
}
