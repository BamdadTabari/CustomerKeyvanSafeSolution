using KeyvanSafe.Server.Configs;
using KeyvanSafe.Shared.Models.Dtos.AutoMapperProfile;

#region builder part
var builder = WebApplication.CreateBuilder(args);
string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

builder.Configuration.AddJsonFile("appsettings.json")
            .AddEnvironmentVariables();
builder.Services.AddAutoMapper(typeof(IdentityAutoMapperProfiles));
ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


// Add services to the container.
builder.Services.AddServices(configuration);
builder.Services.AddControllers();
builder.Services.AddMvc();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region app part
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseConfiguredExceptionHandler(environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();
app.Run();

#endregion
