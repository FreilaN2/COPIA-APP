using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Models
{
    public class SessionModel
    {
        public int ID { get; set; }

        public int IDEntrenador { get; set; }
        public string Descrip { get; set; }
        public DateTime FechaC { get; set; }
        public DateTime FechaI { get; set; }

        //  Como en la base de datos este campo es de tipo TIME,
        //  en C# podemos representarlo con TimeSpan.
        public int Duracion { get; set; }

        public int EsPlantilla { get; set; }

    }
}
