using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interop.ErpBS900;         // Use Primavera interop's [Path em C:\Program Files\Common Files\PRIMAVERA\SG800]
using Interop.StdPlatBS900;
using Interop.StdBE900;



using System.Web;
using System.Net;


namespace FirstREST.Lib_Primavera
{
    public class PriEngine
    {

        public static StdPlatBS Platform { get; set; }
        public static ErpBS Engine { get; set; }

        public static bool InitializeCompany(string Company, string User, string Password)
        {
            var postCompany = HttpContext.Current.Request.Form["company"];
            var postUsername = HttpContext.Current.Request.Form["username"];
            var postPassword = HttpContext.Current.Request.Form["password"];

            StdBSConfApl objAplConf = new StdBSConfApl();
            StdPlatBS Plataforma = new StdPlatBS();
            ErpBS MotorLE = new ErpBS();

            EnumTipoPlataforma objTipoPlataforma = new EnumTipoPlataforma();
            objTipoPlataforma = EnumTipoPlataforma.tpProfissional;

            objAplConf.Instancia = "Default";
            objAplConf.AbvtApl = "GCP";
            objAplConf.PwdUtilizador = postPassword;
            objAplConf.Utilizador = postUsername;
            objAplConf.LicVersaoMinima = "9.00";

            StdBETransaccao objStdTransac = new StdBETransaccao();

            // Opem platform.
            try
            {
                Plataforma.AbrePlataformaEmpresa(ref postCompany, ref objStdTransac, ref objAplConf, ref objTipoPlataforma,"");
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                return false; 
            }

            // Is plt initialized?
            if (Plataforma.Inicializada)
            {

                // Retuns the ptl.
                Platform = Plataforma;

                bool blnModoPrimario = true;

                // Open Engine
                MotorLE.AbreEmpresaTrabalho(EnumTipoPlataforma.tpProfissional, ref postCompany, ref postUsername, ref postPassword, ref objStdTransac, "Default", ref blnModoPrimario);
                MotorLE.set_CacheActiva(false);

                // Returns the engine.
                Engine = MotorLE;

                return true;
            }
            else
            {
                return false;
            }


        }

    }

}
