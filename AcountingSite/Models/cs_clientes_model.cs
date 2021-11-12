using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcountingSite.Models
{
    public class cs_clientes_model
    {
        public string id_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public string codigo_tributario { get; set; }
        public string id_pais { get; set; }
        public string categoria { get; set; }
        public string HID { get; set; }
        public string CSID { get; set; } 
    }
}