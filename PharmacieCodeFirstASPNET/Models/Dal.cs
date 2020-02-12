﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Dal : IDal
    {
        BddContext Bdd = new BddContext();

        public void AjouterAchat(int idCommande, int idClient, int idStock)
        {
            Achat achat = new Achat()
            {
                Stock = Bdd.Stocks.FirstOrDefault(s => s.Id == idStock)

            };
        }

        public void AjouterClient(Client client)
        {
             
            Client client1 = new Client();
            client1.Nom = client.Nom;
            client1.Prenom = client.Prenom;
            client1.Telephone = client.Telephone;
            client1.Password = EncodeMotDePasse(client.Password);
            Bdd.Clients.Add(client1);
            Bdd.SaveChanges();
        }

        public void AjouterProduit(Produit produit)
        {
            //Vérifier que le produit existe déja

            if (!ProduitExisteDeja(produit))
            {


                Produit produit1 = new Produit();
                produit1.NomProduit = produit.NomProduit;
                produit1.Prix_Unite = produit.Prix_Unite;
                produit1.Quantite = produit.Quantite;

                Bdd.Produits.Add(produit1);
                //Lorsque le produit est nouveau, on cree directement un nouveau stock
                AjouterStock(produit1);
                Bdd.SaveChanges();

            }
            else
            {
                Produit produit2 = new Produit();
                produit2.NomProduit = produit.NomProduit;
                produit2.Prix_Unite = produit.Prix_Unite;
                produit2.Quantite = produit.Quantite;
                //On recupere le produit dans les stocks ayant le meme nom
                Stock stoc = Bdd.Stocks.FirstOrDefault(s => s.NomProduit_stock == produit2.NomProduit);
                produit2.Id = stoc.Id;
                Bdd.Produits.Add(produit2);
                UpdateStock(produit2); // Comme c'est un nouveau produit on met à jour le stock
                Bdd.SaveChanges();

            }
        }

        public void AjouterStock(Produit produit)
        {
            Stock newStock = new Stock();
            newStock.NomProduit_stock = produit.NomProduit;   //recuperation du nom du produit
            newStock.Quantite_Produit = produit.Quantite;

            Bdd.Stocks.Add(newStock);
            Bdd.SaveChanges();
            //recuperer id du nouveau stock et mettre comme foreing key dans produit
            Stock stoc = Bdd.Stocks.FirstOrDefault(s => s.Id == newStock.Id);
            produit.Stock.Id = stoc.Id;
            stoc.NomProduit_stock = produit.NomProduit;
            Bdd.SaveChanges();
        }

        //Mettre à jour le stock de la base de donnée
        private void UpdateStock(Produit produit)
        {
            Stock stockUpdate = Bdd.Stocks.FirstOrDefault(s => s.NomProduit_stock == produit.NomProduit);
            //recuperer la somme de tout de tout les produits du meme nom
            int NewStock = Bdd.Produits.Where(p => p.NomProduit == stockUpdate.NomProduit_stock).Sum(n => n.Quantite);
            // mettre à jour le stock

            stockUpdate.Quantite_Produit = NewStock;
            Bdd.SaveChanges();

        }
        public Client Authentifier(Client client)
        {
            return Bdd.Clients.FirstOrDefault(c => c.Nom == client.Nom && c.Password == EncodeMotDePasse(client.Password));
        }

     
        public void Dispose()
        {
            Bdd.Dispose();
        }

        public void ModifierClient(Client client)
        {
            Client client1 = Bdd.Clients.FirstOrDefault(c => c.Id == client.Id);
            client1.Nom = client.Nom;
            client1.Prenom = client.Prenom;
            client1.Telephone = client.Telephone;
            client1.Password = client.Password;
            Bdd.SaveChanges();
        }

        public void ModifierProduit(Produit produit)
        {
            Produit client1 = Bdd.Produits.FirstOrDefault(c => c.Id == produit.Id);
            Produit produit1 = new Produit();
            produit1.NomProduit = produit.NomProduit;
            produit1.Prix_Unite = produit.Prix_Unite;
            produit1.Quantite = produit.Quantite;
            Bdd.SaveChanges();
        }

        public Client ObtenirClient(int id)
        {
            return Bdd.Clients.FirstOrDefault(c => c.Id == id);
        }

        public Client ObtenirClient(string idstr)
        {
            int idEntier;
            if (int.TryParse(idstr, out idEntier))
                return ObtenirClient(idEntier);
            return null;
        }

        public List<Client> ObtenirTousLesClients()
        {
           return Bdd.Clients.ToList();
        }

        public List<Produit> ObtenirTousLesProduits()
        {
            return Bdd.Produits.ToList();
        }

        public List<Stock> ObtenirTousLesStock()
        {
            return Bdd.Stocks.ToList();
        }

        public int PasserCommande()
        {
            Commande commande = new Commande();
            commande.heure_commande = DateTime.Now;
            Bdd.Commandes.Add(commande);
            Bdd.SaveChanges();

            return commande.Id;
        }

        public bool ProduitExisteDeja(Produit produit)
        {
            return Bdd.Produits.Any(p => string.Compare(p.NomProduit,produit.NomProduit,StringComparison.CurrentCultureIgnoreCase) ==0);
        }

        private string EncodeMotDePasse(string motDePass)
        {
            string mdp = "Pharmacie" + motDePass + "ASP";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes((mdp))));
        }

        public void supprimerProduit(Produit produit)
        {
            Produit proDelete = Bdd.Produits.FirstOrDefault(p => p.Id == produit.Id);
            Bdd.Produits.Remove(proDelete);
            Bdd.SaveChanges();
        }
    }
}