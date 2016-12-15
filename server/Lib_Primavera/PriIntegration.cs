using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS900;
using Interop.StdPlatBS900;
using Interop.StdBE900;
using Interop.GcpBE900;
using Interop.ICblBS900;

namespace FirstREST.Lib_Primavera
{
    public class PriIntegration
    {

        #region RecursosHumanos
        public static Model.RecursosHumanos getRecursosHumanos()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista custos = PriEngine.Engine.Consulta("Select DataMov, SUM(Valor+ValorSegSocialEntPatronal+ValorSeguroEntPatronal) as Value FROM MovimentosFuncionarios WHERE (MovimentosFuncionarios.TipoTabela = 1 OR MovimentosFuncionarios.TipoTabela = 0) GROUP BY DataMov");
                List<Model.CustosMensais> custosConv = new List<Model.CustosMensais>();

                while (!custos.NoFim())
                {
                    Model.CustosMensais custo = new Model.CustosMensais();
                    custo.mes = custos.Valor("DataMov");
                    custo.custo = custos.Valor("Value");
                    custosConv.Add(custo);
                    custos.Seguinte();
                }

                custos = PriEngine.Engine.Consulta("SELECT F.Codigo, F.Nome, F.Categoria FROM Funcionarios F INNER JOIN Situacoes S on F.Situacao = S.Situacao  WHERE (S.Tipo < 2 OR S.Tipo > 2)  GROUP BY F.Categoria, F.Codigo, F.Nome");
                Model.RecursosHumanos rh = new Model.RecursosHumanos();
                rh.custosMensais = custosConv;
                rh.numFuncionarios = custos.NumLinhas();
                custos = PriEngine.Engine.Consulta("SELECT  AVG(F.VencimentoMensal) as avg FROM Funcionarios F INNER JOIN Situacoes S on F.Situacao = S.Situacao  WHERE (S.Tipo < 2 OR S.Tipo > 2)  ");
                rh.vencimentoMensal = custos.Valor("avg");
                custos = PriEngine.Engine.Consulta("SELECT  F.VencimentoMensal as Vencimento FROM Funcionarios F INNER JOIN Situacoes S on F.Situacao = S.Situacao  WHERE (S.Tipo < 2 OR S.Tipo > 2)  ");

                List<Double> vencimentos = new List<Double>();
                while (!custos.NoFim())
                {
                    vencimentos.Add(custos.Valor("Vencimento"));
                    custos.Seguinte();
                }

