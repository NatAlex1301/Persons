
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using SendGrid.Helpers.Errors.Model;


internal class Program
{
    public static List<Person> Persons { get; private set; } = new();

    public static (string fullName,string birthDate) GetPerson()

    {
        Console.WriteLine("Введите ФИО:");
        var fullName = Console.ReadLine();
        Console.WriteLine("Введите дату рождения:");
        var birthDate = Console.ReadLine();
        
        return(fullName,birthDate);
    }
    public static void CreatePerson(string fullName,string birthDate)
        
    {
        var id = Persons.Count+1;
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
        try
        {
            var personToDelete = Persons.FirstOrDefault(x => x.Id == id) ??

                throw new NotFoundException($"Человек с идентификатором {id} не найден");
            Persons.Remove(personToDelete);
            Console.WriteLine("Данные удалены");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        Console.ReadLine();
        
        
    }

        

    private static void Main(string[] args)
    {
        Console.WriteLine
            ("""
            Для ввода данных введите команду /new
            Для просмотра данных введите команду /show
            Для остановки приложения введите команду /stop
            Для удаления данных введите команду /delete            
            """);

        while (true)
        {
            string command = Console.ReadLine();

            switch (command)
            {
                case "/new":
                    
                    var (fullName,birthDate)= GetPerson();
                    CreatePerson(fullName,birthDate);

                    Console.WriteLine("Введите следующую команду:");
                    continue;


                case "/show":
                    PrintPersons();
                    Console.WriteLine("Введите следующую команду:");
                    continue;

                case "/stop":
                    break;


                case "/delete":                                       
                                                            
                    Console.WriteLine("Введите ID для удаления:");
                    string input = Console.ReadLine();

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
            break;

        }
    }
       



    

}
    
    

