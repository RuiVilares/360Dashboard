using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstREST.Lib_Primavera.Model
{
    public class Shipment
    {
        public string product
        {
            get;
            set;
        }
        public double quantity
        {
            get;
            set;
        }
        public DateTime shipmentDate
        {
            get;
            set;
        }
        public string client
        {
            get;
            set;
        }
    }
}
