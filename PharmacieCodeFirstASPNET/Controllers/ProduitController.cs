using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class ProduitController : Controller
    {
        private IDal dal;

        public ProduitController(): this(new Dal())
        {

        }

        public ProduitController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Produit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjoutProduit()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult AjoutProduit(Produit produit)
        {
            if (dal.ProduitExisteDeja(produit))
            {

                ModelState.AddModelError("NomProduit", "Ce Nom de produit existe déja! Le stock sera incrémeté");
                return View(produit);
            }
            if (!ModelState.IsValid)
                return View(produit);
           dal.AjouterProduit(produit);
            return View(produit);
        }

        public JsonResult VerifProduitExist(Produit produit)
        {
            bool result = !dal.ProduitExisteDeja(produit);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HistoriqueProduit()
        {
            List<Produit> liste = dal.ObtenirTousLesProduits();
            return PartialView(liste);
        }

        public ActionResult ModifierProduit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = dal.ObtenirTousLesProduits().FirstOrDefault(p => p.Id == id);

            if (produit == null)
                return HttpNotFound();
            return View(produit);
        }

        [HttpPost]

        public ActionResult ModifierProduit(Produit produit)
        {
            dal.ModifierProduit(produit);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailsProduit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = dal.ObtenirTousLesProduits().FirstOrDefault(p => p.Id == id);

            if (produit == null)
                return HttpNotFound();
            return View(produit);
        }

        public ActionResult SupprimerProduit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = dal.ObtenirTousLesProduits().FirstOrDefault(p => p.Id == id);

            if (produit == null)
                return HttpNotFound();
            return View(produit);
        }

        [HttpPost]

        public ActionResult SupprimerProduit(Produit produit)
        {
            dal.supprimerProduit(produit);
            return RedirectToAction("AjoutProduit");
        }
    }
}