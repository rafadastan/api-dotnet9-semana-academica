using SemanaAcademica.Api.Configuration;
using SemanaAcademica.Domain;
using SemanaAcademica.Domain.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDomainDependencies(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.Filters.Add<NotificationFilter>();
});

builder.Services.AddMvcWithJsonExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();           
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
