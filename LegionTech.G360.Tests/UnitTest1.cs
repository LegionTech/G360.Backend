using LegionTech.G360.BizLogic.Data;
using LegionTech.G360.Core;

namespace LegionTech.G360.Tests;

public class UnitTest1
{
  private Todo _Todo1 = new() { Title = "My Task-1" };

  [Fact]
  public void Test1()
  {
    var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("MemDB").Options;

    var context = new AppDbContext(options);

    var repo = new AppRepository(context);

    repo.Create(_Todo1);

    Assert.Single(context.Todos);
  }
}