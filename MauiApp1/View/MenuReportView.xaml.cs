using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Microsoft.Maui.Controls;

namespace YourNamespace
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOutButtonClicked(object sender, EventArgs e)
        {
            // Acción para el botón de "out"
            await DisplayAlert("Salir", "Botón de salir presionado", "OK");
        }

        private async void OnHamburgerButtonClicked(object sender, EventArgs e)
        {
            // Navegar a la página de selección
            await Navigation.PushAsync(new SelectionPage());
        }
    }
}

