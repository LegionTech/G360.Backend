namespace LegionTech.G360.Tests;

using Xunit.Sdk;

public class Test_TodoService : IDisposable, IClassFixture<TodoServiceFixture>
{
  private readonly TodoServiceFixture _fixture;

  public Test_TodoService(TodoServiceFixture fixture)
  {
    _fixture = fixture;    

    var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

    var appDbContext = new AppDbContext(options);

    _fixture.TodoService = new TodoService(appDbContext);

    Todo todo1 = new() { Title = "My Task-A" };

    _fixture.TodoService.Create(todo1);
  }

  [Fact]
  public async Task GetAll_InitializeData_ShouldHaveOneRow()
  {
    //Act
    var result = await _fixture.TodoService.GetAll();

    //Assert
    Assert.Single(result);
  }

  [Fact]
  public async Task GetAll_InitializeData_ShouldHaveCorrectData()
  {
    //Act
    var result = await _fixture.TodoService.GetAll();

    var todo = result.FirstOrDefault();

    //Assert
    Assert.Equal(1, todo?.Id);
    Assert.Equal(TodoStatus.Pending, todo?.Status);
  }

  [Fact]
  public async Task Create_AddInstance_ShouldHaveCorrectData()
  {    
    //Arrange
    var newTodo = new Todo() { Title = "My Task-B"};

    //Act
    await _fixture.TodoService.Create(newTodo);

    var todos = await _fixture.TodoService.GetAll();

    var lastTodo = todos.LastOrDefault();

    //Assert
    Assert.Equal(2, todos.Count());
    Assert.Equal(2, lastTodo?.Id);
  }

  [Fact]
  public async Task Update_ChangeStatusAndTitle_ShouldHaveCorrectData()
  {
    //Arrange
    var id = 1;
    var status = TodoStatus.Complete;
    var title = "Updated Title";

    //Act
    await _fixture.TodoService.Update(1, status, title);

    var todo = await _fixture.TodoService.GetById(id);

    //Assert
    Assert.Equal(status, todo?.Status);
    Assert.Equal(title, todo?.Title);
  }

  [Fact]
  public async Task Delete_CorrectId_ShouldHaveNone()
  {
    //Act
    await _fixture.TodoService.Delete(1);

    var todos = await _fixture.TodoService.GetAll();

    //Assert
    Assert.Empty(todos);
  }
  

  public void Dispose() => _fixture.TodoService.DisposeDatabase();
}