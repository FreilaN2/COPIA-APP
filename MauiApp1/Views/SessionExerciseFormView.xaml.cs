using SpinningTrainer.ViewModel;
using SpinningTrainer.ViewModels;

namespace SpinningTrainer.Views;

public partial class SessionExerciseFormView : ContentPage
{
	public SessionExerciseFormView(SessionExerciseViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}