using Usage.Models;
using Validator;
using Xunit;

namespace ValidatorTests
{
    public class Tests
    {

        [Fact]
        public void Validate_WhenAnyRulesWhereDefined_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903@gmail.com", "");
            var validator = new Validator<Person>();

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.NotNull(validationResults);
            Assert.False(validationResults.IsValid());
            Assert.Empty(validationResults.SucceedResults);
            Assert.Single(validationResults.ValidationErrors);
        }

        [Fact]
        public void Validate_WhenNullWasPassed_ShouldReturnValidationError()
        {
            //Arrange
            Person person = null;
            var validator = new Validator<Person>();

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.NotNull(validationResults);
            Assert.False(validationResults.IsValid());
            Assert.Empty(validationResults.SucceedResults);
            Assert.Single(validationResults.ValidationErrors);
        }

        [Fact]
        public void Validate_WhenAllPropertiesAreValid_ShouldntReturnValidationErrors()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903@gmail.com", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).NotEmpty().NotNull().Matches("name").MinLength(2).MaxLength(5).Build
                .RuleFor(person => person.Surname).CheckIf(surname => surname.Equals("surname")).Build
                .RuleFor(person => person.Email).EmailAdress().NotEmpty().Equal("dash130903@gmail.com").Build
                .RuleFor(person => person.Adress).Null().Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.NotNull(validationResults);
            Assert.Empty(validationResults.ValidationErrors);
            Assert.NotNull(validationResults.SucceedResults);
            Assert.Equal(10, validationResults.SucceedResults.Count);
            Assert.True(validationResults.IsValid());
        }

        [Fact]
        public void Validate_WhenASomePropertiesAreInValid_ShouldReturnValidationErrors()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903@gmail.com", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).NotEmpty().NotNull().Matches("name").MinLength(2).MaxLength(3).Build
                .RuleFor(person => person.Surname).CheckIf(surname => surname.Equals("surname1")).Build
                .RuleFor(person => person.Email).EmailAdress().NotEmpty().Equal("dash13090").Build
                .RuleFor(person => person.Adress).NotNull().Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.NotNull(validationResults);
            Assert.Equal(4, validationResults.ValidationErrors.Count);
            Assert.NotNull(validationResults.SucceedResults);
            Assert.Equal(6, validationResults.SucceedResults.Count);
            Assert.False(validationResults.IsValid());
        }

        [Fact]
        public void Validate_WhenAllEmailPropertyIsInvalid_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Email).EmailAdress().Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenPropertyIsNull_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Adress).NotNull().Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenPropertyIsNotEqual_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).Equal("name1").Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenDontFollowsCheckIfConditions_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).CheckIf(name=> name.Equals("name1")).Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenPropertyDontMatch_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).Matches("^[0-9\\-]+$").Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenMaxAndMinLengthAreInvalid_ShouldReturnValidationErrors()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).MinLength(8).Build
                .RuleFor(person => person.Surname).MaxLength(5).Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Equal(2, validationResults.ValidationErrors.Count);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenNotEmpty_ShouldReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).Empty().Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.False(validationResults.IsValid());
            Assert.Single(validationResults.ValidationErrors);
            Assert.Empty(validationResults.SucceedResults);
        }

        [Fact]
        public void Validate_WhenNotEqual_ShouldntReturnValidationError()
        {
            //Arrange
            var person = new Person("name", "surname", null, "dash130903", "");
            var validator = new Validator<Person>()
                .RuleFor(person => person.Name).NotEqual("name111").Build;

            //Act
            var validationResults = validator.Validate(person);

            //Assert
            Assert.True(validationResults.IsValid());
            Assert.Empty(validationResults.ValidationErrors);
            Assert.Single(validationResults.SucceedResults);
        }

    }
}