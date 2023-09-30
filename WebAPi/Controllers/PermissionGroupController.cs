using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetPermissionGroups, "عرض مجاميع الصلاحيات")]
        public async Task<IActionResult> GetPermissionGroups([FromQuery] GetPermissionsGroupsForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }
        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetPermissionGroup, "عرض مجموعة صلاحيات")]
        public async Task<IActionResult> GetPermissionGroup(int id)
        {
            var res = await _mediator.Send(new GetPermissionGroupForm() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreatePermissionGroup, "انشاء مجموعة صلاحيات")]
        public async Task<IActionResult> CreatePermissionGroup(CreatePermissionGroupForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }
        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdatePermissionGroup, "تعديل مجموعة صلاحيات")]
        public async Task<IActionResult> UpdatePermissionGroup(int id, UpdatePermissionGroupForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeletePermissionGroup, "حذف مجموعة صلاحيات")]
        public async Task<IActionResult> DeletePermissionGroup(int id)
        {
            var res = await _mediator.Send(new DeletePermissionGroupFrom() { Id = id });

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPatch("{id:int}/Permissions")]
        [HasPermission(PermissionsConst.UpdateGroupPermissions, "تعديل صلاحيات مجموعة")]
        public async Task<IActionResult> UpdateGroupPermissions(int id, UpdateGroupPermissionsForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

    }
}
