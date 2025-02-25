using Antlr.Runtime.Tree;
using example20CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace example20CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EmpModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Repository s = new Repository();
                    s.AddEmp(obj);
                    return RedirectToAction("GetAllEmpDetails");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }
        public ActionResult GetAllEmpDetails()
        {
            Repository s = new Repository();
            ModelState.Clear();
            return View(s.GetAllEmployees());
        }
        public ActionResult EditEmp(int id)
        {
            Repository s = new Repository();
            return View(s.GetAllEmployees().Find(e => e.id == id));
        }
        [HttpPost]
        public ActionResult EditEmp(int id,EmpModel obj)
        {
            if(ModelState.IsValid)
            {
                Repository s = new Repository();
                s.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            return View();
        }
        public ActionResult DeleteEmp(int id)
        {
            if (ModelState.IsValid)
            {
                Repository s = new Repository();
                s.DeleteEmployee(id);
                return RedirectToAction("GetAllEmpDetails");
            }
            return View();
        }

    }
}