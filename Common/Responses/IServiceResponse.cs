namespace Common.Responses
{
    public interface IServiceResponse
    {
        string? Message { get; }
        bool Succeeded { get; }
        int ItemsCount { get; }
    }

    public interface IServiceResponse<out T> : IServiceResponse
    {
        T? Data { get; }
    }

}