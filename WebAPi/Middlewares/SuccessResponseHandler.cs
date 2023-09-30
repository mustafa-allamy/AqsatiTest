using Common.Responses;
using LazZiya.ExpressLocalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPi.Middlewares
{
    public class SuccessResponseHandler : IResultFilter
    {
        private readonly ISharedCultureLocalizer _localizer;

        public SuccessResponseHandler(
            ISharedCultureLocalizer localizer)
        {
            _localizer = localizer;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult result &&
                result.Value is SuccessClientResponse<object> dto)
            {
                dto.Message = _localizer[dto.Message];
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Do nothing
        }
    }
}
