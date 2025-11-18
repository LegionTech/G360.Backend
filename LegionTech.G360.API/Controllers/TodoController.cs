namespace LegionTech.G360.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;


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
  public ActionResult<IEnumerable<Todo>> GetAll()
  {
    // var todos = new List<Todo>()
    // {
    //   { new Todo { Title = "Big Ass Titties.",}},
    //   { new Todo { Title = "I'm a dude playing a due disguised as another dude.",}},
    //   { new Todo { Title = "I'm the winner.",}},
    // };

    var todos = _todoService.GetAll();

    return Ok(todos);
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

  [HttpPut]
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

  [HttpPost("{id}")]
  public ActionResult Update([FromRoute] int id, [FromBody] Todo item)
  {
    if (id != item.Id)
      return BadRequest();

    _todoService.Update(item.Id, item.Status, item.Title);

    return Ok();
  }
}

