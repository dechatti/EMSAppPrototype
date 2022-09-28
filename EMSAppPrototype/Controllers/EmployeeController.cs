using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSAppPrototype.EntityModels;
using EMSAppPrototype.Models;

namespace EMSAppPrototype.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EMSDbPtContext db = new EMSDbPtContext();
            List<Employee> employees = db.Employees.ToList();
            return View(employees);
        }
        [HttpGet]
        public ActionResult Create()
        {
            EmployeeViewModel employee = new EmployeeViewModel();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel model)
        {
            try
            {
                EMSDbPtContext db = new EMSDbPtContext();
                Employee emp = new Employee
                {
                    Address = model.Address,
                    DateCreated = DateTime.Now,
                    Email = model.Email,
                    Phone = model.Phone,
                    Salary = model.Salary,
                    IsActive = true
                };
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "An Error Occured while trying to create your account. Please try again later";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("Error");
            int ID = Helper.Decrypt(id);
            EMSDbPtContext db = new EMSDbPtContext();
            Employee employee = db.Employees.FirstOrDefault(x => x.Id == ID);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel model)
        {
            try
            {
                EMSDbPtContext db = new EMSDbPtContext();
                var originalEmployee = db.Employees.FirstOrDefault(x => x.Id == model.Id);
                originalEmployee.Email = model.Email;
                originalEmployee.Address = model.Address;
                originalEmployee.Phone = model.Phone;
                originalEmployee.Salary = model.Salary;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error("An error occured while trying to update your account details. Please try again later.");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View("Error");
            int ID = Helper.Decrypt(id);
            EMSDbPtContext db = new EMSDbPtContext();
            Employee employee = db.Employees.FirstOrDefault(x => x.Id == ID);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            try
            {
                EMSDbPtContext db = new EMSDbPtContext();

                Employee emp = db.Employees.FirstOrDefault(x => x.Id == employee.Id);
                db.Employees.Remove(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "An error occured while trying to delete employee record.";
            }
            return View(employee);
        }
    }
}