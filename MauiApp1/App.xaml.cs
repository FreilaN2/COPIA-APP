using SpinningTrainer.Repositories;
using SpinningTrainer.View;

namespace SpinningTrainer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //if (!DataBaseConnection.TestConnection()) { MainPage = new ConnectionView(); }
            //else { MainPage = new LoginView(); }
        }
    }
}
