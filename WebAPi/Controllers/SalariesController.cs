using Application.CQRS.SystemGeneralInfo.Salaries.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SalariesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetSalaries, "عرض سلم الراتب")]
        public async Task<IActionResult> GetSalaries()
        {
            var res = await _mediator.Send(new GetSalariesForm());

            return Ok(res.ToSuccessClientResponse());
        }
    }
}
