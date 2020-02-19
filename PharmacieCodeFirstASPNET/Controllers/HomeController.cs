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
        const string APPID = "508c868bd2f6e30eed650ee34d8df9d8";
        string cityName;
       


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

      
        
        public ActionResult About(WeatherViewModel viewModel)
        {
            using (WebClient web = new WebClient())
            {
                

                string url = string.Format("http://api.openweathermap.org/data/2.5/weather?zip=92160,fr&APPID={0}&units=metric&cnt=6", APPID);
                var json = web.DownloadString(url);
                var result = JsonConvert.DeserializeObject<WeatherInfo.root>(json);
                WeatherInfo.root outPut = result;
                viewModel.NomVille = outPut.name + ", " + outPut.sys.country;
               // cityName = txt_nom_ville.Text;
               // txt_date_jour.Text = WeatherInfo.Jour() + ", le " + DateTime.Now.Day.ToString() + " " + weatherInfo.MoisEnFrancais() + " " + DateTime.Now.Year.ToString();
                viewModel.Temperature= string.Format(outPut.main.temp.ToString() + "\u00B0" + " C");
               // meteo = txt_Temperature.Text;
               // txt_humidite.Text = outPut.main.humidity.ToString() + "% de précipitation";

                return View(viewModel);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}