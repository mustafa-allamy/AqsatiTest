using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultExcelTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DefaultExcelTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [HasPermission(PermissionsConst.GetDefaultExcelTemplateColumns, "عرض اعمدة الاكسل الاساسية")]
        public async Task<IActionResult> GetGovernorates([FromQuery] GetDefaultExcelTemplateForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }
    }
}
