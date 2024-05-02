using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Model
{
    class Sesiones
    {
        public int Id { get; set; }
        public string Descrip { get; set; }
        public int IdEntrenador { get; set; }
        public string DescripEntrenador { get; set; }
        public DateTime Fecha { get; set; }
    }
}
