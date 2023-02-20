using Usage.Models;
using Validator;

var model = new Person("name", "ttt", null, "dash130903@gmail.com", "");

Validator<Person> validator = new Validator<Person>()
    .RuleFor(p => p.Name).MaxLength(3).MinLength(2).NotNull().Build
    .RuleFor(p => p.Surname).NotNull().MaxLength(1).CheckIf(surname => surname.Equals("ywg")).Build
    .RuleFor(p => p.Adress).NotNull().MaxLength(6).Matches("^[a-z0-9\\-]+$").Build
    .RuleFor(p => p.Specialization).NotEmpty().NotNull().NotEqual("spec").Equal("uu").Build
    .RuleFor(p => p.Email).EmailAdress().Build;

var result = validator.Validate(model);

foreach (var res in result.SucceedResults)
{
    Console.WriteLine(res.message);
}

foreach (var res in result.ValidationErrors)
{
    Console.WriteLine(res.message);
}

Console.ReadLine();