using Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class Result<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; init; }
        public ErrorStatus ErrorStatus { get; init; }
        public T Data { get; init; }
        public Dictionary<string, string[]>? Errors { get; set; }
        public static Result<T> Success(T data, string message = "")
            => new() {  Data = data, IsSuccess = true,  Message = message, ErrorStatus = ErrorStatus.NoError};
        public static Result<T> Failure(string message, ErrorStatus err)
            => new() { IsSuccess = false, Message = message,  ErrorStatus = err };


        public static Result<T> ValidationFailure(Dictionary<string, string[]> errors)
        => new Result<T>()
        {
            IsSuccess = false,
            Message = "Please correct the highlighted fields and try again.",
            Errors = errors,
            ErrorStatus = ErrorStatus.ValidationError
        };
    }
}
