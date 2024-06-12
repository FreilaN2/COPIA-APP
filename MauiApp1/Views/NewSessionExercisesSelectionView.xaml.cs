using System.Collections.ObjectModel;
using SpinningTrainer.Models;
using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views
{
    public partial class NewSessionMovementsSelectionView : ContentPage
    {        
        public NewSessionMovementsSelectionView(int idSession)
        {
            InitializeComponent();
            this.BindingContext = new SessionExerciseViewModel(idSession);
            var exercisesViewModel = new ExercisesViewModel();
            exercisePicker.BindingContext = exercisesViewModel;
            //exercisesViewModel.SelectExercise += OnSelectExercise;

            ////void OnSelectExercise(object sender, ExerciseModel client)
            ////{
            ////    _clientsViewModel.ClientSelected -= OnClientSelected; // Desuscribirse del evento                
                
            ////    Application.Current.MainPage.Navigation.PopAsync();
            ////}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }        

        private void OnAgregarButtonClicked(object sender, EventArgs e)
        {
            //string selectedExercise = exercisePicker.SelectedItem?.ToString();
            //string duration = entMinsMove.Text;
            //string startRPM = RPMStartPicker.SelectedItem?.ToString();
            //string finalRPM = RPMFinalPicker.SelectedItem?.ToString();
            //string style = GetSelectedStyle();

            //if (string.IsNullOrEmpty(selectedExercise) || string.IsNullOrEmpty(duration) ||
            //    string.IsNullOrEmpty(startRPM) || string.IsNullOrEmpty(finalRPM) || string.IsNullOrEmpty(style))
            //{
            //    DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
            //    return;
            //}

            //ExerciseItems.Add(new ExerciseItem
            //{
            //    Exercise = selectedExercise,
            //    Duration = duration,
            //    StartRPM = startRPM,
            //    FinalRPM = finalRPM,
            //    Style = style
            //});
        }

        private string GetSelectedStyle()
        {

            return "Estilo ejemplo";
        }
    }
}


