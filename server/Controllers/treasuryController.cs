using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class treasuryController : ApiController
    {
        [System.Web.Http.HttpGet]
        public List<Tuple<string, double>> get_areceber()
        {
            return Lib_Primavera.PriIntegration.get_Receber();

        }

        [System.Web.Http.HttpGet]
        public List<Tuple<string, double>> get_apagar()
        {
            return Lib_Primavera.PriIntegration.get_Pagar();
        }
    }
}
