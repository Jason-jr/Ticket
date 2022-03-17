using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Mediatr.User.Request;

public class Detail : IRequest<UserDetailVM>
{
    [FromRoute]
    public int Id { get; set; }
}