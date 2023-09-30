#nullable disable
namespace Common.Responses;

public class SuccessClientResponse<T> : ClientResponse
{
    public SuccessClientResponse()
    {
    }

    public SuccessClientResponse(string message = "Success", string details = "")
    {
        Error = false;
        Details = details;
        Message = message;
    }

    public SuccessClientResponse(T data, string message = "Success", int totalCount = 1)
    {
        Error = false;
        Message = message;
        Data = data;
        TotalCount = totalCount;
    }

    public T Data { get; set; }

}
