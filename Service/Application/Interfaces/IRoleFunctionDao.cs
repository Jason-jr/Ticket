using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Domain.Entities;

namespace Application.Interfaces;

public interface IRoleFunctionDao : IDao<RoleFunction>
{
    Task<IList<RoleFunctionVM>> GetFunctionsAsync(int[] roleIds);
}