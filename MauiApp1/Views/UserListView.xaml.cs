using SpinningTrainer.Models;
using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views
{
    public partial class UserListView : ContentPage
    {
        public UserListView()
        {
            InitializeComponent();
        }        

        private async void btnAgregarUsers_Clicked(object sender, EventArgs e)
        {           
            // Obtener el ViewModel del BindingContext
            if (this.BindingContext is UsersViewModel viewModel)
            {
                // Llamar al método SeleccionarCliente del ViewModel                
                await Navigation.PushAsync(new UserDetailsView(viewModel));
            }            
        }        

        private void ltvUserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is UserModel selectedUser)
            {
                // Obtener el ViewModel del BindingContext
                if (this.BindingContext is UsersViewModel viewModel)
                {
                    // Llamar al método SeleccionarCliente del ViewModel
                    viewModel.Edit(selectedUser);
                    Navigation.PushAsync(new UserDetailsView(viewModel));
                }
            }
        }        
    }
}
