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
    public class EmployeeController : Controller
    {
        // GET: Employee
        NorthwindEntities db = new NorthwindEntities();
        // GET: Home

        public ActionResult EmployeeList(int page=1)
        {

            var employees = db.Employees.ToList().ToPagedList(page, 5);
            return View(employees);
        }
        [HttpGet]
        public ActionResult Insert()
        {
            
            List<SelectListItem> cities = (from i in db.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.City,
                                               Value = i.EmployeeID.ToString()
                                               
                                           }).ToList();


            ViewBag.city = cities;

            return View();
        }
        [HttpPost]
        public ActionResult Insert(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                View("Insert");
            }
          
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult Delete(int id)
        {
            var employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult BringEmployee(int id)
        {
            var employee = db.Employees.Find(id);
            List<SelectListItem> cities = (from i in db.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.City,
                                               Value = i.EmployeeID.ToString()

                                           }).ToList();


            ViewBag.city = cities;

            return View("Update", employee);
        }

        public ActionResult UpdateEmployee(Employee e)
        {
            var employee = db.Employees.Find(e.EmployeeID);
            employee.FirstName = e.FirstName;
            employee.LastName = e.LastName;
            var city = db.Employees.Where(x => x.City == e.City).FirstOrDefault();
            employee.City = city.City;
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }
    }


}