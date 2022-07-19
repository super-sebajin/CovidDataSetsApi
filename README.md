# CovidDataSetsApi

- CovidDataSetsApi is a ASP.NET Core Web API that exposes endpoints to gather data from various public data sets related to the ongoing SARS-CoV-2 virus pandemic,
and stores them in a SQLExpress database. 
- This project uses a "code-first" approach via the use of migrations, so to run this project, SQLServer Express should be installed.
- When project is done, this README will be more thorough.
- main development branch is CovidDataSetsApi/tree/dev1 , and will be merged with main branch when project is finished.

# Data Sets Currently In Use:
### [COVID-19 Rich Data Services Postman Collection](https://documenter.getpostman.com/view/2220438/SzYevv9u):
| Data Set Name | HTTP Method | URL |
|---------------|-------------|-----|
| "Visualize COVID-19 cases over time in the U.S." | GET |  [Data Set Url](https://covid19.richdataservices.com/rds/api/query/int/jhu_country/select?cols=date_stamp,cnt_confirmed,cnt_death,cnt_recovered&where=(iso3166_1=US)&format=amcharts&limit=5000) |

# How to set database and run project:
- Clone the repository, of course, adn run `dotnet build` to ensure that the project builds.
- To run this project, an initial migration would have to be run in order to map entities to their respective tables. (`dotnet ef` CLI tool is assumed to be installed) 
- The project uses SQL Server Express for development, and assumes it to be installed so make sure to have a database engine compatible with .Net and have the following in the project's `appsettings.json` as follows:
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "CovidDataSetsDatabase": "Server=DATABASE-ENGINE;Database=CovidDataSetsDb;Trusted_Connection= True"
  },
  "AllowedHosts": "*"
}
```
then make sure that the proper services are configured in the `Program.cs` for use with the database engine that will be used.
- Run `dotnet ef migrations add InitialCreate` and `dotnet ef database update` and EntityFramework will go ahead and create the tables based on the entities in the project's `DataAccessLayer` folder.
- Run `dotnet build` and then `dotnet run` to run the project.
 
