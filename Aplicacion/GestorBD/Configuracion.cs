using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.BD
{
    public class Configuracion
    {
        public Dictionary<string, string> dicc = new Dictionary<string, string>()
        {
            {"DATA SOURCE", "localhost:1521/XE"},
            {"PASSWORD", "123"},
            {"USER ID", "csharp"}
        };

        string code = "";

        public string Generate()
        {
            code = "";

            foreach (var item in dicc)
            {
                Add(item.Key, item.Value);
            }

            return code;
        }

        void Add(string variable, string valor)
        {
            code += variable + " = " + valor + "; ";
        }
        
    }
}
