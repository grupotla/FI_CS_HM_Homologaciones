using CapaDatos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Services;
using WebServiceTLA.Class;
using static WebServiceTLA.Class.CXC;
using static WebServiceTLA.Class.SaldosDiarios;


namespace WebServiceTLA
{
    /// <summary>
    /// 
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Accounting : WebService
    {

        private readonly CultureInfo MyCultureInfo = new CultureInfo("es-ES");
        private readonly List<Documento> data = new List<Documento>();
        private readonly List<DocumentoC> dataC = new List<DocumentoC>();
        private readonly ConexionDeDatos conexion = new ConexionDeDatos();
        private int Contador = 0;
        private string estadoMensaje;
        private string mensaje2 = "";
        private string codigo = "";
        /// <summary>
        /// METODO QUE MAPEA LOS SALDOS DIARIOS 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="dataconvert"></param>
        /// <returns></returns>
        [WebMethod]
        public bool IntSaldosDiarios(string json, ref string dataconvert)
        {
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (dynamic item in array)
            {

                SaldosDiarios saldos = new SaldosDiarios
                {
                    ASIENTO = item.ASIENTO,
                    COD_COMPANIA = item.COD_COMPANIA,
                    COD_PAIS = item.COD_PAIS,
                    CREDITOS_LOCAL = item.CREDITOS_LOCAL,
                    CUENTA_CONTABLE = item.CUENTA_CONTABLE,
                    DEBITO_DOLAR = item.DEBITO_DOLAR,
                    DEBITO_LOCAL = item.DEBITO_LOCAL,
                    DESCRIPCION_NIT = item.DESCRIPCION_NIT,
                    DESCRIPCIÓN = item.DESCRIPCIÓN,
                    FECHA = DateTime.Parse(item.FECHA.ToString(), MyCultureInfo, DateTimeStyles.NoCurrentDateDefault),
                    FUENTE = item.FUENTE,
                    NIT = item.NIT,
                    NOMBRE_COMPANIA = item.NOMBRE_COMPANIA,
                    NOMBRE_PAIS = item.NOMBRE_PAIS,
                    ORIGEN = item.ORIGEN,
                    PAQUETE = item.PAQUETE,
                    REFERENCIA = item.REFERENCIA,
                    TIPO_ASIENTO = item.TIPO_ASIENTO
                };

                bool estado = saldos.InsertSaldosDiarios(saldos, json, ref mensaje2, ref codigo);
                if (estado == true)
                {
                    estadoMensaje = "CORRECTO";
                    mensaje2 = "PROCESO CORRECTO";
                }
                else
                {
                    estadoMensaje = "ERROR";
                }
                data.Add(new Documento());
                data[Contador].ASIENTO = item.ASIENTO;
                data[Contador].COD_COMPANIA = item.COD_COMPANIA;
                data[Contador].COD_PAIS = item.COD_PAIS;
                data[Contador].CODIGO_ERROR = codigo;
                data[Contador].ESTADO = estadoMensaje.ToString();
                data[Contador].MENSAJE = mensaje2;
                Contador++;

            }


            string jsonRespuesta = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(data);
            dataconvert = jsonRespuesta;

            return true;
        }

        [WebMethod]
        public bool IntCXC(string json, ref string dataconvert)
        {
            CXC cXC = new CXC();
            dynamic array = JsonConvert.DeserializeObject(json);
            foreach (dynamic item in array)
            {

                cXC.NUM_FACTURA = item.NUM_FACTURA;
                cXC.NOMBRE_PROVEEDOR = item.NOMBRE_PROVEEDOR;
                cXC.NOMBRE_PAIS = item.NOMBRE_PAIS;
                cXC.NOMBRE_COMPANIA = item.NOMBRE_COMPANIA;
                cXC.NIT_PROVEEDOR = item.NIT_PROVEEDOR;
                cXC.MONTO_SIN_IVA = item.MONTO_SIN_IVA;
                cXC.MONEDA = item.MONEDA;
                cXC.IVA = item.IVA;
                cXC.FECHA = DateTime.Parse(item.FECHA.ToString(), MyCultureInfo, DateTimeStyles.NoCurrentDateDefault);
                cXC.COD_PROVEEDOR = item.COD_PROVEEDOR;
                cXC.COD_PAIS = item.COD_PAIS;
                cXC.COD_COMPANIA = item.COD_COMPANIA;
                cXC.CENTRO_COSTO = item.CENTRO_COSTO;


                bool estado = cXC.InsertCXC(cXC, json, ref mensaje2, ref codigo);
                if (estado == true)
                {
                    estadoMensaje = "101";
                    mensaje2 = "PROCESO CORRECTO";
                }
                else
                {
                    estadoMensaje = "Error";
                }
                dataC.Add(new DocumentoC());
                dataC[Contador].ASIENTO = item.NUM_FACTURA;
                dataC[Contador].COD_COMPANIA = item.COD_COMPANIA;
                dataC[Contador].COD_PAIS = item.COD_PAIS;
                dataC[Contador].CODIGO_ERROR = codigo;
                dataC[Contador].ESTADO = estadoMensaje.ToString();
                dataC[Contador].MENSAJE = mensaje2;
                Contador++;

            }

            string jsonRespuesta = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataC);
            dataconvert = jsonRespuesta;

            return true;
        }

