namespace Common.Responses
{
    public abstract class ClientResponse
    {
        public bool Error { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

        public int TotalCount { get; set; } = 1;

    }
}