using CapaDatos;
using System;
using System.Data;


namespace WebServiceTLA.Class
{
    public class Ingresos_CXC
    {
        private readonly ConexionDeDatos conexion = new ConexionDeDatos();
        public DateTime FECHA { get; set; }
        public string ASIENTO { get; set; }
        public string TIPO_ASIENTO { get; set; }
        public string ORIGEN { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public string DIVICION { get; set; }
        public string COD_COMPANIA { get; set; }
        public string COD_PAIS { get; set; }
        public string CREDITOS_LOCAL { get; set; }
        public string CREDITOS_DOLAR { get; set; }
        public string DEBITO_LOCAL { get; set; }
        public string DEBITO_DOLAR { get; set; }
        public string IVA { get; set; }
        public string MONTO_SIN_IVA { get; set; }
        public string DESCRIPCIÓN { get; set; }
        public string REFERENCIA { get; set; }
        public string FUENTE { get; set; }
        public string NIT { get; set; }
        public string DESCRIPCION_NIT { get; set; }
        public string PAQUETE { get; set; }
        public string NOMBRE_PAIS { get; set; }
        public string NOMBRE_COMPANIA { get; set; }
        public string MONEDA { get; set; }
        public string CENTRO_COSTO { get; set; }
        public string TIPO_CAMBIO { get; set; }







        public class Documento
        {
            public string ASIENTO { get; set; }
            public string COD_COMPANIA { get; set; }
            public string COD_PAIS { get; set; }
            public string CODIGO_ERROR { get; set; }
            public string ESTADO { get; set; }
            public string MENSAJE { get; set; }



        }
        public class Respuesta
        {
            public bool Estado { get; set; }
            public string Mensaje { get; set; }

        }

        public bool InsertIngresos_CXC(Ingresos_CXC saldos, string json, ref string mensaje2, ref string codigo, string ruta)
        {
            bool result = false;
            string vSql = "";    
            bool result_HomTipoDoc = false;
            bool result_HomCuentas = false;
            bool result_HomCompenia = false;

            
            try
            {
                
                System.IO.File.AppendAllText(ruta, "SALDOS DIARIOS POR PROCESAR: " + saldos.ASIENTO + "  Hora: " + DateTime.Now.ToString() + "\n");
                System.IO.File.AppendAllText(ruta, "CREACION DE ASIENTO (BAW -> )  \n");

                 string  Cod_Compania_Exactus = "";
                if (HomCompania(saldos.COD_COMPANIA,ref Cod_Compania_Exactus))
                {
                    result_HomCompenia = true;
                }
                else
                {
                    System.IO.File.AppendAllText(ruta, "COMPAÑÍA NO HOMOLOGADA \n");
                    mensaje2 = "COMPAÑÍA NO HOMOLOGADA= " + saldos.COD_COMPANIA;
                    codigo = "106";
                }

                //HOMOLOGACION DE TIPO DE DOCUMENTO
                string TipoDocumentoExactus = "";

                if (HomTipoDocumento(saldos.TIPO_ASIENTO, ref TipoDocumentoExactus))
                {
                    result_HomTipoDoc = true;
                }
                else
                {
                    System.IO.File.AppendAllText(ruta, "TIPO DE DOCUMENTO NO HOMOLOGADO \n");
                    mensaje2 = "TIPO DE DOCUMENTO NO HOMOLOGADO = " + saldos.TIPO_ASIENTO;
                    codigo = "111";
                }

                /// HOMOLOGACION DE CUENTAS
                string CuentaContableExactusENC = "";
                string CentroCostosENC = "";
                string CuentaContableExactusDET = "";
                string CentroCostosDET = "";

                if (HomCuentasContables(saldos.DIVICION, ref CuentaContableExactusENC, ref CentroCostosENC, ref CuentaContableExactusDET, ref CentroCostosDET))
                {
                    result_HomCuentas = true;
                }
                else
                {
                    System.IO.File.AppendAllText(ruta, "CUANTA CONTABLE NO HOMOLOGADA  \n");
                    mensaje2 = "CUENTA CONTABLE NO HOMOLOGADA = " + saldos.DIVICION.ToString();
                    codigo = "105";
                }

                if (result_HomTipoDoc == true && result_HomCuentas == true && result_HomCompenia == true)
                {

                    vSql = "INSERT INTO INTBAW.INT_INGRESOS_CXC ( ASIENTO,CUENTA_CONTABLE_ENC,CUENTA_CONTABLE_DET,CENTRO_COSTO_ENC,CENTRO_COSTO_DET,COD_COMPANIA_BAW,COD_COMPANIA_EXACTUS,COD_PAIS,CREDITOS_DOLAR, CREDITOS_LOCAL, " +
                            "DIVICION, DEBITO_DOLAR, DEBITO_LOCAL,  DESCRIPCION_NIT, DESCRIPCIÓN, ESTADO,FECHA, FUENTE, IVA," +
                            "MONEDA, MONTO_SIN_IVA, NIT,   NOMBRE_COMPANIA, NOMBRE_PAIS, ORIGEN, PAQUETE, REFERENCIA, TIPO_ASIENTO,TIPO_CAMBIO) " +
                         " VALUES ('" + saldos.ASIENTO + "', " +
                          "'" + CuentaContableExactusENC + "', " +  //CUENTA CONTABLE EXACTUS
                          "'" + CuentaContableExactusDET + "', " + //CUENTA CONTABLE EXACTUS
                          "'" + CentroCostosENC + "', " +
                          "'" + CentroCostosDET + "', " +
                          "'" + saldos.COD_COMPANIA + "', " +    //CUENTA CONTABLE BAW
                          "'" + Cod_Compania_Exactus + "', " +    //CUENTA CONTABLE EXACTUS
                          "'" + saldos.COD_PAIS + "', " +
                          "'" + saldos.CREDITOS_DOLAR + "', " +
                          "'" + saldos.CREDITOS_LOCAL + "', " +
                           "'" + DIVICION + "', " +               //CUENTA CONTABLE BAW
                          "'" + saldos.DEBITO_DOLAR + "', " +
                          "'" + saldos.DEBITO_LOCAL + "', " +
                          "'" + saldos.DESCRIPCION_NIT + "', " +
                          "'" + saldos.DESCRIPCIÓN + "', " +
                          "'0', " +
                          "TO_DATE('" + saldos.FECHA + "','mm/dd/yyyy hh:mi:ss AM'), " +
                          "'" + saldos.FUENTE + "', " +
                          "'" + saldos.IVA + "', " +
                          "'" + saldos.MONEDA + "', " +
                          "'" + saldos.MONTO_SIN_IVA + "', " +
                          "'" + saldos.NIT + "', " +
                          "'" + saldos.NOMBRE_COMPANIA + "', " +
                          "'" + saldos.NOMBRE_PAIS + "', " +
                          "'" + saldos.TIPO_ASIENTO + "', " +   //TIPO DE DOCUMENTO DE BAW
                          "'" + TipoDocumentoExactus + "', " +  //TIPO DE DOCUMENTO DE EXACTUS
                          "'" + saldos.REFERENCIA + "', " +
                          "'" + TipoDocumentoExactus + "', " + //TIPO DE DOCUMENTO DE EXACTUS
                          "'" + saldos.TIPO_CAMBIO + "')";

                    result = conexion.Execute(vSql, "BAW");

                    if (result)
                    {
                       // conexion.SP_INGRESOS_CXC(ruta);
                        System.IO.File.AppendAllText(ruta, "PROCESO CORRECTO  \n");
                        mensaje2 = "PROCESO CORRECTO";
                        codigo = "101";
                    }
                    else
                    {
                        System.IO.File.AppendAllText(ruta, "TRANSACCION DUBLICADA  \n");
                        mensaje2 = "TRANSACCION DUBLICADA";
                        codigo = "104";
                    }
                }
                else
                {


                    //return false;
                }


            }
            catch (Exception EX)
            {
                mensaje2 = "ERROR BD" + EX.ToString();
                codigo = " ";
                System.IO.File.AppendAllText(ruta, "ERROR (BAW -> )" + mensaje2 + "\n");
                return false;
            }
            return result;

        }
        public bool ConfiguracionBaw( ref string Nombre_Bitacora, ref string Ruta, ref string Nombre_Carpeta)
        {
            try
            {
               string query = "SELECT NOMBREBITACORA, RUTA, NOMBRECARPETA" +
                " FROM INTBAW.CONFIGURACION";

                System.Data.DataTable dtData = conexion.Query(query, "BAW");

            foreach (DataRow line in dtData.Rows)
            {
                    Nombre_Bitacora= line["NOMBREBITACORA"].ToString();
                    Ruta= line["RUTA"].ToString();
                    Nombre_Carpeta = line["NOMBRECARPETA"].ToString();
                }
            }
            catch (Exception  ex)
            {
             return false;
                throw;
            }

            return true;
         

        }

        public bool HomCompania(string Compania_Baw,ref string Cod_Compania_Exactus)
        {
            string query = "SELECT  COMPANIA_EXACTUS" +
                " FROM INTBAW.INT_HOM_COMPANIA WHERE COMPANIA_BAW='" + Compania_Baw + "' ";

            System.Data.DataTable dtData = conexion.Query(query, "BAW");

            foreach (DataRow line in dtData.Rows)
            {
                Cod_Compania_Exactus = line["COMPANIA_EXACTUS"].ToString();

                return true;
            }
            return false;

        }
        public bool HomTipoDocumento(string TipoDucumentoBaw, ref string TipoDocumentoExactus)
        {


            string query = "SELECT  TIPO_DOC_EXACTUS" +
                " FROM INTBAW.INT_HOM_TIPO_DOC WHERE TIPO_DOC_BAW='" + TipoDucumentoBaw + "' ";

            System.Data.DataTable dtData = conexion.Query(query, "BAW");
            foreach (DataRow line in dtData.Rows)
            {
                TipoDocumentoExactus = line["TIPO_DOC_EXACTUS"].ToString();

                return true;
            }
            return false;

        }
        public bool HomCuentasContables(string Divicion, ref string CuentaContableExactusENC, ref string CentroCostosENC, ref string CuentaContableExactusDET, ref string CentroCostosDET)
        {


            string query = "SELECT CUENTA_CONTABLE_ENC,CUENTA_CONTABLE_DET,CENTRO_COSTO_ENC,CENTRO_COSTO_DET, COD_COMPANIA, COD_PAIS, CUENTA_CONTABLE_DET,DIVISION CUENTA_CONTABLE_ENC " +
                " FROM INTBAW.INT_HOM_CUENTAS_CONTABLES WHERE DIVISION='" + Divicion + "' ";

            System.Data.DataTable dtData = conexion.Query(query, "BAW");
            foreach (DataRow line in dtData.Rows)
            {
                CuentaContableExactusENC = line["CUENTA_CONTABLE_ENC"].ToString();
                CuentaContableExactusDET = line["CUENTA_CONTABLE_DET"].ToString();
                CentroCostosENC = line["CENTRO_COSTO_ENC"].ToString();
                CentroCostosDET = line["CENTRO_COSTO_DET"].ToString();
                return true;
            }
            return false;

        }
        public string SelectAsientoExactus(string ASIENTO)
        {
            string ASIENTOEXACTUS = "";


            string query = "SELECT ASIENTOEXACTUS FROM INTBAW.INT_INGRESOS_CXC WHERE ASIENTO='" + ASIENTO + "'";

            System.Data.DataTable dtData = conexion.Query(query, "BAW");
            foreach (DataRow line in dtData.Rows)
            {
                ASIENTOEXACTUS = line["ASIENTOEXACTUS"].ToString();

            }
            return ASIENTOEXACTUS;
        }
        public bool DeleteIngresosCXC()
        {
         
            string query = "DELETE FROM INTBAW.INT_INGRESOS_CXC WHERE ESTADO IN('9','0')";
          bool  resul = conexion.Execute(query, "BAW");
           
            return true;
        }
    }
}