using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatStore.Models;

namespace CatStore.Controllers
{
    public class HomeController : Controller
    {   
        Cat cat1 = new Cat { id = "001", name = "Gustaf", price = 10000 };
        Cat cat2= new Cat { id = "002", name = "Kiwi", price = 300 };
        Cat cat3 = new Cat { id = "003", name = "Anders", price=2 };
        Cat cat4 = new Cat { id = "004", name = "Flinga", price = 443 };
        Cat cat5 = new Cat { id = "005", name = "Jessika", price = 55.32 };
        Cat cat6 = new Cat { id = "006", name = "Aqua", price = 5.6 };
        Cat cat7 = new Cat { id = "007", name = "Lexie", price = 2000 };
        Cat cat8 = new Cat { id = "008", name = "Missan", price = 568 };
        Cats catList = new Cats();
        

        public ActionResult Index()
        {

            ViewBag.shoppingkartError = TempData["Error"];
            ViewBag.ordersError = TempData["ordersError"];
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Cats()
        {
            
            catList.cats.Add(cat1);
            catList.cats.Add(cat2);
            catList.cats.Add(cat3);
            catList.cats.Add(cat4);
            catList.cats.Add(cat5);
            catList.cats.Add(cat6);
            catList.cats.Add(cat7);
            catList.cats.Add(cat8);
            Session["cats"] = catList;
            ViewBag.Message = TempData["errorMessage"]; 
            return View(catList);
        }
        public ActionResult Order(string catId)
        {
            
            var cats = Session["cats"] as Cats;
            var order = Session["NewOrder"] as NewOrder;
        
            if (order != null)
            {
                    if (!order.catsIds.Contains(catId))
                    {                   
                        order.catsIds.Add(catId); 
                    }
                    else
                    {
                         TempData["errorMessage"] = " katten finns redan i varukorgen ";
                    }
                return RedirectToAction("cats", "Home");
            }
            else
            {
                NewOrder newOrder = new NewOrder();
                newOrder.catsIds.Add(catId);
                Session["NewOrder"] = newOrder;
                return RedirectToAction("cats","Home");
            }
        }
    }
}