using System.Linq;

namespace Persons
{
    public class Person
    {
        public int Id { get; init; }
        public string FullName { get; init; }
        public DateOnly BirthDate { get; init; }

        private string? _lastName;
        private string? _firstName;
        private string? _middleName;

        public Person(string fullName, DateOnly birthDate, int id)
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;

            string[] names = fullName.Split(' ');
            var nameList = names.ToList();
            _lastName = nameList.FirstOrDefault();

            if(String.IsNullOrEmpty(_lastName))
            {
                 throw new Exception("Вы не ввели фамилию");
            }

            _firstName = nameList.Skip(1).FirstOrDefault();

            if(String.IsNullOrEmpty(_firstName))
            {
                throw new Exception("Вы не ввели имя");
            }

            _middleName = nameList.Skip(2).FirstOrDefault();

            if (String.IsNullOrEmpty(_middleName))
            {
                _middleName = string.Empty;
            }
        }
    }
}