using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class OpcoesBase: Attribute
    {
        public bool ChavePrimaria { get; set; }
        public bool UsaBD { get; set; }
        public bool UsaBusca { get; set; }
    }
}
