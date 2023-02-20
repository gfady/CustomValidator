namespace Validator
{
    public class Result
    {
        public readonly bool result;
        public readonly string message;
        public Result(bool success, string message)
        {
            this.result = success;
            this.message = message;
        }
    }
}
