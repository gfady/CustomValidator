namespace Usage.Models
{
    public class Person
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string? Specialization { get; set; }

        public Person(string name, string surname, string adress, string email, string specialization)
        {
            Name = name;
            Surname = surname;
            Adress = adress;
            Email = email;
            Specialization = specialization;
        }
    }
}
