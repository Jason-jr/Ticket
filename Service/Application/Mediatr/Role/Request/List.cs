using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Role.Request;

public class List : IRequest<IList<RoleVM>>
{
    
}