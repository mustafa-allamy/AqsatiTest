using Application.CQRS.Permission.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdatePermission, "تعديل صلاحية")]
        public async Task<IActionResult> UpdatePermission(int id, UpdatePermissionForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetPermissions, "عرض الصلاحيات")]
        public async Task<IActionResult> GetPermissions([FromQuery] GetPermissionsForm cmd)
        {
            var res = await _mediator.Send(cmd);
            return Ok(res.ToSuccessClientResponse());

        }
    }
}
