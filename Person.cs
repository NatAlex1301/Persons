public class Person
{
    public int Id { get; init; }
    public string FullName { get; init; }
    public string BirthDate { get; init; }
    public Person(string fullName, string birthDate,int id)
    {
        Id = id;
        FullName = fullName;
        BirthDate = birthDate;
    }    
}
