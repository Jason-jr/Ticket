using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Auth.Request;

public class Functions : IRequest<IList<RoleFunctionVM>>
{
    public int[] RoleIds { get; set; }
}