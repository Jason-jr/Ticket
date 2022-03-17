using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Models;

public class RoleFuncitonJoin
{
    public Function F { get; set; }

    public RoleFunction RF { get; set; }
}

public class RoleFunctionVM : IMap
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<IGrouping<Function, RoleFuncitonJoin>, RoleFunctionVM>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Key.Name));
    }

    public string Name { get; set; }

    public string Route { get; set; }
}