                vencimentos.Sort();
                double median;
                if (vencimentos.Count() % 2 == 0)
                {
                    median = (vencimentos[vencimentos.Count() / 2] + vencimentos[(vencimentos.Count() / 2) + 1]) / 2;
                }
                else
                {
                    median = vencimentos[vencimentos.Count() / 2];
                }
                rh.vencimentoMediano = median;
                return rh;
            }
            return null;

        }
        #endregion  

        # region Cliente

        public static List<Model.ClientTimeline> sales_evolution()
        {


            StdBELista obj1;
            StdBELista obj2;
            StdBELista obj3;
            StdBELista obj4;

            StdBELista obj1c;
            StdBELista obj2c;
            StdBELista obj3c;
            StdBELista obj4c;


            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                obj1 = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2016-01-01' AND CabecDoc.Data <= '2016-03-31' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj2 = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2016-04-01' AND CabecDoc.Data <= '2016-06-30' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj3 = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2016-07-01' AND CabecDoc.Data <= '2016-09-30' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj4 = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2016-10-01' AND CabecDoc.Data <= '2016-12-31' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");

                obj1c = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2015-01-01' AND CabecDoc.Data <= '2015-03-31' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj2c = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2015-04-01' AND CabecDoc.Data <= '2015-06-30' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj3c = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2015-07-01' AND CabecDoc.Data <= '2015-09-30' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");
                obj4c = PriEngine.Engine.Consulta(" SELECT SUM(TotalMerc) As TotalMerc, SUM(TotalMerc - TotalDesc) As TotalLiq FROM   ((CabecDoc CabecDoc INNER JOIN DocumentosVenda DocumentosVenda ON CabecDoc.TipoDoc=DocumentosVenda.Documento) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN EstadosConta EstadosConta ON (DocumentosVenda.Estado=EstadosConta.Estado) AND (DocumentosVenda.TipoConta=EstadosConta.TipoConta) WHERE  CabecDoc.Data >= '2015-10-01' AND CabecDoc.Data <= '2015-12-31' AND (CabecDoc.TipoDoc=N'FA' OR CabecDoc.TipoDoc=N'NC' OR CabecDoc.TipoDoc=N'ORC' OR CabecDoc.TipoDoc=N'VD') AND CabecDocStatus.Anulado=0");

                List<Model.ClientTimeline> list = new List<Model.ClientTimeline>();
                Model.ClientTimeline time1 = new Model.ClientTimeline();
                time1.value = obj1.Valor("TotalLiq");
                time1.valuePrev = obj1c.Valor("TotalLiq");
                time1.date = 1;

                Model.ClientTimeline time2 = new Model.ClientTimeline();
                time2.value = obj2.Valor("TotalLiq");
                time2.valuePrev = obj2c.Valor("TotalLiq");
                time2.date = 2;

                Model.ClientTimeline time3 = new Model.ClientTimeline();
                time3.value = obj3.Valor("TotalLiq");
                time3.valuePrev = obj1c.Valor("TotalLiq");
                time3.date = 3;

                Model.ClientTimeline time4 = new Model.ClientTimeline();
                time4.value = obj4.Valor("TotalLiq");
                time4.valuePrev = obj4c.Valor("TotalLiq");
                time4.date = 4;

                list.Add(time1);
                list.Add(time2);
                list.Add(time3);
                list.Add(time4);

                return list;
            }
            return null;
        }

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
                    cliente.address = objList.Valor("Fac_Mor");
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
                    StdBELista objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Entidade, SUM((PrecUnit*(1-(DescEntidade+DescPag)/100))*(LinhasDoc.Quantidade-LinhasDocStatus.QuantTrans)) as Quantia FROM   (((((((CabecDoc CabecDoc INNER JOIN LinhasDoc LinhasDoc ON CabecDoc.Id=LinhasDoc.IdCabecDoc) LEFT OUTER JOIN Clientes Clientes ON CabecDoc.Entidade=Clientes.Cliente) LEFT OUTER JOIN OutrosTerceiros OutrosTerceiros ON CabecDoc.Entidade=OutrosTerceiros.Terceiro) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN Artigo Artigo ON LinhasDoc.Artigo=Artigo.Artigo) INNER JOIN LinhasDocStatus LinhasDocStatus ON LinhasDoc.Id=LinhasDocStatus.IdLinhasDoc) LEFT OUTER JOIN LinhasDocStatus LinhasDocStatus_1 ON LinhasDoc.IdLinhaPai=LinhasDocStatus_1.IdLinhasDoc) LEFT OUTER JOIN Artigo Artigo_1 ON Artigo.ArtigoPai=Artigo_1.Artigo WHERE  CabecDoc.TipoEntidade=N'C' AND CabecDocStatus.Fechado=0 AND LinhasDocStatus.Fechado=0 AND LinhasDocStatus.EstadoTrans<>N'T' AND (LinhasDocStatus.QuantTrans<LinhasDocStatus.Quantidade AND LinhasDocStatus.Quantidade>=0 OR LinhasDocStatus.Quantidade<0 AND LinhasDocStatus.QuantTrans>LinhasDocStatus.Quantidade) AND  NOT (CabecDocStatus.Estado=N'R' OR CabecDocStatus.Estado=N'T') AND CabecDocStatus.Anulado=0 AND CabecDoc.TipoDoc=N'ECL' AND CabecDoc.Entidade='"+codCliente+"' GROUP BY CabecDoc.Entidade");
                 
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);                    
                    myCli.id = objCli.get_Cliente();
                    myCli.name = objCli.get_Nome();
                    myCli.address = objCli.get_Morada();
                    myCli.post_c = objCli.get_CodigoPostal();
                    myCli.city = objCli.get_LocalidadeCodigoPostal();
                    try
                    {
                        myCli.pendentes = objList.Valor("Quantia");
                    }
                    catch
                    {
                        myCli.pendentes = 0;
                    }
                    myCli.divida = objCli.get_DebitoContaCorrente();
                    
                    
                 
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
                    StdBELista productList = PriEngine.Engine.Consulta("SELECT SUM(LinhasDoc.Quantidade) AS Quant, Artigo.Artigo AS Artigo, SUM(LinhasDoc.PrecoLiquido) AS Preco FROM Artigo, LinhasDoc, CabecDoc WHERE CabecDoc.Entidade = '" + codCliente + "' AND LinhasDoc.IdCabecDoc = CabecDoc.id AND CabecDoc.TipoDoc = 'FA' AND LinhasDoc.Artigo = Artigo.Artigo GROUP BY CabecDoc.Entidade, Artigo.Artigo ORDER BY Preco DESC");
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
                    StdBELista objList = PriEngine.Engine.Consulta("EXEC GCP_LST_RESUMO_VENDAS        @Tabela                         = '##ResumoVendasUuserP1672Hd30311826378491387fda695004379d4TOyhSDXF4eTU',        @Campos                         = 'NULL AS GROUP1,Mes,Trimestre,DescontosAnoAct,AcumDescontosAnoAct,LiquidoAnoAnt,AcumLiquidoAnoAnt,LiquidoAnoAct,AcumLiquidoAnoAct,VariacaoLiquido,VariacaoLiquidoPercentual,VariacaoAcmLiquido,VariacaoAcmLiquidoPercentual,DescontosAnoAnt,AcumDescontosAnoAnt,OutrosAnoAct,AcumOutrosAnoAct,OutrosAnoAnt,AcumOutrosAnoAnt,QuantidadeAnoAnt,MercadoriaAnoAct,AcumMercadoriaAnoAnt,QuantidadeAnoAct,MercadoriaAnoAnt,MargemAnoAnt,MargemAnoAct,AcumMercadoriaAnoAct,VariacaoAcmDescontos,VariacaoAcmDescontosPercentual,VariacaoAcmMercadoria,VariacaoAcmMercadoriaPercentual,VariacaoAcmOutros,VariacaoAcmOutrosPercentual,VariacaoDescontos,VariacaoDescontosPercentual,VariacaoMercadoria,VariacaoMercadoriaPercentual,VariacaoOutros,VariacaoOutrosPercentual,NULL AS DATAFILLCOL',        @MoedaVisualizacao              = 'EUR',        @MoedaBase                      = 1,        @SentidoCambio                  = 0,        @CambioActualHistorico          = 1,        @MoedaStocks                    = 'EUR',        @MoedaStocksBase                = 1,        @DocConvertidos                     = '1',        @WhereRestricoes                = '(((doc.Entidade = ''"+id+"'') AND doc.TipoEntidade=''C''))',        @Documentos                     = '( ''DV'', ''FA'', ''FAI'', ''FI'', ''FM'', ''FO'', ''FR'', ''NC'',  ''VD'')',        @AnoReferencia                      = '12/31/2016',       @AnoComparacao                      = '1/1/2015',       @MesFimExercicio                      = 12");
                    List<Model.ClientTimeline> list = new List<Model.ClientTimeline>();

                    while (!objList.NoFim())
                    {
                        Model.ClientTimeline timeline = new Model.ClientTimeline();
                        timeline.value = objList.Valor("LiquidoAnoAct");
                        timeline.valuePrev = objList.Valor("LiquidoAnoAnt");
                        timeline.date = objList.Valor("Mes");
                        list.Add(timeline);
                        objList.Seguinte();
                    }
                    return list;

                }

            }
            return null;


        }

        public static List<Model.TopCliente> ListaMelhoresClientes()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Entidade AS Entidade,  SUM(LinhasDoc.PrecUnit*LinhasDoc.Quantidade) AS Total from LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.TipoDoc = 'FA' GROUP BY CabecDoc.Entidade ORDER BY Total DESC");
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

        public static List<Tuple<string, string, double>> ListaDividaClientes()
        {
            GcpBECliente objCli = new GcpBECliente();
            List<Tuple<string, string, double>> result = new List<Tuple<string, string, double>>(); 
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome FROM Clientes");

                while (!objList.NoFim())
                {
                    string nome = objList.Valor("Nome");
                    string cod = objList.Valor("Cliente");
                    double divida = 0.0;
                    if (PriEngine.Engine.Comercial.Clientes.Existe(cod) == true)
                    {
                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cod);
                        divida = objCli.get_DebitoContaCorrente();
                    }
                    Tuple<string, string, double> client = Tuple.Create(nome, cod, divida);
                    result.Add(client);
                    objList.Seguinte();
                }
                return result;
            }
            return null;

        }

        public static Model.ClientesInfo listaClientes()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                Model.ClientesInfo result = new Model.ClientesInfo();
                StdBELista vl = PriEngine.Engine.Consulta("SELECT Count(Clientes.Nome) as NumCliente FROM Clientes");
                result.numClientes = vl.Valor("NumCliente");
                vl = PriEngine.Engine.Consulta("SELECT SUM((LinhasDocStatus.Quantidade-LinhasDocStatus.QuantTrans)*(LinhasDoc.PrecUnit - (LinhasDoc.PrecUnit*(LinhasDoc.Desconto1/100)))) As Value FROM   (((((((CabecDoc CabecDoc INNER JOIN LinhasDoc LinhasDoc ON CabecDoc.Id=LinhasDoc.IdCabecDoc) LEFT OUTER JOIN Clientes Clientes ON CabecDoc.Entidade=Clientes.Cliente) LEFT OUTER JOIN OutrosTerceiros OutrosTerceiros ON CabecDoc.Entidade=OutrosTerceiros.Terceiro) INNER JOIN CabecDocStatus CabecDocStatus ON CabecDoc.Id=CabecDocStatus.IdCabecDoc) LEFT OUTER JOIN Artigo Artigo ON LinhasDoc.Artigo=Artigo.Artigo) INNER JOIN LinhasDocStatus LinhasDocStatus ON LinhasDoc.Id=LinhasDocStatus.IdLinhasDoc) LEFT OUTER JOIN LinhasDocStatus LinhasDocStatus_1 ON LinhasDoc.IdLinhaPai=LinhasDocStatus_1.IdLinhasDoc) LEFT OUTER JOIN Artigo Artigo_1 ON Artigo.ArtigoPai=Artigo_1.Artigo WHERE  CabecDoc.TipoEntidade=N'C' AND CabecDocStatus.Fechado=0 AND LinhasDocStatus.Fechado=0 AND LinhasDocStatus.EstadoTrans<>N'T' AND (LinhasDocStatus.QuantTrans<LinhasDocStatus.Quantidade AND LinhasDocStatus.Quantidade>=0 OR LinhasDocStatus.Quantidade<0 AND LinhasDocStatus.QuantTrans>LinhasDocStatus.Quantidade) AND  NOT (CabecDocStatus.Estado=N'R' OR CabecDocStatus.Estado=N'T') AND CabecDocStatus.Anulado=0 AND CabecDoc.TipoDoc=N'ECL' AND (LinhasDoc.DataEntrega>= '2015-01-26 00:00:00' AND LinhasDoc.DataEntrega< '2017-01-01 00:00:00') AND (LinhasDocStatus_1.Fechado IS  NULL  OR LinhasDocStatus_1.Fechado IS  NOT  NULL  AND LinhasDocStatus_1.Fechado=0) AND (LinhasDoc.TipoLinha=N'65' OR LinhasDoc.TipoLinha=N'91' OR (LinhasDoc.TipoLinha>=N'10' AND LinhasDoc.TipoLinha<=N'29')) AND Artigo.TratamentoDim = 0");
                result.valorAberto = vl.Valor("Value");
                vl = PriEngine.Engine.Consulta("   EXEC GCP_LST_RESUMO_VENDAS        @Tabela                         = '##ResumoVendasUuserP1448Hd30311826378491387fda695004379d40TBNTsnew5cO',        @Campos                         = 'NULL AS GROUP1,Mes,Trimestre,DescontosAnoAct,AcumDescontosAnoAct,LiquidoAnoAnt,AcumLiquidoAnoAnt,LiquidoAnoAct,AcumLiquidoAnoAct,VariacaoLiquido,VariacaoLiquidoPercentual,VariacaoAcmLiquido,VariacaoAcmLiquidoPercentual,DescontosAnoAnt,AcumDescontosAnoAnt,OutrosAnoAct,AcumOutrosAnoAct,OutrosAnoAnt,AcumOutrosAnoAnt,QuantidadeAnoAnt,MercadoriaAnoAct,AcumMercadoriaAnoAnt,QuantidadeAnoAct,MercadoriaAnoAnt,MargemAnoAnt,MargemAnoAct,AcumMercadoriaAnoAct,VariacaoAcmDescontos,VariacaoAcmDescontosPercentual,VariacaoAcmMercadoria,VariacaoAcmMercadoriaPercentual,VariacaoAcmOutros,VariacaoAcmOutrosPercentual,VariacaoDescontos,VariacaoDescontosPercentual,VariacaoMercadoria,VariacaoMercadoriaPercentual,VariacaoOutros,VariacaoOutrosPercentual,NULL AS DATAFILLCOL',        @MoedaVisualizacao              = 'EUR',        @MoedaBase                      = 1,        @SentidoCambio                  = 0,        @CambioActualHistorico          = 1,        @MoedaStocks                    = 'EUR',        @MoedaStocksBase                = 1,        @DocConvertidos                     = '1',        @WhereRestricoes                = '',        @Documentos                     = '( ''DV'', ''FA'', ''FAI'', ''FI'', ''FM'', ''FO'', ''FR'', ''NC'', ''VD'')',        @AnoReferencia                      = '12/31/2016',       @AnoComparacao                      = '1/1/2016',       @MesFimExercicio                      = 12");
                result.valorFaturado = 0;
                while (!vl.NoFim())
                {
                    result.valorFaturado += vl.Valor("LiquidoAnoAct");
                    vl.Seguinte();
                }

                return result;
            }
            return null;
        }




        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo

        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();
            myArt.retail = new List<double>();
            myArt.profit_margin = new List<double>();
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    StdBELista priceObj = PriEngine.Engine.Consulta("SELECT PVP1, PVP2, PVP3, PVP4, PVP5, PVP6 FROM ArtigoMoeda WHERE ArtigoMoeda.Artigo = '" + codArtigo + "' AND ArtigoMoeda.Moeda = 'EUR'");
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.reference = objArtigo.get_Artigo();
                    myArt.name = objArtigo.get_Descricao();
                    myArt.retail.Add(priceObj.Valor("PVP1"));
                    myArt.retail.Add(priceObj.Valor("PVP2"));
                    myArt.retail.Add(priceObj.Valor("PVP3"));
                    myArt.retail.Add(priceObj.Valor("PVP4"));
                    myArt.retail.Add(priceObj.Valor("PVP5"));
                    myArt.retail.Add(priceObj.Valor("PVP6"));
                    myArt.price = objArtigo.get_PCUltimo();
                    myArt.tax = objArtigo.get_IVA();
                    myArt.profit_margin.Add((myArt.retail[0] - myArt.price) / myArt.retail[0]);
                    myArt.profit_margin.Add((myArt.retail[1] - myArt.price) / myArt.retail[1]);
                    myArt.profit_margin.Add((myArt.retail[2] - myArt.price) / myArt.retail[2]);
                    myArt.profit_margin.Add((myArt.retail[3] - myArt.price) / myArt.retail[3]);
                    myArt.profit_margin.Add((myArt.retail[4] - myArt.price) / myArt.retail[4]);
                    myArt.profit_margin.Add((myArt.retail[5] - myArt.price) / myArt.retail[5]);
                    myArt.stk = objArtigo.get_StkActual();
                    myArt.unit = objArtigo.get_UnidadeBase();
                    myArt.stkValue = objArtigo.get_StkActual()*objArtigo.get_PCMedio();
                    


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

        public static List<Tuple<String, double>> productsInfo(){
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                var result = new List<Tuple<String, double>>();
                StdBELista vlStk = PriEngine.Engine.Consulta("SELECT  SUM(Artigo.PCMedio*Artigo.STKActual) AS ValueStock FROM   (Artigo LEFT OUTER JOIN Familias Familias ON Artigo.Familia=Familias.Familia) WHERE  Artigo.TratamentoDim<>1 ");
                StdBELista objList = PriEngine.Engine.Consulta(" EXEC GCP_LST_RESUMO_VENDAS        @Tabela                         = '##ResumoVendasUuserP1672Hd30311826378491387fda695004379d4UoDgZQpQPQ39',        @Campos                         = 'NULL AS GROUP1,Mes,Trimestre,DescontosAnoAct,AcumDescontosAnoAct,LiquidoAnoAnt,AcumLiquidoAnoAnt,LiquidoAnoAct,AcumLiquidoAnoAct,VariacaoLiquido,VariacaoLiquidoPercentual,VariacaoAcmLiquido,VariacaoAcmLiquidoPercentual,DescontosAnoAnt,AcumDescontosAnoAnt,OutrosAnoAct,AcumOutrosAnoAct,OutrosAnoAnt,AcumOutrosAnoAnt,QuantidadeAnoAnt,MercadoriaAnoAct,AcumMercadoriaAnoAnt,QuantidadeAnoAct,MercadoriaAnoAnt,MargemAnoAnt,MargemAnoAct,AcumMercadoriaAnoAct,VariacaoAcmDescontos,VariacaoAcmDescontosPercentual,VariacaoAcmMercadoria,VariacaoAcmMercadoriaPercentual,VariacaoAcmOutros,VariacaoAcmOutrosPercentual,VariacaoDescontos,VariacaoDescontosPercentual,VariacaoMercadoria,VariacaoMercadoriaPercentual,VariacaoOutros,VariacaoOutrosPercentual,NULL AS DATAFILLCOL',        @MoedaVisualizacao              = 'EUR',        @MoedaBase                      = 1,        @SentidoCambio                  = 0,        @CambioActualHistorico          = 1,        @MoedaStocks                    = 'EUR',        @MoedaStocksBase                = 1,        @DocConvertidos                     = '1',        @WhereRestricoes                = '',        @Documentos                     = '( ''DV'', ''FA'', ''FAI'', ''FI'', ''FM'', ''FO'', ''FR'', ''NC'', ''ORC'', ''VD'')',        @AnoReferencia                      = '12/31/2016',       @AnoComparacao                      = '1/1/2016',       @MesFimExercicio                      = 12");
                double sales = 0;
                while (!objList.NoFim())
                {
                    sales += objList.Valor("LiquidoAnoAct");
                    objList.Seguinte();
                }

                result.Add(new Tuple<String, double>("turnover", sales / vlStk.Valor("ValueStock")));
                result.Add(new Tuple<String, double>("stock_value", vlStk.Valor("ValueStock")));
                return result;



            }
            return null;
        }

        public static List<Model.TopProduto> ListaMelhoresProdutos()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList = PriEngine.Engine.Consulta("SELECT SUM(LinhasDoc.PrecoLiquido) AS LucroBrusco, SUM(LinhasDoc.Quantidade) AS Quantidade, Artigo.Descricao, Artigo.Artigo from LinhasDoc, CabecDoc, Artigo WHERE Artigo.Artigo = LinhasDoc.Artigo AND LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.TipoDoc = 'FA' GROUP BY Artigo.Descricao, Artigo.Artigo ORDER BY LucroBrusco DESC");
                Model.TopProduto produto = new Model.TopProduto();
                List<Model.TopProduto> listaProdutos = new List<Model.TopProduto>();
                double sum = 0;
                int i = 0;
                while (i++ < 10 && !objList.NoFim()) {
                    sum += objList.Valor("LucroBrusco");
                    objList.Seguinte();
                }

                objList.Inicio();

                while (!objList.NoFim())
                {
                    produto = new Model.TopProduto();
                    produto.name = objList.Valor("Descricao");
                    produto.valor = objList.Valor("LucroBrusco");
                    produto.sales_p = (produto.valor / sum) * 100;
                    produto.quantidade = objList.Valor("Quantidade");
                    produto.reference = objList.Valor("Artigo");
                    listaProdutos.Add(produto);
                    objList.Seguinte();
                }



                return listaProdutos;
            }
            return null;

        }

        //This method uses the ClientTimeline model because it serves the exact same purpose
        public static IEnumerable<Lib_Primavera.Model.ClientTimeline> ProductsTimeline()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList = PriEngine.Engine.Consulta("SELECT LinhasDoc.Data as Data, SUM(LinhasDoc.PrecoLiquido) as Pag FROM LinhasDoc, CabecDoc WHERE LinhasDoc.IdCabecDoc = CabecDoc.Id AND CabecDoc.TipoDoc = 'FA' GROUP BY LinhasDoc.Data ORDER BY LinhasDoc.Data DESC");
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

                objList = PriEngine.Engine.Consulta("SELECT CabecDoc.Entidade AS Entidade, LinhasDoc.DataSaida as DataSaida, (LinhasDocStatus.Quantidade - LinhasDocStatus.QuantTrans) AS Patinhos FROM CabecDoc, LinhasDoc, LinhasDocStatus WHERE LinhasDocStatus.Quantidade != LinhasDocStatus.QuantTrans AND LinhasDoc.Id = LinhasDocStatus.IdLinhasDoc AND CabecDoc.Id = LinhasDoc.IdCabecDoc AND CabecDoc.TipoDoc = 'ECL' AND LinhasDoc.Artigo = '" + id + "'");
                List<Model.Shipment> shipList = new List<Model.Shipment>();
                while (!objList.NoFim())
                {
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

        public static List<List<Tuple<String, Decimal>>> getBalanco()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                decimal ancaft = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AFT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancpi = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.PI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal anctrp = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.TRP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancai = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancab = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AB", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancmep = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.MEP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancpfout = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.PF.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancacc = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancout = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancid = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ID", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancancdv = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ANCDV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal acinv = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.inv", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acAB = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.AB", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acCLI = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.CLI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acADF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.ADF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acEST = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.EST", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acACC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acOUTCR = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.OUTCR", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acDIF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.DIF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acAFDN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.AFDN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acOAF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.OAF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acCD = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.CD", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal cpCR = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.CR", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpQP = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.QP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpOUTCP = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.OUTCP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpPE = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.PE", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpRES = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.RES", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpOUTRES = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.OUTRES", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpRT = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.RT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpAJAF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.AJAF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpEREV = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.EREV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpBRES = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.RES", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal pncPRO = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.PRO", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncFIN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.FIN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncBPE = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.BPE", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncPID = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.PID", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncOUTPG = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.OUTPG", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal pcFORN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.FORN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcADC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.ADC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcEST = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.EST", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcACC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcFINO = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.FINO", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcOUTPG = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.OUTPG", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcPFDN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.PFDN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcOUT = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcDIF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.DIF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcPNCDV = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.PNCDV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                                
                var result = new List<List<Tuple<String, Decimal>>>();
                result.Add(new List<Tuple<String, Decimal>>());
                result.Last().Add(new Tuple<String, Decimal>("Activos fixos tangiveis", ancaft));
                result.Last().Add(new Tuple<String, Decimal>("Properiedades de Investivento", ancpi));
                result.Last().Add(new Tuple<String, Decimal>("Trespasse (Goodwill)", anctrp));
                result.Last().Add(new Tuple<String, Decimal>("Activos Intangíveis", ancai));
                result.Last().Add(new Tuple<String, Decimal>("Activos Biológicos", ancab));
                result.Last().Add(new Tuple<String, Decimal>("Participações Financeiras (método de equivalência patrimonial)", ancmep));
                result.Last().Add(new Tuple<String, Decimal>("Participações Financeiras (outros métodos)", ancpfout));
                result.Last().Add(new Tuple<String, Decimal>("Acionistas/Sócios", ancacc));
                result.Last().Add(new Tuple<String, Decimal>("Outros activos financeiros", ancout));
                result.Last().Add(new Tuple<String, Decimal>("Activos por impostos diferidos", ancid));
                result.Last().Add(new Tuple<String, Decimal>("Activos não correntes detidos para venda", ancancdv));

                result.Add(new List<Tuple<String, Decimal>>());
                result.Last().Add(new Tuple<String, Decimal>("Inventários", acinv));
                result.Last().Add(new Tuple<String, Decimal>("Activos Biológicos", acAB));
                result.Last().Add(new Tuple<String, Decimal>("Clientes", acCLI));
                result.Last().Add(new Tuple<String, Decimal>("Adiantamentos a investidores", acADF));
                result.Last().Add(new Tuple<String, Decimal>("Estado e outros entes públicos", acEST));
                result.Last().Add(new Tuple<String, Decimal>("Accionsistas/Sócios", acACC));
                result.Last().Add(new Tuple<String, Decimal>("Outras contas a receber", acOUTCR));
                result.Last().Add(new Tuple<String, Decimal>("Diferimentos", acDIF));
                result.Last().Add(new Tuple<String, Decimal>("Activos financeiros detidos para negociação", acAFDN));
                result.Last().Add(new Tuple<String, Decimal>("Outros ativos financeiros", acOAF));
                result.Last().Add(new Tuple<String, Decimal>("Caixa e depósitos bancários", acCD));

                result.Add(new List<Tuple<String, Decimal>>());
                result.Last().Add(new Tuple<String, Decimal>("Capital realizado", cpCR));
                result.Last().Add(new Tuple<String, Decimal>("Ações (Quotas Próprias)", cpQP));
                result.Last().Add(new Tuple<String, Decimal>("Outros instrumentos de capital próprio", cpOUTCP));
                result.Last().Add(new Tuple<String, Decimal>("Prémios de emissão", cpPE));
                result.Last().Add(new Tuple<String, Decimal>("Reservas Legais", cpRES));
                result.Last().Add(new Tuple<String, Decimal>("Outras Reservas", cpOUTRES));
                result.Last().Add(new Tuple<String, Decimal>("Resultados transitados", cpRT));
                result.Last().Add(new Tuple<String, Decimal>("Ajustamentos em activos financeiros", cpAJAF));
                result.Last().Add(new Tuple<String, Decimal>("Excedentes de revalorização", cpEREV));
                result.Last().Add(new Tuple<String, Decimal>("Resultado líquido do exercício", cpBRES));

                result.Add(new List<Tuple<String, Decimal>>());
                result.Last().Add(new Tuple<String, Decimal>("Provisões", pncPRO));
                result.Last().Add(new Tuple<String, Decimal>("Financiamentos obtidos", pncFIN));
                result.Last().Add(new Tuple<String, Decimal>("Responsabilidades por benefícios pós-emprego", pncBPE));
                result.Last().Add(new Tuple<String, Decimal>("Passivo por impostos diferidos", pncPID));
                result.Last().Add(new Tuple<String, Decimal>("Outras contas a pagar", pncOUTPG));

                result.Add(new List<Tuple<String, Decimal>>());
                result.Last().Add(new Tuple<String, Decimal>("Fornecedores", pcFORN));
                result.Last().Add(new Tuple<String, Decimal>("Adiantamentos de clientes", pcADC));
                result.Last().Add(new Tuple<String, Decimal>("Estado e outros entes públicos", pcEST));
                result.Last().Add(new Tuple<String, Decimal>("Accionistas/Sócios", pcACC));
                result.Last().Add(new Tuple<String, Decimal>("Financiamentos obtidos", pcFINO));
                result.Last().Add(new Tuple<String, Decimal>("Outras contas a pagar", pcOUTPG));
                result.Last().Add(new Tuple<String, Decimal>("Passivos financieros detidos para negociação", pcPFDN));
                result.Last().Add(new Tuple<String, Decimal>("Outros passivos financeiros", pcOUT));
                result.Last().Add(new Tuple<String, Decimal>("Passivos não correntes detidos para venda", pcPNCDV));

                return result;
            }
            return null;

        }

        public static Tuple<double, double> getAtivos_Passivos()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList;
                //Model.Conta conta = new Model.Conta();
                double sumAtivos = 0d;
                double sumPassivos = 0d;
                
                decimal ancaft = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AFT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancpi = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.PI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal anctrp = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.TRP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancai = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancab = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.AB", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancmep = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.MEP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancpfout = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.PF.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancacc = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancout = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancid = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ID", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal ancancdv = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.ANC.ANCDV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal acinv = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.inv", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acAB = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.AB", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acCLI = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.CLI", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acADF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.ADF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acEST = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.EST", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acACC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acOUTCR = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.OUTCR", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acDIF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.DIF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acAFDN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.AFDN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acOAF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.OAF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal acCD = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.AC.CD", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal cpCR = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.CR", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpQP = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.QP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpOUTCP = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.OUTCP", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpPE = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.PE", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpRES = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.RES", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpOUTRES = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.OUTRES", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpRT = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.RT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpAJAF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.AJAF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal cpEREV = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.CP.EREV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                
                decimal pncPRO = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.PRO", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncFIN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.FIN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncBPE = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.BPE", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncPID = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.PID", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pncOUTPG = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PNC.OUTPG", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal pcFORN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.FORN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcADC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.ADC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcEST = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.EST", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcACC = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.ACC", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcFINO = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.FINO", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcOUTPG = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.OUTPG", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcPFDN = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.PFDN", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcOUT = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.OUT", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcDIF = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.DIF", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);
                decimal pcPNCDV = PriEngine.Engine.Contabilidade.Formulas.DaValorRubrica(2016, "B.PC.PNCDV", 0, 12, Interop.CblBE900.ENUMValorNaMoeda.vnmMoedaAltCtActual);

                decimal anc = ancaft + ancpi
                + anctrp
                + ancai
                + ancab
                + ancmep
                + ancpfout
                + ancacc
                + ancout
                + ancid
                + ancancdv;

                decimal ac =
                acinv +               
                acAB +
                acCLI +
                acADF +
                acEST +
                acACC +
                acOUTCR +
                acDIF +
                acAFDN +
                acOAF +
                acCD;

                decimal cp =
                cpCR +
                cpQP +
                cpOUTCP +
                cpPE +
                cpRES +
                cpOUTRES +
                cpRT +
                cpAJAF +
                cpEREV;

                decimal pnc =
                pncPRO +
                pncFIN +
                pncBPE +
                pncPID +
                pncOUTPG;

                decimal pc =
                pcFORN +
                pcADC +
                pcEST +
                pcACC +
                pcFINO +
                pcOUTPG +
                pcPFDN +
                pcOUT +
                pcDIF +
                pcPNCDV;


                return new Tuple<double, double>(Decimal.ToDouble(ac+anc), Decimal.ToDouble(pnc+pc));
            }
            return null;
        }

        public static List<Model.Conta> getBalancete()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista objList;
                string ano = "2015";
                objList = PriEngine.Engine.Consulta("SELECT AcumuladosContas.Conta, PlanoContas.Descricao, SUM(Mes00Cr+Mes01Cr+Mes02Cr+Mes03Cr+Mes04Cr+Mes05Cr+Mes06Cr+Mes07Cr+Mes08Cr+Mes09Cr+Mes10Cr+Mes11Cr+Mes12Cr+Mes13Cr+Mes14Cr+Mes15Cr) AS MesCr, SUM(Mes00Db+Mes01Db+Mes02Db+Mes03Db+Mes04Db+Mes05Db+Mes06Db+Mes07Db+Mes08Db+Mes09Db+Mes10Db+Mes11Db+Mes12Db+Mes13Db+Mes14Db+Mes15Db) AS MesDb FROM AcumuladosContas WITH (NOLOCK) INNER JOIN PlanoContas WITH (NOLOCK) ON AcumuladosContas.Ano=PlanoContas.Ano And AcumuladosContas.Conta = PlanoContas.Conta  WHERE 1=1  AND (((AcumuladosContas.Conta >= '0' AND AcumuladosContas.Conta <= '999999999') AND NOT ((AcumuladosContas.Conta >= '00' AND AcumuladosContas.Conta <= '0999999999') OR (AcumuladosContas.Conta >= '90' AND AcumuladosContas.Conta <= '9999999999'))) OR (AcumuladosContas.Conta >= '00' AND AcumuladosContas.Conta <= '0999999999') OR (AcumuladosContas.Conta >= '90' AND AcumuladosContas.Conta <= '9999999999')) AND AcumuladosContas.Ano = " + ano + " AND AcumuladosContas.Moeda = 'EUR' AND TipoConta = 'R' GROUP BY AcumuladosContas.Conta, Descricao, TipoConta ORDER BY AcumuladosContas.Conta");
                Model.Conta conta = new Model.Conta();
                List<Model.Conta> contas = new List<Model.Conta>();
                while (!objList.NoFim())
                {
                    conta = new Model.Conta();
                    conta.codConta = objList.Valor("Conta");
                    conta.nomeConta = objList.Valor("Descricao");
                    conta.credito = (double) objList.Valor("MesCr");
                    conta.debito = (double) objList.Valor("MesDb");
                    conta.saldo = conta.credito - conta.debito;
                    contas.Add(conta);
                    objList.Seguinte();
                }
                return contas;
            }
            return null;

        }
        #endregion Contabilidade


        #region Fornecedores

        public static Lib_Primavera.Model.Fornecedor getFornecedor(string id)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                if (PriEngine.Engine.Comercial.Fornecedores.Existe(id))
                {
                    Model.Fornecedor forn = new Model.Fornecedor();
                    GcpBEFornecedor obj = PriEngine.Engine.Comercial.Fornecedores.Edita(id);
                    forn.name = obj.get_Nome();
                    forn.address = obj.get_Morada();
                    forn.post_c = obj.get_CodigoPostal();
                    forn.city = obj.get_LocalidadeCodigoPostal();
                    forn.reference = obj.get_Fornecedor();
                    forn.pendente = -obj.get_DebitoContaCorrente();
                    forn.backlog = obj.get_DebitoEncomendasPendentes();
                    return forn;
                }
            }
            return null;
        }

        public static IEnumerable<Model.Fornecedor> listaFornecedor()
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista fornecedores = PriEngine.Engine.Consulta("SELECT Fornecedor, Nome, Morada, Cp, CpLoc, TotalDeb, Tel FROM Fornecedores;");
                List<Model.Fornecedor> fornes = new List<Model.Fornecedor>();

                while (!fornecedores.NoFim())
                {
                    Model.Fornecedor forn = new Model.Fornecedor();
                    forn.address = fornecedores.Valor("Morada");
                    forn.city = fornecedores.Valor("CpLoc");
                    forn.name = fornecedores.Valor("Nome");
                    forn.post_c = fornecedores.Valor("CpLoc");
                    forn.reference = fornecedores.Valor("Fornecedor");
                    forn.pendente = fornecedores.Valor("TotalDeb"); 
                    fornes.Add(forn);
                    fornecedores.Seguinte(); 
                }
                return fornes;
            }
            return null;
        }

        public static IEnumerable<Tuple<DateTime, double>> ranges(string id)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista fornecedores = PriEngine.Engine.Consulta("SELECT CabecCompras.DataDoc AS DataDoc, -SUM(LinhasCompras.PrecoLiquido) As Valor FROM CabecCompras, LinhasCompras WHERE CabecCompras.Entidade = '"+id+"' AND LinhasCompras.IdCabecCompras = CabecCompras.Id AND CabecCompras.TipoDoc = 'VFA' GROUP BY CabecCompras.DataDoc ORDER BY CabecCompras.DataDoc DESC");
                List<Tuple<DateTime, double>> fornes = new List<Tuple<DateTime, double>>();

                while (!fornecedores.NoFim())
                {
                    fornes.Add(new Tuple<DateTime, double>(fornecedores.Valor("DataDoc"), fornecedores.Valor("Valor")));
                    fornecedores.Seguinte();
                }
                return fornes;
            }
            return null;
        }

        public static IEnumerable<Model.FornecedorTopProduct> forn_top_prod(string id)
        {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                StdBELista fornecedores = PriEngine.Engine.Consulta("SELECT LinhasCompras.Artigo, Artigo.Descricao AS Nome, -SUM(LinhasCompras.PrecoLiquido) AS Valor FROM  CabecCompras, LinhasCompras, Artigo WHERE CabecCompras.Entidade ='"+id+"' AND LinhasCompras.IdCabecCompras = CabecCompras.Id AND CabecCompras.TipoDoc = 'VFA' AND LinhasCompras.Artigo = Artigo.Artigo GROUP BY LinhasCompras.Artigo, Artigo.Descricao;");
                List<Model.FornecedorTopProduct> lista = new List<Model.FornecedorTopProduct>();
                double total = 0;
                while (!fornecedores.NoFim())
                {
                    total += fornecedores.Valor("Valor");
                    fornecedores.Seguinte();
                }
                fornecedores.Inicio();

                while (!fornecedores.NoFim())
                {
                    Model.FornecedorTopProduct top = new Model.FornecedorTopProduct();
                    top.reference = fornecedores.Valor("Artigo");
                    top.name = fornecedores.Valor("Nome");
                    top.order_p = (fornecedores.Valor("Valor") / total) * 100;
                    lista.Add(top);
                    fornecedores.Seguinte();
                }
                return lista;
            }
            return null;
        }

        public static Lib_Primavera.Model.FornecedoresInfo listaFornecedores() {
            if (PriEngine.InitializeCompany(FirstREST.Properties.Settings.Default.Company.Trim(), FirstREST.Properties.Settings.Default.User.Trim(), FirstREST.Properties.Settings.Default.Password.Trim()) == true)
            {
                Model.FornecedoresInfo result = new Model.FornecedoresInfo();
                StdBELista vl = PriEngine.Engine.Consulta("SELECT Count(Fornecedores.Nome) as NumFornecedores FROM Fornecedores");
                result.numFornecedores = vl.Valor("NumFornecedores");
                double valoresPendentes = 0.0;
                double valoresBacklog = 0.0;

                StdBELista fornecedoresList = PriEngine.Engine.Consulta("SELECT Fornecedor FROM Fornecedores");

                while (!fornecedoresList.NoFim())
                {
                    string cod = fornecedoresList.Valor("Fornecedor");
                    if (PriEngine.Engine.Comercial.Fornecedores.Existe(cod) == true) {
                        GcpBEFornecedor obj = PriEngine.Engine.Comercial.Fornecedores.Edita(cod);
                        valoresPendentes -= obj.get_DebitoContaCorrente();
                        valoresBacklog += obj.get_DebitoEncomendasPendentes();
                    }
                    fornecedoresList.Seguinte();
                }

                result.valoresPendentes = valoresPendentes;
                result.valoresBacklog = valoresBacklog;

                return result;
            }
            return null;   
        }

        #endregion Fornecedores
       
    }
}