using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SpinningTrainerTV.ViewModelsTV
{
    internal class PlaySessionViewModel : INotifyPropertyChanged
    {
        private Chart _chart;
        private Timer _timer;
        private Random _random = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        public Chart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                OnPropertyChanged(nameof(Chart));
            }
        }

        public PlaySessionViewModel()
        {
            try
            {
                // Inicializar el gráfico con datos iniciales
                UpdateChart();

                // Configurar el temporizador para actualizar el gráfico cada segundo
                _timer = new Timer(1000);
                _timer.Elapsed += (sender, e) => UpdateChart();
                _timer.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        private void UpdateChart()
        {
            try
            {
                var entries = new[]
                {
                new ChartEntry(_random.Next(0, 100))
                    {
                        Label = "Value 1",
                        ValueLabel = _random.Next(0, 100).ToString(),
                        Color = SKColor.Parse("#266489")
                    },
                    new ChartEntry(_random.Next(0, 100))
                    {
                        Label = "Value 2",
                        ValueLabel = _random.Next(0, 100).ToString(),
                        Color = SKColor.Parse("#68B9C0")
                    },
                    new ChartEntry(_random.Next(0, 100))
                    {
                        Label = "Value 3",
                        ValueLabel = _random.Next(0, 100).ToString(),
                        Color = SKColor.Parse("#90D585")
                    }
                };

                Chart = new BarChart { Entries = entries.ToList() };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
