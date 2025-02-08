using Microsoft.EntityFrameworkCore;

namespace Kanboom.Context;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

    }

    
    protected override void OnModelCreating(ModelBuilder mb){
  
    }

}

// dotnet ef migrations add NOME
// dotnet ef database update NOME