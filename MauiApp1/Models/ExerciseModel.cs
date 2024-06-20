using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Models
{
    public class ExerciseModel
    {
        public int ID { get; set; }
        public string Descrip { get; set; }
        public string ZonasDeEnergia { get; set; }
        public int RPMMin { get; set; }

        public int RPMMax { get; set; }
        public string PosicionesDeManos { get; set; }

        
    }
}
