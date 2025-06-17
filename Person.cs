namespace Persons
{
    public class Person
    {
        public int Id { get; init; }
        public DateOnly BirthDate { get; init; }
        public string FullName 
        {
            get
            {
                return string.Join(" ", _lastName, _firstName, _middleName);
            }
        }

        private string _lastName;
        private string _firstName;
        private string? _middleName;

        public Person(string fullName, DateOnly birthDate, int id)
        {
            Id = id;
            BirthDate = birthDate;
            var names = fullName.Split(' ').ToList();

            _lastName = names.FirstOrDefault() ?? string.Empty;

            if(string.IsNullOrEmpty(_lastName))
            {
                 throw new Exception("Вы не ввели фамилию");
            }

            _firstName = names.Skip(1).FirstOrDefault() ?? string.Empty;

            if(string.IsNullOrEmpty(_firstName))
            {
                throw new Exception("Вы не ввели имя");
            }

            _middleName = names?.Skip(2).FirstOrDefault();

            if (string.IsNullOrEmpty(_middleName))
            {
                _middleName = string.Empty;
            }

        }
    }
}