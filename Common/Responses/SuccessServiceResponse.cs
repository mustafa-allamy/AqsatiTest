namespace Common.Responses
{
    public class SuccessServiceResponse : IServiceResponse
    {

        public SuccessServiceResponse()
        {
            Succeeded = true;
        }
        public string? Message { get; set; }
        public bool Succeeded { get; internal set; }
        public int ItemsCount { get; set; } = 1;
        public bool IsPaginated => ItemsCount > 1;

    }

    public class SuccessServiceResponse<T> : SuccessServiceResponse, IServiceResponse<T>
    {
        public SuccessServiceResponse() : base() { }
        public T? Data { get; internal set; }
        public void SetSuccessResponse(T value, string? msg = null)
        {
            Message = msg;
            Data = value;
            Succeeded = true;
        }
    }
}