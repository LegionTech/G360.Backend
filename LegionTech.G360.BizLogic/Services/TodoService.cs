namespace LegionTech.G360.BizLogic.Services;

using System.Collections.Generic;
using System.Linq;
using LegionTech.G360.BizLogic.Interfaces;
using LegionTech.G360.BizLogic.Data;

public class TodoService: ITodoService
{
  private readonly AppDbContext _context;

  public TodoService(AppDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Todo>> GetAll()
  {
    return await _context.Todos.ToListAsync();
  }

  public async Task<Todo> GetById(int id)
  {
    var todo = await _context.Todos.SingleOrDefaultAsync(x=> x.Id == id) ?? throw new Exception("Item does not exist.");
    
    return todo;
  }

  public async Task Create(Todo todo)
  {
    _context.Todos.Add(todo);
    
    await _context.SaveChangesAsync();
  }
  
  public async Task Update(int id, TodoStatus status, string title)
  {
    var todo = _context.Todos.SingleOrDefault(x=> x.Id == id) ?? throw new Exception("Item does not exist.");
    todo.Status = status;
    todo.Title = title;

    _context.Todos.Update(todo);
    await _context.SaveChangesAsync();
  }

  public async Task Delete(int id)
  {
    var todo = _context.Todos.SingleOrDefault(x=> x.Id == id) ?? throw new Exception("Item does not exist.");

    _context.Remove(todo);

    await _context.SaveChangesAsync();
  }

  public async Task DisposeDatabase()
  {
    Console.WriteLine("Serv Dispose DB");
    await _context.DisposeAsync();
  }
}
