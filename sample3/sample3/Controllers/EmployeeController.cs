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
            var employees = await mvcDemoContext.Employees.ToListAsync();
            return View(employees);
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

        //get one user details
        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        { 
            var employee =await mvcDemoContext.Employees.FirstOrDefaultAsync(x=>x.Id == id);

            if (employee != null) {
                var viewModel = new UpdateViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                };
                return await Task.Run(()=> View("View",viewModel));
            }
            return RedirectToAction("Index");
        }

        //update one user details
        [HttpPost]
        public async Task<IActionResult> View(UpdateViewModel updateViewModel)
        {
           var employee = await mvcDemoContext.Employees.FindAsync(updateViewModel.Id);

            if (employee != null) { 
            employee.Name = updateViewModel.Name;
            employee.Email= updateViewModel.Email;
                employee.Salary = updateViewModel.Salary;
                employee.DateOfBirth = updateViewModel.DateOfBirth;
                employee.Department = updateViewModel.Department;

                await mvcDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateViewModel updateViewModel) {
            var employee =await mvcDemoContext.Employees.FindAsync(updateViewModel.Id);
            if (employee != null) {

                mvcDemoContext.Employees.Remove(employee);
                await mvcDemoContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }





    }
}
