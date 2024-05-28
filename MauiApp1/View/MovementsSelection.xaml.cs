namespace SpinningTrainer.View;

public partial class MovementsSelection : ContentPage
{
	public MovementsSelection()
	{
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AddItemsToPicker();
    }

    private void AddItemsToPicker()
    {
        exercisePicker.Items.Add("Plano");
        exercisePicker.Items.Add("Plano de pie / correr");
        exercisePicker.Items.Add("Salto");
        exercisePicker.Items.Add("Escalada sentado");
        exercisePicker.Items.Add("Escalada de pie");
        exercisePicker.Items.Add("Correr en montaña");
        exercisePicker.Items.Add("Saltos en montaña");
        exercisePicker.Items.Add("Sprints en plano");
        exercisePicker.Items.Add("Sprints en montaña");

        RPMStartPicker.Items.Add("30 RPM");
        RPMStartPicker.Items.Add("40 RPM");
        RPMStartPicker.Items.Add("50 RPM");
        RPMStartPicker.Items.Add("60 RPM");
        RPMStartPicker.Items.Add("70 RPM");
        RPMStartPicker.Items.Add("80 RPM");
        RPMStartPicker.Items.Add("90 RPM");
        RPMStartPicker.Items.Add("100 RPM");

        RPMFinalPicker.Items.Add("30 RPM");
        RPMFinalPicker.Items.Add("40 RPM");
        RPMFinalPicker.Items.Add("50 RPM");
        RPMFinalPicker.Items.Add("60 RPM");
        RPMFinalPicker.Items.Add("70 RPM");
        RPMFinalPicker.Items.Add("80 RPM");
        RPMFinalPicker.Items.Add("90 RPM");
        RPMFinalPicker.Items.Add("100 RPM");
        RPMFinalPicker.Items.Add("110 RPM");
        RPMFinalPicker.Items.Add("120 RPM");
    }

    private void entMinsMove_TextChanged(object sender, TextChangedEventArgs e)
    {
        
        }
}
        
