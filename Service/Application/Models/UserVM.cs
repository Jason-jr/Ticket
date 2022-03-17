using System.Collections.Generic;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Models;

public class UserVM : IMap<User>
{
    /// <summary>
    /// user id
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// user account
    /// </summary>
    /// <value></value>
    public string Account { get; set; }

    /// <summary>
    /// user status
    /// </summary>
    /// <value></value>
    public bool Disable { get; set; }
}

public class UserDetailVM : IMap<User>
{
    public int Id { get; set; }

    public string Account { get; set; }

    public bool Disable { get; set; }

    public IList<int> RoleIds { get; set; }
}