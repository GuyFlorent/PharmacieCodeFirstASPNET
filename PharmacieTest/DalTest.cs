using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PharmacieCodeFirstASPNET.Models;

namespace PharmacieTest
{
    [TestClass]
    public class DalTest
    {
        private IDal dal;
        [TestInitialize]
        public void Init_AvantTest()
        {
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
            dal = new Dal();
        }

        [TestCleanup]
        public void ApresChaqueTest()
        {
            dal.Dispose();
        }

        [TestMethod]
        public void CreerUnProduit_AvecUnNouveauProduit_Obtenir_Tout_Les_Produit()
        {

            Produit produit1 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };

            dal.AjouterProduit(produit1);
            List<Produit> produits = dal.ObtenirTousLesProduits();
            
            Assert.IsNotNull(produits);
            Assert.AreEqual(1, produits.Count);
            Assert.AreEqual("Doliprane", produits[0].NomProduit);
            Assert.AreEqual(12, produits[0].Prix_Unite);
            Assert.AreEqual(5, produits[0].Quantite);
            Assert.AreEqual("12/03/2015", produits[0].Date_heure_ajout);



        }

        [TestMethod]
        public void ModifierUnProduit_CreationUnNouveauProduitEtChangerNomEtPrix_Obtenir_ModificationCorrect()
        {

            Produit produit1 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };

            dal.AjouterProduit(produit1);

            Produit produit2 = new Produit()
            {
                Id = 1,
                NomProduit = "Paracetamol",
                Quantite = 0,
                Prix_Unite = 15,
                Date_heure_ajout = null
            };

            dal.ModifierProduit(produit2);

            List<Produit> produits = dal.ObtenirTousLesProduits();

            Assert.IsNotNull(produits);
            Assert.AreEqual(1, produits.Count);
            Assert.AreEqual("Paracetamol", produits[0].NomProduit);
            Assert.AreEqual(15, produits[0].Prix_Unite);
            Assert.AreEqual(0, produits[0].Quantite);
            Assert.IsNull(produits[0].Date_heure_ajout);


        }

        [TestMethod]
        public void ProduitExiste_AvecCreationDunProduit_RenvoieIlExiste()
        {
            Produit produit1 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };
            dal.AjouterProduit(produit1);

            Produit produit2 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };

            bool produitExiste = dal.ProduitExisteDeja(produit2);

            Assert.IsTrue(produitExiste);

        }

        [TestMethod]

        public void ProduitExiste_AvecProduitInexistant_RenvoiQuilExiste()
        {
            Produit produit1 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };

            bool existe = dal.ProduitExisteDeja(produit1);
            Assert.IsFalse(existe);

        }

        [TestMethod]

        public void ObtebnirClient_ClientInexistant_RetourneNull()
        {
            Client client = dal.ObtenirClient(1);
            Assert.IsNull(client);
        }

        [TestMethod]
        public void ObtenirClient_IdNonNumerique_RetourneNull()
        {
            Client client = dal.ObtenirClient("rtyuegej");
            Assert.IsNull(client);
        }

        [TestMethod]
        public void creerUnClient_AvecNouveauClient_ObtenirListeClient()
        {
            Client client = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            dal.AjouterClient(client);
            List<Client> clients = dal.ObtenirTousLesClients();

            Assert.IsNotNull(clients);
            Assert.AreEqual(1, clients.Count);
            Assert.AreEqual("toto", clients[0].Nom);
            Assert.AreEqual("juju", clients[0].Prenom);
            Assert.AreEqual("wfc", clients[0].Email);
            Assert.AreEqual("2545855",clients[0].Telephone);
        }
        [TestMethod]
        public void AjouterClent_NouveauClientEtRecuperation_ClientEstBienRecuperer()
        {
            Client client = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            dal.AjouterClient(client);

            Client ClientRecuper = dal.ObtenirClient(1);
            Assert.IsNotNull(ClientRecuper);
            Assert.AreEqual("juju", ClientRecuper.Prenom);

            Client ClientRecuperIdStr = dal.ObtenirClient("1");
            Assert.IsNotNull(ClientRecuperIdStr);
            Assert.AreEqual("toto", ClientRecuper.Nom);

        }

        [TestMethod]
        public void Authentifier_LoginOKMdpOk_AuthentificationOk()
        {
            Client client = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            dal.AjouterClient(client);

            Client client1 = dal.Authentifier(client);
            Assert.IsNotNull(client1);
            Assert.AreEqual("toto", client1.Nom);
        }

        [TestMethod]
        public void Authentifier_LoginKOMdpOk_AuthentificationKo()
        {
            Client client = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            dal.AjouterClient(client);

            Client client1 = new Client()
            {
                Nom = "bonbon",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            Client client2 = dal.Authentifier(client1);
            Assert.IsNull(client2);
           
        }


        [TestMethod]
        public void Authentifier_LoginOkMdpKo_AuthentificationKo()
        {
            Client client = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            dal.AjouterClient(client);

            Client client1 = new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "paris",
                Email = "wfc",
                Telephone = "2545855"
            };

            Client client2 = dal.Authentifier(client1);
            Assert.IsNull(client2);

        }

        [TestMethod]
        public void AjouterAchat_Avec()
        {
            int idCommande = dal.PasserCommande();

            Client client1 = new Client()
            {
                Nom = "bonbon",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                Telephone = "2545855"
            };

            int idClient = dal.AjouterClientRenvoiId(client1);

            Produit produit1 = new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 50,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            };
            dal.AjouterProduit(produit1);

            int IdStock = dal.AjouterStockId(produit1);

            dal.AjouterAchat(idCommande, idClient, IdStock, 10);

          Achat achat=  dal.achatParIdStoc(IdStock);

            Assert.IsNotNull(achat);
            Assert.AreEqual(120, achat.Prix_Total);
            Assert.AreEqual(10, achat.Quantite_Totale);
            Assert.AreEqual(1, achat.Stock.Id);
                }
    }
}
