using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Validator.Interfaces;

namespace Validator
{
    public class ValidationRule<T> : IValidationRule<T>
    {
        private List<Func<string, bool>> ruleComponents = new List<Func<string, bool>>();
        public readonly Expression<Func<T, string>> Property;

        public ValidationRule(Expression<Func<T, string>> property)
        {
            Property = property;
        }

        public void AddRuleComponent(Func<string, bool> ruleComponent)
        {
            ruleComponents.Add(ruleComponent);
        }

        public ValidationResults Validate(T entity)
        {
            var validationResults = new ValidationResults();

            Func<T, string> property = Property.Compile();

            var propertyToValidate = property(entity);

            foreach (var predicate in ruleComponents)
            {
                bool result = predicate(propertyToValidate);
                string methodName = new Regex(@"(\w+)").Match(predicate.Method.Name).Value;

                validationResults.AddResult(result, result ? $"Succeed: {Property.Parameters[0].Type} {Property.Body}='{propertyToValidate}' follows {methodName} rule"
                    : $"Failed: {Property.Parameters[0].Type} {Property.Body}='{propertyToValidate}' don't follows {methodName} rule");
            }
            return validationResults;
        }
    }
}
