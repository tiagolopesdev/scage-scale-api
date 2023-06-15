using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using SCAGEScale.Application.QuerySide;
using SCAGEScale.Application.Service;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddScoped<IScaleQuery, ScaleQueries>();
builder.Services.AddScoped<IScaleService, ScaleService>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "scale API",
        Description = "Api builded using DDD pattern",
        Contact = new OpenApiContact
        {
            Name = "Tiago Lopes",
            Email = "saxtiago14@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/tiagolopesdev"),           
        }        
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opt.RoutePrefix = string.Empty;
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
