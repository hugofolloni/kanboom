using Kanboom.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace Kanboom.Context;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){

    }

    public DbSet<User>? User { get; set; }
    public DbSet<Board>? Board { get; set; }
    public DbSet<Models.Database.Task>? Task { get; set; }
    public DbSet<Group>? Group { get; set; }
    public DbSet<StageLevels>? StageLevels { get; set; }
    public DbSet<UserGroup>? UserGroup { get; set; }
    public DbSet<BoardUser>? BoardUser { get; set; }

    protected override void OnModelCreating(ModelBuilder mb){
        mb.Entity<User>().HasKey(x => x.Id);
        mb.Entity<User>().Property(x => x.Username).HasMaxLength(100);
        mb.Entity<User>().HasIndex(u => u.Username).IsUnique();
        mb.Entity<User>().Property(x => x.Password).HasMaxLength(200);
        mb.Entity<User>().Property(x => x.Email).HasMaxLength(100);
        mb.Entity<User>().HasMany(x => x.Task).WithOne().HasForeignKey(x => x.Fk_UserAssigned);
        mb.Entity<User>().HasMany(x => x.UserGroup).WithOne().HasForeignKey(x => x.Fk_UserId);
        mb.Entity<User>().HasMany(x => x.BoardUser).WithOne().HasForeignKey(x => x.Fk_UserId);

        mb.Entity<Board>().HasKey(x => x.Id);
        mb.Entity<Board>().Property(x => x.Name).HasMaxLength(100);
        mb.Entity<Board>().Property(x => x.StagesCount);
        mb.Entity<Board>().Property(x => x.IsGroupBoard);
        mb.Entity<Board>().Property(x => x.Invite);
        mb.Entity<Board>().HasMany(x => x.Task).WithOne().HasForeignKey(x => x.Fk_Board);
        mb.Entity<Board>().HasMany(x => x.BoardUser).WithOne().HasForeignKey(x => x.Fk_BoardId);
        mb.Entity<Board>().HasMany(x => x.StageLevels).WithOne().HasForeignKey(x => x.Fk_Board);

    
        mb.Entity<Models.Database.Task>().HasKey(x => x.Id);
        mb.Entity<Models.Database.Task>().Property(x => x.Title).HasMaxLength(100);
        mb.Entity<Models.Database.Task>().Property(x => x.Description).HasMaxLength(1000);
        mb.Entity<Models.Database.Task>().Property(x => x.StageNumber);
      
        mb.Entity<Group>().HasKey(x => x.Id);
        mb.Entity<Group>().Property(x => x.Name).HasMaxLength(100);
        mb.Entity<Group>().Property(x => x.GroupLink).HasMaxLength(1000);
        mb.Entity<Group>().HasMany(x => x.Board).WithOne().HasForeignKey(x => x.Fk_GroupId);
      
        mb.Entity<Group>().HasKey(x => x.Id);
        mb.Entity<Group>().Property(x => x.Name).HasMaxLength(100);
        mb.Entity<Group>().Property(x => x.GroupLink).HasMaxLength(1000);

        mb.Entity<StageLevels>().HasKey(x => x.Id);
        mb.Entity<StageLevels>().Property(x => x.StageName).HasMaxLength(100);
        mb.Entity<StageLevels>().Property(x => x.Fk_Board);
        mb.Entity<StageLevels>().Property(x => x.StageNumber);

        mb.Entity<UserGroup>().HasKey(x => x.Id);
        mb.Entity<UserGroup>().HasIndex(x => new { x.Fk_UserId, x.Fk_GroupId }).IsUnique();
        
        mb.Entity<BoardUser>().HasKey(x => x.Id);
        mb.Entity<BoardUser>().HasIndex(x => new { x.Fk_UserId, x.Fk_BoardId }).IsUnique(); 
    }

}

// dotnet ef migrations add NOME
// dotnet ef database update NOME