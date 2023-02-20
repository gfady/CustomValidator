namespace Validator.Interfaces
{
    public interface IBuilder <T>
    {
        IValidationRule<T> GetRule();
    }
}
