using Adsite.Data;
using Adsite.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private static string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Adsite2;Integrated Security=true;TrustServerCertificate=yes;";
        AdManager manager = new AdManager(_connectionString);


        public IActionResult Index()
        { 
            var ivm = new IndexViewModel
            {
                Ads = manager.GetAds(),
            };
            var myAds = HttpContext.Session.Get<List<int>>("my-ads");
            if (myAds != null)
            {
                ivm.MyAds = myAds;
            }        
            return View(ivm);
        }

        public IActionResult NewAd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAd(Ad ad)
        {
            manager.NewAd(ad);
            var myAds = HttpContext.Session.Get<List<int>>("my-ads");
            if (myAds != null)
            {
                myAds.Add(ad.Id);
            }
            else
            {
                myAds = new List<int>{ad.Id};
            }
            HttpContext.Session.Set("my-ads", myAds);
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public IActionResult DeleteAD(int id)
        {
            manager.DeleteAd(id);
            return Redirect("/home/Index");
        }
    }

}
