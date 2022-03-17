namespace Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }

    public bool Disable { get; set; }
}