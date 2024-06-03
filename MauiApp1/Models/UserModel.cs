using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Model
{
    public class UserModel
    { 
        public int Id { get; set; }
        public string CodUsua { get; set; }
        public string Descrip { get; set; }
        public string Contra { get; set; }
        public string PIN { get; set; }
        public string Email { get; set; }
        public string Telef { get; set; }
        public DateTime FechaC { get; set; }
        public DateTime FechaR { get; set; }
        public DateTime FechaV { get; set; }
        public int TipoUsuario { get; set; }
    }
}