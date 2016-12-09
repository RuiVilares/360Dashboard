using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Artigo
    {
        public string name
        {
            get;
            set;
        }

        public string reference
        {
            get;
            set;
        }

        public double price
        {
            get;
            set;
        }

        public List<double> retail
        {
            get;
            set;
        }

        public string tax
        {
            get;
            set;
        }

        public List<double> profit_margin
        {
            get;
            set;
        }

        public double stk
        {
            get;
            set;
        }

        public double stkValue
        {
            get;
            set;
        }

        public string unit
        {
            get;
            set;
        }
    }
}