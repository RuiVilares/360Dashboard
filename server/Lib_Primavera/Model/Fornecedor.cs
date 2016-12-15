using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Fornecedor
    {
        public string name
        {
            get;
            set;
        }
        public string address
        {
            get;
            set;
        }
        public string post_c
        {
            get;
            set;
        }
        public string city
        {
            get;
            set;
        }
        public string reference
        {
            get;
            set;
        }

        public double pendente
        {
            get;
            set;
        }

        public double backlog
        {
            get;
            set;
        }

    }
}