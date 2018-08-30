using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatStore.Models;

namespace CatStore.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderKart()
        {
            ViewBag.Errormsg = TempData["WrongKart"];
            var newOrder = Session["NewOrder"] as NewOrder;
            var cats = Session["cats"] as Cats;
            var catsList = new List<Cats>();
            if (cats != null && newOrder != null)
            {
                Cats catList = new Cats();
                foreach (var cat in cats.cats)
                {
                    if (newOrder.catsIds.Contains(cat.id))
                    {
                        catList.cats.Add(cat);
                    }

                }
                catsList.Add(catList);
                Session["catsList"] = catsList;
                Session["cats"] = catList;
                
                
                return View(catList);
            }
            else
            {
                TempData["Error"] = "välj katter först";
                return RedirectToAction("cats","Home");
            }
        }
        public ActionResult Order()
        {
            var cats = Session["cats"] as Cats;
            var catsList = Session["catsList"] as List<Cats>;
            var orders = Session["resultOrders"] as List<ResultOrder>;
            var receiptList = Session["receipts"] as Dictionary<Order, Receipt>;
            Order order = new Order { id = Guid.NewGuid().ToString() };
            ResultOrder resOrder = new ResultOrder { orderId = order.id, success = true };
            Receipt rec = new Receipt() { status = "bekräftad", sum = cats.totalPrice() };
            if (cats != null)
            {
                Session["NewOrder"] = null;
                if (orders != null)
                {
                        receiptList.Add(order , rec);
                        orders.Add(resOrder);
                }
                    else
                    {
                    Dictionary<Order, Receipt> receipts = new Dictionary<Order, Receipt>();
                    receipts.Add(order, rec);
                    Session["receipts"] = receipts;
                    List<ResultOrder> orderList = new List<ResultOrder>();
                        orderList.Add(resOrder);
                        Session["ResultOrders"] = orderList;
                    }
                
                ViewBag.Message = "din order är bekräftad: " + order.id;
                ViewBag.totalPrice = rec.sum;
                ViewBag.status = rec.status;

                return View(cats);
            }
            else
            {
                TempData["WrongKart"] = "fel Order";
                return RedirectToAction("OrderKart","order");
            }
        }
        public ActionResult Orders()
        {
            var orders = Session["resultOrders"] as List<ResultOrder>;
            if (orders != null)
                return View(orders);

            else
            {
                TempData["ordersError"] = "skapa ordrar först";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Receipt(string orderId)
        {
            var receiptList = Session["receipts"] as Dictionary<Order, Receipt>;
            if (receiptList != null)
            {
                foreach (var x in receiptList)
                {
                    if (x.Key.id.Equals(orderId))
                    {
                        ViewBag.Id = x.Key.id;
                        return View(x.Value);
                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    
    }
}