using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class HomeController : Controller
    {
        private IDal dal;

        public HomeController(): this(new Dal())
        {

        }

        public HomeController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        public ActionResult Index()
        {
            List<Produit> listeProduits = dal.ObtenirTousLesProduits();
            
            return View(listeProduits);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}