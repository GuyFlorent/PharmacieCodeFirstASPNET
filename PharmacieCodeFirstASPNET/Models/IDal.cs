using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacieCodeFirstASPNET.Models
{
    public interface IDal : IDisposable
    {
        //Méthode concernant le client
        List<Client> ObtenirTousLesClients();
        void AjouterClient(Client client);
        int AjouterClientRenvoiId(string nom, string prenom, string email, string confirmEmail, string date_Naissance, string password, string telephone);
        void ModifierClient(Client client);
        Client Authentifier(Client client);
        Client ObtenirClient(int id);
        Client ObtenirClient(string idstr);
        bool EmailClienExiste(string Email);


        //Méthode concernant les produits
        List<Produit> ObtenirTousLesProduits();
        void AjouterProduit(Produit produit);
        void ModifierProduit(Produit produit);
        bool ProduitExisteDeja(Produit produit);
        void supprimerProduit(Produit produit);



        //Méthode concernant les stocks
        List<Stock> ObtenirTousLesStock();
        void AjouterStock(Produit produit);
        int AjouterStockId(Produit produit);


        //Méthode pour la Commande

        int PasserCommande();

        //Méthode pour l'Achat

        void AjouterAchat(int idCommande, int idClient, int idStock, int quantite);
        Achat achatParIdStoc(int idstock);
    }
}
