using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_CRUDProject.Models;
namespace MVC_CRUDProject.Controllers
{
    public class CustomerController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Customer
        public ActionResult CustomerList(string p)
        {
            var values = from d in db.Users select d;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(x => x.Username.Contains(p));
            }
            return View(values.ToList());
           
        }
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(User user)
        {
            
                if (!ModelState.IsValid)
                {
                    return View("Insert");

                }

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("CustomerList");
            


        }

        public ActionResult Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("CustomerList");
        }
        public ActionResult BringCustomer(int id)
        {
            var customer = db.Users.Find(id);
            return View("BringCustomer", customer);
        }
        public ActionResult Update(User u)
        {
            var user = db.Users.Find(u.ID);
            user.Full_name = u.Full_name;
            user.Username = u.Username;
            user.Phone_or_Email = u.Phone_or_Email;
            db.SaveChanges();
            return RedirectToAction("CustomerList");

        }
    }
}