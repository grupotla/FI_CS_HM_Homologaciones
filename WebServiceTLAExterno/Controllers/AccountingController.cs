using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using static WebServiceTLA.Class.SaldosDiarios;

namespace WebServiceTLAExterno.Controllers
{
    public class AccountingController : ApiController
    {
        private readonly WebServiceTLA.Accounting acciones = new WebServiceTLA.Accounting();

        [Route("api/Accounting/SaldosDiarios")]
        [HttpPost]
        public async Task<IEnumerable<string>> SaldosDiarios()
        {
            string json = await Request.Content.ReadAsStringAsync();
            Respuesta respuesta = new Respuesta();
            string mensaje = "";
            try
            {
                string data = "";
                bool estado = acciones.IntSaldosDiarios(json, ref data);
                mensaje = data.Replace(@"\""", "");

                respuesta.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "ERROR: " + ex.Message;
            }
            return new[] { mensaje };

        }

        [Route("api/Accounting/cxc")]
        [HttpPost]
        public async Task<IEnumerable<string>> cxc()
        {
            string json = await Request.Content.ReadAsStringAsync();
            Respuesta respuesta = new Respuesta();
            string mensaje = "";
            try
            {
                string data = "";
                bool estado = acciones.IntCXC(json, ref data);
                mensaje = data.Replace(@"\""", "");

                respuesta.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "ERROR: " + ex.Message;
            }
            return new[] { mensaje };

        }
        private readonly Accounting.AccountingSoapClient accounting = new Accounting.AccountingSoapClient();


        [Route("api/Accounting/INT_INGRESOS_CXC")]
        [HttpPost]
        public async Task<IEnumerable<string>> INT_INGRESOS_CXC()
        {
            string json = await Request.Content.ReadAsStringAsync();
            Respuesta respuesta = new Respuesta();
            string mensaje = "";
            try
            {
                string data = "";
                bool estado = acciones.INT_INGRESOS_CXC(json, ref data);
                mensaje = data.Replace(@"\""", "");

                respuesta.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                respuesta.Estado = false;
                respuesta.Mensaje = "ERROR: " + ex.Message;
            }
            return new[] { mensaje };

        }



    }
}
