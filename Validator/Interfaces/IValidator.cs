using System.Linq.Expressions;

namespace Validator.Interfaces
{
    public interface IValidator<T>
    {
        ValidationResults Validate(T item);
        Builder<T> RuleFor(Expression<Func<T, string>> expression);
    }
}
