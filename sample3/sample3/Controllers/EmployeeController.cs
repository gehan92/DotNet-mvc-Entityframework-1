using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sample3.Data;
using sample3.Models;
using sample3.Models.Domain;

namespace sample3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoContext mvcDemoContext;

        public EmployeeController(MVCDemoContext mvcDemoContext) {
            this.mvcDemoContext = mvcDemoContext;
        }

        // view employees
        [HttpGet]
        public async Task<IActionResult> index() 
        {
           
            return View();
        }

         


        //view add employee page
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //add emploee
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModelRequest) 
        {
            var employee = new Employees()
            {
                Id =Guid.NewGuid(),
                Name= addEmployeeViewModelRequest.Name,
                Email = addEmployeeViewModelRequest.Email,
                Salary = addEmployeeViewModelRequest.Salary,
                Department = addEmployeeViewModelRequest.Department,
                DateOfBirth = addEmployeeViewModelRequest.DateOfBirth,
            };

            await mvcDemoContext.Employees.AddAsync(employee);
            await mvcDemoContext.SaveChangesAsync();
            return RedirectToAction("Add");

        }


       



    }
}
