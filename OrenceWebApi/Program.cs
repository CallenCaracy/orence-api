using OrenceApi.Data;
using OrenceApi.Setup;
using Scalar.AspNetCore;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var setup = new Setup(builder);
Env.Load();
//Setup
setup.SetupCors();
setup.GeneralApiSetup();
setup.SetupJWT();
setup.SetupDbConn();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.MapScalarApiReference();

ControllerActionEndpointConventionBuilder controllerActionEndpointConventionBuilder = app.MapControllers();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
