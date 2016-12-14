using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;


namespace FirstREST.Controllers
{
    public class RhController : ApiController
    {     
        [System.Web.Http.HttpGet]
        public RecursosHumanos getInfo()
        {
            return Lib_Primavera.PriIntegration.getRecursosHumanos();
        }

    }
}
