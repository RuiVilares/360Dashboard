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
            return Lib_Primavera.PriIntegration.get_client_topprod(id);           
        }

        // GET api/cliente/5    
        [System.Web.Http.HttpGet]
        public IEnumerable<Lib_Primavera.Model.ClientTimeline> range(string id)
        {
            return Lib_Primavera.PriIntegration.ClientTimeline(id);
        }


    }
}
