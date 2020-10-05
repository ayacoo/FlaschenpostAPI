namespace Flaschenpost.Shared
{
    public class ExecutionResult<T> : ExecutionResult
    {
        public T Value { get; }

        public ExecutionResult(T value, string messae, bool isSuccess) : base(messae, isSuccess)
        {
            Value = value;
        }

    }


}
