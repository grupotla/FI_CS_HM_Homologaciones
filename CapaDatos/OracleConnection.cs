using System;
using System.Data;
using System.Data.Common;
using System.Configuration;

namespace CapaDatos
{
    public class ConexionDeDatos
    {

        private static readonly DbProviderFactory providerFactory;
        private readonly IDbConnection connection;


        static ConexionDeDatos()
        {           
            providerFactory = DbProviderFactories.GetFactory("System.Data.OracleClient");
        }
        /// <summary>
        ///CONEXION DE LA BASE DE DATOS DE PRUEBAS 
        /// </summary>
        public ConexionDeDatos()
        {

            //string ip = Utils.GetIPAddress();

            string connName = "pruebas";

            //connName = "produccion";
            //if (ip == Utils.GetServerIP()) connName = "produccion";

            string connectionString = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
        }



        public DataTable Query(string sqlString, string BD)
        {
            try
            {
                switch (BD)
                {
                   
                    case "BAW":
                        connection.Close();
                        connection.Open();
                        DataTable devuelve = EjecutarConsulta(connection, sqlString);
                        if (connection.State == ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return devuelve;

                }
                return null;
            }
            catch (Exception e)
            {

                throw e;

                //DataTable dterror = new DataTable();

                //connection.Close();

                //return dterror;
            }
        }


        public static int EjecutaQuery(string connectionString, string sqlString)
        {
            int result = 0;

            try
            {
                IDbConnection connection2;
                connection2 = providerFactory.CreateConnection();
                connection2.ConnectionString = connectionString;

                DbCommand command = providerFactory.CreateCommand();
                command.Connection = (DbConnection)connection2;              
                result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }



        public DataTable EjecutarConsulta2(string connectionString, string sqlString)
        {
            DataTable dtResult = new DataTable();

            IDbConnection connection2;
            connection2 = providerFactory.CreateConnection();
            connection2.ConnectionString = connectionString;

            dtResult = EjecutarConsulta(connection2, sqlString);

            return dtResult;
        }

        private DataTable EjecutarConsulta(IDbConnection conn, string sqlString)
        {  
                        
            DataTable dtResult = new DataTable();
            
            try
            {
        
            DbDataAdapter adapter = providerFactory.CreateDataAdapter();
            DbCommand command = providerFactory.CreateCommand();
            command.CommandText = sqlString;
            command.Connection = (DbConnection)conn;
            adapter.SelectCommand = command;
          
            adapter.Fill(dtResult);
            conn.Close();
               

            }
            catch (Exception ex )
            {

               

            }

            return dtResult;
        }

        public TEntity EjecutarRow<TEntity>(string sqlString) where TEntity : class, new()
        {
            TEntity data = new TEntity();

            try
            {
                connection.Close();
                connection.Open();

                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = sqlString;
                command.Connection = (DbConnection)connection;

                DbDataReader dtResult = command.ExecuteReader();

                
                while (dtResult.Read())
                {
                    data = CapaDatos.Utils.ReflectType<TEntity>(dtResult);
                }

                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return data;
        }

        public string GetScalar(string sqlString)
        {
            string valor = "";
       
            try
            {
                connection.Close();
                connection.Open();

                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = sqlString;
                command.Connection = (DbConnection)connection;

                valor = (string) command.ExecuteScalar();

                /*
                DbDataReader dtResult = command.ExecuteReader();
                if (dtResult.Read())
                {
                    valor = dtResult.GetInt32(0);
                }
                */

                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return valor;
        }

        public bool Execute(string sqlString)
        {
            try
            {

                connection.Close();
                connection.Open();
                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = sqlString;
                command.Connection = (DbConnection)connection;


                int resultado = command.ExecuteNonQuery();
                connection.Close();

                if (resultado > 0)
                {
                    return true;
                }
            }

            finally
            {
                connection.Close();
            }
            return false;
        }


        public bool Execute(string sqlString, string BD)
        {
            int Resultado = -1;
            try
            {
                switch (BD)
                {
                    case "BAW":

                        Resultado = EjecutarCommando(connection, sqlString);
                        break;


                }

                if (Resultado > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;

                //if (connection.State == ConnectionState.Open)
                //{
                //    connection.Close();
                //}

                //return false;
            }
        }

        private int EjecutarCommando(IDbConnection connection, string sqlString)
        {
            try
            {
                connection.Close();
                connection.Open();
                DbCommand command = providerFactory.CreateCommand();
                command.CommandText = sqlString;
                command.Connection = (DbConnection)connection;

                return command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    
        public bool Insert(string sqlString)
        {
            connection.Close();
            connection.Open();
            DbCommand command = providerFactory.CreateCommand();
            command.CommandText = sqlString;
            command.Connection = (DbConnection)connection;
            connection.Close();
            return command.ExecuteNonQuery() > 0;
        }







    }
}

