using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Project.Repository;
using Project.Repository.Common;
using Project.Service;
using Project.Service.Common;
using Microsoft.Extensions.Configuration;
using Project.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<CustomerRepository>().As<ICustomerRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
    containerBuilder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
