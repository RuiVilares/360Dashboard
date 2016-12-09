using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Cliente
    {         
        public string id
        {
            get;
            set;
        }

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

        public double pendentes
        {
            get;
            set;
        }

        public double divida
        {
            get;
            set;
        }

    }
}