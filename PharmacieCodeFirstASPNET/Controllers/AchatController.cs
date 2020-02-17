using PharmacieCodeFirstASPNET.Models;
using PharmacieCodeFirstASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class AchatController : Controller
    {
        private IDal dal;

        public AchatController() : this(new Dal())
        {

        }

        public AchatController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Achat
        public ActionResult Index()
        {
            ProduitAchatViewModel produitViewModel = new ProduitAchatViewModel() {
                ListeDesProduit = dal.ObtenirTousLesStock().Select(p =>
                new ProduitSelectViewModel { id = p.Id, Image = p.image_stok, Quantite_Stock_Restante = p.Quantite_Produit, NomEtPrix_Unité = string.Format("{0} ({1} €)", p.NomProduit_stock, p.Prix_unite_stock
                ) }).ToList()
        };
            return View(produitViewModel);
        }

        [HttpPost]
        public ActionResult Index(ProduitAchatViewModel viewModel, int id)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
            Client client = dal.ObtenirClient(HttpContext.User.Identity.Name);
            if (client == null)
                return new HttpUnauthorizedResult();

            foreach(ProduitSelectViewModel produitSelectViewModel in viewModel.ListeDesProduit.Where(p => p.EstSelectionne))
            {
                dal.AjouterAchat(id, client.Id, produitSelectViewModel.id, 5);
            };

            return RedirectToAction("Index", "Home");
        }
    }
}