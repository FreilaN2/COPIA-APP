namespace SpinningTrainer.Views;

public partial class NewSessionView : ContentPage
{
	public NewSessionView()
	{
		InitializeComponent();
	}

    private void entSessionName_TextChanged(object sender, TextChangedEventArgs e)
    {
		Entry ent = (Entry)sender;

		btnNextStep.IsEnabled = string.IsNullOrEmpty(ent.Text) ? false : true;
    }

    private void btnNextStep_Clicked(object sender, EventArgs e)
    {
        string sessionName = entSessionName.Text;
        DateTime sessionDate = dpkSessionDate.Date;
        // TimeSpan sessionTime = dpkSessionTime.Time;

        //string sessionLevel =;
    }
}