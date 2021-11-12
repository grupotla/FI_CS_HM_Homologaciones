using System.Collections;
using System.Web.Mvc;

namespace AcountingSite.Controllers
{
    public class HomeController : Controller
    {


        //Array para obtener los roles 
        private readonly ArrayList roles = new ArrayList();
        private readonly string userLog;
        private string[] Datos;
        private static readonly string ClienteEstatico;
        private static readonly string CediEstatico;
        private static string user1;
        private static string user_id = "";
        private static bool bandera;
        private static string cliente = "";
        private static string cedi = "";
        private static string modulo;
        private static string Etapas = "";
        private readonly string userStatic = "";
        private readonly string user_idStatic = "";
        private static bool flagOwner = false;
        private static bool flagCedi = false;
        private static bool flagEtapa = false;
        public ActionResult Index()
        {
            ViewBag.Title = "Acconting-BAW";
            //string modulo = Request.["module_p"];   
            if (Request.Form.Count > 0)
            {
                flagOwner = false;
                flagCedi = false;
                flagEtapa = false;
                cliente = "";
                cedi = "";
                user1 = "";
                user_id = "";
                modulo = "";
                Etapas = "";
            }

            ViewBag.Title = "Home Page";

            bandera = true;
            //static string cliente = "";
            //static string cedi = "";
            string user = "";

            System.Collections.ArrayList clientes = new System.Collections.ArrayList();
            ArrayList cedis = new ArrayList();
            ArrayList Etapa = new ArrayList();

            // Si la variable sesion esta vacia o que sea otro request.
            string username = "";
            string key = "";
            bool firstClient = true;
            bool firstCedi = true;
            bool firstEtapa = true;

            //Inicia en 1 porque siempre el primer valor que viene en el form es el USERNAME
            for (int i = 0; i < Request.Form.Count; i++)
            {
                key = Request.Form.AllKeys[i];
                //Evaluo si la iteracion es un codigo de cliente.
                if (key.Contains("Codigo Cliente"))
                {
                    if (flagOwner == false)
                    {

                        clientes.Add(Request.Form[i]).ToString();
                        if (firstClient)
                        {
                            cliente += Request.Form[i];
                            firstClient = false;
                        }
                        else
                        {
                            cliente += "," + Request.Form[i];
                        }

                        flagOwner = true;

                    }
                }

                //Evaluo si es un codigo de cedi.
                else if (Request.Form.AllKeys[i].Contains("CEDI"))
                {
                    if (flagCedi == false)
                    {

                        cedis.Add(Request.Form[i]);
                        if (firstCedi)
                        {
                            cedi += Request.Form[i];
                            firstCedi = false;
                        }
                        else
                        {
                            cedi += "," + Request.Form[i];
                        }
                        flagCedi = true;
                    }
                }
                //Evaluo si es etapa
                else if (Request.Form.AllKeys[i].Contains("ETAPA"))
                {
                    if (flagEtapa == false)
                    {

                        Etapa.Add(Request.Form[i]);
                        if (firstEtapa)
                        {
                            Etapas += Request.Form[i];
                            flagEtapa = false;
                        }
                        else
                        {
                            Etapas += "," + Request.Form[i];
                        }
                        flagEtapa = true;
                    }
                }
                else if (Request.Form.AllKeys[i].Contains("username"))
                {
                    user = Request.Form[i];
                    user_id = user;
                    ViewBag.user_id = Request.Form[i];


                }
                if (Request.Form.AllKeys[i].Contains("email"))
                {

                    username = Request.Form[i];
                    Datos = username.ToString().Split('@');
                    //ViewBag.user = Datos[0].ToString().ToUpper();
                    user1 = Datos[0].ToString().ToUpper();
                    ViewBag.user = user1;
                    ViewBag.email = Datos[0].ToString().ToUpper();
                    // Session["username"] = Datos[0].ToString().ToUpper();

                }
                if (Request.Form.AllKeys[i].Contains("modulo"))
                {
                    if (modulo != "")
                    {
                        ViewBag.modulo = modulo;
                    }
                    else
                    {
                        modulo = Request.Form[i];
                        ViewBag.modulo = modulo;
                    }

                }

                if (Request.Form.AllKeys[i].Contains("Rol_User"))
                {

                    ViewBag.rol_user = Request.Form[i];

                }

            }

            if (user_id != "")
            {
                ViewBag.cliente = cliente;
                ViewBag.Cedi = cedi;
                ViewBag.user = user1;
                ViewBag.user_id = user_id;
                ViewBag.modulo = modulo;

            }

            if (user == "" && username != "")
            {
                return RedirectToAction("Error", "Home");

            }

            return View();
        }


    }







}
