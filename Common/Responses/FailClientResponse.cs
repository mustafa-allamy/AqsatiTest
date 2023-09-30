namespace Common.Responses
{
    public class FailClientResponse : ClientResponse
    {
        public ICollection<ResponseError> ResponseErrors { get; set; }

        public FailClientResponse()
        {
        }

        public FailClientResponse(string message = "Fail", string details = "")
        {
            Error = true;
            Details = details;
            Message = message;
        }

        public FailClientResponse(ICollection<ResponseError> responseErrors, string message = "Fail", string details = "")
        {
            Error = true;
            ResponseErrors = responseErrors;
            TotalCount = responseErrors.Count;
            Details = details;
            Message = message;
        }
    }
}