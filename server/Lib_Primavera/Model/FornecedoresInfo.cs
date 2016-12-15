using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class FornecedoresInfo
    {
        public int numFornecedores
        {
            get;
            set;
        }

        public double valoresPendentes
        {
            get;
            set;
        }

        public double valoresBacklog
        {
            get;
            set;
        }
    }
}