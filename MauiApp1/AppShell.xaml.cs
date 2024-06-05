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
                SuperUserMenu.IsVisible = true;
                AdminMenu.IsVisible = false;
                TrainerMenu.IsVisible = false;
            }
            else if (userType == 1) // Administrador
            {
                SuperUserMenu.IsVisible = false;
                AdminMenu.IsVisible = true;
                TrainerMenu.IsVisible = false;
            }
            else if (userType == 2) // Entrenador
            {
                SuperUserMenu.IsVisible = false;
                AdminMenu.IsVisible = false;
                TrainerMenu.IsVisible = true;
            }
        }
    }
}
