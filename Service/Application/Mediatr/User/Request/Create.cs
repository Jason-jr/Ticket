using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Mediatr.User.Request;

public class Create : IRequest<int>
{
    /// <summary>
    /// 帳號
    /// </summary>
    /// <value></value>
    [Required]
    public string Account { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    /// <value></value>
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// 是否停用
    /// </summary>
    /// <value></value>
    [Required]
    public bool Disable { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    /// <value></value>
    public IList<int> AddRoleIds { get; set; }
}