using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PharmacieCodeFirstASPNET.ViewModel
{
    public class ClientViewModel
    {
        public Client client { get; set; }
        public bool Authentfie { get; set; }
    }
}