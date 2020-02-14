using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class InitPharmacie : DropCreateDatabaseAlways<BddContext>
    {
        protected override void Seed(BddContext context)
        {
            context.Produits.Add(new Produit()
            {
                NomProduit = "Doliprane",
                Quantite = 5,
                Prix_Unite = 12,
                Date_heure_ajout = "12/03/2015"
            });

            context.Produits.Add(new Produit()
            {
                NomProduit = "Pepsie",
                Quantite = 45,
                Prix_Unite = 32,
                Date_heure_ajout = "12/03/2002"
            });

            context.Produits.Add(new Produit()
            {
                NomProduit = "Paracétamol",
                Quantite = 85,
                Prix_Unite = 2,
                Date_heure_ajout = "12/03/2008"
            });


            context.Clients.Add(new Client()
            {
                Nom = "toto",
                Prenom = "juju",
                Date_Naissance = "12/01/2020",
                Password = "bonjour",
                Email = "wfc",
                ConfirmEmail = "wfc",
                Telephone = "2545855"
            });

            context.Clients.Add(new Client()
            {
                Nom = "jean",
                Prenom = "dubois",
                Date_Naissance = "12/01/2000",
                Password = "fauxpass",
                Email = "www",
                ConfirmEmail = "www",
                Telephone = "2545855256"
            });

            base.Seed(context);
        }
    }
}