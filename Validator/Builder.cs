using System.Text.RegularExpressions;
using Validator.Interfaces;

namespace Validator
{
    public class Builder<T> : IBuilder<T>
    {
        private readonly IValidationRule<T> rule;
        public Validator<T> Build { get; }

        public Builder(IValidationRule<T> rule, Validator<T> ParentValidator)
        {
            this.rule = rule;
            Build = ParentValidator;
        }

        public IValidationRule<T> GetRule() => rule;

        public Builder<T> NotNull()
        {
            rule.AddRuleComponent((x) => x != null);

            return this;
        }

        public Builder<T> NotEmpty()
        {
            rule.AddRuleComponent((x) => x != null && x != "");

            return this;
        }

        public Builder<T> Empty()
        {
            rule.AddRuleComponent((x) => x !=null && x == "");

            return this;
        }

        public Builder<T> Null()
        {
            rule.AddRuleComponent((x) => x == null);

            return this;
        }

        public Builder<T> NotEqual(string compareWith)
        {
            rule.AddRuleComponent((x) => x != null && !x.Equals(compareWith));

            return this;
        }

        public Builder<T> Equal(string compareWith)
        {
            rule.AddRuleComponent((x) => x!=null && x.Equals(compareWith));

            return this;
        }

        public Builder<T> MaxLength(int maxLength)
        {
            rule.AddRuleComponent((x) => x != null && x.Length <= maxLength);

            return this;
        }

        public Builder<T> MinLength(int minLength)
        {
            rule.AddRuleComponent((x) => x != null && x.Length >= minLength);

            return this;
        }

        public Builder<T> CheckIf(Func<string, bool> expression)
        {
            rule.AddRuleComponent((x) => x != null && expression(x));

            return this;
        }

        public Builder<T> Matches(string pattern)
        {
            Regex regex = new Regex(pattern);
            rule.AddRuleComponent((x) => x != null && regex.IsMatch(x));

            return this;
        }

        public Builder<T> EmailAdress()
        {
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z");
            rule.AddRuleComponent((x) => x != null && regex.IsMatch(x));

            return this;
        }
    }
}

