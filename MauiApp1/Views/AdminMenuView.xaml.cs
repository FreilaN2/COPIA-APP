using SpinningTrainer.Views;

namespace SpinningTrainer.Views;

public partial class AdminMenuView : ContentPage
{
	public AdminMenuView()
	{
		InitializeComponent();
	}

    private async void btnOpenDataManagment_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new CompanyDataView());
    }
}