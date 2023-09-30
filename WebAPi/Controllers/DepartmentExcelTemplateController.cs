using Application.CQRS.DepartmentInfo.DepartmentExcelTemplates.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentExcelTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentExcelTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreateDepartmentExcelTemplate, "انشاء قالب اكسل دائرة")]
        public async Task<IActionResult> CreateDepartmentExcelTemplate(int? departmentId, CreateDepartmentExcelTemplateForm cmd)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            cmd.DepartmentId = departmentId;

            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetDepartmentExcelTemplates, "عرض قوالب اكسل الدائرة")]
        public async Task<IActionResult> GetDepartmentExcelTemplates(int? departmentId, [FromQuery] GetDepartmentExcelTemplatesForm query)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }

            query.DepartmentId = departmentId;
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }


        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetDepartmentExcelTemplate, "عرض قالب اكسل الدائرة")]
        public async Task<IActionResult> GetDepartmentExcelTemplate(int id, int? departmentId)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            var res = await _mediator.Send(new GetDepartmentExcelTemplateForm() { Id = id, DepartmentId = departmentId });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateDepartmentExcelTemplate, "تعديل قالب اكسل دائرة")]
        public async Task<IActionResult> UpdateGeneralExcelTemplate(int id, int? departmentId, UpdateDepartmentExcelTemplateForm form)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            form.DepartmentId = departmentId;

            form.Id = id;
            var res = await _mediator.Send(form);

            return Ok(res.ToSuccessClientResponse());
        }
        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeleteDepartmentExcelTemplate, "حذف قالب اكسل الدائرة")]
        public async Task<IActionResult> DeleteDepartmentExcelTemplate(int id, int? departmentId)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId;
            }
            var res = await _mediator.Send(new DeleteDepartmentExcelTemplateForm() { Id = id, DepartmentId = departmentId });

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPut("{id:int}/Services")]
        [HasPermission(PermissionsConst.AddDepartmentExcelTemplateServices, "اضافة خدمات لقالب اكسل دائرة")]
        public async Task<IActionResult> AddDepartmentExcelTemplateServices(int id, int? departmentId, AddDepartmentExcelTemplateServicesForm cmd)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId;
            }
            cmd.DepartmentExcelTemplateId = id;
            cmd.DepartmentId = departmentId;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }
    }
}
