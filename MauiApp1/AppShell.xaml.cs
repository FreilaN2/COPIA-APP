namespace SpinningTrainer
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPageView), typeof(MainPageView));
        }
    }
}
