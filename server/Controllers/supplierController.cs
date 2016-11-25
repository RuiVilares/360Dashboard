using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class supplierController : ApiController
    {
        //
        // GET: /supplier/
        [System.Web.Http.HttpGet]
        public Lib_Primavera.Model.Fornecedor detail(string id)
        {
            return Lib_Primavera.PriIntegration.getFornecedor(id);
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.Fornecedor> list()
        {
            return Lib_Primavera.PriIntegration.listaFornecedor();
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Tuple<DateTime, double>> freq(string id)
        {
            return Lib_Primavera.PriIntegration.ranges(id);
        }
                [System.Web.Http.HttpGet]

        public IEnumerable<Lib_Primavera.Model.FornecedorTopProduct> topprod(string id)
        {
            return Lib_Primavera.PriIntegration.forn_top_prod(id);
        }

    }
}
