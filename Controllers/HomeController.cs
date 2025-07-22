using Microsoft.AspNetCore.Mvc;
using RepositoryDesignPattern.Models;
using RepositoryDesignPattern.Repository;
using System.Diagnostics;

namespace RepositoryDesignPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeRepository _employeerepository;
        public HomeController(ILogger<HomeController> logger,IEmployeeRepository employeeRepository)
        {
            _logger = logger;
            _employeerepository = employeeRepository;
        }

        public async Task<IActionResult> Index()
        {
            var empList=new List<Employee>();
            empList = await _employeerepository.GetAllEmployee();
            return View(empList);
        }
        
        public async Task<IActionResult> AddNew()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Employee employee)
        {
            await _employeerepository.AddEmployee(employee);
            return Redirect("index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _employeerepository.DeleteEmployee(id);
            return Ok();
        }
        public async Task<IActionResult> Edit(int id)
        {
            Employee emp = new Employee();
            emp=await _employeerepository.GetEmployeeByID(id);
            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            await _employeerepository.UpdateEmployee(employee);
            return RedirectToAction("index","home");


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
