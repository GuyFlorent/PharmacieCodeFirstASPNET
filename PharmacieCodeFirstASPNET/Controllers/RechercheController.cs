using PharmacieCodeFirstASPNET.Models;
using PharmacieCodeFirstASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class RechercheController : Controller
    {
        private IDal dal;

        public RechercheController() : this(new Dal())
        {

        }

        public RechercheController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Recherche
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recherche(RechercheProduitViewModel rechercheProduitViewModel)
        {
            
            return View(rechercheProduitViewModel);
        }

        public ActionResult ResultatRecherche(RechercheProduitViewModel rechercheProduitViewModel)
        {
            if (!string.IsNullOrWhiteSpace(rechercheProduitViewModel.Recherche))
            {

                rechercheProduitViewModel.ListeProduits = dal.ObtenirTousLesStock().Where(r => r.NomProduit_stock.ToLower().Contains(rechercheProduitViewModel.Recherche.ToLower())).ToList();
                Thread.Sleep(1000);
            }
            else
            {
                rechercheProduitViewModel.ListeProduits = new List<Stock>();
            }
            return PartialView(rechercheProduitViewModel);
        }
    }
}