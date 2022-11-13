using Microsoft.AspNetCore.Builder;
using MovieService;
using MovieService.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using MovieService.Controller;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSingleton<IMovieService, Service>(); //  created the first time they are requested

builder.Services.AddScoped<IMovieService, Service>(); // once per request
builder.Services.AddSingleton<MovieController>();
builder.Services.AddHostedService<Worker>();

//builder.Services.AddTransient<IMovieService, Service>(); // no difference?

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.RunAsync();


