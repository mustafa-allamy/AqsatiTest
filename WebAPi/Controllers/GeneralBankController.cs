using Application.CQRS.SystemGeneralInfo.GeneralBanks.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralBankController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeneralBankController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetGeneralBanks, "عرض المصارف")]
        public async Task<IActionResult> GetGeneralBanks([FromQuery] GetGeneralBanksForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }


        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetGeneralBank, "عرض المصرف")]
        public async Task<IActionResult> GetGeneralBank(int id)
        {
            var res = await _mediator.Send(new GetGeneralBankForm() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpPost]
        [HasPermission(PermissionsConst.CreateGeneralBank, "اضافة مصرف")]
        public async Task<IActionResult> CreateGeneralBank(CreateGeneralBankForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }
        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateGeneralBank, "تعديل المصرف")]
        public async Task<IActionResult> UpdateGeneralBank(int id, UpdateGeneralBankForm cmd)
        {
            cmd.Id = id;
            var res = await _mediator.Send(cmd);

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeleteGeneralBank, "حذف المصرف")]
        public async Task<IActionResult> DeleteGeneralBank(int id)
        {
            var res = await _mediator.Send(new DeleteGeneralBankFrom() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }

    }
}
