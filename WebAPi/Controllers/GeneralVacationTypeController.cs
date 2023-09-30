using Application.CQRS.SystemGeneralInfo.GeneralVacation.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralVacationTypeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public GeneralVacationTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [HasPermission(PermissionsConst.GetGeneralVacationTypes, "عرض انواع الاجايز العامة")]
        public async Task<IActionResult> GetGeneralVacationTypes([FromQuery] GetGeneralVacationTypesForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }


        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetGeneralVacationType, "عرض نوع اجازة عامة")]
        public async Task<IActionResult> GetGeneralVacationType(int id)
        {
            var res = await _mediator.Send(new GetGeneralVacationTypeForm() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreateGeneralVacationType, "اضافة نوع اجازة عامة")]
        public async Task<IActionResult> CreateGeneralVacationType(CreateGeneralVacationTypeForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }
        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateGeneralVacationType, "تعديل نوع اجازة عامة")]
        public async Task<IActionResult> UpdateGeneralVacationType(int id, UpdateGeneralVacationTypeForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }

        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeleteGeneralVacationType, "حذف نوع اجازة عامة")]
        public async Task<IActionResult> DeleteGeneralVacationType(int id)
        {
            var res = await _mediator.Send(new DeleteGeneralVacationTypeForm() { Id = id });

            return Ok(res.ToSuccessClientResponse());

        }


        [HttpPost("{id:int}/ServiceRule")]
        [HasPermission(PermissionsConst.AddGeneralVacationTypeRule, "اضافة شرط اجازة عامة")]
        public async Task<IActionResult> AddGeneralVacationServiceRuleForm(int id, AddGeneralVacationServiceRuleForm cmd)
        {
            cmd.VacationTypeId = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());

        }


        [HttpDelete("{id:int}/ServiceRule/{ruleId:int}")]
        [HasPermission(PermissionsConst.DeleteGeneralVacationTypeRule, "حذف شرط اجازة عامة")]
        public async Task<IActionResult> DeleteGeneralVacationServiceRuleForm(int id, int ruleId)
        {
            var res = await _mediator.Send(new DeleteGeneralVacationServiceRuleForm() { Id = ruleId });

            return Ok(res.ToSuccessClientResponse());

        }
    }
}
