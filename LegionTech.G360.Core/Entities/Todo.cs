namespace LegionTech.G360.Core;

using LegionTech.G360.Core.Enums;

public class Todo
{
  public int Id { get; set; }  
  
  public required string Title { get; set; }

  public TodoStatus Status { get; set; }
}
