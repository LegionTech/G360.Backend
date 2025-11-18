namespace LegionTech.G360.BizLogic.Interfaces;

public interface ITodoService
{
  public Task<IEnumerable<Todo>> GetAll();
  public Task<Todo> GetById(int id);
  public Task Create(Todo task);  
  public Task Update(int id, TodoStatus status, string title);
  public Task Delete(int id);
  public Task DisposeDatabase();
}
