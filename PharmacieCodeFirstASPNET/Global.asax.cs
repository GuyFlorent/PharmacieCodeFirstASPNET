using PharmacieCodeFirstASPNET.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Device.Location;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PharmacieCodeFirstASPNET
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string lattitude;
        private string longitude;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IDatabaseInitializer<BddContext> init = new InitPharmacie();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());

            GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
           


                // Do not suppress prompt, and wait 1000 milliseconds to start.
                watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));
              
                GeoCoordinate coord = watcher.Position.Location;


                if (coord.IsUnknown != true)
                {

                    lattitude = coord.Latitude.ToString();

                    longitude = coord.Longitude.ToString();
                }
            
        }
    }
}
