using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace DAL.Context;

public sealed class ContactsDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; } = null!;

    public ContactsDbContext(DbContextOptions options) : base(options) => Database.EnsureCreated();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        RelationalModelBuilderExtensions.UseCollation(modelBuilder, "utf8mb3_general_ci");
        
        modelBuilder.HasCharSet("utf8mb3");
        
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
    }
}