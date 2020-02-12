using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime heure_commande { get; set; }
        //public string statut_commande { get; set; }  // à faire apres
        //public string statut_livraison { get; set; }
        public virtual Client Client { get; set; }
        public virtual List<Achat> Achats { get; set; }
    }
}