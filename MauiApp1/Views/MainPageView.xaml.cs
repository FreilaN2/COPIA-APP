using Microsoft.Maui.Controls.PlatformConfiguration;
using SpinningTrainer.Model;
using SpinningTrainer.Models;
using SpinningTrainer.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpinningTrainer.Views;

public partial class MainPageView : ContentPage
{
    ObservableCollection<SessionModel> infoSesiones = new ObservableCollection<SessionModel>();

	public MainPageView()
	{
		InitializeComponent();

		infoSesiones.Add(new SessionModel { ID = 5, Descrip = "Sesión Basica", IDEntrenador = 2 });
        infoSesiones.Add(new SessionModel { ID = 3, Descrip = "Sesión Media", IDEntrenador = 2 });
        infoSesiones.Add(new SessionModel { ID = 8, Descrip = "Sesión Alta Dificultad", IDEntrenador = 2 });
		
		lvInfoSesiones.ItemsSource = infoSesiones;
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        var stackLayout1 = imageButton.Parent as StackLayout;
        var stackLayout2 = stackLayout1.Parent as StackLayout;
        var frame = stackLayout2.Parent as Frame;
        var grid = frame.Parent as Grid;
        var viewCell = grid.Parent as ViewCell;        
        
        var listView = viewCell.Parent as ListView;        

        await ShowPopupMenu();
    }

    private async Task ShowPopupMenu()
    {        
        var actions = new List<string> { "Duplicar", "Eliminar"};
        var selectedAction = await DisplayActionSheet("Seleccionar opción", "Cancelar", null, actions.ToArray());

        // Aquí puedes manejar la opción seleccionada
        switch (selectedAction)
        {
            case "Opción 1":
                // Manejar la opción 1
                break;
            case "Opción 2":
                // Manejar la opción 2
                break;
            case "Opción 3":
                // Manejar la opción 3
                break;
            default:
                // Cancelar o ningún botón seleccionado
                break;
        }
    }

    private async void btnCreateNewSession_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewSessionView());
    }
}