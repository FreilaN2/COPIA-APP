using Configurador_WPF.Data;

namespace SpinningTrainer.View;

public partial class SplashScreenView : ContentPage
{
	public SplashScreenView()
	{
		InitializeComponent();
	}

    private async void ContentPage_Appearing(object sender, EventArgs e)
    {
        var (tituloConsejo, consejo) = BuscaConsejo();

        lblTituloConsejo.Text = tituloConsejo;
        lblConsejo.Text = consejo;

        lblMuestraMensajeCargandoAlUsuario.Text = "Cargando... Comprobando archivos.";
        await pbLoadProgress.ProgressTo(0.2, 4000, Easing.Linear);
        
        await Task.Delay(1000);

        lblMuestraMensajeCargandoAlUsuario.Text = "Cargando... Comprobando conexión a la base de datos.";
        if (DataBaseConnection.TestConnection())
        {
            await pbLoadProgress.ProgressTo(0.7, 4000, Easing.Linear);

            ///ACA TIENE QUE IR LA LOGICA DE COMPROBACION Y CREACION DE BASE DE DATOS
            lblMuestraMensajeCargandoAlUsuario.Text = "Cargando... Comprobando base de datos";
            if (DataBaseConnection.CompruebaBaseDatos())
            {
                await pbLoadProgress.ProgressTo(1, 4000, Easing.Linear);

                await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
            }
            else
            {
                await DisplayAlert("Error en Creación de Base de Datos", "Ha ocurrido un error al crear la base de datos, por favor verifique que la conexión es realizada de manera correcta.", "Aceptar");
            }
        }
        else
        {
            await pbLoadProgress.ProgressTo(1, 500, Easing.Linear);
            await Shell.Current.GoToAsync($"//{nameof(ConnectionView)}");
        }
    }

    private (string, string) BuscaConsejo()
    {        
        string[,] arrayConsejos = new string[,]
        {
            {"Individuos Inactivos", "Vigílalos de cerca para asegurar que entrenen a un nivel adecuado (puedes guiarlos desde fuera de la bicicleta)." },
            {"Individuos Inactivos", "Corrige su postura para que desarrollen una técnica de pedaleo adecuada." },
            {"Individuos Inactivos", "Mantén las clases sencillas, enfocándote en lo básico (posición, movimientos, manos, intensidad) para que no se sobrecarguen." },
            {"Individuos Que Buscan Recreación", "Ofrece clases variadas con desafíos de destreza para mantenerlos motivados." },
            {"Individuos Que Buscan Recreación", "Utiliza música y formatos de clase divertidos para que disfruten del ejercicio." },
            {"Individuos Que Buscan Rendimiento", "Implementa sesiones periodizadas y entrenamientos basados en la potencia o la frecuencia cardíaca." },
            {"Individuos Que Buscan Rendimiento", "Anímalos a compartir sus conocimientos y experiencias con el resto de la clase." },
            {"Individuos Que Buscan Rendimiento", "No te desanimes si los ciclistas experimentados entrenan a su propio ritmo." },
            {"Individuos Que Buscan Rendimiento", "Ofrece consejos sobre la forma y la técnica para mejorar la eficiencia y el rendimiento." },
            {"Poblaciones Especiales", "Ellos necesitan una atención especial y deben ser dirigidos para ir a su propio ritmo." },
            {"Poblaciones Especiales", "Préstales atención periódicamente para asegurarse si están realizando los ejercicios a una intensidad adecuada." },
            {"Preparacion", "No esperes al último minuto. Selecciona o crea tu música y perfiles de Spinning con una semana de anticipación. Repásalos nuevamente la noche anterior a la clase y otra vez justo antes de iniciar." },
            {"Preparacion", "No des por sentado que todo está en orden. Comprueba la configuración adecuada de las bicicletas para alumnos nuevos y experimentados. Haz los ajustes necesarios para garantizar una postura cómoda y segura." },
            {"Preparacion", "Refresca la memoria de los alumnos. Diles que no olviden traer una toalla y una botella de agua llena para mantenerse hidratados durante la clase." },
            {"Preparacion", "Pide a los alumnos que comiencen a pedalear tan pronto como sea posible para calentar sus piernas y prepararse para la sesión." },
        };

        Random random = new Random(); // Crea una instancia de la clase Random
        int numeroAleatorio = random.Next(0, 14); // Genera un número aleatorio entre 0 y 14 (inclusive)

        return (arrayConsejos[numeroAleatorio, 0], arrayConsejos[numeroAleatorio, 1]);
    }
}