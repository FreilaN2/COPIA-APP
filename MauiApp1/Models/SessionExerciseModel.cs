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
        public int PosicionManos { get; set; }
        public short TipoEjercicio { get; set; }
        public int Fase { get; set; }

        // public int RPMMin { get; set; } // Si se requiere, descomentar esta línea
        public int RPMMed { get; set; }
        public int RPMFin { get; set; }
        public int DuracionSeg { get; set; }

    }
}
