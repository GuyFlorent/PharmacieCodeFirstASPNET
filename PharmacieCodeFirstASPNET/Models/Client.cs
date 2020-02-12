using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Date_Naissance { get; set; }
        public string Telephone { get; set; }
    }
}