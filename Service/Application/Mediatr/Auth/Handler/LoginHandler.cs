using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;

namespace Application.Mediatr.Auth.Handler;

public class LoginHandler : IRequestHandler<Request.Login, int?>
{
    private readonly IDao<Domain.Entities.User> _user;

    public LoginHandler(IDao<Domain.Entities.User> user) => _user = user;

    public async Task<int?> Handle(Request.Login request, CancellationToken cancellationToken)
    {
        var user = await _user.GetFirstAsync(u => u.Account == request.Account && !u.Disable, u => u);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password)) return null;
        
        return user.Id;
    }
}