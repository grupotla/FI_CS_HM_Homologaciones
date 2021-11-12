using AcountingSite.Models;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace AcountingSite.Controllers.Api
{
    public class Catalogos_APIController : ApiController
    {
        public Catalogos_APIController()
        {
        }

        private readonly CapaDatos.ConexionDeDatos cn = new CapaDatos.ConexionDeDatos();

        public class IERP_FILTROS
        {
            public string IERPF_OPERADOR { get; set; }
            public string IERPF_CAMPO { get; set; }
            public string IERPF_COMPARATIVO { get; set; }
            public string IERPF_CAMPO_PROGRA { get; set; }
            public string IERPQF_SEP1 { get; set; }
            public string IERPQF_SEP2 { get; set; }

        }

        public class IERP_QUERYS
        {
            public string IERPQ_ID { get; set; }
            public string IERPQ_SQL { get; set; }
            public string IERPQ_SQL_ORDER { get; set; }
            public string IERPCAT_ID { get; set; }
            public string IERPCAT_CATEGO { get; set; }
            public string IERPQ_SQL_UPDATE { get; set; }


            public string EMPRESA_COD_INTERNO { get; set; }
            public string EMPRESA_ESQUEMA { get; set; }
            public string IERPE_CODIGO { get; set; }
            public string IERPE_ERP_CODIGO { get; set; }
            public string EMPRESA_DBLINK { get; set; }

            public string IERPCAT_CATEGO_DES { get; set; }
        }

        public class IB_CATALOGOS
        {
            public string IERP_ID { get; set; }
            public string IERPM_ID { get; set; }
            public string IERP_TITULO { get; set; }
            public string IERP_CODIGO { get; set; }
            public string IERP_ERP_CODIGO { get; set; }
            public string IERP_STATUS { get; set; }
            public string IERP_USER { get; set; }
            public string IERP_DATE { get; set; }
            public string IERPS_SEC { get; set; }
            public string IERP_SQL { get; set; }
            public string IERP_CAMPO_PROGRA { get; set; }
            public string IERP_ORDEN { get; set; }
            public string IERP_OPERADOR { get; set; }
        }

        public class IB_HOMOLOGACIONES
        {
            public string IERPH_CODIGO { get; set; }
            public string IERPH_NOMBRE { get; set; }
            public string IERPH_ERP_CODIGO { get; set; }
            public string IERPH_ERP_NOMBRE { get; set; }
            public string IERPH_NIT { get; set; }
            public string IERPH_CAT { get; set; }
            public string IERPH_CAT_DES { get; set; }
            public string IERPH_ID { get; set; }
            public string IERPH_USER { get; set; }
            public string IERPH_DATE { get; set; }
            public string IERPH_STATUS { get; set; }
            public string IERPH_STATUS_DES { get; set; }
            public string IERPM_ID { get; set; }
            public string IERPC_ID { get; set; }
            public string IERPP_ID { get; set; }
            public string IERPE_ID { get; set; }
            public string IERPH_ACTIVO { get; set; }
            public string IERPH_ACTIVO_DES { get; set; }
            public string IERPH_CODIGO2 { get; set; }
            public string IERPH_ERP_CODIGO2 { get; set; }
            public string IERPH_SYSTEMA { get; set; }
            public string IERPH_TIPO_MONEDA { get; set; }
            public string IERPH_HOMOLOGADO { get; set; }
        }

        public class IB_STANDAR
        {
            public string data1 { get; set; }
            public string data2 { get; set; }
            public string data3 { get; set; }
            public string data4 { get; set; }

        }


        /*
        public DataTable EjecutarConsulta(string o, string IERPM_ID, string sql)
        {
            string query = "SELECT IERPM_DBTYPE as data1, IERPM_CONEXION as data2 FROM INTBAW.IERP_MODULOS WHERE IERPM_STATUS = '1' AND IERPM_ID = " + IERPM_ID;
            //, IERPM_ESQUEMA as data1, IERPM_ERP_ESQUEMA as data2, IERPM_CONEXION as data3, IERPM_ERP_CONEXION as data4DBTY
            IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(query);

            DataTable ViewTable = null;

            switch (data.data1) {
                case "POSTGRES":
                    if (o == "c")
                        ViewTable = CapaDatos.Postgres.EjecutarConsulta(data.data2, sql);
                    if (o == "e")
                        CapaDatos.Postgres.EjecutaQuery(data.data2, sql);
                    break;
        op
                case "ORACLE":
                    if (o == "c")
                        ViewTable = cn.GetListStandar(data.data2, sql);
                    if (o == "e")
                        CapaDatos.ConexionDeDatos.EjecutaQuery(data.data2, sql);
                    break;
            }

            return ViewTable;
        }
        */





        public DataTable GetDataTable(string OPT, string IERPM_ID, string sql) 
        {
            DataTable ViewTable = null;

            try
            {
                string query = "SELECT IERPM_DBTYPE as data1, IERPM_CONEXION as data2, IERPM_ERP_ESQUEMA as data3 FROM INTBAW.IERP_MODULOS WHERE IERPM_STATUS = '1' AND IERPM_ID = " + IERPM_ID;
                IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(query);

                switch (data.data1)
                {
                    case "POSTGRES":
                        if (OPT == "c")
                            ViewTable = CapaDatos.Postgres.EjecutarConsulta(data.data2, sql);
                        if (OPT == "e")
                            CapaDatos.Postgres.EjecutaQuery(data.data2, sql);
                        break;

                    case "ORACLE":
                        if (OPT == "c")
                            ViewTable = cn.EjecutarConsulta2(data.data2, sql);
                        if (OPT == "e")
                            CapaDatos.ConexionDeDatos.EjecutaQuery(data.data2, sql);
                        break;
                } 
            }
            catch (Exception e)
            {
                throw e;
            }
            return ViewTable;
        }


        public List<TEntity> GetListStandar<TEntity>(string OPT, string IERPM_ID, string sql) where TEntity : class, new()
        {
            List<TEntity> rows = new List<TEntity>();
            try
            {
                DataTable ViewTable = GetDataTable(OPT, IERPM_ID, sql);
                if (ViewTable != null)
                rows = Utils.get_list_struct2<TEntity>(ViewTable);
            }
            catch (Exception e)
            {
                throw e;
            }
            return rows;
        }

        



        public void UpdateHomologacionesOperativo(string cod_cs, string nam_cs, string cod_ex, string nam_ex, string estado, string module, string module_name, string empresa_id, string catalogo_cs, string catalogo_erp, string otros, string pais, string pais_erp, string modulo) 
        {

            string sql = @"SELECT a.IERPM_ID as IERPQ_ID, a.IERPE_CODIGO, a.IERPE_ERP_CODIGO, b.EMPRESA_ESQUEMA, b.EMPRESA_DBLINK
FROM  INTBAW.IERP_EMPRESAS a 
INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = a.IERPE_ERP_CODIGO
WHERE a.IERPE_ID = " + empresa_id;

            IERP_QUERYS data = cn.EjecutarRow<IERP_QUERYS>(sql);


            sql = @"INSERT INTO exactus_homologaciones (
            eh_empresa,
            eh_erp_empresa,
            eh_erp_esquema,
            eh_categoria,
            eh_erp_categoria,
            eh_codigo,
            eh_descripcion,
            eh_estado,
            eh_erp_codigo,
            eh_erp_descripcion,
            eh_otros,
            eh_pais,
            eh_erp_pais	,
            eh_modulo
) VALUES ('" + data.IERPE_CODIGO + "', '" + data.IERPE_ERP_CODIGO + "', '" + data.EMPRESA_ESQUEMA + "', '" + catalogo_cs + @"', '" + catalogo_erp + @"',

            '" + cod_cs + "','" + nam_cs + "','" + estado + "','" + cod_ex + "','" + nam_ex + "','" + otros + "','" + pais + "','" + pais_erp + "','" + modulo + "')";

            GetListStandar<IB_STANDAR>("e", data.IERPQ_ID, sql);

            //exactus_homologaciones_fn
            //exactus_homologaciones_tg

//            UPDATE exactus_homologaciones SET eh_estado = 2 WHERE eh_estado = 1 AND eh_codigo = '#*5*#' AND eh_erp_categoria = '#*4*#' AND eh_erp_esquema = 'TRANSIT'; 
//            INSERT INTO exactus_homologaciones(eh_empresa, eh_erp_empresa, eh_erp_esquema, eh_otros, eh_estado, eh_erp_codigo, eh_erp_descripcion, eh_erp_categoria, eh_codigo) 
//            SELECT 'CRTLA', 1, 'TRANSIT', CAST(NOW() as text), #*1*#, '#*2*#', '#*3*#', '#*4*#', '#*5*#';
        }



        //GET_ROLUSER
        [Route("api/Catalogos_Calls_API/GetRolUser")]
        public IB_STANDAR GetRolUser(string usuario)
        {            
            try
            {

                if (usuario == null) usuario = "SOPORTE7";

                IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(@"SELECT UPPER(IERPUR_USERNAME) as data1, IERPUR_ROLL as data2, IERPUR_ADMIN as data3, IERPURM_PK_CONTACT data4 
                FROM INTBAW.IERP_USER_ROLES WHERE IERPUR_STATUS = '1' AND UPPER(IERPUR_USERNAME) = '" + usuario.ToUpper() + "'");

                return data;

            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }

        }


        ///////////////////////////////////////////////////////  GET TEST  //////////////////////////////////////////////////////////
        [Route("api/Catalogos_Calls_API/GetTest")]
        public string GetTest()
        {

            /* CARGA DE ARCHIVO PARA HOMOLOGAR
            string test = "", sql = "", erp_code = "", fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string fileexcel = @"C:\Progra\HOMOLOGACIONES\ArticulosHomologar.csv";

            string filelog = @"C:\Progra\HOMOLOGACIONES\" + fecha.Substring(0,10) + ".txt";

            int counter = 0; Boolean res = false;
            string line;

            test = @"---------------------------------------" + fecha + @"------------------------------------------------------

               ";


            try
            {

                System.IO.File.AppendAllText(filelog, test + Environment.NewLine);

                // Read the file and display it line by line.  
                System.IO.StreamReader file = new System.IO.StreamReader(fileexcel);
                while ((line = file.ReadLine()) != null)
                {
                    string[] columns = line.Split(';');

                    erp_code = columns[5].ToUpper().Trim();

                    if (counter > 0 && erp_code != "NO TIENE")
                    {

                        System.IO.File.AppendAllText(filelog, @"

-- " + counter + ".- " + Environment.NewLine);

sql = @"INSERT INTO INTBAW.IERP_HOMOLOGACIONES (
IERPM_ID,
IERPC_ID,
IERPP_ID,
IERPE_ID,
IERPH_CODIGO,
IERPH_NOMBRE,
IERPH_ERP_CODIGO,
IERPH_ERP_NOMBRE,
IERPH_STATUS,
IERPH_USER,
IERPS_SEC,
IERPH_TIPO_MONEDA
) VALUES (1, 161, 201, 21, '" + columns[0] + "', '" + columns[3] + "', '" + erp_code + "', (SELECT COALESCE(UPPER(DESCRIPCION),'') FROM TRANSIT.ARTICULO WHERE ARTICULO = '" + erp_code + "'), '1', '', 0, '')";

                        
                        // to_char(CURRENT_DATE,'DD/MM/YYYY')

                        System.IO.File.AppendAllText(filelog, sql + Environment.NewLine);

                        try {

                            sql = sql.Replace("\r\n", "");

                            res = cn.Execute(sql);

                            test = (res ? "OK" : "");

                        }
                        catch (Exception ex)
                        {
                            test = ex.Message;
                        }

                        test = @"---------------------------------------" + test + @"------------------------------------------------------
                  ";

                        System.IO.File.AppendAllText(filelog, test + Environment.NewLine);

                    }

                    if (erp_code != "NO TIENE")
                        counter++;
                }

                file.Close();

                test = "PROCESO CORRECTO";

            }
            catch (Exception ex)
            {
                test = ex.Message;
            }
*/


            //trae el query de monedas operativo
            //string query = "SELECT IERPQ_SQL FROM INTBAW.IERP_QUERYS WHERE IERPQ_SISTEM = 'CARGO SYSTEM' AND IERPQ_STATUS = '1' AND IERPQ_CATEGO = '08'";

            //query = cn.GetScalar(query);

            //IB_HOMOLOGACIONES data = CapaDatos.Postgres.EjecutarRow<IB_HOMOLOGACIONES>(query,"CARGO SYSTEM"); //trae una fila 

            //string test = data.IERPH_NOMBRE;

            string ip = Utils.GetIPAddress();

            string query = @"SELECT 'CONEXION CORRECTA' as data1 FROM usuarios_empresas WHERE id_usuario = '1237'";

            List<IB_STANDAR> listView =  GetListStandar<IB_STANDAR>("c", "1", query);

            return ip + " :: " + listView[0].data1;
        }



        //SET_TIPO_MONEDA
        [Route("api/Catalogos_Calls_API/Set_TipoMoneda")]
        public string Set_TipoMoneda()
        {
            string query = "SELECT IERPTM_CODIGO as data1, IERPTM_DESCRIPCION as data2 FROM INTBAW.IERP_TIPO_MONEDA WHERE IERPTM_STATUS = '1' ORDER BY IERPTM_ID";

            DataTable ViewTable = cn.Query(query, "BAW");

            string select = Utils.BuildOptions(ViewTable, "");

            return select;
        }


        //GET_CATALOGOS
        [Route("api/Catalogos_Calls_API/GetCategorias")]
        public IB_STANDAR GetCategorias(string IERPC_ID)
        {
            string query = @"SELECT a.IERPCAT_CATEGO data1, a.IERPCAT_DESCRIPCION data2, b.IERPCAT_CATEGO data3, b.IERPCAT_DESCRIPCION data4

FROM INTBAW.IERP_CATALOGOS c

INNER JOIN INTBAW.IERP_CATEGORIAS a ON a.IERPCAT_ID = c.IERPC_CODIGO

INNER JOIN INTBAW.IERP_CATEGORIAS b ON b.IERPCAT_ID = c.IERPC_ERP_CODIGO

WHERE c.IERPC_ID = '" + IERPC_ID + @"'";

            IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(query);

            return data;
        }


        //GET_MODULOS - EMPRESAS
        [Route("api/Catalogos_Calls_API/GetModuleSelect")]
        public List<string> GetModuleSelect(string usuario)
        {
            List<string> data = new List<string>();

            string select = "";

            try
            {
                ////////////////////  MODULOS

                string query = @"SELECT DISTINCT IERPM_ID as data1, IERPM_NOMBRE as data2 
                FROM INTBAW.IERP_MODULOS 
                INNER JOIN INTBAW.IERP_USER_ROLES ON UPPER(IERPUR_USERNAME) = '" + usuario + @"' AND (IERPURM_ID = IERPM_ID OR IERPUR_ADMIN = 1) AND IERPUR_STATUS = '1'
                WHERE IERPM_STATUS = '1' 
                ORDER BY IERPM_NOMBRE";

                DataTable ViewTable = cn.Query(query, "BAW");

                select = Utils.BuildOptions(ViewTable, "");

                data.Add(select);



                //////////////////  EMPRESAS

        
                //2021-09-06
                query = @"SELECT DISTINCT d.IERPE_ID as data1, a.EMPRESA || ' - ' || d.IERPE_CODIGO as data2, d.IERPE_ERP_CODIGO as data3 
                FROM (
                    SELECT b.EMPRESA_ID, c.IERPE_ERP_CODIGO || ' - ' || b.EMPRESA_ESQUEMA as EMPRESA
                    FROM INTBAW.IERP_EMPRESAS c
                    INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = c.IERPE_ERP_CODIGO AND b.EMPRESA_STATUS = '1'
                    INNER JOIN INTBAW.IERP_USER_ROLES ON UPPER(IERPUR_USERNAME) = '" + usuario + @"' AND (IERPURE_ID = c.IERPE_ID OR IERPUR_ADMIN = 1) AND IERPUR_STATUS = '1'
                    WHERE c.IERPE_STATUS = '1'
                    GROUP BY  b.EMPRESA_ID, c.IERPE_ERP_CODIGO || ' - ' || b.EMPRESA_ESQUEMA
                ) a
                INNER JOIN INTBAW.IERP_EMPRESAS d ON d.IERPE_ERP_CODIGO = a.EMPRESA_ID AND d.IERPE_STATUS = '1'
                ORDER BY d.IERPE_ID FETCH NEXT 1 ROWS ONLY";

                ViewTable = cn.Query(query, "BAW");

                select = Utils.BuildOptions(ViewTable, "");

                data.Add(select);

            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }

            return data;
        }




        //////// GET_COMBOS CATALOGOS - PAISES  ////////////
        [Route("api/Catalogos_Calls_API/Get_IB_HOMOLOGACIONES")]
        public List<string> Get_IB_HOMOLOGACIONES(string IERPHM_ID, string IERPH_OPCION, string IERPC_ROL, string IERPHE_ID)
        {

            List<string> listView2 = new List<string>();

            string select;

            //List<IB_CATALOGOS> listView = null;

            try
            {
                string query = "";


                ///////////////// CATALOGOS


                /*
                 query = @"SELECT IERPC_ID as IERP_ID, c.IERPM_ID, c.IERPC_TITULO as IERP_TITULO, 

a.IERPCAT_CATEGO || ' - ' || a.IERPCAT_DESCRIPCION || ' - ' || IERPC_CODIGO as IERP_CODIGO, 

b.IERPCAT_CATEGO || ' - ' || b.IERPCAT_DESCRIPCION  || ' - ' || IERPC_ERP_CODIGO as IERP_ERP_CODIGO, 

COALESCE(IERPS_NOMBRE, '') as IERP_STATUS, 

IERPC_USER as IERP_USER, IERPC_DATE as IERP_DATE, c.IERPS_SEC

 FROM INTBAW.IERP_CATALOGOS c

LEFT JOIN INTBAW.IERP_STATUS d ON c.IERPC_STATUS = d.IERPS_CODIGO

LEFT JOIN INTBAW.IERP_CATEGORIAS a ON a.IERPCAT_ID = c.IERPC_CODIGO

LEFT JOIN INTBAW.IERP_CATEGORIAS b ON b.IERPCAT_ID = c.IERPC_ERP_CODIGO

WHERE c.IERPM_ID = '" + IERPHM_ID + @"' AND IERPC_ROL LIKE '%" + IERPC_ROL + @"%'

AND c.IERPC_STATUS = '1' 

ORDER BY  b.IERPCAT_CATEGO";
* 

                listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(catalogos);

                select = "<option value=''>Seleccione</option>";
                foreach (IB_CATALOGOS row in listView)
                {
                    select += "<option value=" + row.IERP_ID + ">" + row.IERP_ERP_CODIGO + " : " + row.IERP_CODIGO + "</option>";
                }
                */


                query = @"SELECT IERP_ID as data1,  IERP_ERP_CODIGO || ' : ' || IERP_CODIGO as data2 FROM(

                SELECT IERPC_ID as IERP_ID,

                a.IERPCAT_CATEGO || ' - ' || a.IERPCAT_DESCRIPCION || ' - ' || IERPC_CODIGO as IERP_CODIGO,

                b.IERPCAT_CATEGO || ' - ' || b.IERPCAT_DESCRIPCION || ' - ' || IERPC_ERP_CODIGO as IERP_ERP_CODIGO
                
                FROM INTBAW.IERP_CATALOGOS c              

                INNER JOIN INTBAW.IERP_STATUS d ON c.IERPC_STATUS = d.IERPS_CODIGO               

                INNER JOIN INTBAW.IERP_CATEGORIAS a ON a.IERPCAT_ID = c.IERPC_CODIGO AND a.IERPCAT_STATUS = '1'                

                INNER JOIN INTBAW.IERP_CATEGORIAS b ON b.IERPCAT_ID = c.IERPC_ERP_CODIGO AND b.IERPCAT_STATUS = '1'
                
                WHERE c.IERPM_ID = '" + IERPHM_ID + @"' AND IERPC_ROL LIKE '%" + IERPC_ROL + @"%' AND c.IERPC_STATUS = '1'

                ORDER BY  b.IERPCAT_CATEGO, b.IERPCAT_DESCRIPCION, a.IERPCAT_CATEGO, a.IERPCAT_DESCRIPCION) x";

                DataTable catalogos = cn.Query(query, "BAW");

                select = Utils.BuildOptions(catalogos, "");

                listView2.Add(select);

                //////////////////////// PAISES



                /*

                //2021-09-10
                query = @"
SELECT 
a.IERPP_ID as IERP_ID, 
a.IERPM_ID, 
a.IERPP_CODIGO as IERP_CODIGO, 
a.IERPP_ERP_CODIGO as IERP_ERP_CODIGO, 
a.IERPP_STATUS as IERP_STATUS, 
a.IERPP_USER as IERP_USER, 
a.IERPP_DATE as IERP_DATE, 
a.IERPS_SEC
FROM INTBAW.IERP_PAISES a
INNER JOIN INTBAW.IERP_EMPRESAS b ON b.IERPE_ID = a.IERPE_ID
INNER JOIN INTBAW.EMPRESAS c ON c.EMPRESA_ID = b.IERPE_ERP_CODIGO
WHERE a.IERPP_STATUS = '1' AND a.IERPM_ID = '" + IERPHM_ID + "' AND a.IERPE_ID = '" + IERPHE_ID + @"' AND IERPP_CODIGO = c.EMPRESA_PAIS
ORDER BY a.IERPP_CODIGO";

                listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(catalogos);

                select = "<option value=''>Seleccione</option>";
                foreach (IB_CATALOGOS row in listView)
                {
                    select += "<option value=" + row.IERP_ID + ">" + row.IERP_CODIGO + " : " + row.IERP_ERP_CODIGO + "</option>";
                }*/

                query = @"SELECT a.IERPP_ID as data1, 
a.IERPP_ERP_CODIGO || ' : ' || a.IERPP_CODIGO as data2
FROM INTBAW.IERP_PAISES a
INNER JOIN INTBAW.IERP_EMPRESAS b ON b.IERPE_ID = a.IERPE_ID
INNER JOIN INTBAW.EMPRESAS c ON c.EMPRESA_ID = b.IERPE_ERP_CODIGO
WHERE a.IERPP_STATUS = '1' AND a.IERPM_ID = '" + IERPHM_ID + "' AND a.IERPE_ID = '" + IERPHE_ID + @"' AND IERPP_CODIGO = c.EMPRESA_PAIS
ORDER BY a.IERPP_CODIGO";

                catalogos = cn.Query(query, "BAW");

                select = Utils.BuildOptions(catalogos, "");

                listView2.Add(select);




                query = "SELECT IERPM_SQL_CS as data1 FROM INTBAW.IERP_MODULOS WHERE IERPM_STATUS = '1' AND IERPM_ID = " + IERPHM_ID;
                IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(query);
                listView2.Add(data.data1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listView2;
            ;
        }


        //GET_CATALOGO  get los catalogos y los combos auxiliares
        [Route("api/Catalogos_Calls_API/Get_IB_CATALOGOS")]
        public List<List<IB_CATALOGOS>> Get_IB_CATALOGOS(string IERPM_ID, string IERP_OPCION, string IERPM_DES, string IERP_CHK, string IERP_ERP_CODIGO, string IERPC_ROL, string IERPHE_ID, string usuario)
        {
            // IERP_ERP_CODIGO se utiliza solo para filtrar categorias por sistema, viene de onchange 

            List<IB_CATALOGOS> listView = null;
            List<IB_CATALOGOS> listView2 = new List<IB_CATALOGOS>();
            List<IB_CATALOGOS> listView3 = new List<IB_CATALOGOS>();

            List <List<IB_CATALOGOS>> result = new List<List<IB_CATALOGOS>>();

            try
            {
                string query = "";

                switch (IERP_OPCION)
                {

                    case "ADMIN CENTRO COSTO":

                        query = @"SELECT 
  IERPH_ID as IERP_ID,			
  IERPM_ID,          	
  IERPH_CODIGO as IERP_CODIGO,		
  IERPH_ERP_CODIGO as IERP_ERP_CODIGO,		

    IERPH_NOMBRE as IERP_TITULO,  

  IERPS_NOMBRE as IERP_STATUS,	  	
  IERPH_USER as IERP_USER,  	
  IERPH_DATE as IERP_DATE  	

FROM INTBAW.IERP_HOMOLOGACIONES

LEFT JOIN INTBAW.IERP_STATUS ON IERPH_STATUS = IERPS_CODIGO

WHERE IERPM_ID = '" + IERPM_ID + "' AND IERPH_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " AND IERPE_ID = '" + IERPHE_ID + "' AND IERPC_ID IS NULL AND IERPP_ID IS NULL ORDER BY IERPH_CODIGO";



                        break;

                    case "ADMIN CATALOGOS":

                        query = @"SELECT IERPC_ID as IERP_ID, c.IERPM_ID, c.IERPC_TITULO as IERP_TITULO, 

IERPC_CODIGO || ' - ' || a.IERPCAT_CATEGO || ' - ' || a.IERPCAT_DESCRIPCION as IERP_CODIGO, 

IERPC_ERP_CODIGO || ' - ' || b.IERPCAT_CATEGO || ' - ' || b.IERPCAT_DESCRIPCION 

as IERP_ERP_CODIGO, 

COALESCE(IERPS_NOMBRE, '') as IERP_STATUS,IERPC_USER as IERP_USER, IERPC_DATE as IERP_DATE, c.IERPS_SEC

 FROM INTBAW.IERP_CATALOGOS c

LEFT JOIN INTBAW.IERP_STATUS d ON c.IERPC_STATUS = d.IERPS_CODIGO

LEFT JOIN INTBAW.IERP_CATEGORIAS a ON a.IERPCAT_ID = c.IERPC_CODIGO

LEFT JOIN INTBAW.IERP_CATEGORIAS b ON b.IERPCAT_ID = c.IERPC_ERP_CODIGO

WHERE c.IERPM_ID = '" + IERPM_ID + @"'

AND c.IERPC_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + @"

AND IERPC_ROL LIKE '%" + IERPC_ROL + @"%'

ORDER BY  b.IERPCAT_CATEGO"; // c.IERPC_TITULO";

                        break;

                    // PAISES 
                    case "ADMIN PAISES":
                        // AGREGAR COLUMNAS NOMBRE Y ERP_NOMBRE PARA DISPLAY DE PAISES 
                        query = @"SELECT 
  IERPP_ID as IERP_ID,			
  IERPM_ID,          	
  IERPP_CODIGO as IERP_CODIGO,		
  IERPP_ERP_CODIGO as IERP_ERP_CODIGO,		
  IERPS_NOMBRE as IERP_STATUS,	  	
  IERPP_USER as IERP_USER,  	
  IERPP_DATE as IERP_DATE,  	
  IERPS_SEC  	
FROM INTBAW.IERP_PAISES 

LEFT JOIN INTBAW.IERP_STATUS ON IERPP_STATUS = IERPS_CODIGO

WHERE IERPM_ID = '" + IERPM_ID + "' AND IERPP_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " AND IERPE_ID = '" + IERPHE_ID + "' ORDER BY IERPP_ID DESC";

                        break;



                    // USUARIOS 
                    case "ADMIN USUARIOS":
                       
                        query = @"SELECT             
  IERPURM_PK_CONTACT as IERP_ID,        
  UPPER(IERPUR_USERNAME) || ' - ' || IERPUR_CONTACT_NAME as IERP_CODIGO,        
  IERPUR_ROLL  || ' - ' || IERPROL_NOMBRE  as IERP_ERP_CODIGO,        
  IERPS_NOMBRE as IERP_STATUS,          
  IERPUR_USER as IERP_USER,      
  IERPUR_DATE as IERP_DATE
FROM INTBAW.IERP_USER_ROLES 
INNER JOIN INTBAW.IERP_ROLES ON IERPUR_ROLL = IERPROL_CODIGO
LEFT JOIN INTBAW.IERP_STATUS ON IERPUR_STATUS = IERPS_CODIGO
WHERE  IERPUR_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " AND IERPURE_ID = '" + IERPHE_ID + @"' AND IERPURM_ID = '" + IERPM_ID + @"' ORDER BY IERPURM_PK_CONTACT";


                        break;


                    // EMPRESAS
                    case "ADMIN EMPRESAS":
                        
                        query = @"SELECT 

    IERPE_ID as IERP_ID,
    c.IERPM_ID,
    IERPE_NOMBRE as IERP_CODIGO,
    IERPE_ERP_CODIGO || ' - ' || b.EMPRESA_ESQUEMA || ' - ' ||  IERPE_ERP_NOMBRE as IERP_ERP_CODIGO,
    IERPS_NOMBRE as IERP_STATUS,
    IERPE_USER as IERP_USER,
    IERPE_DATE as IERP_DATE,
    c.IERPS_SEC

FROM INTBAW.IERP_EMPRESAS c 

INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = c.IERPE_ERP_CODIGO 

LEFT JOIN INTBAW.IERP_STATUS d ON IERPE_STATUS = IERPS_CODIGO

WHERE c.IERPM_ID = '" + IERPM_ID + "' AND c.IERPE_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " ORDER BY IERPE_ID DESC";



                        break;

/*
                    // RUBROS
                    case "ADMIN RUBROS":

                        query = @"SELECT 
  IERPR_ID as IERP_ID,
  IERPM_ID,
  IERPR_CODIGO as IERP_CODIGO,
  IERPR_ERP_CODIGO as IERP_ERP_CODIGO,
  IERPR_STATUS as IERP_STATUS,
  IERPR_USER as IERP_USER,
  IERPR_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_RUBROS WHERE IERPM_ID = '" + IERPM_ID + "' AND IERPR_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " ORDER BY  IERPR_ID DESC";
                        
                        break;

                    // MONEDAS
                    case "ADMIN MONEDAS":
                        query = @"SELECT 
  IERPMON_ID as IERP_ID,
  IERPM_ID,
  IERPMON_CODIGO as IERP_CODIGO,
  IERPMON_ERP_CODIGO as IERP_ERP_CODIGO,
  IERPMON_STATUS as IERP_STATUS,
  IERPMON_USER as IERP_USER,
  IERPMON_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_MONEDAS WHERE IERPM_ID = '" + IERPM_ID + "' AND IERPMON_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " ORDER BY IERPMON_ID DESC";
                        
                        break;
*/
                    // CATEGORIAS
                    case "ADMIN CATEGORIAS":

                        query = @"SELECT 
  IERPCAT_ID as IERP_ID,
  IERPM_ID,
  IERPCAT_DESCRIPCION as IERP_TITULO,  
  IERPCAT_SISTEMA as IERP_CODIGO,
  IERPCAT_CATEGO as IERP_ERP_CODIGO,
  IERPS_NOMBRE as IERP_STATUS,
  IERPCAT_USER as IERP_USER,
  IERPCAT_DATE as IERP_DATE,
  IERPS_SEC

FROM INTBAW.IERP_CATEGORIAS 

LEFT JOIN INTBAW.IERP_STATUS ON IERPCAT_STATUS = IERPS_CODIGO 

WHERE IERPM_ID = '" + IERPM_ID + "' ";

                        if (IERP_ERP_CODIGO != null && IERP_ERP_CODIGO != "")
                            query += " AND IERPCAT_SISTEMA = '" + IERP_ERP_CODIGO + "' ";

                        query += " AND IERPCAT_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " ORDER BY IERPCAT_SISTEMA, IERPCAT_CATEGO";

                        break;

                    // QUERYS
                    case "ADMIN QUERYS":

                        query = @"SELECT IERPQ_ID as IERP_ID, 0 as IERPM_ID, IERPQ_SQL as IERP_TITULO,  
/*
CASE WHEN IERPQ_SISTEM = 'ERP' THEN
REPLACE(IERPQ_SQL, 'TRANSIT', (SELECT b.EMPRESA_ESQUEMA FROM  INTBAW.IERP_EMPRESAS a INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = a.IERPE_ERP_CODIGO WHERE a.IERPE_ID = " + IERPHE_ID + @"))
ELSE IERPQ_SQL END as IERP_TITULO,  */

IERPQ_SISTEM || ' - ' || IERPQ_CATEGO || ' - ' || REPLACE(SUBSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,50), 0, INSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,20), ' ')-1),'TRANSIT.','' ) as IERP_CODIGO,
IERPQ_SQL_ORDER as IERP_ERP_CODIGO, IERPS_NOMBRE as IERP_STATUS, IERPQ_USER as IERP_USER, IERPQ_DATE as IERP_DATE, IERPS_SEC
FROM INTBAW.IERP_QUERYS 
LEFT JOIN INTBAW.IERP_STATUS ON IERPQ_STATUS = IERPS_CODIGO
WHERE IERPQ_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " AND IERPQ_SISTEM IN ('ERP','" + IERPM_DES + "') ";                      

                        if (IERP_ERP_CODIGO != null && IERP_ERP_CODIGO != "")
                            query += " AND IERPQ_SISTEM = '" + IERP_ERP_CODIGO + "' ";

                        query += " ORDER BY IERPQ_SISTEM, IERPQ_CATEGO";

                        break;

                    // FILTROS
                    case "ADMIN FILTROS":

                        query = @"SELECT IERPQF_ID as IERP_ID, 0 as IERPM_ID, IERPQF_ORDER || ' - ' || IERPQF_OPERADOR || ' - ' || IERPQF_CAMPO as IERP_CODIGO,
  IERPQF_COMPARATIVO || ' ' || IERPQF_CAMPO_PROGRA  as IERP_ERP_CODIGO,
IERPQ_SISTEM || ' - ' || 
IERPQ_CATEGO || ' - ' || 
REPLACE(
SUBSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,50), 0, INSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,20), ' ')-1) 
,'TRANSIT.','')
 as IERP_TITULO,
