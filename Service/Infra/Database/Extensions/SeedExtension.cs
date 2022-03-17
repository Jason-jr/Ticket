using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Database.Extensions;

public static class SeedExtension
{
    public static void SeedDefaultData(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<TicketDbContext>();

            if (!context.User.Any())
                context.User.AddRange(new[] {
                    new User { Account = "QA", Password = BCrypt.Net.BCrypt.HashPassword("qa") },
                    new User { Account = "RD", Password = BCrypt.Net.BCrypt.HashPassword("rd") },
                    new User { Account = "PM", Password = BCrypt.Net.BCrypt.HashPassword("pm") },
                    new User { Account = "admin", Password = BCrypt.Net.BCrypt.HashPassword("admin") }
                });

            if (!context.Role.Any())
                context.Role.AddRange(new[] {
                    new Role { Name = "QA" },
                    new Role { Name = "RD" },
                    new Role { Name = "PM" },
                    new Role { Name = "Administrator" },
                });

            if (!context.UserRole.Any())
                context.UserRole.AddRange(new[] {
                    new UserRole { UserId = 1, RoleId = 1 },
                    new UserRole { UserId = 2, RoleId = 2 },
                    new UserRole { UserId = 3, RoleId = 3 },
                    new UserRole { UserId = 4, RoleId = 4 },
                });

            if (!context.Function.Any())
                context.Function.AddRange(new[] {
                    new Function { Name = "Ticket" },
                    new Function { Name = "User" }
                });

            if (!context.RoleFunction.Any())
                context.RoleFunction.AddRange(new[] {
                    new RoleFunction { RoleId = 1, FunctionId = 1 },
                    new RoleFunction { RoleId = 2, FunctionId = 1 },
                    new RoleFunction { RoleId = 3, FunctionId = 1 },
                    new RoleFunction { RoleId = 4, FunctionId = 1 },
                    new RoleFunction { RoleId = 4, FunctionId = 2 },
                });

            if (!context.RoleTicketType.Any())
                context.RoleTicketType.AddRange(new[] {
                    new RoleTicketType { RoleId = 1, Type = TicketType.Bug, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = false },
                    new RoleTicketType { RoleId = 1, Type = TicketType.Test, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = true },
                    new RoleTicketType { RoleId = 2, Type = TicketType.Bug, CanCreate = false, CanUpdate = false, CanDelete = false, CanResolve = true },
                    new RoleTicketType { RoleId = 2, Type = TicketType.Feature, CanCreate = false, CanUpdate = false, CanDelete = false, CanResolve = true },
                    new RoleTicketType { RoleId = 3, Type = TicketType.Feature, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = false },
                    new RoleTicketType { RoleId = 4, Type = TicketType.Bug, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = true },
                    new RoleTicketType { RoleId = 4, Type = TicketType.Test, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = true },
                    new RoleTicketType { RoleId = 4, Type = TicketType.Feature, CanCreate = true, CanUpdate = true, CanDelete = true, CanResolve = true },
                });

            context.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}