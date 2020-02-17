using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    [Authorize]
    public class CommandeController : Controller
    {
        private IDal dal;

        public CommandeController() : this(new Dal())
        {

        }

        public CommandeController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Commande
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            int idCommande = dal.PasserCommande();

            return RedirectToAction("Index","Achat",new { id = idCommande});
        }
    }
}