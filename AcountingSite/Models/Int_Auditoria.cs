using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcountingSite.Models
{
    public class Int_Auditoria
    {
        public string EVENTO { get; set; }
        public string FECHA { get; set; }
        public string TABLA { get; set; }
        public string USUARIO { get; set; }
        public string VALORNUEVO { get; set; }
        public string VALORVIEJO { get; set; }

      
    }
}