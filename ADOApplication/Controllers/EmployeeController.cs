using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADOApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ADOApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IRepository.IRepo _repo;
      
        public EmployeeController(IRepository.IRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            
            
        }
        public IActionResult Index()
        {
            var list =_repo.GetEmployees();
            return View(list);
        }

        [HttpGet]
        public IActionResult Insert()
        {

            return View(new Employee());  
        }

        [HttpPost]
        public IActionResult Insert(Employee employee)
        {
            _repo.Insert(employee);
            return RedirectToAction("Index");
        }

        public IActionResult GetEmployeeById(int id)
        {
            Employee employee = _repo.GetEmployeeById(id);
            if (employee != null)
                return View(employee);
            else
                ViewBag.msg = "No Record";
            return View();
        }
        public IActionResult Edit(int id)
        {
            Employee employee = _repo.GetEmployeeById(id);
            if (employee != null)
                return View(employee);
            else
                ViewBag.msg = "No Record";
            return View();

        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            _repo.Edit(employee.Id, employee);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            Employee employee = _repo.GetEmployeeById(id);
            if (employee != null)
                return View(employee);
            else
                ViewBag.msg = "No Record";
            return View();

        }

        [HttpPost]
        public IActionResult Delete(int id ,Employee employee)
        {
            _repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
