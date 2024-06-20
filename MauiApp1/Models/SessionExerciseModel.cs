using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Models
{
    public class SessionExerciseModel
    {
        public int ID { get; set; }
        public int IDSesion { get; set; }
        public int IDMovimiento { get; set; }
        public string DescripMov { get; set; }
        public string ZonaDeEnergia { get; set; }
        public string PosicionManos { get; set; }
        public int RPMMed { get; set; }
        public int RPMFin { get; set; }
        public int DuracionMin { get; set; }
    }
}
