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
    public class CategoryController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Category
        public ActionResult ListCategories(int page=1)
        {
            //List<Category> categories = db.Categories.ToList();
            var categories = db.Categories.ToList().ToPagedList(page, 5);
            return View(categories);
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Category category)
          {
            if (!ModelState.IsValid)
            {
                return View("Insert");

            }
        
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("ListCategories");
        }
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();

            return RedirectToAction("ListCategories");
        }
        public ActionResult BringCategory(int id)
        {
            var category = db.Categories.Find(id);
            return View("BringCategory", category);
        }

        public ActionResult Update(Category c)
        {
            var category = db.Categories.Find(c.CategoryID);
            category.CategoryName = c.CategoryName;
            category.Description = c.Description;
            db.SaveChanges();
            return RedirectToAction("ListCategories");
        }
       
    }
}