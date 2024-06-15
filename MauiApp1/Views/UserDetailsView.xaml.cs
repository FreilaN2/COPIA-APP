using SpinningTrainer.ViewModels;
using Microsoft.Maui.Controls;

namespace SpinningTrainer.Views
{
    public partial class UserDetailsView : ContentPage
    {
        // Constructor sin par�metros para permitir la navegaci�n
        public UserDetailsView()
        {
            InitializeComponent();
        }

        // Constructor que acepta un UsersViewModel para establecer el BindingContext
        public UserDetailsView(UsersViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (BindingContext is UsersViewModel viewModel)
            {
                viewModel.Clean();
            }
        }
    }
}