using PharmacieCodeFirstASPNET.Models;
using PharmacieCodeFirstASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private IDal dal;
        public ClientController () : this(new Dal())
        {

        }

        public ClientController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        // GET: Client
        public ActionResult Index()
        {
            
            Client client = dal.ObtenirClient(HttpContext.User.Identity.Name);
            if (client == null)
                return HttpNotFound();
            return View(client);
        }

        public ActionResult ModifierClient(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = dal.ObtenirTousLesClients().FirstOrDefault(c => c.Id == id);
            if (client == null)
                return HttpNotFound();
            ClientModification client1 = new ClientModification()
            {
                Id = client.Id,
                Nom = client.Nom,
                Prenom = client.Prenom,
                Email = client.Email,
          
                ConfirmEmail = client.ConfirmEmail,
                Password = client.Password,
                Date_Naissance = client.Date_Naissance,
                Telephone = client.Telephone
            };
            return View(client1);
        }

        [HttpPost]

        public ActionResult ModifierClient(ClientModification client)
        {
            if (ModelState.IsValid)
            {
                //dal.ModifierClient(client);
                dal.ModifierClient(client.Id, client.Nom, client.Prenom, client.Email, client.ConfirmEmail, client.Date_Naissance, client.Password,client.Telephone);
                return RedirectToAction("Index");
            }
            else return View(client);
        }
        public JsonResult VerifPassword(ClientPassword client)
        {
            bool result = dal.MotDePassCorrect(client.Password);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ModifierPassword(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = dal.ObtenirTousLesClients().FirstOrDefault(c => c.Id == id);
            if (client == null)
                return HttpNotFound();
            ClientPassword cli = new ClientPassword() { id = id.Value };
            return View(cli);
        }

        [HttpPost]
        public ActionResult ModifierPassword(ClientPassword client )
        {
            if (ModelState.IsValid)
            {
                if (!dal.MotDePassClienExiste(client.id, client.Password))
                {
                    ModelState.AddModelError("Password", "L'ancien mot de passe n'est pas correct");
                    return View(client);
                }
                   
                dal.ModifierMdp(client.id, client.Password, client.NewPass);
                return RedirectToAction("Index");
            }
            else return View(client);
           
                
          
        }

        }
}