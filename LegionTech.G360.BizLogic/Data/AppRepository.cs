namespace LegionTech.G360.BizLogic.Data;

public class AppRepository
{
  private readonly AppDbContext _context;

  public AppRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<List<Todo>> GetAll()
  {
    return await _context.Todos.ToListAsync();
  }

  public async void Create(Todo todo)
  {
    _context.Todos.Add(todo);
    await _context.SaveChangesAsync();
  }
}
