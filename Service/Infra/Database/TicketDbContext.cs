using System.ComponentModel.DataAnnotations.Schema;
using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database;

public class TicketDbContext : DbContext
{
    private readonly ICurrentUserService _currentUser;

    public TicketDbContext(DbContextOptions<TicketDbContext> options, ICurrentUserService currentUser) : base(options)
    {
        _currentUser = currentUser;
    }

    public DbSet<User> User { get; set; }

    public DbSet<Role> Role { get; set; }

    public DbSet<UserRole> UserRole { get; set; }

    public DbSet<Function> Function { get; set; }

    public DbSet<RoleFunction> RoleFunction { get; set; }

    public DbSet<Ticket> Ticket { get; set; }

    public DbSet<RoleTicketType> RoleTicketType { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().HasIndex(u => u.Account).IsUnique();
        builder.Entity<User>().Property(u => u.Account).IsRequired().HasMaxLength(50);
        builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(255);

        builder.Entity<Role>().HasKey(r => r.Id);
        builder.Entity<Role>().Property(r => r.Id).ValueGeneratedOnAdd();
        builder.Entity<Role>().Property(r => r.Name).IsRequired().HasMaxLength(15);

        builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Entity<Function>().HasKey(f => f.Id);
        builder.Entity<Function>().Property(f => f.Id).ValueGeneratedOnAdd();
        builder.Entity<Function>().Property(f => f.Name).IsRequired().HasMaxLength(50);

        builder.Entity<RoleFunction>().HasKey(rf => new { rf.RoleId, rf.FunctionId });

        builder.Entity<Ticket>().HasKey(t => t.Id);
        builder.Entity<Ticket>().Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Entity<RoleTicketType>().HasKey(rtt => new { rtt.RoleId, rtt.Type });
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedUser = _currentUser.UserId.GetValueOrDefault(0);
                    entry.Entity.CreatedTime = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedUser = _currentUser.UserId.GetValueOrDefault(0);
                    entry.Entity.UpdatedTime = DateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

}