using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class ClientTimeline
    {
        public int date
        {
            get;
            set;
        }

        public double value
        {
            get;
            set;
        }

        public double valuePrev
        {
            get;
            set;
        }
    }
}