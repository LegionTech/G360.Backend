var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddDbContext<AppDbContext>(opts=> opts.UseInMemoryDatabase(databaseName: "Default"));

builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  using(var scope = app.Services.CreateScope())
  {
    var dbcontext = scope.ServiceProvider.GetService<AppDbContext>();

    if(!dbcontext.Todos.Any())
    {
      var todos = new List<Todo>()
      {
        { new Todo { Title = "Have a coffee.",}},        
        { new Todo { Title = "Read emails.",}},
        { new Todo { Title = "Make phone calls.",}},
        { new Todo { Title = "Fill in the worksheets.",}},
        { new Todo { Title = "Coding practises.",}},
        { new Todo { Title = "Play basketball.",}},
      };

      dbcontext.Todos.AddRange(todos);
      await dbcontext.SaveChangesAsync();
    }
  }
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();