using System;

namespace Flaschenpost.Shared
{
    public class ExecutionResult
    {
        public string Message { get; }
        public bool IsSuccess { get; }

        protected ExecutionResult(string messae, bool isSuccess)
        {
            Message = messae;
            IsSuccess = isSuccess;
        }

        public static ExecutionResult<T> Error<T>(Exception ex,T value = default)
        {
            return new ExecutionResult<T>(value, ex.Message, false);
        }

        public static ExecutionResult<T> Error<T>(T value,string message)
        {
            return new ExecutionResult<T>(default, message, false);
        }

        public static ExecutionResult<T> Success<T>(T value, string message = "")
        {
            return new ExecutionResult<T>(value, message, true);
        }
    }


}
