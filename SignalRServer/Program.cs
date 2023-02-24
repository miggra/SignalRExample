using Microsoft.AspNetCore.SignalR;
using SignalRServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins("null")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

app.MapHub<ChatHub>("/chatHub");

app.MapGet("/", () => "Hello World!");

app.Run();
