namespace Web.Models;

public class UserDetailViewModel
{
    public int? Id { get; set; }

    public UserModel User { get; set; }
}

public class UserModel
{
    public string Account { get; set; }

    public string Password { get; set; }

    public bool Disable { get; set; }

    public IList<int> RoleIds { get; set; } = new List<int>();
}