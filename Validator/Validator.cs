using System.Linq.Expressions;
using Validator.Interfaces;

namespace Validator
{
    public class Validator<T> : IValidator<T>
    {
        private List<ValidationRule<T>> rules = new List<ValidationRule<T>>();

        public Builder<T> RuleFor(Expression<Func<T, string>> expression)
        {
            var rule = new ValidationRule<T>(expression);
            rules.Add(rule);
            return new Builder<T>(rule, this);
        }

        public ValidationResults Validate(T entity)
        {
            ValidationResults results = new ValidationResults();

            if (entity == null)
            {
                results.ValidationErrors.Add(new Result(false, "ValidationError: Passed object is null"));
                return results;
            }

            if (rules.Count == 0)
            {
                results.ValidationErrors.Add(new Result(false, "ValidationError: No rules are defined for this validator"));
                return results;
            }

            foreach (var rule in rules)
            {
                var currentValidationResults = rule.Validate(entity);

                if (currentValidationResults.ValidationErrors.Any())
                {
                    results.ValidationErrors.AddRange(currentValidationResults.ValidationErrors);
                }

                if (currentValidationResults.SucceedResults.Any())
                {
                    results.SucceedResults.AddRange(currentValidationResults.SucceedResults);
                }

            }

            return results;
        }
    }
}