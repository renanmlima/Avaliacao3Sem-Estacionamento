using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Veiculo : Base
    {

        public Veiculo() 
        {
            NumeroVaga = new List<string>();      
        }
        private List<string> NumeroVaga { get; set; }

        [OpcoesBase(ChavePrimaria = true, UsaBD = true, UsaBusca = true)]
        public int Id{ get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Placa { get; set; }
        [OpcoesBase(UsaBD = true)]
        public string Modelo { get; set; }

        [OpcoesBase(UsaBD = true)]
        public int Horario { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string DataEntrada { get; set; }
        [OpcoesBase(UsaBD = true)]
        public string DataSaida{ get; set; }

        [OpcoesBase(UsaBD = true)]
        public decimal Valor { get; set; }

        [OpcoesBase(UsaBD = true)]
        public string Pago { get; set; }
    }
}
