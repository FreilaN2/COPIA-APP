using System.Collections.ObjectModel;
using SpinningTrainer.Models;
using SpinningTrainer.ViewModel;
using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views
{
    public partial class SessionExercisesListView : ContentPage
    {        
        public SessionExercisesListView(SessionModel session, bool isEditing)
        {
            InitializeComponent();
            this.BindingContext = new SessionExerciseViewModel(session, isEditing);            
        }

        private async void btnAddSessionExercise_Clicked(object sender, EventArgs e)
        {
            // Obtener el ViewModel del BindingContext
            if (this.BindingContext is SessionExerciseViewModel viewModel)
            {
                // Llamar al método SeleccionarCliente del ViewModel                
                await Navigation.PushAsync(new SessionExerciseFormView(viewModel));
            }
        }

        private void lvSessionExercises_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is SessionExerciseModel selectedSessionExercise)
            {
                // Obtener el ViewModel del BindingContext
                if (this.BindingContext is SessionExerciseViewModel viewModel)
                {
                    // Llamar al método SeleccionarCliente del ViewModel
                    viewModel.EnableEdit(selectedSessionExercise);
                    Navigation.PushAsync(new SessionExerciseFormView(viewModel));
                }
            }
        }
    }
}


