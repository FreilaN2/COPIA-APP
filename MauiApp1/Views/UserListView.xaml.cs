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
            await Navigation.PopAsync();
        }

        private async void btnEliminarUsers_Clicked(object sender, EventArgs e)
        {
            /*var usuariosSeleccionados = ((List<Usuario>)ltvUserListView.ItemsSource).Where(u => u.IsSelected).ToList();

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
            }*/
        }

        private void ltvUserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}
