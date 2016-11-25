using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class TopProduto
    {
        public string name
        {
            set;
            get;
        }
        public double valor
        {
            set;
            get;
        }

        public double sales_p
        {
            set;
            get;
        }

        public double quantidade
        {
            set;
            get;
        }

        public string reference
        {
            get;
            set;
        }
    }
}