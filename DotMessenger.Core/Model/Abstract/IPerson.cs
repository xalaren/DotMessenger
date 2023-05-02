namespace DotMessenger.Core.Model.Abstract
{
    public interface IPerson
    {
        string Name { get; set; }
        string Lastname { get; set; }
        DateTimeOffset BirthDate { get; set; }
        string PhoneNumber { get; set; }
    }
}