IERPQF_CAMPO_PROGRA as IERP_CAMPO_PROGRA, 
IERPQF_ORDER as IERP_ORDEN,
IERPQF_OPERADOR as IERP_OPERADOR,
  IERPQF_USER as IERP_USER,
  IERPQF_DATE as IERP_DATE,
IERPS_NOMBRE as IERP_STATUS,
  INTBAW.IERP_QUERYS_FILTROS.IERPS_SEC
FROM INTBAW.IERP_QUERYS_FILTROS 

INNER JOIN INTBAW.IERP_QUERYS ON INTBAW.IERP_QUERYS.IERPQ_ID = INTBAW.IERP_QUERYS_FILTROS.IERPQ_ID

LEFT JOIN INTBAW.IERP_STATUS ON IERPQF_STATUS = IERPS_CODIGO

WHERE IERPQF_STATUS " + (IERP_CHK == "1" ? " <> '1' " : " = '1'") + " AND IERPQ_SISTEM IN ('ERP','" + IERPM_DES + "') ";

                        if (IERP_ERP_CODIGO != null && IERP_ERP_CODIGO != "")
                            query += " AND INTBAW.IERP_QUERYS_FILTROS.IERPQ_ID = '" + IERP_ERP_CODIGO + "' ";

                        query += " ORDER BY IERPQ_SISTEM, TO_NUMBER(IERPQ_CATEGO), IERPQF_ORDER";

                        break;
                }

                DataTable catalogos = cn.Query(query, "BAW");

                listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(catalogos);

                result.Add(listView);


                ////////////////////////////////////////////// COMBOS AUXILIARES ADMIN CATALOGOS

                if (IERP_OPCION == "ADMIN CATALOGOS") // || IERP_OPCION == "ADMIN EMPRESAS") se creo una tabla nueva EMPRESAS como catalogo para CARGO SYSTEM Y ERP (esquemas)
                {
                    string tipo = IERP_OPCION.Replace("ADMIN ", "").Substring(0,1);

                    tipo = " AND IERPCAT_TIPO " + (tipo == "E" ? " = 'EMPRESA' " : " <> 'EMPRESA' ");

                    tipo = ""; //2021-06-18

                    /////////////// CATEGORIAS CARGO SYSTEM

                    query = @"SELECT IERPCAT_ID as data1, IERPCAT_CATEGO || ' - ' || IERPCAT_DESCRIPCION as data2 
                        
                    FROM INTBAW.IERP_CATEGORIAS WHERE IERPCAT_STATUS = '1' AND IERPM_ID = '" + IERPM_ID + "' AND (IERPCAT_ROL LIKE '%" + IERPC_ROL + @"%' OR IERPCAT_ROL IS NULL) 

                    AND IERPCAT_SISTEMA = '" + IERPM_DES + "' " + tipo + " ORDER BY IERPCAT_CATEGO";

                    DataTable ViewTable = cn.Query(query, "BAW");

                    string select = Utils.BuildOptions(ViewTable, "id = IERP_CODIGO onchange = CategoriasDescripcion(); required style = 'width:100%'");

                    listView2.Add( new IB_CATALOGOS {  IERP_TITULO = select });

                    result.Add(listView2);



                    /////////////// CATEGORIAS ERP

                    query = @"SELECT IERPCAT_ID as data1, IERPCAT_CATEGO || ' - ' || IERPCAT_DESCRIPCION as IERP_TITULO 
                        
                    FROM INTBAW.IERP_CATEGORIAS 

                    WHERE IERPCAT_STATUS = '1' AND IERPM_ID = '" + IERPM_ID + @"' 
                        
                    AND IERPCAT_SISTEMA = 'ERP' " + tipo + @" 

                    AND (IERPCAT_ROL LIKE '%" + IERPC_ROL + @"%' OR IERPCAT_ROL IS NULL) 
                        
                    ORDER BY IERPCAT_CATEGO";

                    ViewTable = cn.Query(query, "BAW");

                    select = Utils.BuildOptions(ViewTable, "id=IERP_ERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%'");

                    listView3.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView3);
                }


                if (IERP_OPCION == "ADMIN EMPRESAS") // agregado 2021-06-07
                {

                    /////////////// EMPRESAS CARGO SYSTEM

                    
                    query = "SELECT IERPQ_SQL || ' ORDER BY ' || IERPQ_SQL_ORDER FROM INTBAW.IERP_QUERYS WHERE IERPQ_SISTEM = '" + IERPM_DES + @"' AND IERPQ_STATUS = '1' 
AND IERPQ_CATEGO = '11'";

                    query = cn.GetScalar(query);

                    /*
                    List<IB_HOMOLOGACIONES> listViewX = GetListStandar<IB_HOMOLOGACIONES>("c", IERPM_ID, query);

                    string select = @"
                    <select id=IERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%' >
                        <option value = ''>Seleccione</option>";

                    foreach (IB_HOMOLOGACIONES row in listViewX)
                    {
                        select += "<option value=" + row.IERPP_ID + ">" + row.IERPP_ID + " - " + row.IERPH_NOMBRE + "</option>";
                    }

                    select += "</select>";
                    */

                    DataTable ViewTable = GetDataTable("c", IERPM_ID, query);

                    string select = Utils.BuildOptions(ViewTable, "id = IERP_CODIGO onchange = CategoriasDescripcion(); required style = 'width:100%'");

                    listView2.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView2);



                    /////////////// EMPRESAS ERP
                    /*
                    query = @"SELECT EMPRESA_ID as IERP_ID, EMPRESA_CODIGO as IERP_CODIGO, EMPRESA_NOMBRE || ' - ' || EMPRESA_ESQUEMA as IERP_TITULO FROM INTBAW.EMPRESAS 
           
                    WHERE EMPRESA_STATUS = '1' AND EMPRESA_SISTEMA = 'ERP' AND EMPRESA_ROL LIKE '%" + IERPC_ROL + @"%' ORDER BY EMPRESA_CODIGO";

                    ViewTable = cn.Query(query, "BAW");
                    
                    listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                    select = @"
                    <select id=IERP_ERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%' >
                        <option value = ''>Seleccione</option>";

                    foreach (IB_CATALOGOS row in listView)
                    {
                        select += "<option value=" + row.IERP_ID + ">" + row.IERP_CODIGO + " - " + row.IERP_TITULO + "</option>";
                    }

                    select += "</select>";*/

             


                    query = @"SELECT EMPRESA_ID as data1, EMPRESA_CODIGO || ' - ' || EMPRESA_NOMBRE || ' - ' || EMPRESA_ESQUEMA as data2 FROM INTBAW.EMPRESAS 
           
                    WHERE EMPRESA_STATUS = '1' AND EMPRESA_SISTEMA = 'ERP' AND EMPRESA_ROL LIKE '%" + IERPC_ROL + @"%' ORDER BY EMPRESA_CODIGO";

                    ViewTable = cn.Query(query, "BAW");

                    select = Utils.BuildOptions(ViewTable, "id=IERP_ERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%'");

                    listView3.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView3);
                }



                if (IERP_OPCION == "ADMIN PAISES")
                {

                    ////////////////////////////// TRAE QUERYS DE PAISES CARGO SYSTEM / ERP
                    //query = "SELECT IERPQ_ID as IERP_ID, IERPQ_SISTEM as IERPM_ID, IERPQ_SQL as IERP_SQL, IERPQ_CATEGO as IERPS_SEC, IERPQ_SQL_UPDATE as IERP_CAMPO_PROGRA FROM INTBAW.IERP_QUERYS WHERE IERPQ_ID IN(145,163) ORDER BY IERPQ_ID";

                    /*
                    query = @"SELECT IERPQ_ID as IERP_ID, IERPQ_SISTEM as IERPM_ID, IERPQ_SQL as IERP_SQL, IERPQ_CATEGO as IERPS_SEC, IERPQ_SQL_UPDATE as IERP_CAMPO_PROGRA

FROM INTBAW.IERP_QUERYS WHERE IERPQ_STATUS = '1' AND

(IERPQ_SISTEM = 'ERP' AND IERPQ_CATEGO = '07') OR (IERPQ_SISTEM = '" + IERPM_DES + @"' AND IERPQ_CATEGO = '10')

ORDER BY IERPQ_SQL_UPDATE DESC";
                    */

                    query = @"SELECT IERPQ_ID as IERP_ID, IERPQ_SISTEM as IERPM_ID, 

CASE WHEN IERPQ_SISTEM = 'ERP' THEN

REPLACE(IERPQ_SQL, 'TRANSIT', (SELECT b.EMPRESA_ESQUEMA FROM  INTBAW.IERP_EMPRESAS a INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = a.IERPE_ERP_CODIGO WHERE a.IERPE_ID = " + IERPHE_ID + @"))

ELSE IERPQ_SQL END as IERP_SQL, IERPQ_CATEGO as IERPS_SEC, IERPQ_SQL_UPDATE as IERP_CAMPO_PROGRA

FROM INTBAW.IERP_QUERYS WHERE IERPQ_STATUS = '1' AND

(IERPQ_SISTEM = 'ERP' AND IERPQ_CATEGO = '07') OR (IERPQ_SISTEM = '" + IERPM_DES + @"' AND IERPQ_CATEGO = '10')";


                    DataTable ViewTable = cn.Query(query, "BAW");

                    listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                    string query_erp = "";
                    string query_cs = "";

                    if (listView.Count > 0)
                        if (listView[0].IERPM_ID == "ERP")
                            query_erp = listView[0].IERP_SQL;

                    if (listView.Count > 1)
                        if (listView[1].IERPM_ID != "ERP")
                            query_cs = listView[1].IERP_SQL;

                    /////////////// PAISES CARGO SYSTEM
                    /*
                    string select = @"
                    <select id=IERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%' >
                        <option value = ''>Seleccione</option>";

                    listView = GetListStandar<IB_CATALOGOS>("c", IERPM_ID, query_cs);
 
                    foreach (IB_CATALOGOS row in listView)
                    {
                        select += "<option value=" + row.IERP_CODIGO + ">" + row.IERP_TITULO + "</option>";
                    }
                    
                    select += "</select>";
                    */

                    ViewTable = GetDataTable("c", IERPM_ID, query_cs);

                    string select = Utils.BuildOptions(ViewTable, "id=IERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%'");

                    listView2.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView2);




                    /////////////// PAISES ERP 
                    /*
                    select = @"
                    <select id=IERP_ERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%' >
                        <option value = ''>Seleccione</option>";

                    ViewTable = cn.Query(query_erp, "BAW");

                    listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                    foreach (IB_CATALOGOS row in listView)
                    {
                        select += "<option value=" + row.IERP_CODIGO + ">" + row.IERP_TITULO + "</option>";
                    }

                    select += "</select>";
                    */

                    ViewTable = cn.Query(query_erp, "BAW");

                    //ViewTable = GetDataTable("c", IERPM_ID, query_erp);

                    select = Utils.BuildOptions(ViewTable, "id=IERP_CODIGO onchange=CategoriasDescripcion(); required style='width:100%'");

                    listView3.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView3);
                }


                if (IERP_OPCION == "ADMIN FILTROS")
                {
                    /////////////// QUERYS
                    /*
                    string select = @"
                    <option value=''>Seleccione</option>";

                    query = @"SELECT IERPQ_ID as IERP_ID, IERPQ_CATEGO as IERP_CODIGO, IERPQ_SISTEM as IERP_ERP_CODIGO, IERPQ_SQL as IERP_TITULO,  
REPLACE(
SUBSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,50), 0, INSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,20), ' ')-1) 
,'TRANSIT.','') as IERP_SQL
FROM INTBAW.IERP_QUERYS WHERE IERPQ_STATUS = '1' AND IERPQ_SISTEM IN ('ERP','" + IERPM_DES + "') ORDER BY IERPQ_SISTEM, IERPQ_CATEGO";

                    DataTable ViewTable = cn.Query(query, "BAW");

                    listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                    foreach (IB_CATALOGOS row in listView)
                    {
                        select += "<option value=" + row.IERP_ID + ">" + row.IERP_ERP_CODIGO + " - " + row.IERP_CODIGO + " - " + row.IERP_SQL + "</option>"; // + row.IERP_TITULO.Substring(0,10)
                    }
                    */


                    query = @"SELECT IERPQ_ID as data1, IERPQ_CATEGO || ' - ' || IERPQ_SISTEM || ' - ' || IERPQ_SQL as datda2,  
REPLACE(
SUBSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,50), 0, INSTR(SUBSTR(IERPQ_SQL,INSTR(REPLACE(IERPQ_SQL,'TRANSIT',''),'FROM ')+5,20), ' ')-1) 
,'TRANSIT.','') as IERP_SQL
FROM INTBAW.IERP_QUERYS WHERE IERPQ_STATUS = '1' AND IERPQ_SISTEM IN ('ERP','" + IERPM_DES + "') ORDER BY IERPQ_SISTEM, IERPQ_CATEGO";

                    DataTable ViewTable = cn.Query(query, "BAW");

                    string select = Utils.BuildOptions(ViewTable, "");

                    listView2.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView2);

                }


                if (IERP_OPCION == "ADMIN USUARIOS")
                {
                    //string esquema = "INTBAW";

                    //string ip = Utils.GetIPAddress();

                    //if (ip == Utils.GetServerIP()) esquema = "SECURITY";


                    //USUARIOS
                    //STATUS = 'A' AND 
                    /*
                                         string select = @"
                    <select id=IERP_CODIGO required style='width:100%'>
                        <option value = ''>Seleccione</option>";
* 
                                      query = @"SELECT PK_CONTACT as IERP_ID, UPPER(USERNAME) as IERP_CODIGO, CONTACT_NAME || ' ' || LAST_NAME_1 || ' ' || LAST_NAME_2 IERP_TITULO, CONTACT_TYPE_ID as data4  
                                      FROM " + esquema + @".SEC_CONTACTS WHERE PK_CONTACT IN 
                                      (SELECT PK_CONTACT FROM " + esquema + ".SEC_CONTACT_MODULE_ROLE_ORGA WHERE PK_MODULE = 38)";

                                      DataTable ViewTable = cn.Query(query, "BAW");

                                      listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                                      foreach (IB_CATALOGOS row in listView)
                                      {

                                          select += "<option value='" + row.IERP_ID +  "|"+  row.IERP_CODIGO + "|" + row.IERP_TITULO + "' title ='" + row.IERP_CODIGO + "'>" + row.IERP_TITULO + "</option>";
                                      }

                                      select += "</select>";
                                      */

                    query = "SELECT IERPM_DBTYPE as data1, IERPM_CONEXION as data2, IERPM_ERP_ESQUEMA as data3 FROM INTBAW.IERP_MODULOS WHERE IERPM_STATUS = '1' AND IERPM_ID = " + IERPM_ID;
                    IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(query);

                    query = @"SELECT 

TRIM( PK_CONTACT || '|' || REGEXP_SUBSTR(UPPER(EMAIL), '[^@]+', 1, 1 ) || '|' ||  TRIM(CONTACT_NAME) || ' ' || TRIM(LAST_NAME_1) || ' ' || TRIM(LAST_NAME_2) ) as data1, 

TRIM(CONTACT_NAME) || ' ' || TRIM(LAST_NAME_1) || ' ' || TRIM(LAST_NAME_2) data2 
                   
FROM " + data.data3 + @".SEC_CONTACTS WHERE PK_CONTACT IN (SELECT PK_CONTACT FROM " + data.data3 + @".SEC_CONTACT_MODULE_ROLE_ORGA WHERE PK_MODULE = 38)";

                    DataTable ViewTable = cn.Query(query, "BAW");

                    string select = Utils.BuildOptions(ViewTable, "id=IERP_CODIGO required style='width:100%'");

                    listView2.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView2);


                    ///////  ROLES 

                    query = @"SELECT IERPROL_CODIGO as IERP_CODIGO, IERPROL_CODIGO || ' - ' || IERPROL_NOMBRE as IERP_TITULO 

