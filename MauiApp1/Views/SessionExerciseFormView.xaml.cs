using SpinningTrainer.ViewModel;
using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views;

public partial class SessionExerciseFormView : ContentPage
{
    private SessionExerciseViewModel _sessionExerciseViewModel;
	public SessionExerciseFormView(SessionExerciseViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
        _sessionExerciseViewModel = viewModel;

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        _sessionExerciseViewModel.ClearSelection();
    }
}