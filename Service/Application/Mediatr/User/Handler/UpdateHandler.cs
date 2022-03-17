using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.User.Handler;

public class UpdateHandler : IRequestHandler<Request.Update>
{
    private readonly IDao<Domain.Entities.User> _user;

    private readonly IDao<UserRole> _userRole;

    public UpdateHandler(IDao<Domain.Entities.User> user, IDao<UserRole> userRole)
    {
        _user = user;
        _userRole = userRole;
    }

    public async Task<Unit> Handle(Request.Update request, CancellationToken cancellationToken)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var entity = await _user.GetFirstAsync(u => u.Id == request.Id, u => u);

        if (!string.IsNullOrEmpty(request.Password))
            entity.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

        entity.Disable = request.Disable;
        await _user.UpdateAsync(entity);

        await _userRole.DeleteAsync(ur => ur.UserId == request.Id && request.DeleteRoleIds.Contains(ur.RoleId));
        await _userRole.AddEnumerableAsync(request.AddRoleIds.Select(r => new UserRole { UserId = request.Id, RoleId = r }));
        scope.Complete();
        return Unit.Value;
    }
}
