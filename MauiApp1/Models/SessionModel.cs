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

        public int Duracion { get; set; }

        public int EsPlantilla { get; set; }

    }
}
