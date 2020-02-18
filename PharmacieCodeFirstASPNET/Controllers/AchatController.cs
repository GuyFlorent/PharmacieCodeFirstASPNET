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
            //ProduitAchatViewModel produitViewModel = new ProduitAchatViewModel() {
            //    ListeDesProduit = dal.ObtenirTousLesStock().Select(p =>
            //    new ProduitSelectViewModel { id = p.Id, Image = p.image_stok, Quantite_Stock_Restante = p.Quantite_Produit, NomEtPrix_Unité = string.Format("{0} ({1} €)", p.NomProduit_stock, p.Prix_unite_stock
            //    ) }).ToList()
        //};
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            if (!ModelState.IsValid)
                return View();
            Client client = dal.ObtenirClient(HttpContext.User.Identity.Name);
            if (client == null)
                return new HttpUnauthorizedResult();
            List<ProduitPanierViewModel> panier = (List<ProduitPanierViewModel>)Session["achat"];
            int id = dal.PasserCommande();
            //foreach (ProduitSelectViewModel produitSelectViewModel in viewModel.ListeDesProduit.Where(p => p.EstSelectionne))
            //{
            //    dal.AjouterAchat(id, client.Id, produitSelectViewModel.id, 5);
            //};

            foreach (ProduitPanierViewModel produitPanier in panier)
            {
                dal.AjouterAchat(id, client.Id, produitPanier.stock.Id, produitPanier.Quantite);
            };

            return RedirectToAction("HistoriqueCommandeClient");
        }


        public ActionResult AjoutPanier(int id)
        {
            if(Session["achat"] == null)
            {
                List<ProduitPanierViewModel> panier = new List<ProduitPanierViewModel>();
                panier.Add(new ProduitPanierViewModel() { stock = dal.ObtenirTousLesStock().FirstOrDefault(s => s.Id == id), Quantite = 1 });
                Session["achat"] = panier;
            }
            else
            {
                List<ProduitPanierViewModel> panier = (List<ProduitPanierViewModel>)Session["achat"];
                int index = isExist(id);
                if(index != -1)
                {
                    panier[index].Quantite++;
                }
                else
                {
                    panier.Add(new ProduitPanierViewModel() { stock = dal.ObtenirTousLesStock().FirstOrDefault(s => s.Id == id), Quantite = 1 });
                }
                Session["achat"] = panier;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SupprimeProduitPanier(int id)
        {
            List<ProduitPanierViewModel> panier = (List<ProduitPanierViewModel>)Session["achat"];
            int index = isExist(id);
            panier.RemoveAt(index);
            Session["achat"] = panier;
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<ProduitPanierViewModel> panier = (List<ProduitPanierViewModel>)Session["achat"];
            for (int i = 0; i < panier.Count; i++)
                if (panier[i].stock.Id.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult HistoriqueCommandeClient()
        {
            Client client = dal.ObtenirClient(HttpContext.User.Identity.Name);
            List<Commande> listeCommande = dal.ObtenirLaListeDesCommandes().Where(c => c.Client == client).ToList();
            foreach(Commande com in listeCommande)
            {
                ViewBag.HeureCommande = com.heure_commande;
                ViewBag.ListeAchats = com.Achats;
                foreach(var acha in com.Achats)
                {
                    List<Stock> hisostock = dal.ObtenirTousLesStock().Where(s => s.Id ==acha.Stock.Id).ToList();
                    ViewBag.NomProduit = hisostock;
                }
            }
            return View();
        }
    }
}