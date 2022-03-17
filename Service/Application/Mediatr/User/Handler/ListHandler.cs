using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using LinqKit;
using MediatR;

namespace Application.Mediatr.User.Handler;

public class ListHandler : IRequestHandler<Request.List, IList<UserVM>>
{
    private readonly IDao<Domain.Entities.User> _user;
    
    public ListHandler(IDao<Domain.Entities.User> user) => _user = user;

    public async Task<IList<UserVM>> Handle(Request.List request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Domain.Entities.User>(true);
        if (!string.IsNullOrEmpty(request.Account)) predicate = predicate.And(u => u.Account.Contains(request.Account));

        return await _user.GetListAsync<UserVM>(predicate);
    }
}