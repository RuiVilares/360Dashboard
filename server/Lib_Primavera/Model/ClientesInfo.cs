using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FirstREST.Lib_Primavera.Model
{
    public class ClientesInfo
    {
        public int numClientes
        {
            get;
            set;
        }

        public double valorAberto
        {
            get;
            set;
        }

        public double valorFaturado
        {
            get;
            set;

        }

        public List<ClientTimeline> topEndividados
        {
            get;
            set;

        }
    }
}