using CovidDataSetsApi.DataAccessLayer;
using CovidDataSetsApi.Repositories;
using Microsoft.EntityFrameworkCore;


//create the application builder
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true)
    .AddEnvironmentVariables();


builder.Services.AddDbContext<CovidDataSetsDbContext>(
    options => options.UseSqlServer(connectionString:
        builder.Configuration.GetValue<string>("ConnectionStrings:CovidDataSetsDatabase"),
        x=>x.MigrationsAssembly("CovidDataSetsApi"))
);


builder.Services.AddControllers();

//build Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);


//Dependency injection for repository methods in controllers
builder.Services.AddScoped<IDataSetsRepository, DataSetsRepository>();
builder.Services.AddScoped<IRichDataServicesDataSetsRepository, RichDataServicesDataSetsRepository>();


//build the app
var app = builder.Build();





if (app.Environment.IsDevelopment())
{
    //configure the HTTP requests pipeline
    app.UseSwagger();
    app.UseSwaggerUI();

}



//use routing 
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();