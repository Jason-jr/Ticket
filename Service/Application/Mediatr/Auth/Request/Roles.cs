using System.Collections.Generic;
using MediatR;

namespace Application.Mediatr.Auth.Request;

public class Roles : IRequest<IList<int>>
{
    public int UserId { get; set; }
}