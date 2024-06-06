using AbidiHiringTask.Application.Common_Tools;
using AbidiHiringTask.Application.Data_Transfer_Objects;
using AbidiHiringTask.Application.Services_Interfaces;
using AbidiHiringTask.Models;
using AutoMapper;
using HiringTask.Domain.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbidiHiringTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly IMapper _mapper;

        public HomeController(IEmployeeServices employeeServices, IMapper mapper)
        {
            _employeeServices = employeeServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeForm(CancellationToken cancellationToken)
        {
            List<EmployeeOutputDto> employees = await _employeeServices.GetEmployeesAsync(cancellationToken);

            return View(employees);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEmployee(EmployeeInputDto model, List<IFormFile> files, CancellationToken cancellationToken)
        {

            ResultModel resultModel = await _employeeServices.AddEmployeeAsync(model, files, cancellationToken);

            List<EmployeeOutputDto> employees = new();

            if (resultModel.Status == ResultStatus.Success)
            {
                employees = await _employeeServices.GetEmployeesAsync(cancellationToken);
            }

            return RedirectToAction("EmployeeForm", employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken)
        {
            EmployeeOutputDto employeeOutput = await _employeeServices.GetEmployeeByIdAsync(id, cancellationToken);

            return PartialView("_EditEmployeePartial", employeeOutput);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeOutputDto model, CancellationToken cancellationToken)
        {
            // اینجا برای اینکه سریع تر تمومش کنم مجبور شدم :))

            EmployeeInputDto employeeInputDto = _mapper.Map<EmployeeInputDto>(model);

            ResultModel resultModel = await _employeeServices.UpdateEmployeeAsync(model.Id,employeeInputDto, cancellationToken);

            List<EmployeeOutputDto> employees = new();

            if (resultModel.Status == ResultStatus.Success)
            {
                employees = await _employeeServices.GetEmployeesAsync(cancellationToken);
            }

            return RedirectToAction("EmployeeForm", employees);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                ResultModel resultModel = await _employeeServices.DeleteEmployeeAsync(id, cancellationToken);

                List<EmployeeOutputDto> employees = new();

                if (resultModel.Status == ResultStatus.Success)
                {
                    employees = await _employeeServices.GetEmployeesAsync(cancellationToken);
                }

                return RedirectToAction("EmployeeForm", employees);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}