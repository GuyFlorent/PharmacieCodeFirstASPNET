using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Achat> Achats { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}