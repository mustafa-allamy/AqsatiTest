﻿namespace Common.Responses
{
    public class ResponseError : IResponseError
    {
        public ResponseError(string? errorTypeCode, string message, string errorDetails = "")
        {
            ErrorCode = errorTypeCode ?? "";
            Message = message;
            DetailMessage = errorDetails;
        }
        public ResponseError(string message, string errorDetails = "")
        {
            ErrorCode = "0";
            Message = message;
            DetailMessage = errorDetails;
        }
        public string Message { get; set; }
        public string DetailMessage { get; set; }
        public string ErrorCode { get; set; }
        public override string ToString()
        {
            return Message;
        }
    }
}