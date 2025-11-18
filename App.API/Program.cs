using App.Infrastructure;
using App.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using App.API.Utilities;

    var builder = WebApplication.CreateBuilder(args);

    Env.Load();
    builder.Configuration.AddEnvironmentVariables();


    builder.Services.Configure(builder.Configuration);


    var app = builder.Build();


    app.Configure();

    app.Run();
