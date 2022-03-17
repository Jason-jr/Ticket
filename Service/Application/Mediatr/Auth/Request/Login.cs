using MediatR;

namespace Application.Mediatr.Auth.Request;

public class Login : IRequest<int?>
{
    /// <summary>
    /// 帳號
    /// </summary>
    /// <value></value>
    public string Account { get; set; }

    /// <summary>
    /// 密碼
    /// </summary>
    /// <value></value>
    public string Password { get; set; }
}