using MediatR;
using System.Reflection;
using WebAPI.Extensions;
using Application.Extensions;
using Infrastructure.Extensions;

#region Add services to the container

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAuthDependencies(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.RegisterGrapQLDependencies();
builder.Services.RegisterApplicationDependencies();
builder.Services.RegisterInfrastructureDependencies();
var app = builder.Build();

#endregion

#region Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.Services.MigrateDatabase();
}
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints => { endpoints.MapGraphQL(); });
app.Run();

#endregion