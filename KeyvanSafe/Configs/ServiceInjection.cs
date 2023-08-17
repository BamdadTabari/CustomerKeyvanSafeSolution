using KeyvanSafe.Domain.EntityFramework.Interfaces.IUnitOfWorks;
using KeyvanSafe.Domain.EntityFramework.Repositories.UnitOfWorks;
using KeyvanSafe.Shared.EntityFramework.Configs;
using KeyvanSafe.Shared.Persistence.HttpObjects;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;

namespace KeyvanSafe.Server.Configs;

public static class ServiceInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Add services to the container.
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddScoped<IHttpService, HttpService>();
        // register an HttpClient that points to itself
        services.AddSingleton(sp =>
        {
            // Get the address that the app is currently running at
            var server = sp.GetRequiredService<IServer>();
            var addressFeature = server.Features.Get<IServerAddressesFeature>();
            var baseAddress = addressFeature.Addresses.First();
            return new HttpClient { BaseAddress = new Uri(baseAddress) };
        });

        services.AddScoped<IHttpService, HttpService>();

        services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("ServerDbConnection"))
          .EnableDetailedErrors());

        services.AddScoped<IUnitOfWorkIdentity, UnitOfWorkIdentity>();
        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}