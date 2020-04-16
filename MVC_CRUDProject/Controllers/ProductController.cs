using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUDProject.Models;
using PagedList;
using PagedList.Mvc;
namespace MVC_CRUDProject.Controllers
{
    public class ProductController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Product
        public ActionResult ProductList(int page=1)
        {
            var products = db.Products.ToList().ToPagedList(page, 5);

            return View(products);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            List<SelectListItem> degerler = (from i in db.Categories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Product product)
        {
            var category = db.Categories.Where(x => x.CategoryID == product.Category.CategoryID).FirstOrDefault();
            product.Category = category;
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }

        public ActionResult BringProduct(int id) {
            var product = db.Products.Find(id);
            List<SelectListItem> degerler = (from i in db.Categories.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.CategoryName,
                                                 Value = i.CategoryID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View("BringProduct",product);

        }
        public ActionResult ProductUpdate(Product p)
        {
            var product = db.Products.Find(p.ProductID);
            product.ProductName = p.ProductName;
            //product.CategoryID=p.CategoryID;
            var category = db.Categories.Where(x => x.CategoryID == p.Category.CategoryID).FirstOrDefault();
            product.CategoryID = category.CategoryID;
            product.UnitsInStock = p.UnitsInStock;
            product.UnitPrice = p.UnitPrice;
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}