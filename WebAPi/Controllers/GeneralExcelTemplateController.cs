using Application.CQRS.SystemGeneralInfo.ExcelTemplate.Forms;
using Common.Extensions;
using Infrastructure.Authentication;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WebAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralExcelTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeneralExcelTemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        [HasPermission(PermissionsConst.CreateGeneralExcelTemplate, "انشاء قالب اكسل عام")]
        public async Task<IActionResult> CreateGeneralExcelTemplate(CreateGeneralExcelTemplateForm cmd)
        {
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpGet]
        [HasPermission(PermissionsConst.GetGeneralExcelTemplates, "عرض قوالب الاكسل العامة")]
        public async Task<IActionResult> GetGeneralExcelTemplates([FromQuery] GetGeneralExcelTemplatesForm query)
        {
            var res = await _mediator.Send(query);

            return Ok(res.ToSuccessClientResponse());
        }


        [HttpGet("{id:int}")]
        [HasPermission(PermissionsConst.GetGeneralExcelTemplate, "عرض قالب الاكسل العام")]
        public async Task<IActionResult> GetGeneralExcelTemplate(int id)
        {
            var res = await _mediator.Send(new GetGeneralExcelTemplateForm() { Id = id });

            return res.Match<IActionResult>(
                success => Ok(success.ToSuccessClientResponse()),
                fail => BadRequest(fail.ToFailClientResponse()));
        }
        [HttpDelete("{id:int}")]
        [HasPermission(PermissionsConst.DeleteGeneralExcelTemplate, "حذف قالب الاكسل العام")]
        public async Task<IActionResult> DeleteGeneralExcelTemplate(int id)
        {
            var res = await _mediator.Send(new DeleteGeneralExcelTemplateForm() { Id = id });

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPatch("{id:int}")]
        [HasPermission(PermissionsConst.UpdateGeneralExcelTemplate, "تعديل قالب الاكسل العام")]
        public async Task<IActionResult> UpdateGeneralExcelTemplate(int id, UpdateGeneralExcelTemplateForm form)
        {
            form.Id = id;
            var res = await _mediator.Send(form);

            return Ok(res.ToSuccessClientResponse());
        }

        [HttpPut("{id:int}/Services")]
        [HasPermission(PermissionsConst.AddGeneralExcelTemplateServices, "اضافة حدمات لقالب اكسل عام")]
        public async Task<IActionResult> AddGeneralExcelTemplateServices(int id, AddGeneralExcelTemplateServicesForm cmd)
        {
            cmd.GeneralExcelTemplateId = id;
            var res = await _mediator.Send(cmd);

            return Ok(res.ToSuccessClientResponse());
        }
    }
}
