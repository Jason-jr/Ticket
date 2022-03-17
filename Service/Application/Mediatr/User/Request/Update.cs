using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;

namespace Application.Mediatr.User.Request;

public class Update : IRequest
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Password { get; set; }

    [Required]
    public bool Disable { get; set; }

    [Required]
    public IList<int> AddRoleIds { get; set; }

    [Required]
    public IList<int> DeleteRoleIds { get; set; }
}