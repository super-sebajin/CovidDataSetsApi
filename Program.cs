using System.Reflection;
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


builder.Services.AddSwaggerGen();//uncomment if xml documentation comments are disabled for this project 
//builder.Services.AddSwaggerGen(options =>//enable xml documentation comments for this project, if not enable please comment out
//{
//    var filePath = Path.Combine(AppContext.BaseDirectory, "CovidDataSetsApi.xml");
//    options.IncludeXmlComments(filePath,includeControllerXmlComments: true);
//});

builder.Services.AddAutoMapper(typeof(Program).Assembly);


//Dependency injection for repository methods in controllers
builder.Services.AddScoped<ICovidDataSetsRepository, CovidDataSetsRepository>();
builder.Services.AddScoped<IRichDataServicesDataSetsRepository, RichDataServicesDataSetsRepository>();


//build the app
var app = builder.Build();





if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
//configure the HTTP requests pipeline



//use routing 
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();