namespace SpinningTrainer.Views
{
    public partial class SuperUserMenuView : ContentPage
    {
        public SuperUserMenuView()
        {
            InitializeComponent();
        }
        
        private async void btnOpenSelectionUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserListView());
        }
    }
}