using AcountingSite.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;

namespace AcountingSite.Controllers.Api
{
    public class Acount_APIController : ApiController
    {
        private readonly CapaDatos.ConexionDeDatos cn = new CapaDatos.ConexionDeDatos();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("api/Acount_Tipo_DOC_API/GetHomTipeDocGrid")]
        public List<HomTipoDoc> GetHomTipeDocGrid(string username)
        {
            List<HomTipoDoc> listView = new List<HomTipoDoc>();
            try
            {
                DataTable ViewReporte = cn.Query("SELECT TIPO_DOC_EXACTUS, TIPO_DOC_BAW, COD_PAIS,COD_COMPANIA FROM INTBAW.INT_HOM_TIPO_DOC", "BAW");

                foreach (DataRow line in ViewReporte.Rows)
                {
                    HomTipoDoc view = new HomTipoDoc
                    {
                        TIPO_DOC_EXACTUS = line["TIPO_DOC_EXACTUS"].ToString(),
                        TIPO_DOC_BAW = line["TIPO_DOC_BAW"].ToString(),
                        COD_PAIS = line["COD_PAIS"].ToString(),
                        COD_COMPANIA = line["COD_COMPANIA"].ToString()
                    };
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TIPO_DOC_EXACTUS"></param>
        /// <param name="TIPO_DOC_BAW"></param>
        /// <param name="COD_PAIS"></param>
        /// <param name="COD_COMPANIA"></param>
        /// <returns></returns>
        [Route("api/Acount_Tipo_DOC_API/Create_HomTipeDoc")]
        [HttpGet]

        public IHttpActionResult Create_HomTipeDoc(string TIPO_DOC_EXACTUS, string TIPO_DOC_BAW, string COD_PAIS, string COD_COMPANIA)
        {
            bool resul = false;
            try
            {
                string QUERY_Select = ("INSERT INTO INTBAW.INT_HOM_TIPO_DOC(TIPO_DOC_EXACTUS, TIPO_DOC_BAW, COD_PAIS,COD_COMPANIA)" +
                    " VALUES(  '" + TIPO_DOC_EXACTUS + "' ,'" + TIPO_DOC_BAW + "','" +
                    "" + COD_PAIS + "','" + COD_COMPANIA + "')");

                resul = cn.Execute(QUERY_Select, "BAW");

                if (resul)
                {
                    return Ok("Documento Creado Correctamente");
                }
                else
                {

                    DataTable ViewReporte = cn.Query("SELECT TIPO_DOC_EXACTUS, TIPO_DOC_BAW, COD_PAIS,COD_COMPANIA FROM INTBAW.INT_HOM_TIPO_DOC" +
                          " WHERE TIPO_DOC_EXACTUS = '" + TIPO_DOC_EXACTUS + "' AND TIPO_DOC_BAW = '" + TIPO_DOC_BAW + "' AND COD_PAIS = '" +
                    "" + COD_PAIS + "' AND COD_COMPANIA = '" + COD_COMPANIA + "'", "BAW");

                    if (ViewReporte.Rows.Count > 0)
                    {
                        return Ok("El Documento ya existe");
                    }
                    else
                    {
                        return Ok("Error al Crear el Documento");
                    }


                }

            }
            catch (Exception)
            {

                return Ok("false"); ;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TIPO_DOC_EXACTUS"></param>
        /// <param name="TIPO_DOC_BAW"></param>
        /// <param name="COD_PAIS"></param>
        /// <param name="COD_COMPANIA"></param>
        /// <returns></returns>
        [Route("api/Acount_Tipo_DOC_API/Delete_HomTipeDoc")]
        [HttpGet]

        public IHttpActionResult Delete_HomTipeDoc(string TIPO_DOC_EXACTUS, string TIPO_DOC_BAW, string COD_PAIS, string COD_COMPANIA)
        {
            bool resul = false;
            try
            {
                string QUERY_DELETE = ("DELETE FROM INTBAW.INT_HOM_TIPO_DOC" +
                    " WHERE TIPO_DOC_EXACTUS = '" + TIPO_DOC_EXACTUS + "' AND TIPO_DOC_BAW = '" + TIPO_DOC_BAW + "' AND COD_PAIS = '" +
                    "" + COD_PAIS + "' AND COD_COMPANIA = '" + COD_COMPANIA + "'");

                resul = cn.Execute(QUERY_DELETE, "BAW");

                if (resul)
                {
                    return Ok("true"); ;
                }
                else
                {
                    return Ok("false"); ;
                }

            }
            catch (Exception)
            {

                return Ok("false"); ;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("api/Acount_Cuentas_API/GetHomCuentasGrid")]
        public List<Hom_Cuentas> GetHomCuentasGrid(string username)
        {
            List<Hom_Cuentas> listView = new List<Hom_Cuentas>();
            try
            {
                DataTable ViewReporte = cn.Query("SELECT CUENTA_CONTABLE_ENC, CUENTA_CONTABLE_DET, CENTRO_COSTO_ENC,CENTRO_COSTO_DET, DIVISION,COD_COMPANIA,COD_PAIS FROM INTBAW.INT_HOM_CUENTAS_CONTABLES", "BAW");

                foreach (DataRow line in ViewReporte.Rows)
                {
                    Hom_Cuentas view = new Hom_Cuentas
                    {
                        CUENTA_CONTABLE_ENC = line["CUENTA_CONTABLE_ENC"].ToString(),
                        CUENTA_CONTABLE_DET = line["CUENTA_CONTABLE_DET"].ToString(),
                        CENTRO_COSTO_ENC = line["CENTRO_COSTO_ENC"].ToString(),
                        CENTRO_COSTO_DET = line["CENTRO_COSTO_DET"].ToString(),
                        DIVISION = line["DIVISION"].ToString(),
                        COD_COMPANIA = line["COD_COMPANIA"].ToString(),
                        COD_PAIS = line["COD_PAIS"].ToString()
                    };
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }





        [Route("api/Acount_Cuentas_API/Create_HomCuentas")]
        [HttpGet]

        public IHttpActionResult Create_HomCuentas(string CUENTA_CONTABLE_ENC, string CUENTA_CONTABLE_DET, string CENTRO_COSTO_ENC, string CENTRO_COSTO_DET, string DIVISION, string COD_COMPANIA, string COD_PAIS)
        {
            bool resul = false;
            try
            {
                string QUERY_Select = ("INSERT INTO INTBAW.INT_HOM_CUENTAS_CONTABLES(CUENTA_CONTABLE_ENC, CUENTA_CONTABLE_DET, CENTRO_COSTO_ENC,CENTRO_COSTO_DET,DIVISION,COD_COMPANIA,COD_PAIS)" +
                    " VALUES(  '" + CUENTA_CONTABLE_ENC + "' ,'" + CUENTA_CONTABLE_DET + "','" +
                    "" + CENTRO_COSTO_ENC + "','" + CENTRO_COSTO_DET + "','" +
                    "" + DIVISION + "','" + COD_COMPANIA + "','" + COD_PAIS + "')");

                resul = cn.Execute(QUERY_Select, "BAW");

                if (resul)
                {
                    return Ok("Documento Creado Correctamente");
                }
                else
                {

                    DataTable ViewReporte = cn.Query("SELECT CUENTA_CONTABLE_ENC, CUENTA_CONTABLE_DET, CENTRO_COSTO_ENC,CENTRO_COSTO_DET, DIVISION,COD_COMPANIA,COD_PAIS FROM INTBAW.INT_HOM_CUENTAS_CONTABLES" +
                          " WHERE CUENTA_CONTABLE_ENC = '" + CUENTA_CONTABLE_ENC + "' AND CUENTA_CONTABLE_DET = '" + CUENTA_CONTABLE_DET + "'" +
                          " CENTRO_COSTO_ENC = '" + CENTRO_COSTO_ENC + "' AND CENTRO_COSTO_DET = '" + CENTRO_COSTO_DET + "'" +
                          " AND DIVISION = '" + DIVISION + "' AND COD_COMPANIA = '" + COD_COMPANIA + "' AND  COD_PAIS= '" + COD_PAIS + "'", "BAW");


                    if (ViewReporte.Rows.Count > 0)
                    {
                        return Ok("La Cuenta Contable ya existe");
                    }
                    else
                    {
                        return Ok("Error al Crear la Cuenta Contable");
                    }


                }

            }
            catch (Exception)
            {

                return Ok("false"); 
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TIPO_DOC_EXACTUS"></param>
        /// <param name="TIPO_DOC_BAW"></param>
        /// <param name="COD_PAIS"></param>
        /// <param name="COD_COMPANIA"></param>
        /// <returns></returns>
        [Route("api/Acount_Cuentas_API/Delete_HomCuentas")]
        [HttpGet]

        public IHttpActionResult Delete_HomCuentas(string CUENTA_CONTABLE_ENC, string CUENTA_CONTABLE_DET, string CENTRO_COSTO_ENC, string CENTRO_COSTO_DET, string DIVISION, string COD_COMPANIA, string COD_PAIS)
        {
            bool resul = false;
            try
            {
                string QUERY_DELETE = ("DELETE FROM INTBAW.INT_HOM_CUENTAS_CONTABLES" +
                         " WHERE CUENTA_CONTABLE_ENC = '" + CUENTA_CONTABLE_ENC + "' AND CUENTA_CONTABLE_DET = '" + CUENTA_CONTABLE_DET + "'" +
                          " AND CENTRO_COSTO_ENC = '" + CENTRO_COSTO_ENC + "' AND CENTRO_COSTO_DET = '" + CENTRO_COSTO_DET + "'" +
                          " AND DIVISION = '" + DIVISION + "' AND COD_COMPANIA = '" + COD_COMPANIA + "' AND  COD_PAIS= '" + COD_PAIS + "'");

                resul = cn.Execute(QUERY_DELETE, "BAW");

                if (resul)
                {
                    return Ok("true"); ;
                }
                else
                {
                    return Ok("false"); ;
                }

            }
            catch (Exception)
            {

                return Ok("false"); ;
            }

        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="Tabla"></param>
        /// <param name="UserName"></param>
        /// <param name="ValorViejo"></param>
        /// <param name="ValorNuevo"></param>
        /// <returns></returns>
        [Route("api/Acount_API/Audit")]
        [HttpGet]

        public IHttpActionResult Audit(string Tabla, string UserName, string ValorViejo, string ValorNuevo, string Evento)
        {
            bool resul = false;
            try
            {

                string QUERY_Create = (" INSERT INTO INTBAW.INT_AUDITORIA(  VALORVIEJO, VALORNUEVO, USUARIO,TABLA, FECHA,EVENTO)" +
                    " VALUES ( '" + ValorViejo + "','" + ValorNuevo + "','" +
                    "" + UserName + "','" + Tabla + "', sysdate,'" + Evento + "')");

                resul = cn.Execute(QUERY_Create, "BAW");

                if (resul)
                {
                    return Ok("true"); ;
                }
                else
                {
                    return Ok("false"); ;
                }

            }
            catch (Exception)
            {

                return Ok("false"); ;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        [Route("api/Int_Saldos/GetIntSaldosDiarios")]
        public List<Int_SaldosDiarios> GetIntSaldosDiarios(string datestar, string dateend)
        {
            List<Int_SaldosDiarios> listView = new List<Int_SaldosDiarios>();
            try
            {
                string fecha = "BETWEEN TO_DATE( '" + datestar + "','mm-dd-yyyy') AND TO_DATE( '" + dateend + "','mm-dd-yyyy')";

                DataTable ViewReporte = cn.Query("SELECT ASIENTO, COD_COMPANIA, COD_PAIS,CREDITOS_LOCAL, CUENTA_CONTABLE," +
                                                " DEBITO_DOLAR,DEBITO_LOCAL, DESCRIPCION_NIT, DESCRIPCIÓN," +
                                                " FECHA, FUENTE, NIT,NOMBRE_COMPANIA, NOMBRE_PAIS, ORIGEN, " +
                                                " PAQUETE, REFERENCIA, TIPO_ASIENTO FROM INTBAW.INT_SALDOS_DIARIOS WHERE FECHA " + fecha, "BAW");

                foreach (DataRow line in ViewReporte.Rows)
                {
                    Int_SaldosDiarios view = new Int_SaldosDiarios
                    {
                        ASIENTO = line["ASIENTO"].ToString(),
                        COD_COMPANIA = line["COD_COMPANIA"].ToString(),
                        COD_PAIS = line["COD_PAIS"].ToString(),
                        CREDITOS_LOCAL = line["CREDITOS_LOCAL"].ToString(),
                        CUENTA_CONTABLE = line["CUENTA_CONTABLE"].ToString(),
                        DEBITO_DOLAR = line["DEBITO_DOLAR"].ToString(),
                        DESCRIPCION_NIT = line["DESCRIPCION_NIT"].ToString(),
                        DESCRIPCIÓN = line["DESCRIPCIÓN"].ToString(),
                        FECHA = line["FECHA"].ToString(),
                        FUENTE = line["FUENTE"].ToString(),
                        NIT = line["NIT"].ToString(),
                        NOMBRE_COMPANIA = line["NOMBRE_COMPANIA"].ToString(),
                        NOMBRE_PAIS = line["NOMBRE_PAIS"].ToString(),
                        ORIGEN = line["ORIGEN"].ToString(),
                        PAQUETE = line["PAQUETE"].ToString(),
                        REFERENCIA = line["REFERENCIA"].ToString(),
                        TIPO_ASIENTO = line["TIPO_ASIENTO"].ToString()
                    };
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>

        [Route("api/Int_Auditoria/GetIntAuditoria")]
        public List<Int_Auditoria> GetIntAuditoria(string username)
        {
            List<Int_Auditoria> listView = new List<Int_Auditoria>();
            try
            {
                DataTable ViewReporte = cn.Query(" SELECT  EVENTO, FECHA, TABLA,USUARIO, VALORNUEVO, VALORVIEJO FROM INTBAW.INT_AUDITORIA"
                                                , "BAW");

                foreach (DataRow line in ViewReporte.Rows)
                {
                    Int_Auditoria view = new Int_Auditoria
                    {
                        EVENTO = line["EVENTO"].ToString(),
                        FECHA = line["FECHA"].ToString(),
                        TABLA = line["TABLA"].ToString(),
                        USUARIO = line["USUARIO"].ToString(),
                        VALORNUEVO = line["VALORNUEVO"].ToString(),
                        VALORVIEJO = line["VALORVIEJO"].ToString(),

                    };
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("api/Int_CXC/GetInt_CXCALL")]
        public List<Int_CXC> GetInt_CXCALL(string datestar, string dateend)
        {
            List<Int_CXC> listView = new List<Int_CXC>();
            try
            {

                string fecha = "BETWEEN TO_DATE( '" + datestar + "','mm-dd-yyyy') AND TO_DATE( '" + dateend + "','mm-dd-yyyy')";

                DataTable ViewReporte = cn.Query(" SELECT  CENTRO_COSTO, COD_COMPANIA, COD_PAIS, COD_PROVEEDOR, FECHA, IVA, " +
                    " MONEDA, MONTO_SIN_IVA, NIT_PROVEEDOR,NOMBRE_COMPANIA, NOMBRE_PAIS, NOMBRE_PROVEEDOR,NUM_FACTURA FROM INTBAW.INT_CXC WHERE FECHA " + fecha, "BAW");


                foreach (DataRow line in ViewReporte.Rows)
                {
                    Int_CXC view = new Int_CXC
                    {
                        CENTRO_COSTO = line["CENTRO_COSTO"].ToString(),
                        COD_COMPANIA = line["COD_COMPANIA"].ToString(),
                        COD_PAIS = line["COD_PAIS"].ToString(),
                        COD_PROVEEDOR = line["COD_PROVEEDOR"].ToString(),
                        FECHA = line["FECHA"].ToString(),
                        IVA = line["IVA"].ToString(),
                        MONEDA = line["MONEDA"].ToString(),
                        MONTO_SIN_IVA = line["MONTO_SIN_IVA"].ToString(),
                        NIT_PROVEEDOR = line["NIT_PROVEEDOR"].ToString(),
                        NOMBRE_COMPANIA = line["NOMBRE_COMPANIA"].ToString(),
                        NOMBRE_PAIS = line["NOMBRE_PAIS"].ToString(),
                        NOMBRE_PROVEEDOR = line["NOMBRE_PROVEEDOR"].ToString(),
                        NUM_FACTURA = line["NUM_FACTURA"].ToString(),

                    };
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [Route("api/Int_Ingresos_CXC/GetInt_Ingresos_CXC")]
        public List<Int_Ingresos_CXC> GetInt_Ingresos_CXC(string datestar, string dateend)
        {
            List<Int_Ingresos_CXC> listView = new List<Int_Ingresos_CXC>();
            try
            {

                string fecha = "BETWEEN TO_DATE( '" + datestar + "','mm-dd-yyyy') AND TO_DATE( '" + dateend + "','mm-dd-yyyy')";

                DataTable ViewReporte = cn.Query("SELECT ASIENTO,CUENTA_CONTABLE_ENC,CUENTA_CONTABLE_DET,CENTRO_COSTO_ENC,CENTRO_COSTO_DET,COD_COMPANIA,COD_PAIS, " +
                    " CREDITOS_DOLAR, CREDITOS_LOCAL, DEBITO_DOLAR, DEBITO_LOCAL," +
                    " DESCRIPCION_NIT, DESCRIPCIÓN, ESTADO, FECHA, FUENTE, IVA,  MONEDA, MONTO_SIN_IVA, NIT,NOMBRE_COMPANIA, NOMBRE_PAIS, ORIGEN," +
                    " PAQUETE, REFERENCIA, TIPO_ASIENTO,TIPO_CAMBIO ,ESTADO " +
                    " FROM INTBAW.INT_INGRESOS_CXC  WHERE FECHA " + fecha, "BAW");


                foreach (DataRow line in ViewReporte.Rows)
                {
                    Int_Ingresos_CXC view = new Int_Ingresos_CXC
                    {
                        ASIENTO = line["ASIENTO"].ToString(),
                        CUENTA_CONTABLE_ENC = line["CUENTA_CONTABLE_ENC"].ToString(),
                        CUENTA_CONTABLE_DET = line["CUENTA_CONTABLE_DET"].ToString(),
                        CENTRO_COSTO_ENC = line["CENTRO_COSTO_ENC"].ToString(),
                        CENTRO_COSTO_DET = line["CENTRO_COSTO_DET"].ToString(),
                        COD_COMPANIA = line["COD_COMPANIA"].ToString(),
                        COD_PAIS = line["COD_PAIS"].ToString(),
                        CREDITOS_DOLAR = line["CREDITOS_DOLAR"].ToString(),
                        CREDITOS_LOCAL = line["CREDITOS_LOCAL"].ToString(),
                        DEBITO_DOLAR = line["DEBITO_DOLAR"].ToString(),
                        DEBITO_LOCAL = line["DEBITO_LOCAL"].ToString(),
                        DESCRIPCION_NIT = line["DESCRIPCION_NIT"].ToString(),
                        DESCRIPCIÓN = line["DESCRIPCIÓN"].ToString(),
                        FECHA = line["FECHA"].ToString(),
                        IVA = line["IVA"].ToString(),
                        MONEDA = line["MONEDA"].ToString(),
                        MONTO_SIN_IVA = line["MONTO_SIN_IVA"].ToString(),
                        NIT = line["NIT"].ToString(),
                        NOMBRE_COMPANIA = line["NOMBRE_COMPANIA"].ToString(),
                        NOMBRE_PAIS = line["NOMBRE_PAIS"].ToString(),
                        ORIGEN = line["ORIGEN"].ToString(),
                        PAQUETE = line["PAQUETE"].ToString(),
                        REFERENCIA = line["REFERENCIA"].ToString(),
                        TIPO_ASIENTO = line["TIPO_ASIENTO"].ToString(),
                        TIPO_CAMBIO = line["TIPO_CAMBIO"].ToString(),
                        ESTADO = line["ESTADO"].ToString()

                    };


                    if (line["ESTADO"].ToString() == "1")
                    {
                        view.ESTADO = "PROCESADO";
                    }

                    if (line["ESTADO"].ToString() == "0")
                    {
                        view.ESTADO = "SIN PROCESAR";
                    }
                    if (line["ESTADO"].ToString() == "9")
                    {
                        view.ESTADO = "ERROR";
                    }
                    listView.Add(view);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return listView;
        }
    }
}