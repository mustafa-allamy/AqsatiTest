using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Application.CQRS.DepartmentInfo.Departments.Forms;
using Application.CQRS.DepartmentInfo.DepartmentServices.Forms;
using Application.CQRS.DepartmentInfo.DepartmentVacationTypes.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreateDepartment, "انشاء دائرة")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }
        [HttpGet]
        [HasPermission(PermissionsConst.GetDepartments, "عرض الدوائر")]
        public async Task<IActionResult> GetDepartments([FromQuery] GetDepartmentsForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }


        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetDepartment, "عرض دائرة")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var res = await _mediator.Send(new GetDepartmentForm() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPut("{id:int}/GeneralExcelTemplate")]
        [HasPermission(PermissionsConst.AddGeneralExcelTemplateToDepartment, "اضافة قوالب اكسل عامة لدائرة")]
        public async Task<IActionResult> AddGeneralExcelTemplatesToDepartment(int id, AddGeneralExcelTemplatesToDepartmentForm cmd)
        {
            cmd.DepartmentId = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPut("{id:int}/GeneralServices")]
        [HasPermission(PermissionsConst.AddGeneralServicesToDepartment, "اضافة خدمات عامة لدائرة")]
        public async Task<IActionResult> AddGeneralServicesToDepartment(int id, AddDepartmentServicesForm cmd)
        {
            cmd.DepartmentId = id;

            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPut("{id:int}/GeneralVacationTypes")]
        [HasPermission(PermissionsConst.AddGeneralVacationTypesToDepartment, "اضافة اجايز عامة لدائرة")]
        public async Task<IActionResult> AddGeneralVacationTypesToDepartment(int id, AddGeneralVacationTypesToDepartmentForm cmd)
        {
            cmd.DepartmentId = id;

            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }

    }
}
