namespace LegionTech.G360.BizLogic.Data;

public class AppDbContext: DbContext
{
  public DbSet<Todo> Todos { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
  {

  }
}
