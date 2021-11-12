using CapaDatos;
using System;

namespace WebServiceTLA.Class
{
    public class SaldosDiarios
    {
        private readonly ConexionDeDatos conexion = new ConexionDeDatos();
        public string ASIENTO { get; set; }
        public string COD_COMPANIA { get; set; }
        public string COD_PAIS { get; set; }
        public float CREDITOS_LOCAL { get; set; }
        public string CUENTA_CONTABLE { get; set; }
        public float DEBITO_DOLAR { get; set; }
        public float DEBITO_LOCAL { get; set; }
        public string DESCRIPCION_NIT { get; set; }
        public string DESCRIPCIÓN { get; set; }
        public Nullable<DateTime> FECHA { get; set; }
        public string FUENTE { get; set; }
        public string NIT { get; set; }
        public string NOMBRE_COMPANIA { get; set; }
        public string NOMBRE_PAIS { get; set; }
        public string ORIGEN { get; set; }
        public string PAQUETE { get; set; }
        public string REFERENCIA { get; set; }
        public string TIPO_ASIENTO { get; set; }

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
        public bool InsertSaldosDiarios(SaldosDiarios saldos, string json, ref string mensaje2, ref string codigo)
        {
            bool result = false;
            string vSql = "";
            string ruta = "";
            //string Dirreccion = System.Configuration.ConfigurationManager.AppSettings["ruta"].ToString(); ;
            string Direccion = "\\\\pasqui-fs\\EMPRESAS\\SISTEMAS\\WSFILES\\";
            try
            {
                ruta = Direccion + "BAW" + "\\" + "BITACORA.log";
                System.IO.File.AppendAllText(ruta, "--------------------------------------------------------------------------------------------------------------------------------------------- \n");
                System.IO.File.AppendAllText(ruta, " Json Resivido: \n");
                System.IO.File.AppendAllText(ruta, json + "\n");
                System.IO.File.AppendAllText(ruta, "--------------------------------------------------------------------------------------------------------------------------------------------- \n");
                System.IO.File.AppendAllText(ruta, "SALDOS DIARIOS POR PROCESAR: " + saldos.ASIENTO + "  Hora: " + DateTime.Now.ToString() + "\n");
                System.IO.File.AppendAllText(ruta, "CREACION DE ASIENTO (BAW -> )  \n");





                vSql = "INSERT INTO INTBAW.INT_SALDOS_DIARIOS ( ASIENTO, COD_COMPANIA, COD_PAIS, CREDITOS_LOCAL, CUENTA_CONTABLE, DEBITO_DOLAR, DEBITO_LOCAL, DESCRIPCION_NIT, DESCRIPCIÓN," +
                      " FECHA, FUENTE, NIT,  NOMBRE_COMPANIA, NOMBRE_PAIS, ORIGEN, PAQUETE, REFERENCIA, TIPO_ASIENTO,ESTADO) " +
                     " VALUES ('" + saldos.ASIENTO + "', " +
                      "'" + saldos.COD_COMPANIA + "', " +
                      "'" + saldos.COD_PAIS + "', " +
                       "'" + saldos.CREDITOS_LOCAL + "', " +
                      "'" + saldos.CUENTA_CONTABLE + "', " +
                      "'" + saldos.DEBITO_DOLAR + "', " +
                      "'" + saldos.DEBITO_LOCAL + "', " +
                      "'" + saldos.DESCRIPCION_NIT + "', " +
                      "'" + saldos.DESCRIPCIÓN + "', " +
                      "TO_DATE('" + saldos.FECHA + "','mm/dd/yyyy hh:mi:ss AM'), " +
                      "'" + saldos.FUENTE + "', " +
                      "'" + saldos.NIT + "', " +
                      "'" + saldos.NOMBRE_COMPANIA + "', " +
                      "'" + saldos.NOMBRE_PAIS + "', " +
                      "'" + saldos.ORIGEN + "', " +
                      "'" + saldos.PAQUETE + "', " +
                      "'" + saldos.REFERENCIA + "', " +
                      "'" + saldos.TIPO_ASIENTO + "', " +
                      "'" + "0" + "')";



                result = conexion.Execute(vSql, "BAW");

                if (result)
                {
                   
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
            catch (Exception EX)
            {
                mensaje2 = "ERROR BD" + EX.ToString();
                codigo = " ";
                System.IO.File.AppendAllText(ruta, "ERROR (BAW -> )" + mensaje2 + "\n");
                return false;
            }
            return result;

        }

        public void SelectSaldosDiarios()
        {
            string query = "SELECT ASIENTO, COD_COMPANIA, COD_PAIS, CREDITOS_LOCAL, CUENTA_CONTABLE, DEBITO_DOLAR,DEBITO_LOCAL, DESCRIPCION_NIT, DESCRIPCIÓN," +
               "  FECHA, FUENTE, NIT,   NOMBRE_COMPANIA, NOMBRE_PAIS, ORIGEN,  PAQUETE, REFERENCIA, TIPO_ASIENTO FROM INTBAW.INT_SALDOS_DIARIOS";

            System.Data.DataTable dtCliente = conexion.Query(query, "BAW");

        }



    }
}