using System.Collections.Generic;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Mediatr.User.Request;

public class List : IRequest<IList<UserVM>>
{
    [FromQuery]
    public string Account { get; set; }
}