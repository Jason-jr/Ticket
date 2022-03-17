using Application.Interfaces;
using Domain.Entities;

namespace Application.Models;

public class RoleVM : IMap<Role>
{
    public int Id { get; set; }

    public string Name { get; set; }
}