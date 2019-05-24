using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterApi.Clases
{
    public class Cancelar
    {        
        public string Usuario { get; set; }        
        public string Password { get; set; }        
        public string RFC { get; set; }        
        public string PFX { get; set; }        
        public string PFXPassword { get; set; }        
        public List<string> UUIDs { get; set; }
    }
}
