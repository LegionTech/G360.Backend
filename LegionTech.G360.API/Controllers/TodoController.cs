namespace LegionTech.G360.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using LegionTech.G360.API.Models;

[Route("api/todo")]
[ApiController]
public class TodoController : ControllerBase
{
  private readonly ITodoService _todoService;

  public TodoController(ITodoService todoService)
  {
    _todoService = todoService;
  }

  [HttpGet]
  public async Task<ActionResult<IList<ModelTodo>>> GetAll()
  {
    var todos = await _todoService.GetAll();

    var models = todos.Select(x=> new ModelTodo(x)).ToList();
    
    return models;
  }

  [HttpGet("{id}")]
  public ActionResult<Todo> Get([FromRoute] int id)
  {
    var todo = _todoService.GetById(id);

    if (todo == null)
    {
      return NotFound();
    }

    return Ok(todo);
  }

  [HttpPost]
  public ActionResult Add([FromBody] Todo item)
  {
    _todoService.Create(item);

    return Ok();
  }

  [HttpDelete("{id}")]
  public ActionResult Delete(int id)
  {
    var result = _todoService.Delete(id);

    return Ok();
  }

  [HttpPut("{id}")]
  public ActionResult Update([FromRoute] int id, [FromBody] Todo item)
  {
    if (id != item.Id)
      return BadRequest();

    _todoService.Update(item.Id, item.Status, item.Title);

    return Ok();
  }
}

