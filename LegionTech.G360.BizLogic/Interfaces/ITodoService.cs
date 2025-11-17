namespace LegionTech.G360.BizServices.Interfaces;

public interface ITodoService
{
  public IEnumerable<Todo> GetAll();
  public Todo GetById(int id);
  public void Create(Todo task);
  public void Update(Todo task);
  public bool Delete(int id);
}
