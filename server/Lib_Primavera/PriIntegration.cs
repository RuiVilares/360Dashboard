using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using ADODB;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {


        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {


            StdBELista objList;

            List<Model.Cliente> listClientes = new List<Model.Cliente>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Fac_Cp, Fac_Cploc, Fac_Mor FROM  CLIENTES WHERE Cliente != 'VD'");


                while (!objList.NoFim())
                {
                    Model.Cliente cliente = new Model.Cliente();
                    string str = objList.ToString();
                    cliente.id = objList.Valor("Cliente");                    
                    cliente.name = objList.Valor("Nome");
                    cliente.post_c = objList.Valor("Fac_Cp");
                    cliente.city = objList.Valor("Fac_Cploc");
                    cliente.address  = objList.Valor("Fac_Mor");
                    listClientes.Add(cliente);
                    objList.Seguinte();
                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {


            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.id = objCli.get_Cliente();
                    myCli.name = objCli.get_Nome();
                    myCli.address = objCli.get_Morada();
                    myCli.post_c = objCli.get_CodigoPostal();
                    myCli.city = "coimbra";
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static IEnumerable<Lib_Primavera.Model.TopClienteProduct> get_client_topprod(string codCliente)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    StdBELista productList = PriEngine.Engine.Consulta("SELECT SUM(LinhasDoc.Quantidade) AS Quant, Artigo.Artigo AS Artigo, SUM(LinhasDoc.PrecoLiquido) AS Preco FROM Artigo, LinhasDoc, CabecDoc WHERE CabecDoc.Entidade = '"+codCliente+"' AND LinhasDoc.IdCabecDoc = CabecDoc.id AND CabecDoc.TipoDoc = 'FA' AND LinhasDoc.Artigo = Artigo.Artigo GROUP BY CabecDoc.Entidade, Artigo.Artigo ORDER BY Preco DESC");
                    List<Model.TopClienteProduct> resultado = new List<Model.TopClienteProduct>();
                    double sum = 0;
                    int i = 0;
                    while (i != 10 && !productList.NoFim())
                    {
                        sum += productList.Valor("Preco");
                        productList.Seguinte();
                        i++;
                    }
                    productList.Inicio();
                    i = 0;
                    while (i != 10 && !productList.NoFim())
                    {
                        Model.TopClienteProduct top = new Model.TopClienteProduct();
                        top.reference = productList.Valor("Artigo");
                        top.sales_p = (productList.Valor("Preco") / sum) * 100.0;
                        resultado.Add(top);
                        productList.Seguinte();
                        i++;
                    }
                    return resultado;
                }

            }
            return null;

        }

        public static IEnumerable<Lib_Primavera.Model.ClientTimeline> ClientTimeline(string id)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Clientes.Existe(id) == true)
                {
                    StdBELista objList =  PriEngine.Engine.Consulta("SELECT LinhasDoc.Data as Data, SUM(LinhasDoc.PrecoLiquido) as Pag FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.TipoDoc = 'FA' AND CabecDoc.Entidade = '" + id + "' GROUP BY LinhasDoc.Data ORDER BY LinhasDoc.Data DESC");
                    List<Model.ClientTimeline> list = new List<Model.ClientTimeline>();

                    while (!objList.NoFim())
                    {
                        Model.ClientTimeline timeline = new Model.ClientTimeline();
                        timeline.value = objList.Valor("Pag");
                        timeline.date = objList.Valor("Data");
                        list.Add(timeline);
                        objList.Seguinte();
                    }
                    return list;

                }

            }
            return null;
        }
         



        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                
                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    StdBELista priceObj = PriEngine.Engine.Consulta("SELECT PVP1 FROM ArtigoMoeda WHERE ArtigoMoeda.Artigo = '" + codArtigo + "' AND ArtigoMoeda.Moeda = 'EUR'");
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.reference = objArtigo.get_Artigo();
                    myArt.name = objArtigo.get_Descricao();
                    myArt.retail = priceObj.Valor("PVP1");
                    myArt.price = objArtigo.get_PCUltimo();
                    myArt.tax = objArtigo.get_IVA();
                    myArt.profit_margin =  (myArt.retail - myArt.price)/myArt.retail;
                    

                    return myArt;
                }

            }
            else
            {
                return null;
            }

        }

           

        public static List<Model.TopCliente> ListaMelhoresClientes(string idArtigo)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Entidade AS Entidade,  SUM(LinhasDoc.PrecUnit*LinhasDoc.Quantidade) AS Total from LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.TipoDoc = 'FA' AND Artigo = '" + idArtigo + "' GROUP BY CabecDoc.Entidade ORDER BY Total DESC");
                Model.TopCliente cliente = new Model.TopCliente();
                List<Model.TopCliente> listaClientes = new List<Model.TopCliente>();
                double sum = 0; 

                while (!objList.NoFim())
                {                  
                    sum += objList.Valor("Total");
                    objList.Seguinte();
                }

                objList.Inicio();

                while (!objList.NoFim())
                {
                    cliente = new Model.TopCliente();
                    cliente.name = objList.Valor("Entidade");
                    cliente.valor = objList.Valor("Total");
                    cliente.sales_p = (cliente.valor / sum) * 100;
                    listaClientes.Add(cliente);
                    objList.Seguinte();
                }



                return listaClientes;
            }
            return null;

        }

        public static List<Model.Artigo> ListaArtigos()
        {

            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.name = objList.Valor("artigo");
                    art.reference = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }

        public static List<Model.Shipment> ListaEncomendas(string id)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList;
                Model.Shipment ship = new Model.Shipment();

                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Entidade AS Entidade, LinhasDoc.DataSaida as DataSaida, (LinhasDocStatus.Quantidade - LinhasDocStatus.QuantTrans) AS Patinhos FROM CabecDoc, LinhasDoc, LinhasDocStatus WHERE LinhasDocStatus.Quantidade != LinhasDocStatus.QuantTrans AND LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND CabecDoc.TipoDoc = 'ECL' AND LinhasDoc.Artigo = '"+id+"'");
                List<Model.Shipment> shipList = new List<Model.Shipment>();
                while(!objList.NoFim()){
                    ship = new Model.Shipment();
                    ship.client = objList.Valor("Entidade");
                    ship.product = id;
                    ship.shipmentDate = objList.Valor("DataSaida");
                    ship.quantity = objList.Valor("Patinhos");
                    shipList.Add(ship);
                    objList.Seguinte();
                }

                return shipList;
            }
            return null;

        }

        #endregion Artigo



        #region DocCompra


        public static List<Model.DocCompra> VGR_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;

                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }


        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();


            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    //PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR,rl);
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        #endregion DocCompra


        #region DocsVenda

        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();

            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();

            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();

            try
            {
                if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    //PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                    // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    //PriEngine.Engine.Comercial.Vendas.Edita Actualiza(myEnc, "Teste");
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }



        public static List<Model.DocVenda> Encomendas_List()
        {

            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }




        public static Model.DocVenda Encomenda_Get(string numdoc)
        {


            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {


                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }

        #endregion DocsVenda

        #region Contabilidade

        public static Tuple<double, double> getAtivos_Passivos()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList;
                //Model.Conta conta = new Model.Conta();
                double sumAtivos = 0;
                double sumPassivos = 0;

                string ano = "2015";
                objList = PriEngine.Engine.Consulta("SELECT AcumuladosContas.Conta, SUM(Mes00Cr+Mes01Cr+Mes02Cr+Mes03Cr+Mes04Cr+Mes05Cr+Mes06Cr+Mes07Cr+Mes08Cr+Mes09Cr+Mes10Cr+Mes11Cr+Mes12Cr+Mes13Cr+Mes14Cr+Mes15Cr) AS MesCr, SUM(Mes00Db+Mes01Db+Mes02Db+Mes03Db+Mes04Db+Mes05Db+Mes06Db+Mes07Db+Mes08Db+Mes09Db+Mes10Db+Mes11Db+Mes12Db+Mes13Db+Mes14Db+Mes15Db) AS MesDb FROM AcumuladosContas WITH (NOLOCK) INNER JOIN PlanoContas WITH (NOLOCK) ON AcumuladosContas.Ano=PlanoContas.Ano And AcumuladosContas.Conta = PlanoContas.Conta  WHERE 1=1  AND (((AcumuladosContas.Conta >= '0' AND AcumuladosContas.Conta <= '999999999') AND NOT ((AcumuladosContas.Conta >= '00' AND AcumuladosContas.Conta <= '0999999999') OR (AcumuladosContas.Conta >= '90' AND AcumuladosContas.Conta <= '9999999999'))) OR (AcumuladosContas.Conta >= '00' AND AcumuladosContas.Conta <= '0999999999') OR (AcumuladosContas.Conta >= '90' AND AcumuladosContas.Conta <= '9999999999')) AND AcumuladosContas.Ano = " + ano + " AND AcumuladosContas.Moeda = 'EUR' AND TipoConta = 'R' GROUP BY AcumuladosContas.Conta, Descricao, TipoConta ORDER BY AcumuladosContas.Conta");
                while (!objList.NoFim())
                {
                    int codConta = Int32.Parse(objList.Valor("Conta"));
                    int tipoConta = codConta/10;
                    int subTipoConta = codConta % 10;
                    switch (tipoConta)
                    {
                        case 1:
                            sumAtivos += objList.Valor("MesCr") - objList.Valor("MesDb");
                            break;
                        case 2:
                            if(subTipoConta == 2 || subTipoConta == 3 || subTipoConta == 4){
                                sumPassivos += objList.Valor("MesCr") - objList.Valor("MesDb");
                            }
                            else
                            {
                                sumAtivos += objList.Valor("MesCr") - objList.Valor("MesDb");
                            }
                            break;
                        case 3:
                        case 4:
                            sumAtivos += objList.Valor("MesCr") - objList.Valor("MesDb");
                            break;
                        default:
                            break;
                    }
                }
                return new Tuple<double,double>(sumAtivos, sumPassivos);
            }
        }

        #endregion Contabilidade
    }
}