        [WebMethod]
        [Obsolete]
        public bool INT_INGRESOS_CXC(string json, ref string dataconvert)
        {
            Ingresos_CXC cXC = new Ingresos_CXC();
            string ASIENTOEXACTUS = "";
            dynamic array = JsonConvert.DeserializeObject(json);
            string Nombre_bitacora = "";
            string Ruta = "";
            string Nombre_Carpeta = "";
            string Direccion = "";
            bool Completo = true;

            bool configuracion = cXC.ConfiguracionBaw(ref Nombre_bitacora, ref Ruta, ref Nombre_Carpeta);
            Direccion = Ruta + Nombre_Carpeta + "\\" + Nombre_bitacora;
            System.IO.File.AppendAllText(Direccion, "--------------------------------------------------------------------------------------------------------------------------------------------- \n");
            System.IO.File.AppendAllText(Direccion, " Json Resivido: \n");
            System.IO.File.AppendAllText(Direccion, json + "\n");
            System.IO.File.AppendAllText(Direccion, "--------------------------------------------------------------------------------------------------------------------------------------------- \n");
            foreach (dynamic item in array)
            {

                cXC.ASIENTO = item.ASIENTO;
                cXC.TIPO_ASIENTO = item.TIPO_ASIENTO;
                cXC.ORIGEN = " ";
                cXC.CUENTA_CONTABLE = item.CUENTA_CONTABLE;
                cXC.DIVICION = item.CUENTA_CONTABLE;
                cXC.COD_COMPANIA = item.COD_COMPANIA;
                cXC.COD_PAIS = item.COD_PAIS;
                cXC.CREDITOS_DOLAR = item.CREDITOS_DOLAR;
                cXC.CREDITOS_LOCAL = item.CREDITOS_LOCAL;
                cXC.FECHA = DateTime.Parse(item.FECHA.ToString(), MyCultureInfo, DateTimeStyles.NoCurrentDateDefault);
                cXC.DEBITO_DOLAR = item.DEBITO_DOLAR;
                cXC.DEBITO_LOCAL = item.DEBITO_LOCAL;
                cXC.DESCRIPCIÓN = item.DESCRIPCIÓN;
                cXC.FUENTE = item.FUENTE;
                cXC.REFERENCIA = item.REFERENCIA;
                cXC.NIT = item.NIT;
                cXC.DESCRIPCION_NIT = item.DESCRIPCION_NIT;
                cXC.PAQUETE = " ";
                cXC.MONEDA = item.MONEDA;
                cXC.MONTO_SIN_IVA = item.MONTO_SIN_IVA;
                cXC.IVA = item.IVA;
                cXC.CENTRO_COSTO = item.CENTRO_COSTO;
                cXC.TIPO_CAMBIO = item.TIPO_CAMBIO;


                bool estado = cXC.InsertIngresos_CXC(cXC, json, ref mensaje2, ref codigo, Direccion);
                if (estado == true)
                {
                    estadoMensaje = "101";
                    mensaje2 = "PROCESO CORRECTO";
                    string ASIENTO = item.ASIENTO;

                   
                }
                else
                {
                    Completo = false;
                    estadoMensaje = "Error";
                }
                dataC.Add(new DocumentoC());
                dataC[Contador].ASIENTO = item.ASIENTO;
                dataC[Contador].ASIENTOEXACTUS = ASIENTOEXACTUS;
                dataC[Contador].COD_COMPANIA = item.COD_COMPANIA;
                dataC[Contador].COD_PAIS = item.COD_PAIS;
                dataC[Contador].CODIGO_ERROR = codigo;
                dataC[Contador].ESTADO = estadoMensaje.ToString();
                dataC[Contador].MENSAJE = mensaje2;
                Contador++;
            }

            if (Completo == false)
            {
                bool BorradoInformacion = cXC.DeleteIngresosCXC();
                string jsonRespuesta = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataC);
                dataconvert = jsonRespuesta;
            }
            else
            {
                conexion.SP_INGRESOS_CXC(Direccion);

                foreach (DocumentoC item in dataC)
                {
                    item.ASIENTOEXACTUS = cXC.SelectAsientoExactus(item.ASIENTO);
                }
                string jsonRespuesta = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(dataC);
                dataconvert = jsonRespuesta;
            }
            return true;
        }
    }
}