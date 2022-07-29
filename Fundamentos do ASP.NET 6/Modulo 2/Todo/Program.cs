using Todo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
//Injeta a dependencia do AppDBContext em forma de serviço
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
