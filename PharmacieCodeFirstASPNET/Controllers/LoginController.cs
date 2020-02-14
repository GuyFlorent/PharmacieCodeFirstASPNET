using PharmacieCodeFirstASPNET.Models;
using PharmacieCodeFirstASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class LoginController : Controller
    {
        private IDal dal;

        public LoginController() : this(new Dal())
        {

        }

        public LoginController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Login
        public ActionResult Index()
        {
            ClientViewModel vm = new ClientViewModel() { Authentfie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                vm.client = dal.ObtenirClient(HttpContext.User.Identity.Name);
            }
            return View(vm);
        }

        [HttpPost]

        public ActionResult Index(ClientViewModel clientView, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Client client = dal.Authentifier(clientView.client);
                if(client!= null)
                {
                    FormsAuthentication.SetAuthCookie(client.Id.ToString(), false);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    return Redirect("/");
                }
                ModelState.AddModelError("Client.Email", "Email et/ou mot de passe incorrect(s)");
            }
            return View(clientView);
        }

        public ActionResult CreerCompte()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreerCompte(Client client)
        {
            if (ModelState.IsValid)
            {
                int id = dal.AjouterClientRenvoiId(client);
                FormsAuthentication.SetAuthCookie(id.ToString(), false);
                return Redirect("/");
            }
            return View(client);
        }

        //public JsonResult verifEmail(Client client)
        //{
        //    bool result = !dal.EmailClienExiste(client);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}