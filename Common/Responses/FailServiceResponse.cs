namespace Common.Responses
{
    public class FailServiceResponse : IServiceResponse
    {

        private readonly List<IResponseError> _errors = new();
        public FailServiceResponse()
        {
            Succeeded = false;
        }
        public IReadOnlyCollection<IResponseError> Errors => _errors.AsReadOnly();
        public string? Message { get; set; }
        public bool Succeeded { get; }
        public int ItemsCount { get; }



        public bool Failed => !Succeeded;


        public void AddError(string errorMessage)
        {
            _errors.Add(new ResponseError(errorMessage));
        }
        public void AddErrors(IEnumerable<IResponseError> errors)
        {
            _errors.AddRange(errors);
        }
        public void AddError(string errorCode, string errorMessage, string errorDetails)
        {
            _errors.Add(new ResponseError(errorCode, errorMessage, errorDetails));
        }
    }



}