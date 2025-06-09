using Microsoft.Extensions.Hosting;

namespace Persons
{
    public class Worker() : BackgroundService
    {
        PersonsRepository personsRepository = new();
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
                try
                {
                    switch (command)
                    {
                        case "/new":
                            var (fullName, birthDate) = ReadPersonDataFromConsole();
                            personsRepository.CreatePerson(fullName, birthDate);
                            continue;

                        case "/show":
                         var _persons = personsRepository.FindPersons();
                            PrintPersons(_persons);
                            continue;

                        case "/stop":
                            break;

                        case "/delete":
                           int id = ReadPersonsId();
                           personsRepository.FindPerson(id);
                           personsRepository.DeletePerson(id);
                            continue;

                        default:
                            Console.WriteLine("Вы ввели неверную команду");
                            continue;
                    }
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
                throw new Exception("Нет данных");
            }

            return (fullName, birthDate);
        }
        public static void PrintPersons(List<Person>_persons)
        {
            foreach (var person in _persons)
            {
                Console.WriteLine("ID:" + person.Id);
                Console.WriteLine(person.FullName);
                Console.WriteLine(person.BirthDate);
            }
        }
        public static int ReadPersonsId()
        {
            Console.WriteLine("Введите ID для удаления:");
            string? input = Console.ReadLine();
            if (!int.TryParse(input, out int id))
            {
                throw new Exception("Введены некорректные данные");
            }

            return id;
        }
    }
}