FROM INTBAW.IERP_ROLES 

INNER JOIN INTBAW.IERP_USER_ROLES ON UPPER(IERPUR_USERNAME) = '" + usuario + @"' AND IERPURM_ID = '" + IERPM_ID + @"' AND IERPUR_STATUS = '1'

WHERE (IERPROL_STATUS = '1' OR IERPUR_ADMIN = 1)

ORDER BY IERPROL_CODIGO";

                    ViewTable = cn.Query(query, "BAW");

                    /*
                    select = @"
                    <select id=IERP_ERP_CODIGO required style='width:100%'>
                        <option value = ''>Seleccione</option>";

                    listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(ViewTable);

                    foreach (IB_CATALOGOS row in listView)
                    {
                        select += "<option value=" + row.IERP_CODIGO + ">" + row.IERP_TITULO + "</option>";
                    }

                    select += "</select>";
                    */

                    select = Utils.BuildOptions(ViewTable, "id=IERP_ERP_CODIGO required style='width:100%'");



                    listView3.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView3);

                }




                if (IERP_OPCION == "ADMIN CENTRO COSTO")
                {

                    query = @"SELECT IERPH_CODIGO as data1, IERPH_CODIGO || ' ' || LOWER(IERPH_NOMBRE) as data2 FROM vw_consecutivo_pedidos ORDER BY IERPH_NOMBRE";

                    DataTable ViewTable = GetDataTable("c", IERPM_ID, query);

                    string select = Utils.BuildOptions(ViewTable, "id=IERP_CODIGO required style='width:100%'");

                    listView2.Add(new IB_CATALOGOS { IERP_TITULO = select });

                    result.Add(listView2);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }


        //EDIT CATALOGO
        [Route("api/Catalogos_Calls_API/Edit_IB_CATALOGOS")]
        public List<IB_CATALOGOS> Gettt_IB_CATALOGOS(string IERP_ID, string IERP_OPCION)
        {
            List<IB_CATALOGOS> listView = null;

            try
            {
                string query = "";


                switch (IERP_OPCION)
                {

                    case "ADMIN CENTRO COSTO":

                        query = @"SELECT 
  IERPH_ID as IERP_ID,			
  IERPM_ID,          	
  IERPH_CODIGO as IERP_CODIGO,		
  IERPH_ERP_CODIGO as IERP_ERP_CODIGO,		
  IERPH_STATUS as IERP_STATUS,	
  IERPH_USER as IERP_USER,  	
  IERPH_DATE as IERP_DATE

FROM INTBAW.IERP_HOMOLOGACIONES

WHERE IERPH_ID = '" + IERP_ID + "' ";



                        break;

                    case "ADMIN CATALOGOS":

query = @"SELECT IERPC_ID as IERP_ID, IERPM_ID, IERPC_TITULO as IERP_TITULO, IERPC_CODIGO as IERP_CODIGO, IERPC_ERP_CODIGO as IERP_ERP_CODIGO, IERPC_STATUS as IERP_STATUS, IERPC_USER as IERP_USER, IERPC_DATE as IERP_DATE, IERPS_SEC 
                      
FROM INTBAW.IERP_CATALOGOS WHERE IERPC_ID = '" + IERP_ID + "'"; 
                        
                        break;

                    case "ADMIN PAISES":

                        query = @"SELECT 
  IERPP_ID as IERP_ID,			
  IERPM_ID,          	
  IERPP_CODIGO as IERP_CODIGO,		
  IERPP_ERP_CODIGO as IERP_ERP_CODIGO,		
  IERPP_STATUS as IERP_STATUS,	  	
  IERPP_USER as IERP_USER,  	
  IERPP_DATE as IERP_DATE,  	
  IERPS_SEC  	
FROM INTBAW.IERP_PAISES WHERE IERPP_ID = '" + IERP_ID + "'"; // AND IERPP_STATUS = '1' ORDER BY  IERPP_ID DESC";

                        break;

                    case "ADMIN EMPRESAS":
                        
                            query = @"SELECT 
  IERPE_ID as IERP_ID,
  IERPM_ID,
  IERPE_CODIGO as IERP_CODIGO,
  IERPE_ERP_CODIGO as IERP_ERP_CODIGO,
  IERPE_STATUS as IERP_STATUS,
  IERPE_USER as IERP_USER,
  IERPE_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_EMPRESAS WHERE IERPE_ID = '" + IERP_ID + "'"; // AND IERPE_STATUS = '1' ORDER BY  IERPE_ID DESC";

                        break;
/*
                    case "ADMIN RUBROS":
   
                        query = @"SELECT 
  IERPR_ID as IERP_ID,
  IERPM_ID,
  IERPR_CODIGO as IERP_CODIGO,
  IERPR_ERP_CODIGO as IERP_ERP_CODIGO,
  IERPR_STATUS as IERP_STATUS,
  IERPR_USER as IERP_USER,
  IERPR_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_RUBROS WHERE IERPR_ID = '" + IERP_ID + "'"; // AND IERPR_STATUS = '1' ORDER BY  IERPR_ID DESC";

                        break;


                    case "ADMIN MONEDAS":
                        query = @"SELECT 
  IERPMON_ID as IERP_ID,
  IERPM_ID,
  IERPMON_CODIGO as IERP_CODIGO,
  IERPMON_ERP_CODIGO as IERP_ERP_CODIGO,
  IERPMON_STATUS as IERP_STATUS,
  IERPMON_USER as IERP_USER,
  IERPMON_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_MONEDAS WHERE IERPMON_ID = '" + IERP_ID + "'"; // AND IERPMON_STATUS = '1' ORDER BY IERPMON_ID DESC";
                        break;
*/

                    case "ADMIN CATEGORIAS":
                        query = @"SELECT 
  IERPCAT_ID as IERP_ID,
  IERPM_ID,
  IERPCAT_CATEGO as IERP_CODIGO,
  IERPCAT_SISTEMA as IERP_ERP_CODIGO,
  IERPCAT_DESCRIPCION as IERP_TITULO,
  IERPCAT_STATUS as IERP_STATUS,
  IERPCAT_USER as IERP_USER,
  IERPCAT_DATE as IERP_DATE,
  IERPS_SEC
FROM INTBAW.IERP_CATEGORIAS WHERE IERPCAT_ID = '" + IERP_ID + "'"; // AND IERPCAT_STATUS = '1' ORDER BY IERPCAT_ID DESC";
                        break;


                    case "ADMIN QUERYS":
                        query = @"SELECT 
  IERPQ_ID as IERP_ID,
  0 as IERPM_ID,
  IERPQ_SQL_ORDER as IERP_CODIGO,
  IERPQ_SISTEM as IERP_ERP_CODIGO,
  IERPQ_CATEGO as IERP_TITULO,  
  IERPQ_STATUS as IERP_STATUS,
  IERPQ_USER as IERP_USER,
  IERPQ_DATE as IERP_DATE,
  IERPS_SEC,
    IERPQ_SQL as IERP_SQL
FROM INTBAW.IERP_QUERYS WHERE IERPQ_ID = '" + IERP_ID + "'"; // AND IERPQ_STATUS = '1' ORDER BY IERPQ_ID DESC";

                        break;



                    case "ADMIN FILTROS":

                        query = @"SELECT 
  IERPQF_ID as IERP_ID,
  0 as IERPM_ID,
  IERPQF_CAMPO as IERP_CODIGO,
  IERPQF_COMPARATIVO as IERP_ERP_CODIGO,
  IERPQ_ID as IERP_TITULO,  
IERPQF_CAMPO_PROGRA as IERP_CAMPO_PROGRA, 
IERPQF_ORDER as IERP_ORDEN,
IERPQF_OPERADOR as IERP_OPERADOR,
  IERPQF_USER as IERP_USER,
  IERPQF_DATE as IERP_DATE,
  IERPQF_STATUS as IERP_STATUS,
  IERPS_SEC
FROM INTBAW.IERP_QUERYS_FILTROS WHERE IERPQF_ID = '" + IERP_ID + "'"; // AND IERPQF_STATUS = '1' ";
 
                        break;

                    case "ADMIN USUARIOS":

                        query = @"SELECT 
  IERPURM_PK_CONTACT as IERP_ID,
  UPPER(IERPUR_USERNAME) as IERP_CODIGO,
  IERPUR_ROLL as IERP_ERP_CODIGO,
  IERPUR_CONTACT_NAME as IERP_TITULO,
  IERPUR_USER as IERP_USER,
  IERPUR_DATE as IERP_DATE,
  IERPUR_STATUS as IERP_STATUS
FROM INTBAW.IERP_USER_ROLES WHERE IERPURM_PK_CONTACT = '" + IERP_ID + "' AND IERPUR_STATUS = '1'"; 

                        break;
                }



                DataTable catalogos = cn.Query(query, "BAW");

                listView = CapaDatos.Utils.get_list_struct2<IB_CATALOGOS>(catalogos);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listView;

        }


        public string Opera(string IERP_OPCION, string OPE, List<string> query)
        {
            Boolean res = false;
            string result = "";
            string COD_EXIS = "0";

            if (OPE != "DEL" && query[0] != null)
            {
                IB_CATALOGOS data = cn.EjecutarRow<IB_CATALOGOS>(query[0]);
                if (data.IERP_ID == null)
                    data.IERP_ID = "0";

                if (int.Parse(data.IERP_ID) > 0)
                    return "Registro ya existente";
            }


            try
            {
                //if (OPE == "DEL" && query.Count > 5)

                if (OPE == "DEL" && query[5] != null)                    
                {
                    //verifica si hay dependencias antes de borrar
                    COD_EXIS = cn.GetScalar(query[5]);

                    if (int.Parse(COD_EXIS) > 0)
                        return "Registro tiene dependencias";
                }
            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }



            //////////// REALIZA EL INSERT
            if (query[1] != null)
            {
                res = cn.Execute(query[1]);
            }


            ///////////// SECCION EXTRA ACTUALIZA RELACIONADOS
            string nextid = "0";
            try
            {
                //if (res && OPE == "UPD" && query.Count > 3)
                if (res && OPE == "UPD" && query[3] != null)
                {
                    nextid = cn.GetScalar(query[3]); // read last id inserted
                }

            } catch (Exception ex)
            {
                string test = ex.Message;
            }

            try
            {
                //if (res && OPE == "UPD" && query.Count > 4 && nextid != "0")
                if (res && OPE == "UPD" && query[4] != null && nextid != "0")
                {                  
                    query[4] = query[4].Replace("nextid", nextid);
                    cn.Execute(query[4]);
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }

            try
            {
                //if (res && OPE == "UPD" && query.Count > 6 && nextid != "0")
                if (res && OPE == "UPD" && query[6] != null && nextid != "0")
                {
                    query[5] = query[6].Replace("nextid", nextid); //categorias erp codigo
                    cn.Execute(query[6]);
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }


            try
            {
                if (res && query[2] != null)
                {
                    switch (OPE)
                    {
                        case "UPD":
                        case "DEL":
                            res = cn.Execute(query[2]);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                string test = ex.Message;
            }



            result = res ? "Exito al " : "Error al ";

            result += (OPE == "DEL" ? "Borrar" : "Guardar");

            return result;
        }


        //SAVE CATALOGO
        [Route("api/Catalogos_Calls_API/Save_IB_CATALOGOS")]
        public string Getttt_IB_CATALOGOS(string OPE, string IERP_ID, string IERPM_ID, string IERPM_DES, string IERP_TITULO, string IERP_CODIGO, string IERP_ERP_CODIGO, string IERPS_SEC, string IERP_OPCION, string usuario, string IERP_CAMPO_PROGRA, string IERP_ORDEN, string IERP_SQL, string IERP_OPERADOR, string IERPHE_ID)
        {
            string result = "";
            string sqlString = "";
            string estado = OPE == "DEL" ? "3" : "1";

            List<string> query = new List<string>();


            try
            {
                //if (usuario == null) usuario = "soporte7";

                if (IERP_ID == null) IERP_ID = "0";

                if (OPE == "INS" || IERPS_SEC == null)
                    IERPS_SEC = "INTBAW.IERP_SECUENCIAS.nextval from dual";
             
                switch (IERP_OPCION)
                {

                    case "ADMIN USUARIOS":

                        IERP_ID = IERP_CODIGO.Split('|')[0];

                        IERP_TITULO = IERP_CODIGO.Split('|')[2];

                        IERP_CODIGO = IERP_CODIGO.Split('|')[1];


                        sqlString = @"SELECT IERPURM_PK_CONTACT as IERP_ID, UPPER(IERPUR_USERNAME) as IERP_CODIGO, IERPUR_ROLL as IERP_ERP_CODIGO FROM INTBAW.IERP_USER_ROLES 
                            
                        WHERE IERPUR_STATUS = '1' AND IERPURE_ID = '" + IERPHE_ID + @"' AND IERPURM_ID = '" + IERPM_ID + @"' AND
                        
                        IERPURM_PK_CONTACT = '" + IERP_ID + "' AND UPPER(IERPUR_USERNAME) = '" + IERP_CODIGO + "'"; // AND TRIM(IERPUR_ROLL) = '" + IERP_ERP_CODIGO + "')";

                        IB_CATALOGOS data = cn.EjecutarRow<IB_CATALOGOS>(sqlString);

                        if (data.IERP_ID == null) { 
                        
                            sqlString = @"        
                            INSERT INTO INTBAW.IERP_USER_ROLES(
                            IERPURM_PK_CONTACT,
                            IERPUR_USERNAME, 
                            IERPUR_CONTACT_NAME, 
                            IERPUR_ROLL,
                            IERPUR_STATUS,
                            IERPUR_USER,
                            IERPURE_ID,
                            IERPURM_ID
                            ) ";

                            if (IERPHE_ID == null)
                                IERPHE_ID = "";

                            sqlString += " VALUES (";

                            sqlString += "'" + IERP_ID + "', '" + IERP_CODIGO + "', '" + IERP_TITULO + "', '" + IERP_ERP_CODIGO + "', '" + estado + "', '" + usuario + "', '" + IERPHE_ID + "', '" + IERPM_ID + "'";

                            sqlString += ")";
                        }
                        else {

                            if (OPE == "DEL")
                                sqlString = @"UPDATE INTBAW.IERP_USER_ROLES SET IERPUR_STATUS = '3' ";
                            else
                                sqlString = @"UPDATE INTBAW.IERP_USER_ROLES SET IERPUR_ROLL = '" + IERP_ERP_CODIGO.Trim() + "' ";

                            sqlString += @" WHERE UPPER(IERPUR_USERNAME) = '" + IERP_CODIGO.Trim() + "' AND IERPURE_ID = '" + IERPHE_ID + "' AND IERPURM_ID = '" + IERPM_ID + "' ";
                        }

                        result = cn.Execute(sqlString) ? "OK" : "Error";

                        break;



                    case "ADMIN CENTRO COSTO":

 

                        sqlString = @"SELECT 
  IERPH_ID as IERP_ID,			
  IERPM_ID,          	
  IERPH_CODIGO as IERP_CODIGO,		
  IERPH_ERP_CODIGO as IERP_ERP_CODIGO,		  	
  IERPH_USER as IERP_USER,  	
  IERPH_DATE as IERP_DATE  	

FROM INTBAW.IERP_HOMOLOGACIONES

WHERE IERPM_ID = '" + IERPM_ID + "' AND IERPH_STATUS = '1' AND IERPE_ID = '" + IERPHE_ID + "' AND IERPH_CODIGO = '" + IERP_CODIGO + "' AND IERPH_ERP_CODIGO = '" + IERP_ERP_CODIGO + "' ";

                        IERP_TITULO = IERP_TITULO.Replace(IERP_CODIGO, "").Trim().ToUpper();

                        IB_CATALOGOS data1 = cn.EjecutarRow<IB_CATALOGOS>(sqlString);

                        if (data1.IERP_ID == null)
                        {

                            sqlString = @"        
                            INSERT INTO INTBAW.IERP_HOMOLOGACIONES (
                            IERPM_ID,
                            IERPE_ID, 
                            IERPH_CODIGO, 
                            IERPH_NOMBRE,
                            IERPH_ERP_CODIGO,
                            IERPH_STATUS,
                            IERPH_USER
                            ) ";
                            
                            sqlString += " VALUES (";                            

                            sqlString += "'" + IERPM_ID + "', '" + IERPHE_ID + "', '" + IERP_CODIGO + "', '" + IERP_TITULO + "', '" + IERP_ERP_CODIGO + "', '" + estado + "', '" + usuario + "'  ";

                            sqlString += ")";
                        }
                        else
                        {

                            if (OPE == "DEL")
                                sqlString = @"UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPH_STATUS = '3' ";
                            else
                                sqlString = @"UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPH_CODIGO = '" + IERP_CODIGO.Trim() + "', IERPH_ERP_CODIGO = '" + IERP_ERP_CODIGO.Trim() + "' ";

                            sqlString += @" WHERE IERPH_ID = '" + IERP_ID + "' ";
                        }

                        result = cn.Execute(sqlString) ? "OK" : "Error";


                        UpdateHomologacionesOperativo(IERP_CODIGO, IERP_TITULO, IERP_ERP_CODIGO, "", estado, IERPM_ID, IERPM_DES, IERPHE_ID, "", "", "", "", "", IERPM_ID); //centro costo


                        break;



                    case "ADMIN CATALOGOS":
                        sqlString = @"SELECT IERPC_ID as IERP_ID, IERPC_TITULO as IERP_TITULO, IERPC_CODIGO as IERP_CODIGO, IERPC_ERP_CODIGO as IERP_ERP_CODIGO FROM INTBAW.IERP_CATALOGOS                            
                        WHERE IERPC_STATUS = '1' AND IERPM_ID = '" + IERPM_ID + @"' AND                 
                        (TRIM(IERPC_TITULO) = '" + IERP_TITULO.Trim() + "' AND TRIM(IERPC_CODIGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPC_ERP_CODIGO) = '" + IERP_ERP_CODIGO.Trim() + "')";
                        query.Add(sqlString); // 0

                        sqlString = @"SELECT IERPCAT_ROL FROM INTBAW.IERP_CATEGORIAS WHERE IERPCAT_ID = '" + IERP_ERP_CODIGO.Trim() + "'";
                        string rol = cn.GetScalar(sqlString); //2021-07-30

                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_CATALOGOS(
                        IERPM_ID,
                        IERPC_TITULO,
                        IERPC_CODIGO,
                        IERPC_ERP_CODIGO,
                        IERPC_STATUS,
                        IERPC_USER,
                        IERPC_ROL,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_TITULO.Trim() + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + estado + "', '" + usuario + "', '" + rol + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1

                        sqlString = "UPDATE INTBAW.IERP_CATALOGOS SET IERPC_STATUS = '2' WHERE IERPC_ID = " + IERP_ID + "";                     
                        query.Add(sqlString); // 2

                        sqlString = "select TO_CHAR(INTBAW.IERP_CATALOGOS_SEQ.currval) from dual";
                        query.Add(sqlString); // 3

                        sqlString = "UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPC_ID = 'nextid' WHERE IERPC_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 4

                        sqlString = "SELECT TO_CHAR(IERPC_ID) FROM INTBAW.IERP_HOMOLOGACIONES WHERE IERPC_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 5

                        result = Opera(IERP_OPCION, OPE, query);

                        break;

                    case "ADMIN PAISES":

                        sqlString = @"SELECT IERPP_ID as IERP_ID, IERPP_CODIGO as IERP_CODIGO, IERPP_ERP_CODIGO as IERP_ERP_CODIGO FROM INTBAW.IERP_PAISES 
                            
                        WHERE IERPP_STATUS = '1' AND IERPE_ID = '" + IERPHE_ID + @"' AND IERPM_ID = '" + IERPM_ID + @"' AND
                        
                        (TRIM(IERPP_CODIGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPP_ERP_CODIGO) = '" + IERP_ERP_CODIGO.Trim() + "')";

                        query.Add(sqlString); // 0

                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_PAISES(
                        IERPM_ID,
                        IERPP_CODIGO,
                        IERPP_ERP_CODIGO,
                        IERPP_STATUS,
                        IERPP_USER,
                        IERPE_ID,
                        IERPS_SEC
                        ) ";

                        if (IERPHE_ID == null)
                            IERPHE_ID = "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + estado + "', '" + usuario + "', '" + IERPHE_ID + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1

                        sqlString = "UPDATE INTBAW.IERP_PAISES SET IERPP_STATUS = '2' WHERE IERPP_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        sqlString = "select TO_CHAR(INTBAW.IERP_PAISES_SEQ.currval) from dual";
                        query.Add(sqlString); // 3

                        sqlString = "UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPP_ID = 'nextid' WHERE IERPP_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 4

                        sqlString = "SELECT TO_CHAR(IERPP_ID) FROM INTBAW.IERP_HOMOLOGACIONES WHERE IERPP_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 5

                        result = Opera(IERP_OPCION, OPE, query);



                        UpdateHomologacionesOperativo(IERP_CODIGO, "", IERP_ERP_CODIGO, "", estado, IERPM_ID, IERPM_DES, IERPHE_ID, "10", "", "", "", "", IERPM_ID); //paises





                        break;

                    case "ADMIN EMPRESAS":

                        sqlString = @"SELECT IERPM_ID as IERP_ID, IERPE_CODIGO as IERP_CODIGO, IERPE_ERP_CODIGO as IERP_ERP_CODIGO FROM INTBAW.IERP_EMPRESAS 
                            
                        WHERE IERPE_STATUS = '1' AND IERPM_ID = '" + IERPM_ID + @"' AND
                        
                        (TRIM(IERPE_CODIGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPE_ERP_CODIGO) = '" + IERP_ERP_CODIGO.Trim() + "')";

                        query.Add(sqlString); // 0



                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_EMPRESAS(
                        IERPM_ID,
                        IERPE_CODIGO,
                        IERPE_ERP_CODIGO,
                        IERPE_STATUS,
                        IERPE_USER,
                        IERPE_NOMBRE,
                        IERPE_ERP_NOMBRE,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + estado + "', '" + usuario + "', '" + IERP_CAMPO_PROGRA + "', '" + IERP_ORDEN + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1


                        sqlString = "UPDATE INTBAW.IERP_EMPRESAS SET IERPE_STATUS = '2' WHERE IERPE_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        sqlString = "select TO_CHAR(INTBAW.IERP_EMPRESAS_SEQ.currval) from dual";
                        query.Add(sqlString); // 3

                        sqlString = "UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPE_ID = 'nextid' WHERE IERPE_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 4

                        sqlString = "SELECT TO_CHAR(IERPE_ID) FROM INTBAW.IERP_HOMOLOGACIONES WHERE IERPE_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 5

                        result = Opera(IERP_OPCION, OPE, query);




                        //2021-09-06
                        sqlString = @"SELECT DISTINCT d.IERPE_ID as data1, a.EMPRESA || ' - ' || d.IERPE_CODIGO as data2, d.IERPE_ERP_CODIGO as data3 
                FROM (
                    SELECT b.EMPRESA_ID, c.IERPE_ERP_CODIGO || ' - ' || b.EMPRESA_ESQUEMA as EMPRESA
                    FROM INTBAW.IERP_EMPRESAS c
                    INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = c.IERPE_ERP_CODIGO AND b.EMPRESA_STATUS = '1'
                    INNER JOIN INTBAW.IERP_USER_ROLES ON UPPER(IERPUR_USERNAME) = '" + usuario + @"' AND (IERPURE_ID = c.IERPE_ID OR IERPUR_ADMIN = 1) AND IERPUR_STATUS = '1'
                    WHERE c.IERPE_STATUS = '1'
                    GROUP BY  b.EMPRESA_ID, c.IERPE_ERP_CODIGO || ' - ' || b.EMPRESA_ESQUEMA
                ) a
                INNER JOIN INTBAW.IERP_EMPRESAS d ON d.IERPE_ERP_CODIGO = a.EMPRESA_ID AND d.IERPE_STATUS = '1'
                ORDER BY d.IERPE_ID FETCH NEXT 1 ROWS ONLY";

                        sqlString = @"
                                SELECT * FROM (
                                       " + sqlString + @"
                                ) b WHERE data3 = '" + IERP_ERP_CODIGO + @"'";

                        IB_STANDAR data2 = cn.EjecutarRow<IB_STANDAR>(sqlString);

                        IERPHE_ID = data2.data1;

                        UpdateHomologacionesOperativo(IERP_CODIGO, "", IERP_ERP_CODIGO, "", estado, IERPM_ID, IERPM_DES, IERPHE_ID, "11", "", "", "", "", IERPM_ID); //empresas



                        break;
/*
                    case "ADMIN RUBROS":

                        sqlString = @"SELECT IERPM_ID as IERP_ID, IERPR_CODIGO as IERP_CODIGO, IERPR_ERP_CODIGO as IERP_ERP_CODIGO FROM INTBAW.IERP_RUBROS 
                            
                        WHERE IERPR_STATUS = '1' AND 
                        
                        (TRIM(IERPR_CODIGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPR_ERP_CODIGO) = '" + IERP_ERP_CODIGO.Trim() + "')";

                        query.Add(sqlString); // 0



                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_RUBROS(
                        IERPM_ID,
                        IERPR_CODIGO,
                        IERPR_ERP_CODIGO,
                        IERPR_STATUS,
                        IERPR_USER,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', " + (OPE == "DEL" ? '3' : '1') + ", '" + usuario + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1


                        sqlString = "UPDATE INTBAW.IERP_RUBROS SET IERPR_STATUS = '2' WHERE IERPR_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        result = Opera(IERP_OPCION, OPE, query);

                        break;



                    case "ADMIN MONEDAS":

                        sqlString = @"SELECT IERPMON_ID as IERP_ID, IERPMON_CODIGO as IERP_CODIGO, IERPMON_ERP_CODIGO as IERP_ERP_CODIGO FROM INTBAW.IERP_MONEDAS 
                            
                        WHERE IERPMON_STATUS = '1' AND 
                        
                        TRIM(IERPMON_CODIGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPMON_ERP_CODIGO) = '" + IERP_ERP_CODIGO.Trim() + "')";

                        query.Add(sqlString); // 0


                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_MONEDAS(
                        IERPM_ID,
                        IERPMON_CODIGO,
                        IERPMON_ERP_CODIGO,
                        IERPMON_STATUS,
                        IERPMON_USER,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', " + (OPE == "DEL" ? '3' : '1') + ", '" + usuario + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1

   
                        sqlString = "UPDATE INTBAW.IERP_MONEDAS SET IERPMON_STATUS = '2' WHERE IERPMON_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        result = Opera(IERP_OPCION, OPE, query);

                        break;
*/

                    case "ADMIN CATEGORIAS":

                        sqlString = @"SELECT IERPCAT_ID as IERP_ID, IERPCAT_CATEGO as IERP_CODIGO, IERPCAT_SISTEMA as IERP_ERP_CODIGO, IERPCAT_DESCRIPCION as IERP_TITULO FROM INTBAW.IERP_CATEGORIAS 
                            
                        WHERE IERPCAT_STATUS = '1' AND IERPM_ID = '" + IERPM_ID + @"' AND
                        
                        (TRIM(IERPCAT_CATEGO) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPCAT_DESCRIPCION) = '" + IERP_TITULO.Trim() + "'AND TRIM(IERPCAT_SISTEMA) = '" + IERP_ERP_CODIGO.Trim() + "')";

                        // valida que no se repitan valores
                        query.Add(sqlString); // 0

                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_CATEGORIAS(                        
                        IERPM_ID,
                        IERPCAT_CATEGO,
                        IERPCAT_SISTEMA,
                        IERPCAT_DESCRIPCION,
                        IERPCAT_STATUS,
                        IERPCAT_USER,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERPM_ID + "', '" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + IERP_TITULO.Trim() + "', '" + estado + "', '" + usuario + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        // insert registro
                        query.Add(sqlString); // 1

                        // registro anterior lo desactiva
                        sqlString = "UPDATE INTBAW.IERP_CATEGORIAS SET IERPCAT_STATUS = '2' WHERE IERPCAT_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        // obtiene el ultimo id inserted
                        sqlString = "select TO_CHAR(INTBAW.IERP_CATEGORIAS_SEQ.currval) from dual";
                        query.Add(sqlString); // 3

                        // actualiza dependencias en catalogos OPERATIVO
                        sqlString = "UPDATE INTBAW.IERP_CATALOGOS SET IERPC_CODIGO = 'nextid' WHERE IERPC_CODIGO = '" + IERP_ID + "'";
                        query.Add(sqlString); // 4

                        // dependencias en catalogos OPERATIVO
                        sqlString = "SELECT IERPC_CODIGO FROM INTBAW.IERP_CATALOGOS WHERE IERPC_CODIGO = '" + IERP_ID + "'";
                        query.Add(sqlString); // 5

                        // dependencias en catalogos ERP
                        sqlString = "SELECT IERPC_ERP_CODIGO FROM INTBAW.IERP_CATALOGOS WHERE IERPC_ERP_CODIGO = '" + IERP_ID + "'";
                        query.Add(sqlString); // 6

                        // actualiza dependencias en catalogos ERP
                        sqlString = "UPDATE INTBAW.IERP_CATALOGOS SET IERPC_ERP_CODIGO = 'nextid' WHERE IERPC_ERP_CODIGO = '" + IERP_ID + "'";
                        query.Add(sqlString); // 8

                        result = Opera(IERP_OPCION, OPE, query);

                        break;


                    case "ADMIN QUERYS":
                        if (IERP_CODIGO == null) IERP_CODIGO = "";
                        sqlString = @"SELECT IERPQ_ID as IERP_ID, IERPQ_SQL_ORDER as IERP_CODIGO, IERPQ_SISTEM as IERP_ERP_CODIGO, IERPQ_CATEGO as IERP_TITULO FROM INTBAW.IERP_QUERYS 
                            
                        WHERE IERPQ_STATUS = '1' AND 
                        
                        TRIM(IERPQ_SQL_ORDER) = '" + IERP_CODIGO.Trim() + "' AND TRIM(IERPQ_CATEGO) = '" + IERP_TITULO.Trim() + "' AND TRIM(IERPQ_SISTEM) = '" + IERP_ERP_CODIGO.Trim() + @"'
                            
                        AND TRIM(IERPQ_SQL) = '" + IERP_SQL.Trim() + "'";

                        query.Add(sqlString); // 0


                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_QUERYS(                        
                        IERPQ_SQL_ORDER,
                        IERPQ_SISTEM,
                        IERPQ_CATEGO,
                        IERPQ_SQL,
                        IERPQ_STATUS,
                        IERPQ_USER,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";

                        sqlString += "'" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + IERP_TITULO.Trim() + "', '" + IERP_SQL.Trim() + "', '" + estado + "', '" + usuario + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1

                        sqlString = "UPDATE INTBAW.IERP_QUERYS SET IERPQ_STATUS = '2' WHERE IERPQ_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        sqlString = "select TO_CHAR(INTBAW.IERP_QUERYS_SEQ.currval) from dual";
                        query.Add(sqlString); // 3

                        sqlString = "UPDATE INTBAW.IERP_QUERYS_FILTROS SET IERPQ_ID = 'nextid' WHERE IERPQ_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 4

                        sqlString = "SELECT TO_CHAR(IERPQ_ID) FROM INTBAW.IERP_QUERYS_FILTROS WHERE IERPQ_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 5

                        result = Opera(IERP_OPCION, OPE, query);

                        break;


                    case "ADMIN FILTROS":
             
                        sqlString = @"SELECT IERPQF_ID as IERP_ID, IERPQF_CAMPO as IERP_CODIGO, IERPQF_COMPARATIVO as IERP_ERP_CODIGO, IERPQ_ID as IERP_TITULO, 

    IERPQF_CAMPO_PROGRA as IERP_CAMPO_PROGRA, IERPQF_ORDER as IERP_ORDEN, IERPQF_OPERADOR as IERP_OPERADOR

                        FROM INTBAW.IERP_QUERYS_FILTROS  
                            
                        WHERE IERPQF_STATUS = '1' AND
                        
                        TRIM(IERPQF_CAMPO) = '" + IERP_CODIGO.Trim() + "' AND IERPQ_ID = '" + IERP_TITULO.Trim() + "' AND TRIM(IERPQF_COMPARATIVO) = '" + IERP_ERP_CODIGO.Trim() + @"'

                        AND TRIM(IERPQF_CAMPO_PROGRA) = '" + IERP_CAMPO_PROGRA.Trim() + "' AND IERPQF_ORDER = '" + IERP_ORDEN + "' AND IERPQF_OPERADOR = '" + IERP_OPERADOR + "' ";

                        query.Add(sqlString); // 0


                       

                        sqlString = @"        
                        INSERT INTO INTBAW.IERP_QUERYS_FILTROS(                        
                        IERPQF_CAMPO,
                        IERPQF_COMPARATIVO,
                        IERPQ_ID,
                        IERPQF_CAMPO_PROGRA,
                        IERPQF_ORDER,
                        IERPQF_OPERADOR,
                        IERPQF_STATUS,
                        IERPQF_USER,
                        IERPS_SEC
                        ) ";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += " SELECT ";
                        else
                            sqlString += " VALUES (";
                        if (IERP_OPERADOR == null) IERP_OPERADOR = "";
                        sqlString += "'" + IERP_CODIGO.Trim() + "', '" + IERP_ERP_CODIGO.Trim() + "', '" + IERP_TITULO.Trim() + "', '" + IERP_CAMPO_PROGRA.Trim() + "', '" + IERP_ORDEN.Trim() + "', '" + IERP_OPERADOR.Trim() + "', '" + estado + "', '" + usuario + "', " + IERPS_SEC + "";

                        if (OPE == "INS" || IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                            sqlString += "";
                        else
                            sqlString += ")";

                        query.Add(sqlString); // 1

             
                        sqlString = "UPDATE INTBAW.IERP_QUERYS_FILTROS SET IERPQF_STATUS = '2' WHERE IERPQF_ID = " + IERP_ID + "";
                        query.Add(sqlString); // 2

                        result = Opera(IERP_OPCION, OPE, query);

                        break;
                }

                
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }







        ///////////////////////////////////////////////////////  HOMOLOGCIONES  //////////////////////////////////////////////////////////


        public IERP_QUERYS Get_IERP_QUERYS(string IERPHC_ID, string IERPHM_DES, string IERPHE_ID)
        {        

            // 2021-06-29 se desligo empresas cargo system de la tabla EMPRESAS (empresas_parametros)
            string query = @"SELECT a.IERPQ_ID, a.IERPQ_SQL, a.IERPQ_SQL_ORDER, IERPCAT_ID, IERPCAT_CATEGO, IERPQ_SQL_UPDATE,  

            d.IERPE_ID, b.EMPRESA_ESQUEMA, d.IERPE_CODIGO, d.IERPE_ERP_CODIGO, b.EMPRESA_DBLINK, IERPCAT_DESCRIPCION as IERPCAT_CATEGO_DES 

            FROM INTBAW.IERP_CATALOGOS

            INNER JOIN INTBAW.IERP_CATEGORIAS ON " + (IERPHM_DES == "ERP" ? "IERPC_ERP_CODIGO" : "IERPC_CODIGO") + @" = IERPCAT_ID

            INNER JOIN INTBAW.IERP_QUERYS a ON SUBSTR(IERPCAT_CATEGO,0,2) = a.IERPQ_CATEGO AND a.IERPQ_STATUS = '1' AND a.IERPQ_SISTEM = IERPCAT_SISTEMA

            INNER JOIN INTBAW.IERP_EMPRESAS d ON d.IERPE_ID = " + IERPHE_ID + @"

            INNER JOIN INTBAW.EMPRESAS b ON b.EMPRESA_ID = d.IERPE_ERP_CODIGO

            WHERE IERPC_ID = " + IERPHC_ID;

            IERP_QUERYS data = cn.EjecutarRow<IERP_QUERYS>(query);

            data.IERPQ_SQL = data.IERPQ_SQL.Replace("'' as IERPH_CAT", "'" + data.IERPCAT_CATEGO + "' as IERPH_CAT");

            return data;
        }

        ///////////////////////////////////////////////////////  GET QUERY  //////////////////////////////////////////////////////////
        public string GetQueryCatalogos(string IERPHC_ID, string IERPH_NOMBRE, string IERPH_NIT, string IERPHP_ID, string IERPHM_DES, string IERPHE_ID)
        {

            //////////////////////////////  IERP_QUERYS

            IERP_QUERYS data = Get_IERP_QUERYS(IERPHC_ID, IERPHM_DES, IERPHE_ID);

            if (IERPHM_DES == "ERP")
                data.IERPQ_SQL = data.IERPQ_SQL.Replace("TRANSIT.", data.EMPRESA_ESQUEMA + "."); // EMPRESA_ESQUEMA SOLO PARA ERP




            //////////////////////////////  IERP_QUERYS_FILTROS

            string query = "SELECT IERPQF_OPERADOR as IERPF_OPERADOR, IERPQF_CAMPO as IERPF_CAMPO, IERPQF_COMPARATIVO as IERPF_COMPARATIVO, IERPQF_CAMPO_PROGRA as IERPF_CAMPO_PROGRA, IERPQF_SEP1, IERPQF_SEP2  FROM INTBAW.IERP_QUERYS_FILTROS WHERE IERPQ_ID = " + data.IERPQ_ID + " AND IERPQF_STATUS = '1' ORDER BY IERPQF_ORDER";

            DataTable DataFiltros = cn.Query(query, "BAW");

            List<IERP_FILTROS> LstFiltros = CapaDatos.Utils.get_list_struct2<IERP_FILTROS>(DataFiltros);



            if (IERPH_NOMBRE == null) IERPH_NOMBRE = "";

            if (IERPH_NIT == null) IERPH_NIT = "";



            if (IERPH_NOMBRE == "" && IERPH_NIT != "") IERPH_NOMBRE = IERPH_NIT;

            if (IERPH_NOMBRE != "" && IERPH_NIT == "") IERPH_NIT = IERPH_NOMBRE;

            string filtros = "", campo;

            foreach (IERP_FILTROS row in LstFiltros)
            {
                campo = "";

                switch (row.IERPF_CAMPO_PROGRA)
                {
                    case "IERPH_CODIGO":
                        campo = IERPH_NOMBRE;
                        break;

                    case "IERPH_NOMBRE":

                        campo = IERPH_NOMBRE;

                        if (IERPHM_DES == "ERP" && IERPH_NIT != "")                            
                            campo = IERPH_NIT;
                           
                        break;


                    case "IERPP_ID":

                        //////////////////////////////  IERP_PAISES

                        query = "SELECT " + (IERPHM_DES == "ERP" ? "IERPP_ERP_CODIGO" : "IERPP_CODIGO") + " as IERPP_ID FROM INTBAW.IERP_PAISES WHERE IERPP_ID = " + IERPHP_ID;
                        IB_HOMOLOGACIONES data1 = cn.EjecutarRow<IB_HOMOLOGACIONES>(query);

                        campo = data1.IERPP_ID;  //pais en su mayoria los querys se les quito este filtro

                        break;
                    case "IERPH_CAT":
                        //040411 debe quitar los primeros 2 chars
                        campo = data.IERPCAT_CATEGO.Remove(0,2);
                        campo = int.Parse(campo).ToString();  //categoria
                        break;
                }


                if (IERPHM_DES == "ERP")                   
                    row.IERPF_CAMPO = "Translate(" + row.IERPF_CAMPO + ", 'ÁáÉéÍíÓóÚú', 'AaEeIiOoUu')"; 
                    
                //                   AND  OR                        (                      XXXXXX             LIKE / ILIKE / =
                filtros += " " + row.IERPF_OPERADOR + " " + row.IERPQF_SEP1 + " " + row.IERPF_CAMPO + " " + row.IERPF_COMPARATIVO + " ";

                switch (row.IERPF_COMPARATIVO)
                {
                    case "ILIKE":
                    case "LIKE":
                        filtros += campo == "" ? row.IERPF_CAMPO : "'%" + campo.ToUpper() + "%'";   
                        break;

                    case "=":
                        filtros += campo == "" ? row.IERPF_CAMPO : "'" + campo.ToUpper() + "'";
                        break;
                }

                filtros += " " + row.IERPQF_SEP2 + " "; // )


            }

            if (filtros == "") filtros = " 1 = 1 ";

            query = data.IERPQ_SQL + " " + filtros + " " + (data.IERPQ_SQL_ORDER == null || data.IERPQ_SQL_ORDER.Trim() == "" ? "" : " ORDER BY " + data.IERPQ_SQL_ORDER);

            return query + "#" + data.IERPCAT_CATEGO + " - " +  data.IERPCAT_CATEGO_DES;
        }

        ///////////////////////////////////////////////////////  GET HOMO  //////////////////////////////////////////////////////////
        public List<IB_HOMOLOGACIONES> GetHomo(string IERPHM_ID, string IERPHC_ID, string IERPHP_ID, string IERPHE_ID, string IERPH_NOMBRE, string IERPHM_CHK, string IERPH_NIT, string IERPHM_DES, string tipo)
        {
            List<IB_HOMOLOGACIONES> LstHomo = new List<IB_HOMOLOGACIONES>();

            if (IERPH_NIT == null) IERPH_NIT = "";
            if (IERPH_NOMBRE == null) IERPH_NOMBRE = "";
            if (IERPHC_ID == null) IERPHC_ID = "";
            if (IERPHP_ID == null) IERPHP_ID = "";



            string query = @"
SELECT a.IERPH_ID, 
CASE WHEN '" + IERPHM_DES + @"' <> 'ERP' AND '" + tipo + @"' = 'CONSULTA' THEN a.IERPH_CODIGO ELSE f.IERPE_CODIGO || '/' || g.IERPP_CODIGO || '/' || d.IERPCAT_CATEGO || '/' || a.IERPH_CODIGO END AS IERPH_CODIGO, 
a.IERPH_NOMBRE, 
CASE WHEN '" + IERPHM_DES + @"' = 'ERP' AND '" + tipo + @"' = 'CONSULTA' THEN a.IERPH_ERP_CODIGO ELSE f.IERPE_ERP_CODIGO || '/' || g.IERPP_ERP_CODIGO || '/' || e.IERPCAT_CATEGO || '/' || a.IERPH_ERP_CODIGO END AS IERPH_ERP_CODIGO, 
a.IERPH_ERP_NOMBRE, 
a.IERPH_USER, a.IERPH_DATE, a.IERPH_STATUS, b.IERPS_NOMBRE as IERPH_STATUS_DES, '' as IERPH_NIT, 'C' as IERPH_ACTIVO, 

CASE WHEN '" + tipo + @"' = 'OPERACION' THEN 
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN d.IERPCAT_ID ELSE e.IERPCAT_ID END   
ELSE
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN e.IERPCAT_ID ELSE d.IERPCAT_ID END
END AS IERPC_ID,

CASE WHEN '" + tipo + @"' = 'OPERACION' THEN 
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN d.IERPCAT_CATEGO ELSE e.IERPCAT_CATEGO END   
ELSE
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN e.IERPCAT_CATEGO ELSE d.IERPCAT_CATEGO END
END AS IERPH_CAT,
CASE WHEN '" + tipo + @"' = 'OPERACION' THEN 
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN d.IERPCAT_DESCRIPCION ELSE e.IERPCAT_DESCRIPCION END   
ELSE
    CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN e.IERPCAT_DESCRIPCION ELSE d.IERPCAT_DESCRIPCION END
END AS IERPH_CAT_DES,
CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN f.IERPE_CODIGO ELSE f.IERPE_ERP_CODIGO END as IERPE_ID,
CASE WHEN '" + IERPHM_DES + @"' = 'ERP' THEN g.IERPP_CODIGO ELSE g.IERPP_ERP_CODIGO END as IERPP_ID,
a.IERPH_CODIGO AS IERPH_CODIGO2,
a.IERPH_ERP_CODIGO AS IERPH_ERP_CODIGO2,
a.IERPM_ID,
'" + IERPHM_DES + @"' as IERPH_SYSTEMA,
 a.IERPH_TIPO_MONEDA

FROM INTBAW.IERP_HOMOLOGACIONES a
INNER JOIN INTBAW.IERP_STATUS b ON a.IERPH_STATUS = b.IERPS_CODIGO 
INNER JOIN INTBAW.IERP_CATALOGOS c ON  c.IERPC_ID = a.IERPC_ID
INNER JOIN INTBAW.IERP_CATEGORIAS d ON d.IERPCAT_ID = c.IERPC_CODIGO
INNER JOIN INTBAW.IERP_CATEGORIAS e ON e.IERPCAT_ID = c.IERPC_ERP_CODIGO
INNER JOIN INTBAW.IERP_EMPRESAS f ON  f.IERPE_ID = a.IERPE_ID
INNER JOIN INTBAW.IERP_PAISES g ON  g.IERPP_ID = a.IERPP_ID
WHERE a.IERPH_STATUS " + (IERPHM_CHK == "1" ? " <> '1' " : " = '1'") + @" AND a.IERPM_ID = " + IERPHM_ID + " AND a.IERPE_ID = " + IERPHE_ID + " ";


            if (IERPHC_ID != "") {
                query += " AND a.IERPC_ID = " + IERPHC_ID + " ";
            }

            if (IERPHP_ID != "" && IERPHP_ID != null)
            {
                query += " AND a.IERPP_ID= " + IERPHP_ID + " ";
            }


            if (IERPH_NOMBRE != "" || IERPH_NIT != "")
            {
                query += " AND (";
            }

            if (IERPH_NOMBRE != "")
            {
                //query += @"(a.IERPH_CODIGO LIKE '%" + IERPH_NOMBRE.ToUpper() + @"%' OR a.IERPH_NOMBRE LIKE '%" + IERPH_NOMBRE.ToUpper() + @"%' OR a.IERPH_ERP_CODIGO LIKE '%" + IERPH_NOMBRE.ToUpper() + @"%' OR a.IERPH_ERP_NOMBRE LIKE '%" + IERPH_NOMBRE.ToUpper() + @"%')";
                query += "(a.IERPH_CODIGO LIKE '%" + IERPH_NOMBRE.ToUpper() + @"%' OR a.IERPH_NOMBRE LIKE '%" + IERPH_NOMBRE.ToUpper() + "%')";
            }

            if (IERPH_NIT != "")
            {
                if (IERPH_NOMBRE != "")
                    query += " OR ";

                //query += @" (a.IERPH_CODIGO LIKE '%" + IERPH_NIT.ToUpper() + @"%' OR a.IERPH_NOMBRE LIKE '%" + IERPH_NIT.ToUpper() + @"%' OR a.IERPH_ERP_CODIGO LIKE '%" + IERPH_NIT.ToUpper() + @"%' OR a.IERPH_ERP_NOMBRE LIKE '%" + IERPH_NIT.ToUpper() + @"%')";
                query += "(a.IERPH_ERP_CODIGO LIKE '%" + IERPH_NIT.ToUpper() + @"%' OR a.IERPH_ERP_NOMBRE LIKE '%" + IERPH_NIT.ToUpper() + "%')";
            }


            if (IERPH_NOMBRE != "" || IERPH_NIT != "")
            {
                query += ") ";
            }



            DataTable homologaciones = cn.Query(query, "BAW");

            LstHomo = CapaDatos.Utils.get_list_struct2<IB_HOMOLOGACIONES>(homologaciones);

            return LstHomo;
        }



        ///////////////////////////////////////////////////////  LLENA GRID OPERATIVO //////////////////////////////////////////////////////////
        [Route("api/Catalogos_Calls_API/Set_IB_HOMOLOGACION")]
        public List<List<IB_HOMOLOGACIONES>> GetHomCatalogos2Grid(string OPE1, string IERPH_STATUS, string IERPHM_ID, string IERPHM_DES, string IERPHC_ID, string IERPHP_ID, string IERPHE_ID, string IERPH_NOMBRE, string IERPH_NIT, string usuario, string IERPH_OPCION, string IERPHM_CHK)
        {
            string query = "";
            

            List <IB_HOMOLOGACIONES> listView = new List<IB_HOMOLOGACIONES>();

            List<IB_HOMOLOGACIONES> LstHomo = new List<IB_HOMOLOGACIONES>();

            List<List<IB_HOMOLOGACIONES>> result = new List<List<IB_HOMOLOGACIONES>>();

            try
            {

                string QueryResult = "", Title = "";

                if (IERPH_NOMBRE == null) IERPH_NOMBRE = "";
                if (IERPH_NIT == null) IERPH_NIT = "";

                if (IERPH_OPCION == "OPERACION DE HOMOLOGACIONES") {

                    /////////////////////////////  CATALOGOS QUERY
                    QueryResult = GetQueryCatalogos(IERPHC_ID, IERPH_NOMBRE, IERPH_NIT, IERPHP_ID, IERPHM_DES, IERPHE_ID); //CARGO_SYSTEM

                    query = QueryResult.Split('#')[0];
                    Title = QueryResult.Split('#')[1];


                    /////////////////////////// DATA DE OPERACIONES
                    //DataTable ViewTable = CapaDatos.Postgres.EjecutarConsulta(query, IERPHM_ID);

                    List<IB_HOMOLOGACIONES> listViewTemp = GetListStandar<IB_HOMOLOGACIONES>("c", IERPHM_ID, query);

                    if (listViewTemp.Count > 0)
                    {

                        //// HOMOLOGACIONES
                        LstHomo = GetHomo(IERPHM_ID, IERPHC_ID, IERPHP_ID, IERPHE_ID, IERPH_NOMBRE, "", IERPH_NIT, IERPHM_DES, "OPERACION");

                        foreach (IB_HOMOLOGACIONES row_data in listViewTemp)
                        {
                            row_data.IERPH_ID = "0";
                            row_data.IERPH_ERP_CODIGO = "";
                            row_data.IERPH_ERP_CODIGO2 = "";
                            row_data.IERPH_ERP_NOMBRE = "";
                            row_data.IERPH_USER = "";
                            row_data.IERPH_DATE = "";
                            row_data.IERPH_STATUS = "1";
                            row_data.IERPH_STATUS_DES = "<font color=red>NO HOMOLOGADO</font>";
                            row_data.IERPE_ID = "";
                            row_data.IERPM_ID = "";
                            row_data.IERPH_CAT_DES = Title;
                            row_data.IERPH_CODIGO2 = row_data.IERPH_CODIGO;
                            row_data.IERPH_SYSTEMA = IERPHM_DES;
                            row_data.IERPH_TIPO_MONEDA = "";

                            row_data.IERPH_HOMOLOGADO = "N";

                            if (row_data.IERPH_ACTIVO == null) row_data.IERPH_ACTIVO = "S";

                            if (row_data.IERPH_ACTIVO == "S") row_data.IERPH_ACTIVO_DES = "ACTIVO"; else row_data.IERPH_ACTIVO_DES = "DESHABILITADO";

                            string cate = "", codi = "";

                            foreach (IB_HOMOLOGACIONES row_homo in LstHomo)
                            {

                                cate = row_homo.IERPH_CODIGO.Split('/')[2];
                                if (cate.Length > 2)
                                    cate = int.Parse(cate.Remove(0, 2)).ToString();

                                codi = row_homo.IERPH_CODIGO.Split('/')[3];



                                if (row_data.IERPH_CODIGO == codi && row_data.IERPH_CAT == cate)
                                {
                                    row_data.IERPH_ID = row_homo.IERPH_ID;
                                    row_data.IERPH_ERP_CODIGO = row_homo.IERPH_ERP_CODIGO;
                                    row_data.IERPH_ERP_CODIGO2 = row_homo.IERPH_ERP_CODIGO2;
                                    row_data.IERPH_ERP_NOMBRE = row_homo.IERPH_ERP_NOMBRE;
                                    row_data.IERPH_USER = row_homo.IERPH_USER;
                                    row_data.IERPH_DATE = row_homo.IERPH_DATE;
                                    row_data.IERPH_STATUS = row_homo.IERPH_STATUS;
                                    row_data.IERPH_STATUS_DES = row_homo.IERPH_STATUS_DES;

                                    row_data.IERPE_ID = row_homo.IERPE_ID;
                                    row_data.IERPM_ID = row_homo.IERPM_ID;
                                    row_data.IERPP_ID = row_homo.IERPP_ID;
                                    row_data.IERPC_ID = row_homo.IERPC_ID;
                                    row_data.IERPH_CAT = row_homo.IERPH_CAT;
                                    row_data.IERPH_CAT_DES = row_homo.IERPH_CAT_DES;
                                    row_data.IERPH_TIPO_MONEDA = row_homo.IERPH_TIPO_MONEDA.Trim() != "" ? "(" + row_homo.IERPH_TIPO_MONEDA + ")" : "";

                                    if (Int32.Parse(row_homo.IERPH_ID) > 0) row_data.IERPH_HOMOLOGADO = "S";

                                    break;
                                }

                            }


                            listView.Add(row_data);
                        }
                    }

                }


                if (IERPH_OPCION == "CONSULTA DE HOMOLOGACIONES")
                {
                    //// HOMOLOGACIONES
                    listView = GetHomo(IERPHM_ID, IERPHC_ID, IERPHP_ID, IERPHE_ID, IERPH_NOMBRE, IERPHM_CHK, IERPH_NIT, IERPHM_DES, "CONSULTA");

                    if (listView.Count > 0)
                        Title = listView[0].IERPH_CAT + " - " + listView[0].IERPH_CAT_DES;
                }


                if (listView.Count == 0)
                {
                    //listView.Add(new IB_HOMOLOGACIONES { IERPH_NOMBRE = "Busqueda '" + IERPH_NOMBRE + "' sin resultados", IERPH_ACTIVO = "S", IERPH_ID = "0", IERPH_STATUS = "3", IERPH_ERP_CODIGO = "" });

                }


                List<IB_HOMOLOGACIONES> LstTitle = new List<IB_HOMOLOGACIONES>() {
                    new IB_HOMOLOGACIONES { IERPH_NOMBRE = IERPH_NOMBRE.Trim() != "" ? Title + " [" + IERPH_NOMBRE + "]" : Title }
                };

                result.Add(listView);
                result.Add(LstTitle);

            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }

            return result;
        }


        ///////////////////////////////////////////////////////  LLENA GRID ERP //////////////////////////////////////////////////////////
        [Route("api/Catalogos_Calls_API/Set_IB_HOMOLOGACION_ERP")]
        //public List<IB_HOMOLOGACIONES> GetHomCatalogos3Grid(string IERPHM_ID, string IERPHC_ID, string IERPHP_ID, string IERPHE_ID, string IERPH_NOMBRE, string IERPH_NIT, object datos, string , string IERPH_OPCION, string IERPHM_CHK)
        public List<List<IB_HOMOLOGACIONES>> GetHomCatalogos3Grid(string IERPHM_ID, string IERPHC_ID, string IERPHP_ID, string IERPHE_ID, string IERPH_NOMBRE, string IERPH_NIT, string usuario, string IERPH_OPCION, string IERPHM_CHK)
        {
            List<IB_HOMOLOGACIONES> listView = new List<IB_HOMOLOGACIONES>();

            List<List<IB_HOMOLOGACIONES>> result = new List<List<IB_HOMOLOGACIONES>>();

            string query = "";
            Boolean activos = false;

            try
            {

                if (IERPH_NOMBRE == null) IERPH_NOMBRE = "";
                if (IERPH_NIT == null) IERPH_NIT = "";

                string QueryResult = "", Title = "";

                if (IERPH_OPCION == "OPERACION DE HOMOLOGACIONES")
                {

                    /////////////////////////////  CATALOGOS QUERY
                    QueryResult = GetQueryCatalogos(IERPHC_ID, IERPH_NOMBRE, IERPH_NIT, IERPHP_ID, "ERP", IERPHE_ID);

                    query = QueryResult.Split('#')[0];
                    Title = QueryResult.Split('#')[1];

                    /////////////////////////// DATA DE ERP
                    DataTable ViewTable = cn.Query(query, "BAW");

                    List<IB_HOMOLOGACIONES> listViewTemp = CapaDatos.Utils.get_list_struct2<IB_HOMOLOGACIONES>(ViewTable);


                    if (listViewTemp.Count > 0)
                    {


                        List<IB_HOMOLOGACIONES> LstHomo = null;
                        //if (datos == null) //si homologaciones ya vienen de operaciones
                        {

                            //if (IERPHC_ID != "321") // CONSECUTIVOS no debe traer la homologacion del lado ERP
                            {
                                //// HOMOLOGACIONES
                                LstHomo = GetHomo(IERPHM_ID, IERPHC_ID, IERPHP_ID, IERPHE_ID, IERPH_NOMBRE, "", IERPH_NIT, "ERP", "OPERACION");
                            }
                        }
                        //else
                        {
                            //LstHomo = (List<IB_HOMOLOGACIONES>)datos;
                        }


                        string cate = "", codi = "";
                        foreach (IB_HOMOLOGACIONES row_data in listViewTemp)
                        {
                            row_data.IERPH_ID = "0";
                            row_data.IERPH_CODIGO = "";
                            row_data.IERPH_CODIGO2 = "";
                            row_data.IERPH_NOMBRE = "";
                            row_data.IERPH_USER = "";
                            row_data.IERPH_DATE = "";
                            row_data.IERPH_STATUS = "1";
                            row_data.IERPH_STATUS_DES = "<font color=red>NO HOMOLOGADO</font>";
                            row_data.IERPE_ID = "";
                            row_data.IERPM_ID = "";
                            row_data.IERPH_CAT_DES = Title;
                            row_data.IERPH_ERP_CODIGO2 = row_data.IERPH_ERP_CODIGO;
                            row_data.IERPH_SYSTEMA = "ERP";
                            row_data.IERPH_TIPO_MONEDA = "";
                            row_data.IERPH_HOMOLOGADO = "N";

                            if (row_data.IERPH_ACTIVO == null) row_data.IERPH_ACTIVO = "N";

                            if (row_data.IERPH_ACTIVO == "S") { row_data.IERPH_ACTIVO_DES = "ACTIVO"; activos = true; } else row_data.IERPH_ACTIVO_DES = "DESHABILITADO";

                            //if (IERPHC_ID != "321") // CONSECUTIVOS no debe traer la homologacion del lado ERP
                            {

                                foreach (IB_HOMOLOGACIONES row_homo in LstHomo)
                                {
                                    cate = row_homo.IERPH_ERP_CODIGO.Split('/')[2];

                                    if (cate.Length > 2)
                                        cate = int.Parse(cate.Remove(0, 2)).ToString();

                                    codi = row_homo.IERPH_ERP_CODIGO.Split('/')[3];

                                    if (row_data.IERPH_ERP_CODIGO == codi && row_data.IERPH_CAT == cate)
                                    {
                                        row_data.IERPH_ID = row_homo.IERPH_ID;
                                        row_data.IERPH_CODIGO = row_homo.IERPH_CODIGO;
                                        row_data.IERPH_CODIGO2 = row_homo.IERPH_CODIGO2;
                                        row_data.IERPH_NOMBRE = row_homo.IERPH_NOMBRE;
                                        row_data.IERPH_USER = row_homo.IERPH_USER;
                                        row_data.IERPH_DATE = row_homo.IERPH_DATE;
                                        row_data.IERPH_STATUS = row_homo.IERPH_STATUS;
                                        row_data.IERPH_STATUS_DES = row_homo.IERPH_STATUS_DES;

                                        row_data.IERPE_ID = row_homo.IERPE_ID;
                                        row_data.IERPM_ID = row_homo.IERPM_ID;
                                        row_data.IERPP_ID = row_homo.IERPP_ID;
                                        row_data.IERPC_ID = row_homo.IERPC_ID;

                                        row_data.IERPH_CAT = row_homo.IERPH_CAT;
                                        row_data.IERPH_CAT_DES = row_homo.IERPH_CAT_DES;
                                        row_data.IERPH_TIPO_MONEDA = row_homo.IERPH_TIPO_MONEDA;

                                        if (Int32.Parse(row_homo.IERPH_ID) > 0) row_data.IERPH_HOMOLOGADO = "S";

                                        break;
                                    }

                                }
                            }

                            listView.Add(row_data);
                        }

                    }

                }


                if (IERPH_OPCION == "CONSULTA DE HOMOLOGACIONES")
                {
                    //// HOMOLOGACIONES
                    listView = GetHomo(IERPHM_ID, IERPHC_ID, IERPHP_ID, IERPHE_ID, IERPH_NOMBRE, IERPHM_CHK, IERPH_NIT, "ERP", "CONSULTA");

                    if (listView.Count > 0)
                        Title = listView[0].IERPH_CAT + " - " + listView[0].IERPH_CAT_DES;

                }


                List<IB_HOMOLOGACIONES> LstTitle = new List<IB_HOMOLOGACIONES>() {                 
                    new IB_HOMOLOGACIONES { IERPH_ERP_NOMBRE = IERPH_NIT.Trim() != "" ? Title + " [" + IERPH_NIT + "]" : Title }
                };


                if (listView.Count == 0 || activos == false)
                {
                    //listView.Add(new IB_HOMOLOGACIONES { IERPH_ERP_NOMBRE = "Busqueda '" + (IERPH_NIT == "" && IERPH_NOMBRE != "" ? IERPH_NOMBRE : IERPH_NIT) + "' sin resultados", IERPH_ID = "0", IERPH_ACTIVO = (IERPH_OPCION == "CONSULTA DE HOMOLOGACIONES" ? "C" : "S"), IERPH_STATUS = "3", IERPH_CODIGO = "" });

                }


                result.Add(listView);
                result.Add(LstTitle);

            }
            catch (Exception e)
            {
                //return null;
                throw e;
            }

            return result;
        }


        //////////////////////////////  NUEVO PROC ASOCIAR / DESASOCIAR
        [Route("api/Catalogos_Calls_API/SetHomHomologacionAsociar")]
        public List<string> GetHomCatalogos6Grid(string OPE1, string IERPH_STATUS, string IERPHM_ID, string IERPHM_DES, string IERPHC_ID, string IERPHP_ID, string IERPHE_ID, string CDID, string CDIDStr, string EXID, string EXIDStr, string IERPH_ID, string usuario, string IERPH_TIPO_MONEDA, string NIT)
        {
            List<string> result = new List<string>();

            string response = "";
            string nextid = "0";
            Boolean res;
            //CDID = "0";
            //EXID = "0";

            //if (user == null) user = "soporte7";
            if (CDIDStr == null) CDIDStr = "";
            if (EXIDStr == null) EXIDStr = "";

            if (IERPHC_ID == null) IERPHC_ID = "";
            if (IERPHP_ID == null) IERPHP_ID = "";
            if (IERPHE_ID == null) IERPHE_ID = "";
            if (NIT == null) NIT = "";

            try
            {
                string IERPS_SEC = "0";
                string sqlString = "";

                int id = int.Parse(IERPH_ID);

                if (id > 0)
                {
                    //// LEER SECUENCIA DEL REGISTRO IERPH_ID
                    sqlString = "SELECT COALESCE(IERPS_SEC,0) as IERPH_ID, IERPH_CODIGO, IERPH_ERP_CODIGO, IERPC_ID, IERPP_ID, IERPE_ID FROM INTBAW.IERP_HOMOLOGACIONES WHERE IERPH_ID = " + IERPH_ID;
                    IB_HOMOLOGACIONES data = cn.EjecutarRow<IB_HOMOLOGACIONES>(sqlString);
                    if (data.IERPH_ID != null) IERPS_SEC = data.IERPH_ID;
                    if (data.IERPH_CODIGO != null) CDID = data.IERPH_CODIGO;
                    if (data.IERPH_ERP_CODIGO != null) EXID = data.IERPH_ERP_CODIGO;

                    if (data.IERPC_ID != null) IERPHC_ID = data.IERPC_ID;
                    if (data.IERPP_ID != null) IERPHP_ID = data.IERPP_ID;
                    if (data.IERPE_ID != null) IERPHE_ID = data.IERPE_ID;

                    if (IERPS_SEC == "" || IERPS_SEC == null)
                        IERPS_SEC = "0";
                }
       
                if (IERPH_STATUS == "1" || IERPS_SEC == "0")
                    IERPS_SEC = "INTBAW.IERP_SECUENCIAS.nextval from dual";

                   

                /// ESTE CODIGO OBTIENE LAs CATEGORIAs 
                sqlString = @"SELECT b.IERPCAT_CATEGO as IERP_ERP_CODIGO, a.IERPCAT_CATEGO as IERP_CODIGO 

 FROM INTBAW.IERP_CATALOGOS c

LEFT JOIN INTBAW.IERP_CATEGORIAS a ON a.IERPCAT_ID = c.IERPC_CODIGO

LEFT JOIN INTBAW.IERP_CATEGORIAS b ON b.IERPCAT_ID = c.IERPC_ERP_CODIGO

WHERE c.IERPC_ID = '" + IERPHC_ID + @"' AND c.IERPM_ID = '" + IERPHM_ID + @"' ";

                IB_CATALOGOS categ = cn.EjecutarRow<IB_CATALOGOS>(sqlString);

                if (categ.IERP_ERP_CODIGO.Length > 2)
                {
                    categ.IERP_ERP_CODIGO = categ.IERP_ERP_CODIGO.Remove(0, 2);
                    categ.IERP_ERP_CODIGO = int.Parse(categ.IERP_ERP_CODIGO).ToString();  //categoria
                }

                if (categ.IERP_CODIGO != "08")  //SI NO ES MONEDA CS
                    IERPH_TIPO_MONEDA = "";

                if (IERPH_TIPO_MONEDA == null || IERPH_TIPO_MONEDA == "") IERPH_TIPO_MONEDA = "|";

                string IERPH_NIVEL_PRECIO = "";

                string[] columns = IERPH_TIPO_MONEDA.Split('|');

                IERPH_TIPO_MONEDA = columns[0];
                IERPH_NIVEL_PRECIO = columns[1];

                /*
                switch (IERPH_TIPO_MONEDA) {
                    case "L":
                            IERPH_NIVEL_PRECIO = "ND-LOCAL";
                            break;
                    case "D":
                            IERPH_NIVEL_PRECIO = "ND-DOLAR";
                            break;
                    default:
                            IERPH_NIVEL_PRECIO = "";
                            break;
                }
                */

                sqlString = @"INSERT INTO INTBAW.IERP_HOMOLOGACIONES (
  IERPM_ID          ,
  IERPC_ID          ,
  IERPP_ID          ,
  IERPE_ID          ,
  IERPH_CODIGO      ,
  IERPH_NOMBRE      ,
  IERPH_ERP_CODIGO  ,
  IERPH_ERP_NOMBRE  ,
  IERPH_STATUS      ,
  IERPH_USER        ,
  IERPH_TIPO_MONEDA ,
  IERPH_NIVEL_PRECIO,
  IERPS_SEC         
) ";

                if (IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                    sqlString += " SELECT ";

                else
                    sqlString += " VALUES (";

                sqlString += "'" + IERPHM_ID + "', '" + IERPHC_ID + "', '" + IERPHP_ID + "', '" + IERPHE_ID + "', '" + CDID.Trim() + "', '" + CDIDStr.Trim() + "', '" + EXID.Trim() + "', '" + EXIDStr.Trim() + "', '" + IERPH_STATUS + "', '" + usuario + "', '" + IERPH_TIPO_MONEDA + "', '" + IERPH_NIVEL_PRECIO + "', " + IERPS_SEC + "";

                if (IERPS_SEC == "INTBAW.IERP_SECUENCIAS.nextval from dual")
                    sqlString += "";

                else
                    sqlString += ")";

                // realiza el insert 1 ó 3
                res = cn.Execute(sqlString);

                if (IERPH_STATUS == "1" && res)
                {
                    sqlString = "select TO_CHAR(INTBAW.IERP_HOMOLOGACIONES_SEQ.currval) from dual";
                    nextid = cn.GetScalar(sqlString);
                }

                if (id > 0 && OPE1 == "DEL")
                {
                    // inactiva el actual despues de haber insertado el 3
                    sqlString = "UPDATE INTBAW.IERP_HOMOLOGACIONES SET IERPH_STATUS = '2' WHERE IERPH_ID = " + IERPH_ID + "";
                    res = cn.Execute(sqlString);
                }

                response = res ? "OK" : "ALGO SALIO MAL AL INTENTAR " + (IERPH_STATUS == "1" ? "" : "DES") + "ASOCIAR";


                try
                {
                    sqlString = "SELECT  IERPP_CODIGO data1, IERPP_ERP_CODIGO data2 FROM INTBAW.IERP_PAISES WHERE IERPP_ID = " + IERPHP_ID;
                    IB_STANDAR data = cn.EjecutarRow<IB_STANDAR>(sqlString);


                    ///// ACTUALIZA EL CAMPO HOMOLOGADO EN CARGO SYSTEM
                    //IERP_QUERYS data1 = Get_IERP_QUERYS(IERPHC_ID, "", IERPHE_ID); // "" significa diferente a ERP

                    //if (data1.IERPQ_SQL_UPDATE.Trim() != "")
                    //{
                    if (IERPH_TIPO_MONEDA == "")
                            IERPH_TIPO_MONEDA = NIT;

                        UpdateHomologacionesOperativo(CDID.Trim(), CDIDStr.Trim(), EXID.Trim(), EXIDStr.Trim(), IERPH_STATUS, IERPHM_ID, IERPHM_DES, IERPHE_ID, categ.IERP_CODIGO, categ.IERP_ERP_CODIGO, IERPH_TIPO_MONEDA, data.data1, data.data2, IERPHM_ID);

                    //}

                }
                catch (Exception ex)
                {
                    response += " -  CATALOGOS : " + ex.Message;
                }

            }
            catch (Exception ex)
            {

                response += " -  ERROR : " + ex.Message;

            }

            result.Add(response);
            result.Add(nextid);
            result.Add(CDID);
            result.Add(EXID);

            return result;
        }





    }
}