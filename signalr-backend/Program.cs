using SignalR.DataService;
using SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("reactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSingleton<SharedDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseCors("reactApp");

app.MapHub<ChatHub>("/Chat");

app.Run();
