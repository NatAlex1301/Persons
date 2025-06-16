using Microsoft.Extensions.Hosting;

namespace Persons
{
    public class Worker() : BackgroundService
    {
        private readonly PersonsRepository _personsRepository = new();

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
                            var fullName = ReadFullNameFromConsole();
                            var birthDate = ReadBirthDateFromConsole();
                            _personsRepository.CreatePerson(fullName, birthDate);
                            continue;

                        case "/show":
                            var persons = _personsRepository.FindPersons();
                            PrintPersons(persons);
                            continue;

                        case "/stop":
                            break;

                        case "/delete":
                            int id = ReadPersonsId();
                            _personsRepository.DeletePerson(id);
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

        public static string ReadFullNameFromConsole()
        {
            Console.WriteLine("Введите ФИО:");
            var fullName = Console.ReadLine();

            if (String.IsNullOrEmpty(fullName))
            {
                throw new Exception("Нет данных");
            }

            return fullName;

        }

        public static DateOnly ReadBirthDateFromConsole()
        {
            Console.WriteLine("Введите дату рождения в формате: ДД.ММ.ГГГГ");
            var input = Console.ReadLine();

            if (!DateOnly.TryParse(input, out DateOnly birthDate))
            {
                throw new Exception("Нет данных или неверный формат даты");
            }

            return birthDate;
        }
        public static void PrintPersons(List<Person> persons)
        {
            foreach (var person in persons)
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

