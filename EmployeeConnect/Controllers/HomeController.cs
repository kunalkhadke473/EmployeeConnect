using EmployeeConnect.DAL;
using EmployeeConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeConnect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        EmployeeDBContext db = new EmployeeDBContext();
        public IActionResult About()
        {
            return View();
        }
            public IActionResult Index()
        {
            /*List<Employee> lst = new List<Employee>()
            {
            new Employee() {Id= 11, Name="Ganesh", Address="Pune"},
            new Employee() {Id= 12, Name="Suresh", Address="Satara"},
            new Employee() {Id= 13, Name="Dipesh", Address="Devgad"},
            new Employee() {Id= 14, Name="Nagesh", Address="Nagpur"},
            };*/


            List<Employee> lst = db.SelectEmpRecord();
            return View(lst);
        }


        public IActionResult InsertEmpData()
        {
            return View("InsertEmp");
        }

        public IActionResult AfterInsert(Employee emp)
        {
            int rowsAffted = db.InsertEmpRecord(emp);
            if (rowsAffted > 0)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View("InsertEmpData");
            }
        }

        /*public IActionResult UpdateEmpData(int id)
        {
            List<Employee> emps = db.SelectEmpRecord().ToList();

            var emp = (from e in emps
                       where e.Id == id
                       select e).First();
            return View(emp);
        }

        public IActionResult AfterUpdateEmp(Employee emp)
        {
            List<Employee> emps = db.SelectEmpRecord().ToList();

            var empToBeUpdted = (from e in emps
                                 where e.Id == emp.Id
                                 select e).First();
            db.UpdateEmpRecord(emp);
            return Redirect("/Home/Index");
        }*/

        public IActionResult UpdateEmpData(int id)
        {
            // Fetch the employee directly from the database using the ID
            var emp = db.SelectEmpRecord().FirstOrDefault(e => e.Id == id);

            // Check if the employee exists
            if (emp == null)
            {
                return NotFound("Employee not found.");
            }

            return View(emp);
        }

        public IActionResult AfterUpdateEmp(Employee emp)
        {
            if (emp == null)
            {
                return BadRequest("Invalid employee data.");
            }

            // Fetch the employee to be updated from the database using the ID
            var empToBeUpdated = db.SelectEmpRecord().FirstOrDefault(e => e.Id == emp.Id);

            // Check if the employee exists
            if (empToBeUpdated == null)
            {
                return NotFound("Employee not found for update.");
            }

            // Update the employee record
            db.UpdateEmpRecord(emp);
            return RedirectToAction("Index", "Home");
        }


        public IActionResult DeleteEmp(int id)
        { 
            db.DeleteEmpRecord(id);
            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
