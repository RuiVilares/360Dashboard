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
    public class productController : ApiController
    {
        //
        // GET: /Artigos/
        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.Artigo> list()
        {
            return Lib_Primavera.PriIntegration.ListaArtigos();
        }


        // GET api/artigo/5    
        [System.Web.Http.HttpPost]
        public Artigo Get_Info(string id)
        {
            id = id.Replace("_", ".");
            Lib_Primavera.Model.Artigo artigo = Lib_Primavera.PriIntegration.GetArtigo(id);            
            if (artigo == null)
            {
                throw new HttpResponseException(
                  Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return artigo;
            }
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.TopCliente> Get_top10c(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.ListaMelhoresClientes(id);
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.Shipment> get_shipments(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.ListaEncomendas(id);
            
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.TopCliente> Get_top10c()
        {
            return Lib_Primavera.PriIntegration.ListaMelhoresClientes();
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.TopProduto> Get_top10p()
        {
            return Lib_Primavera.PriIntegration.ListaMelhoresProdutos();
        }

        [System.Web.Http.HttpPost]
        public IEnumerable<Lib_Primavera.Model.ClientTimeline> Get_Evolution()
        {
            return Lib_Primavera.PriIntegration.sales_evolution();
        }

        [System.Web.Http.HttpPost]
        public List<Tuple<String, double>> products_info()
        {
            return Lib_Primavera.PriIntegration.productsInfo();
        }


    }
}

