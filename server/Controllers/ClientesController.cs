using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class ClientesController : ApiController
    {
        //
        // GET: /Clientes/
        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.Cliente> list()
        {
                return Lib_Primavera.PriIntegration.ListaClientes();
        }


        // GET api/cliente/5    
        [System.Web.Http.HttpGet]
        public Cliente detail(string id)
        {
            id = id.Replace("_", ".");
            Lib_Primavera.Model.Cliente cliente = Lib_Primavera.PriIntegration.GetCliente(id);
            if (cliente == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return cliente;
            }
        }

        // GET api/cliente/5    
        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.TopClienteProduct> topprod(string id) 
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.get_client_topprod(id);           
        }

        // GET api/cliente/5    
        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.ClientTimeline> range(string id)
        {
            id = id.Replace("_", ".");
            return Lib_Primavera.PriIntegration.ClientTimeline(id);
        }

        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.TopCliente> get_top10c()
        {
            return Lib_Primavera.PriIntegration.ListaMelhoresClientes();
        }

        [System.Web.Http.HttpGet]
        public List<Tuple<string, string, double>> get_top10divida()
        {
            return Lib_Primavera.PriIntegration.ListaDividaClientes();
        }

        [System.Web.Http.HttpGet]
        public ClientesInfo get_client_info()
        {
            return Lib_Primavera.PriIntegration.listaClientes();
        }
    }
}
