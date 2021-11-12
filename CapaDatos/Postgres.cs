using Npgsql;
using System;
using System.Data;


namespace CapaDatos
{
    public class Postgres
    {

        static Postgres()
        {
        }

        public Postgres()
        {
        }


        public static string GetScalar(string connectionString, string sqlString)
        {
            string valor = "";

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);

                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlString;

                valor = (string) comm.ExecuteScalar();

            }
            catch (Exception ex)
            {
                valor = ex.Message;
            }

            valor += " | " + connectionString;

            return valor;
        }

        public static int EjecutaQuery(string connectionString, string sqlString)
        {
            int result = 0;

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);

                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlString;

                result = comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }


        public static DataTable EjecutarConsulta(string connectionString, string sqlString)
        {
            DataTable dtResult = new DataTable();

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString); 

                conn.Open();

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(sqlString, conn);

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


        public static TEntity EjecutarRow<TEntity>(string connectionString, string sqlString) where TEntity : class, new()
        {
            TEntity data = new TEntity();

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);

                conn.Open();
                NpgsqlCommand comm = new NpgsqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sqlString;


                System.Data.Common.DbDataReader dtResult = comm.ExecuteReader();

                while (dtResult.Read())
                {
                    data = CapaDatos.Utils.ReflectType<TEntity>(dtResult);
                }

                conn.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return data;
        }




        public static void CloseObj(NpgsqlDataReader rd, NpgsqlCommand comm, NpgsqlConnection conn)
        {
            try
            {
                rd.Close();
                comm.Dispose();
                conn.Close();
                //conn.ClearPool();
                conn.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
