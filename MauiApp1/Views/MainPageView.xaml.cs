using Microsoft.Maui.Controls.PlatformConfiguration;
using SpinningTrainer.Model;
using SpinningTrainer.View;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SpinningTrainer;

public partial class MainPageView : ContentPage
{
    ObservableCollection<Sessions> infoSesiones = new ObservableCollection<Sessions>();

	public MainPageView()
	{
		InitializeComponent();

		infoSesiones.Add(new Sessions { Id = 5, Descrip = "Sesi�n Basica", IdEntrenador = 2, DescripEntrenador = "Diego Estevez", Fecha = DateTime.Now });
        infoSesiones.Add(new Sessions { Id = 3, Descrip = "Sesi�n Media", IdEntrenador = 2, DescripEntrenador = "Diego Estevez", Fecha = DateTime.Now });
        infoSesiones.Add(new Sessions { Id = 8, Descrip = "Sesi�n Alta Dificultad", IdEntrenador = 2, DescripEntrenador = "Diego Estevez", Fecha = DateTime.Now });
		
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
        var selectedAction = await DisplayActionSheet("Seleccionar opci�n", "Cancelar", null, actions.ToArray());

        // Aqu� puedes manejar la opci�n seleccionada
        switch (selectedAction)
        {
            case "Opci�n 1":
                // Manejar la opci�n 1
                break;
            case "Opci�n 2":
                // Manejar la opci�n 2
                break;
            case "Opci�n 3":
                // Manejar la opci�n 3
                break;
            default:
                // Cancelar o ning�n bot�n seleccionado
                break;
        }
    }

    private async void btnCreateNewSession_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NewSessionView());
    }
}