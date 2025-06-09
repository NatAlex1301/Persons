namespace Persons
{
    public class PersonsRepository
    {
        private readonly List<Person> _persons = new();

        public Person? FindPerson(int id)
        {
            return _persons.FirstOrDefault(x => x.Id == id);
        }

        public List<Person> FindPersons()
        { 
            return new(_persons);
        }

        public bool DeletePerson(int id)
        {
            var personToDelete = FindPerson(id) ??
                throw new Exception($"Человек с идентификатором {id} не найден");
            _persons.Remove(personToDelete);

            return true;
        }

        public Person CreatePerson(string fullName,string birthDate)
        {
            var id = _persons.Count + 1;
            var person = new Person(fullName, birthDate, id);
            _persons.Add(person);

            return person;
        }
    }
}

