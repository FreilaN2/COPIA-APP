using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views
{
    public partial class UserDetailsView : ContentPage
    {                
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
