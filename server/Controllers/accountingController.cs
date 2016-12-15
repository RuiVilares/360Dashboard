using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class accountingController : ApiController
    {
        [System.Web.Http.HttpPost]
        public Tuple<double, double> getAtivos_Passivos()
        {
            return Lib_Primavera.PriIntegration.getAtivos_Passivos();
        }

        [System.Web.Http.HttpPost]
        public List<Lib_Primavera.Model.Conta> getBalancete()
        {
            return Lib_Primavera.PriIntegration.getBalancete();
        }

        [System.Web.Http.HttpPost]
        public List<List<Tuple<String, Decimal>>> getBalanco()
        {
            return Lib_Primavera.PriIntegration.getBalanco();
        }
        [System.Web.Http.HttpPost]
        public List<List<Tuple<String, Decimal>>> getDemoResultados()
        {
            return Lib_Primavera.PriIntegration.getDemoResultados();
        }

    }
}
