using Games.Infrastructure;
using Games.Infrastructure.Repository;
using Games.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); // Swagger
builder.Services.AddEndpointsApiExplorer();

// �������� �������� ���� �����
builder.Services.AddDbContext<GamesContext>();

// �������� ���������� �� CRUD �����
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
