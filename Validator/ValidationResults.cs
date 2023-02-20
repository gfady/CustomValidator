namespace Validator
{
    public class ValidationResults
    {
        public List<Result> ValidationErrors { get; private set; } = new List<Result>();
        public List<Result> SucceedResults { get; private set; } = new List<Result>();

        public bool IsValid() => 
        !ValidationErrors.Any();

        public void AddResult(bool result, string message)
        {
            var currentResult = new Result(result, message);

            if (!result)
            {
                ValidationErrors.Add(currentResult);
            }

            else
            {
                SucceedResults.Add(currentResult);
            }

        }
    }
}
