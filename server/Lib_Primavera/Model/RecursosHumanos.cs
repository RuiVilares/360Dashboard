using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class RecursosHumanos
    {
        public IEnumerable<CustosMensais> custosMensais
        {
            get;
            set;
        }

        public int numFuncionarios
        {
            get;
            set;
        }

        public double vencimentoMensal
        {
            get;
            set;
        }
        public double vencimentoMediano
        {
            get;
            set;
        }
    }
}