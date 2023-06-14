using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Gerenciamento
    {
        public decimal CalculaValor(int horas)
        {
            decimal valorTotal = 10 ;
            if (horas > 1)
            {
                valorTotal = valorTotal + (horas * 4);
                return valorTotal;
            }
            else 
                return valorTotal; 
        }

        public DateTime Saida(int horas, DateTime data)
        {
            TimeSpan hora = new TimeSpan(horas, 0, 0);
            data = data.Add(hora);
            return data;
        }
    }
}
