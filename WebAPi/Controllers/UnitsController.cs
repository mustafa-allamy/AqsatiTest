using Application.CQRS.DepartmentInfo.DepartmentUnits.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UnitsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetUnits, "عرض اقسام دائرة")]
        public async Task<IActionResult> GetUnits(int? departmentId, [FromQuery] GetUnitsForm query)
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
        [HasPermission(PermissionsConst.GetUnit, "عرض قسم دائرة")]
        public async Task<IActionResult> GetUnit(int id, int? departmentId)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            var res = await _mediator.Send(new GetUnitForm() { Id = id, DepartmentId = departmentId });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreateUnit, "اضافة قسم دائرة")]
        public async Task<IActionResult> CreateUnit(int? departmentId, CreateUnitForm cmd)
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
        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateUnit, "تعديل قسم دائرة")]
        public async Task<IActionResult> UpdateUnit(int id, int? departmentId, UpdateUnitForm cmd)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            cmd.DepartmentId = departmentId;
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }

        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeleteUnit, "حذف قسم دائرة")]
        public async Task<IActionResult> DeleteUnit(int id, int? departmentId)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            var res = await _mediator.Send(new DeleteUnitForm() { Id = id, DepartmentId = departmentId });

            return Ok(res.ToSuccessClientResponse());

        }
    }
}
