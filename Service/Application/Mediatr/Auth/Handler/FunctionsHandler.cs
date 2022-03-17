using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.Mediatr.Auth.Handler;

public class FunctionsHandler : IRequestHandler<Request.Functions, IList<RoleFunctionVM>>
{
    private readonly IRoleFunctionDao _roleFunction;

    public FunctionsHandler(IRoleFunctionDao roleFunction) => _roleFunction = roleFunction;
    
    public async Task<IList<RoleFunctionVM>> Handle(Request.Functions request, CancellationToken cancellationToken)
    {
        return await _roleFunction.GetFunctionsAsync(request.RoleIds);
    }
}