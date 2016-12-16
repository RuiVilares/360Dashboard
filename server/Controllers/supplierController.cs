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
        [System.Web.Http.HttpPost]
        public Lib_Primavera.Model.Fornecedor detail(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.getFornecedor(id);
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.Fornecedor> list()
        {
            return Lib_Primavera.PriIntegration.listaFornecedor();
        }

        [System.Web.Http.HttpPost]
        public List<ClientTimeline> freq(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.ranges(id);
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.FornecedorTopProduct> topprod(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.forn_top_prod(id);
        }

        [System.Web.Http.HttpPost]
        public FornecedoresInfo get_provider_info()
        {
            return Lib_Primavera.PriIntegration.listaFornecedores();
        }

        [System.Web.Http.HttpPost]
        public List<Tuple<string, string, double>> get_toppendentes()
        {
            return Lib_Primavera.PriIntegration.fornecedoresPendentes();
        }

        [System.Web.Http.HttpPost]
        public List<Lib_Primavera.Model.TopFornecedor> get_topf()
        {
            return Lib_Primavera.PriIntegration.MelhoresFornecedores();
        }
    }
}
