namespace Validator.Interfaces
{
    public interface IValidationRule<T>
    {
        void AddRuleComponent(Func<string, bool> ruleComponent);
        ValidationResults Validate(T entity);
    }
}
