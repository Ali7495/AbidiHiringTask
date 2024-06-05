using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Data_Transfer_Objects;
using AbidiHiringTask.Application.Services_Interfaces;
using AbidiHiringTask.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbidiHiringTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;

        public HomeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }

        [HttpGet]
        public IActionResult EmployeeForm()
        {
            return View(new EmployeeInputDto());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEmployee(EmployeeInputDto model, List<IFormFile> files, CancellationToken cancellationToken)
        {

            ResultModel resultModel = await _employeeServices.AddEmployeeAsync(model, files, cancellationToken);

            return RedirectToAction("EmployeeForm");
        }

    }
}