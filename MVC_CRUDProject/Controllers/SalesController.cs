using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUDProject.Models;
namespace MVC_CRUDProject.Controllers
{
    public class SalesController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewSales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewSales(Order_Detail order)
        {
            db.Order_Details.Add(order);
            db.SaveChanges();
            return View("Index");
        }
    }
}