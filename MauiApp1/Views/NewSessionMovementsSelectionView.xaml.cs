using System.Collections.ObjectModel;

namespace SpinningTrainer.Views
{
    public partial class NewSessionMovementsSelectionView : ContentPage
    {
        public ObservableCollection<ExerciseItem> ExerciseItems { get; set; }
        public NewSessionMovementsSelectionView()
        {
            InitializeComponent();

            ExerciseItems = new ObservableCollection<ExerciseItem>();
            exerciseListView.ItemsSource = ExerciseItems;
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

        private void OnAgregarButtonClicked(object sender, EventArgs e)
        {
            string selectedExercise = exercisePicker.SelectedItem?.ToString();
            string duration = entMinsMove.Text;
            string startRPM = RPMStartPicker.SelectedItem?.ToString();
            string finalRPM = RPMFinalPicker.SelectedItem?.ToString();
            string style = GetSelectedStyle();

            if (string.IsNullOrEmpty(selectedExercise) || string.IsNullOrEmpty(duration) ||
                string.IsNullOrEmpty(startRPM) || string.IsNullOrEmpty(finalRPM) || string.IsNullOrEmpty(style))
            {
                DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
                return;
            }

            ExerciseItems.Add(new ExerciseItem
            {
                Exercise = selectedExercise,
                Duration = duration,
                StartRPM = startRPM,
                FinalRPM = finalRPM,
                Style = style
            });
        }

        private string GetSelectedStyle()
        {

            return "Estilo ejemplo";
        }
    }
}

public class ExerciseItem
    {
    public string Exercise { get; set; }
    public string Duration { get; set; }
    public string StartRPM { get; set; }
    public string FinalRPM { get; set; }
    public string Style { get; set; }
    }

