using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Database.Extensions;

public static class MigrationExtension
{
    public static void MigrateDbContext(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<TicketDbContext>();
            context.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}