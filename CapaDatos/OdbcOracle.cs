using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class OdbcOracle
    {
        private static readonly string connString = "Dsn=CS;uid=TRANSITINT;Pwd=Tr3nS1T1nt@2021;dbq=CERTDALI;dba=W;apa=T;exc=F;fen=T;qto=T;frc=10;fdl=10;lob=T;rst=T;btd=F;bnf=F;bam=IfAllSuccessful;num=NLS;dpm=F;mts=T;mdi=F;csr=F;fwc=F;fbs=64000;tlo=O;mld=0;oda=F";

        public  OdbcOracle()
        {
        }
        static OdbcOracle()
        {
        }
        public static DataTable EjecutarConsulta(string sqlString)
        {
            DataTable dtResult = new DataTable();
            try
            {
                OdbcConnection conn = new OdbcConnection(connString);
               
                conn.Open();

                OdbcDataAdapter adapter = new OdbcDataAdapter(sqlString, conn);

                DataSet dataSet = new DataSet();
                
                adapter.Fill(dataSet, "result_data");
                
                dtResult = dataSet.Tables["result_data"];

                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return dtResult;
        }

        
    }
}
