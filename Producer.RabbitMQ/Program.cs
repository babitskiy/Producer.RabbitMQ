using Microsoft.EntityFrameworkCore;
using Producer.RabbitMQ;
using Producer.RabbitMQ.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<OrderDbContext>(options =>
//    options.UseInMemoryDatabase("ASPNETCoreRabbitMQ"));
builder.Services.AddDbContext<OrderDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDbContext")));

// Добавляем другие сервисы...

// Разрешаем использование контекста базы данных в контроллерах
builder.Services.AddScoped<OrderDbContext>();


// Add services to the container.
builder.Services.AddScoped<IOrderDbContext, OrderDbContext>();
builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using (var serviceScope = serviceScopeFactory.CreateScope())
//{
//    var dbContext = serviceScope.ServiceProvider.GetService<OrderDbContext>();
//    dbContext.Database.EnsureCreated();
//}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<OrderDbContext>();

        context.Database.EnsureCreated();
    }
    catch (Exception exception)
    {

    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

