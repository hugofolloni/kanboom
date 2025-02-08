using Kanboom.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Kanboom.Context;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

    }

    public DbSet<User>? User { get; set; }
    
    protected override void OnModelCreating(ModelBuilder mb){
        mb.Entity<User>().HasKey(x => x.Id);
        mb.Entity<User>().Property(x => x.Username).HasMaxLength(100);
        mb.Entity<User>().Property(x => x.Password).HasMaxLength(200);
        mb.Entity<User>().Property(x => x.Email).HasMaxLength(100);
    }

}

// dotnet ef migrations add NOME
// dotnet ef database update NOME