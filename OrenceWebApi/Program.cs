using OrenceApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen(); error
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
