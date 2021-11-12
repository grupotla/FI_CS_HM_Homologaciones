using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Utils
    {

        /*public static string ip_server = "12:12:12";

        public static string GetServerIP()
        {
            return ip_server;
        }*/


        public static string BuildOptions(DataTable data, string html)
        {
            string options = "", codigo = "";
            
            try
            {
                if (html != "")
                    options = "<select " + html + ">";
                    
                options += "<option value = ''>Seleccione</option>";

                foreach (DataRow row in data.Rows)
                {
                    var t = row.ItemArray;

                    codigo = t[0].ToString();

                    var dat = codigo.Split('|');

                    if (dat.Count() > 1)
                        codigo = dat[1];
                    
                    options += "<option value='" + t[0] + "' title='" + codigo + "'>" + t[1] + "</option>";
                }

                if (html != "")
                    options += "</select>";
            }
            catch (Exception e)
            {
                throw e;
            }
            return options;
        }



        public static List<TEntity> get_list_struct2<TEntity>(DataTable ViewReporte) where TEntity : class, new()
        {
            List<TEntity> rows = new List<TEntity>();
            try
            {

                foreach (DataRow line in ViewReporte.Rows)
                {
                    TEntity data = ReflectType2<TEntity>(line);
                    rows.Add(data);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            return rows;
        }

        public static TEntity ReflectType2<TEntity>(DataRow dr) where TEntity : class, new()
        {
            TEntity instanceToPopulate = new TEntity();

            System.Reflection.PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties
            (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //for each public property on the original
            foreach (System.Reflection.PropertyInfo pi in propertyInfos)
            {
                var n = pi.Name;
                //var t = pi.GetType();

                try
                {
                    object dbValue = dr[n];

                    if (dbValue != null)
                    {
                        pi.SetValue(instanceToPopulate, Convert.ChangeType
                        (dbValue, pi.PropertyType, System.Globalization.CultureInfo.InvariantCulture), null);
                    }
                }
                catch (Exception e)
                {
                    //throw e; aca no aplica, debe continuar
                }

            }

            return instanceToPopulate;
        }

        public static TEntity ReflectType<TEntity>(DbDataReader dr) where TEntity : class, new()
        {
            TEntity instanceToPopulate = new TEntity();

            System.Reflection.PropertyInfo[] propertyInfos = typeof(TEntity).GetProperties
            (System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //for each public property on the original
            foreach (System.Reflection.PropertyInfo pi in propertyInfos)
            {
                var n = pi.Name;
                //var t = pi.GetType();

                try
                {
                    object dbValue = dr[n];

                    if (dbValue != null)
                    {
                        pi.SetValue(instanceToPopulate, Convert.ChangeType
                        (dbValue, pi.PropertyType, System.Globalization.CultureInfo.InvariantCulture), null);
                    }
                }
                catch (Exception e)
                {
                    //throw e; aca no aplica, debe continuar
                }

            }

            return instanceToPopulate;
        }


        public static string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); // `Dns.Resolve()` method is deprecated.
            IPAddress ipAddress = ipHostInfo.AddressList[7];

            return ipAddress.ToString();
        }



    }
}
