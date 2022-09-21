using System.Data;
using System.Reflection;
using Business.Abstract;
using Business.Concrete;
using Business.Rules.ProductRules;
using Business.Validators.ProductValidators;
using Core.Utilities.Ioc;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Repository.Abstract;
using Repository.Concrete;
using Repository.Contexts;

namespace Business.Modules;

public class DIModule : ICoreModule
{
    private IConfiguration _configuration;

    public DIModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Load(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<BaseDbContext>(options => options.UseNpgsql(
                _configuration.GetConnectionString("PostgreSQL"),x => x.MigrationsAssembly("WebAPI")));

        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

        serviceCollection.AddScoped<IProductService, ProductManager>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddScoped<ProductBusinessRules>();
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssemblyContaining<ProductCreateValidator>();

        // serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}