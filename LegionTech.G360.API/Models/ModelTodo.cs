namespace LegionTech.G360.API.Models;

public class ModelTodo
{
  public int Id { get; private set;}
  public string Title { get; set;}
  public string Status { get; set;}

  public ModelTodo(Todo todo)
  {
    this.Id = todo.Id;
    this.Title = todo.Title;
    this.Status = todo.Status.ToString();  
  }
}
