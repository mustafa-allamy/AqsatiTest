using Common.Responses;
using FluentValidation.Results;

namespace Common.Extensions
{
    public static class ResponseExtension
    {

        public static FailServiceResponse WithMessage(this FailServiceResponse serviceResponse, string message)
        {
            serviceResponse.Message = message;
            return serviceResponse;
        }
        public static FailServiceResponse WithError(this FailServiceResponse serviceResponse, string error)
        {
            serviceResponse.AddError(error);
            return serviceResponse;
        }

        public static FailServiceResponse WithError(this FailServiceResponse serviceResponse, string errorCode, string errorMessage, string errorDetails)
        {
            serviceResponse.AddError(errorCode, errorMessage, errorDetails);
            return serviceResponse;
        }

        public static FailServiceResponse WithErrors(this FailServiceResponse serviceResponse, List<ResponseError> errors)
        {
            serviceResponse.AddErrors(errors);
            return serviceResponse;
        }
        public static FailClientResponse ToFailClientResponse(this FailServiceResponse serviceResponse)
        {
            return new FailClientResponse(
                serviceResponse.Errors.Select(x => new ResponseError(x.ErrorCode, x.Message, x.DetailMessage)).ToList(), serviceResponse.Message ?? "");
        }



        public static SuccessServiceResponse WithMessage(this SuccessServiceResponse serviceResponse, string message)
        {
            serviceResponse.Message = message;
            return serviceResponse;
        }
        public static SuccessServiceResponse<T> WithMessage<T>(this SuccessServiceResponse<T> serviceResponse, string message)
        {
            serviceResponse.Message = message;
            return serviceResponse;
        }

        public static SuccessServiceResponse<T> WithData<T>(this SuccessServiceResponse<T> serviceResponse, T data)
        {
            serviceResponse.Data = data;
            return serviceResponse;
        }
        public static SuccessClientResponse<T> ToSuccessClientResponse<T>(this SuccessServiceResponse<T> serviceResponse)
        {
            return new SuccessClientResponse<T>(serviceResponse.Data, message: serviceResponse.Message,
                serviceResponse.ItemsCount);
        }
        public static SuccessClientResponse<bool> ToSuccessClientResponse(this SuccessServiceResponse serviceResponse)
        {
            return new SuccessClientResponse<bool>(serviceResponse.Succeeded, message: serviceResponse.Message,
                serviceResponse.ItemsCount);
        }
        public static SuccessServiceResponse<T> WithCount<T>(this SuccessServiceResponse<T> serviceResponse, int count)
        {
            serviceResponse.ItemsCount = count;
            return serviceResponse;
        }

        public static List<ResponseError> ToResponseErrors(this List<ValidationFailure> validationErrors)
        {
            return validationErrors.Select(x => new ResponseError("", x.PropertyName, x.ErrorMessage)).ToList();
        }



    }
}