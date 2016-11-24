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
        [System.Web.Http.HttpGet]
        public Tuple<double, double> getAtivos_Passivos()
        {
            return Lib_Primavera.PriIntegration.getAtivos_Passivos();
        
        }

        public List<Lib_Primavera.Model.Conta> getBalancete()
        {
            return Lib_Primavera.PriIntegration.getBalancete();
        }
    }
}
