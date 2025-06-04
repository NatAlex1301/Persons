using Microsoft.Extensions.Hosting;
using SendGrid.Helpers.Errors.Model;

namespace Persons
{
    public class Worker() : BackgroundService
    {
        public static List<Person> Persons { get; private set; } = new();
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine
                         ("""
                          Для ввода данных введите команду /new
                          Для просмотра данных введите команду /show
                          Для остановки приложения введите команду /stop
                          Для удаления данных введите команду /delete            
                          """);

            while (!stoppingToken.IsCancellationRequested)
            {
                string? command = Console.ReadLine();
                if (command == null)
                {
                    Console.WriteLine("Нет данных");
                    continue;
                }
                try
                {
                    switch (command)
                    {
                        case "/new":
                            var (fullName, birthDate) = ReadPersonDataFromConsole();
                            CreatePerson(fullName, birthDate);
                            continue;

                        case "/show":
                            PrintPersons();
                            continue;

                        case "/stop":
                            break;

                        case "/delete":
                            Console.WriteLine("Введите ID для удаления:");
                            string? input = Console.ReadLine();
                            if (!int.TryParse(input, out int id))
                            {
                                Console.WriteLine("Введены некорректные данные");
                                continue;
                            }

                            RemovePerson(id);
                            continue;

                        default:
                            Console.WriteLine("Вы ввели неверную команду");
                            continue;
                    }
                    continue;
                    throw new Exception("Нет данных");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }

            return Task.CompletedTask;
        }
        public static (string fullName, string birthDate) ReadPersonDataFromConsole()
        {
            Console.WriteLine("Введите ФИО:");
            var fullName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения:");
            var birthDate = Console.ReadLine();
            if (String.IsNullOrEmpty(fullName) || String.IsNullOrEmpty(birthDate))
            {
                Console.WriteLine("Нет данных");
                throw new Exception();
            }

            return (fullName, birthDate);
        }
        public static void CreatePerson(string fullName, string birthDate)
        {
            var id = Persons.Count + 1;
            var person = new Person(fullName, birthDate, id);
            Persons.Add(person);
        }
        public static void PrintPersons()
        {
            foreach (var person in Persons)
            {
                Console.WriteLine("ID:" + person.Id);
                Console.WriteLine(person.FullName);
                Console.WriteLine(person.BirthDate);
            }
        }
        public static void RemovePerson(int id)
        {
            var personToDelete = Persons.FirstOrDefault(x => x.Id == id) ??
                throw new NotFoundException($"Человек с идентификатором {id} не найден");
            Persons.Remove(personToDelete);
            Console.WriteLine("Данные удалены");
        }
    }
}

