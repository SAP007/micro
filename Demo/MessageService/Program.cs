using System.Xml.Linq;
using MessageService;
using MessageService.Controller;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MessageController>();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();

