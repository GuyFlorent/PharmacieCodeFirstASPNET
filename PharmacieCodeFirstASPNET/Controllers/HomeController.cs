using Newtonsoft.Json;
using PharmacieCodeFirstASPNET.Models;
using PharmacieCodeFirstASPNET.ViewModel;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PharmacieCodeFirstASPNET.Controllers
{
    public class HomeController : Controller
    {
        private IDal dal;
        //const string APPID = "508c868bd2f6e30eed650ee34d8df9d8";
        const string APPID = "210f5ad6dde467ddb96d95ad1b89e684";
    
       


        public HomeController(): this(new Dal())
        {

        }

        public HomeController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        public ActionResult Index()
        {
            List<Stock> listestocks = dal.ObtenirTousLesStock();
            
            return View(listestocks);
        }

      
        
        public ActionResult Meteo( double? lat, double? lon)
        {
            using (WebClient web = new WebClient())
            {
                CoordonnerViewModel coordonner = new CoordonnerViewModel();

                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&APPID={2}&units=metric&cnt=6",lat,lon, APPID);
                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                WeatherInfo.root outPut = result;
                coordonner.nomPays = outPut.name + ", " + outPut.sys.country;
                // cityName = txt_nom_ville.Text;
                // txt_date_jour.Text = WeatherInfo.Jour() + ", le " + DateTime.Now.Day.ToString() + " " + weatherInfo.MoisEnFrancais() + " " + DateTime.Now.Year.ToString();
                coordonner.Temperature = string.Format(outPut.main.temp.ToString() + "\u00B0" + " C");
                // meteo = txt_Temperature.Text;
                coordonner.Precipitation = outPut.main.humidity.ToString() + "% de précipitation";

                return PartialView("Mete",coordonner);
            }
   
        }

        public ActionResult Mete(CoordonnerViewModel coordonner)
        {
            return PartialView(coordonner);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}