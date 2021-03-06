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

        public void AjouterAchat(int idCommande, int idClient, int idStock, int quantite)
        {
            Stock stockById = Bdd.Stocks.FirstOrDefault(s => s.Id == idStock);
            double prix = Bdd.Produits.FirstOrDefault(p => p.NomProduit == stockById.NomProduit_stock).Prix_Unite;
            if (stockById.Quantite_Produit >= quantite)
            {
                
                Achat achat = new Achat()
                {
                    Stock = stockById,
                    Quantite_Totale = quantite,
                    Prix_Total = prix * quantite
                };
                Commande commande = Bdd.Commandes.FirstOrDefault(c => c.Id == idCommande);
                commande.Client = Bdd.Clients.FirstOrDefault(c => c.Id == idClient);
                if (commande.Achats == null)
                    commande.Achats = new List<Achat>();
                commande.Achats.Add(achat);
                Bdd.SaveChanges();
                DecrementeQuantiteStock(idStock, quantite);
            }
        }

        private void DecrementeQuantiteStock(int id, int quantiteAretirer)
        {
            Stock stock = Bdd.Stocks.FirstOrDefault(s => s.Id == id);
            stock.Quantite_Produit -= quantiteAretirer;
            Bdd.SaveChanges();
        }

        public void AjouterClient(Client client)
        {
             
            Client client1 = new Client();
            client1.Nom = client.Nom;
            client1.Prenom = client.Prenom;
            client1.Telephone = client.Telephone;
            client1.Email = client.Email;
            client1.ConfirmEmail = client.ConfirmEmail;
            client1.Date_Naissance = client.Date_Naissance;
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
                produit1.Date_heure_ajout = produit.Date_heure_ajout;
                produit1.Image = produit.Image;

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
                produit2.Date_heure_ajout = produit.Date_heure_ajout;
                produit2.Image = produit.Image;
                //On recupere le produit dans les stocks ayant le meme nom
                Stock stoc = Bdd.Stocks.FirstOrDefault(s => s.NomProduit_stock == produit2.NomProduit);
                produit2.Stock = stoc;
                Bdd.Produits.Add(produit2);
               
                Bdd.SaveChanges();
                UpdateStock(produit2); // Comme c'est un nouveau produit on met à jour le stock


            }
        }

        public void AjouterStock(Produit produit)
        {
            Stock newStock = new Stock();
            newStock.NomProduit_stock = produit.NomProduit;   //recuperation du nom du produit
            newStock.Quantite_Produit = produit.Quantite;
            newStock.Prix_unite_stock = produit.Prix_Unite;
            newStock.image_stok = produit.Image;

          //  Bdd.Stocks.Add(newStock);
          //  Bdd.SaveChanges();
            //recuperer id du nouveau stock et mettre comme foreing key dans produit
          //  Stock stoc = Bdd.Stocks.FirstOrDefault(s => s.Id == newStock.Id);
            produit.Stock = newStock;
           // stoc.NomProduit_stock = produit.NomProduit;
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
            //stockUpdate.image_stok = produit.Image;
            //stockUpdate.Prix_unite_stock = produit.Prix_Unite;
            
            Bdd.SaveChanges();

        }
        public Client Authentifier(Client client)
        {
            string mdpEncoder = EncodeMotDePasse(client.Password);
            Client client1 = Bdd.Clients.FirstOrDefault(c => c.Email == client.Email && c.Password == mdpEncoder);
            return client1;
        }

     
        public void Dispose()
        {
            Bdd.Dispose();
        }

        public void ModifierClient(Client client)
        {
            Client client1 = Bdd.Clients.FirstOrDefault(c => c.Id == client.Id);
            string ancienPassword = client1.Password;
            
            string newpass = EncodeMotDePasse(client.Password);
            if (client1.Password == null)
            {

                client1.Nom = client.Nom;
                client1.Prenom = client.Prenom;
                client1.Telephone = client.Telephone;
                client.ConfirmEmail = client.ConfirmEmail;
                client1.Date_Naissance = client.Date_Naissance;
                client1.Password = ancienPassword;
                Bdd.SaveChanges();
            }
        }

        public void ModifierProduit(Produit produit)
        {
            if (ProduitExisteDeja(produit))
            {
                Produit produit1 = Bdd.Produits.FirstOrDefault(c => c.Id == produit.Id);

                // produit1.NomProduit = produit.NomProduit;
                produit1.Prix_Unite = produit.Prix_Unite;
                produit1.Quantite = produit.Quantite;
                produit1.Date_heure_ajout = produit.Date_heure_ajout;
                produit1.Image = produit.Image;
              
                Bdd.SaveChanges();
                UpdateStock(produit1);
            }
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
            Produit produit1 = Bdd.Produits.FirstOrDefault(p => p.NomProduit == produit.NomProduit);
            if (produit1 != null)
                return true;
            return false;
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

       

        public int AjouterStockId(Produit produit)
        {
           
            Stock stoc = Bdd.Stocks.FirstOrDefault(s => s.NomProduit_stock == produit.NomProduit);
          
            return stoc.Id;
        }

        public Achat achatParIdStoc(int idstock)
        {
            Stock sc = Bdd.Stocks.FirstOrDefault(s => s.Id == idstock);
            return Bdd.Achats.FirstOrDefault(a => a.Stock.Id == sc.Id);
        }

        public bool EmailClienExiste(string Email)
        {
            Client cli = Bdd.Clients.FirstOrDefault(m => m.Email == Email);
                if(cli == null)
                return false;
            return true;
        }

        public int AjouterClientRenvoiId(string nom, string prenom, string email, string confirmEmail, string date_Naissance, string password, string telephone)
        {
            Client client1 = new Client();
            client1.Nom = nom;
            client1.Prenom = prenom;
            client1.Telephone = telephone;
            client1.Email = email;
            client1.ConfirmEmail = confirmEmail;
            client1.Date_Naissance = date_Naissance;
            client1.Password = EncodeMotDePasse(password);
            Bdd.Clients.Add(client1);
            Bdd.SaveChanges();
            return client1.Id;
        }

        public void ModifierClient(int id,string nom, string prenom, string email, string confirmEmail, string date_Naissance, string password, string telephone)
        {
            Client client1 = Bdd.Clients.FirstOrDefault(c => c.Id == id);
            string ancienPassword = client1.Password;

            string newpass = EncodeMotDePasse(password);
            if (password == null)
            {
                client1.Nom = nom;
                client1.Prenom = prenom;
                client1.Telephone = telephone;
                client1.Password = ancienPassword;
                client1.Email = email;
                client1.ConfirmEmail = confirmEmail;
                client1.Date_Naissance = date_Naissance;
                Bdd.SaveChanges();
            }
        }

        public bool MotDePassCorrect(string password)
        {
            string pass = EncodeMotDePasse(password);
            Client client = Bdd.Clients.FirstOrDefault(c => c.Password == pass);
            
            if (client == null)
            
                return false;
           
            return true;
        }

        public void ModifierMdp(int id, string AncienPass, string NouveauPass)
        {
            Client client = Bdd.Clients.FirstOrDefault(c => c.Id == id);
            string pass = EncodeMotDePasse(AncienPass);
            string newpass = EncodeMotDePasse(NouveauPass);
            if (client.Password == pass)
            {
                client.Password = newpass;
                Bdd.SaveChanges();
            }
        }

        public bool MotDePassClienExiste(int id, string pass)
        {
            Client cli = Bdd.Clients.FirstOrDefault(m => m.Id == id);
            string password = EncodeMotDePasse(pass);
            if (cli == null)
                return false;
            if (cli.Password != password)
                return false;
            return true;
        }

        public List<Commande> ObtenirLaListeDesCommandes()
        {
            return Bdd.Commandes.ToList();
        }

        public List<Achat> ObtenirLaListeDesAchats()
        {
            return Bdd.Achats.ToList();
        }
    }
}