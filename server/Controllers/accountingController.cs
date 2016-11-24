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
        public IEnumerable<Lib_Primavera.Model.Conta> getAtivos_Passivos()
        {
            return null;
        
        }
    }
}
