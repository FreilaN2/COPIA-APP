using SpinningTrainer.Views;

namespace SpinningTrainer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPageView), typeof(MainPageView));
        }

        public void SetUserType(int userType)
        {
            if (userType == 0) // Super Usuario
            {
                AdminMenu.IsVisible = false;
                TrainerMenu.IsVisible = true;
            }
            else if (userType == 1) // Administrador
            {
                AdminMenu.IsVisible = true;
                TrainerMenu.IsVisible = false;
            }
        }
    }
}
