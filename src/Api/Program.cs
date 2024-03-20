using Cocktail.Api.Configurations;
using Cocktail.Api.Extensions;
using Cocktail.Api.Interfaces;
using Cocktail.Api.Middlewares;
using Cocktail.Application.Extensions;
using Cocktail.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services
    .AddApi( builder.Configuration )
    .AddApplication( builder.Configuration )
    .AddInfrastructure( builder.Configuration )
    .AddControllers();

var app = builder.Build();
if ( builder.Environment.IsDevelopment() ) 
    app.UseDeveloperExceptionPage();

app.UseMiddleware< ExceptionHandlingMiddleware >();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseDefaultCorsPolicy();
app.UseSerilogRequestLogging();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseSwagger(builder.Configuration);
app.MapControllers();

var startupTasks = app.Services.GetServices<IStartupTask>();
foreach(var startupTask in startupTasks)
{
    await startupTask.Execute();
}

await app.RunAsync();