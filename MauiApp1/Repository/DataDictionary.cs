using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinningTrainer.Repository
{
    internal class DataDictionary
    {
        public static Dictionary<string, string> TablesName { get; } = new Dictionary<string, string>
        {
            {"Users", "Usuarios" },            
        };

        public static Dictionary<string, string> UsersFieldNames { get; } = new Dictionary<string, string>
        {

        };
    }
}
