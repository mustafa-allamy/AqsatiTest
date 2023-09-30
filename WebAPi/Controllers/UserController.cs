using Application.CQRS.Permission.Forms;
using Application.CQRS.User.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetUser, "عرض معلومات مستخدم")]
        public async Task<IActionResult> GetUser(int id)
        {
            var res = await _mediator.Send(new GetUserForm() { UserId = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetUsers, "عرض المستخدمين")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersForm query, int? departmentId)
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
        [HttpPost]
        [HasPermission(PermissionsConst.RegisterUser, "تسجيل مستخدم")]
        public async Task<IActionResult> Register(RegisterForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }
        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateUser, "تعديل مستخدم")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPut("{id:Int}/Permissions")]
        [HasPermission(PermissionsConst.UpdateUserPermissions, "تعديل صلاحيات مستخدم")]
        public async Task<IActionResult> AddUserPermissions(int id, UpdateUserPermissionsForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpGet("Units")]
        [HasPermission(PermissionsConst.GetUserUnits, "عرض اقسام مستخدم")]
        public async Task<IActionResult> GetUserUnits()
        {

            var res = await _mediator.Send(new GetUserUnitsForm() { UserId = HttpContext.GetCurrentUserIdFromToken() });

            return Ok(res.ToSuccessClientResponse());

        }
        [HttpPut("{id:Int}/Units")]
        [HasPermission(PermissionsConst.UpdateUserUnits, "تعديل اقسام مستخدم")]
        public async Task<IActionResult> UpdateUserUnits(int id, int? departmentId, AddRemoveUserUnitsForm cmd)
        {
            var userDepartmentId = HttpContext.GetDepartmentId();
            if (userDepartmentId != null)
            {
                departmentId = userDepartmentId.Value;
            }
            cmd.DepartmentId = departmentId;
            cmd.UserId = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }


        [HttpPut("{id:Int}/PermissionsGroup")]
        [HasPermission(PermissionsConst.AddUserPermissionsGroup, "اضافة مجموعة صلاحيات لمستخدم")]
        public async Task<IActionResult> AddUserPermissionsGroup(int id, AddPermissionGroupToUserForm cmd)
        {
            cmd.UserId = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }
        [HttpDelete("{id:Int}/PermissionsGroup")]
        [HasPermission(PermissionsConst.RemoveUserPermissionsGroup, "حذف مجموعة صلاحيات مستخدم")]
        public async Task<IActionResult> RemoveUserPermissionsGroup(int id, DeleteUserPermissionGroupForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }

    }
}
