using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.User.Handler;

public class CreateHandler : IRequestHandler<Request.Create, int>
{
    private readonly IDao<Domain.Entities.User> _user;

    private readonly IDao<UserRole> _userRole;

    public CreateHandler(IDao<Domain.Entities.User> user, IDao<UserRole> userRole)
    {
        _user = user;
        _userRole = userRole;
    }

    public async Task<int> Handle(Request.Create request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var entity = await _user.AddAsync(new Domain.Entities.User
        {
            Account = request.Account,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Disable = request.Disable
        });

        await _userRole.AddEnumerableAsync(request.AddRoleIds.Select(r => new UserRole { UserId = entity.Id, RoleId = r }));
        scope.Complete();
        return entity.Id;
    }
}