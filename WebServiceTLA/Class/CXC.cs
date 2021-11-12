using CapaDatos;
using System;

namespace WebServiceTLA.Class
{

    public class CXC
    {
        private readonly ConexionDeDatos conexion = new ConexionDeDatos();
        public string NUM_FACTURA { get; set; }
        public string NOMBRE_PROVEEDOR { get; set; }
        public string NOMBRE_PAIS { get; set; }
        public string NOMBRE_COMPANIA { get; set; }
        public string NIT_PROVEEDOR { get; set; }
        public float MONTO_SIN_IVA { get; set; }
        public string MONEDA { get; set; }
        public float IVA { get; set; }
        public Nullable<DateTime> FECHA { get; set; }
        public string COD_PROVEEDOR { get; set; }
        public string COD_PAIS { get; set; }
        public string COD_COMPANIA { get; set; }
        public string CENTRO_COSTO { get; set; }





        public class DocumentoC
        {
            public string ASIENTO { get; set; }
            public string ASIENTOEXACTUS { get; set; }
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
        public bool InsertCXC(CXC saldos, string json, ref string mensaje2, ref string codigo)
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
                System.IO.File.AppendAllText(ruta, "CXC POR PROCESAR: " + saldos.NUM_FACTURA + "  Hora: " + DateTime.Now.ToString() + "\n");
                System.IO.File.AppendAllText(ruta, "CREACION DE FACTURA (BAW -> )  \n");





                vSql = "INSERT INTO INTBAW.INT_CXC( NUM_FACTURA, NOMBRE_PROVEEDOR, NOMBRE_PAIS, NOMBRE_COMPANIA, NIT_PROVEEDOR, MONTO_SIN_IVA," +
                    " MONEDA, IVA, FECHA,COD_PROVEEDOR, COD_PAIS, COD_COMPANIA,CENTRO_COSTO,ESTADO)" +

                     " VALUES ('" + saldos.NUM_FACTURA + "', " +
                      "'" + saldos.NOMBRE_PROVEEDOR + "', " +
                      "'" + saldos.NOMBRE_PAIS + "', " +
                       "'" + saldos.NOMBRE_COMPANIA + "', " +
                      "'" + saldos.NIT_PROVEEDOR + "', " +
                      "'" + saldos.MONTO_SIN_IVA + "', " +
                      "'" + saldos.MONEDA + "', " +
                      "'" + saldos.IVA + "', " +
                      "TO_DATE('" + saldos.FECHA + "','mm/dd/yyyy hh:mi:ss AM'), " +
                      "'" + saldos.COD_PROVEEDOR + "', " +
                      "'" + saldos.COD_PAIS + "', " +
                      "'" + saldos.COD_COMPANIA + "', " +
                       "'" + saldos.CENTRO_COSTO + "', " +
                      "'" + "0"+ "')";



                result = conexion.Execute(vSql, "BAW");

                if (result)
                {
                    System.IO.File.AppendAllText(ruta, "PROCESO CORRECTO \n");
                    mensaje2 = "PROCESO CORRECTO";
                    codigo = "101";
                }
                else
                {
                    System.IO.File.AppendAllText(ruta, "TRANSACCION DUBLICADA \n");
                    mensaje2 = "TRANSACCION DUBLICADA";
                    codigo = "104";
                }



            }
            catch (Exception EX)
            {
                mensaje2 = "ERROR BD, ERROR :" + EX.ToString();
                codigo = "ERROR";
                System.IO.File.AppendAllText(ruta, "ERROR (BAW -> )" + mensaje2 + "\n");
                return false;
            }
            return result;

        }

    }
}