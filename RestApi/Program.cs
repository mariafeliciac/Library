using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.DAOForADO;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model;



var build = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = build.Build();
string connectionString = configuration.GetConnectionString("LibraryDatabase");


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataAccessADO>(provider =>
{
    return new DataAccessADO(connectionString);
});
builder.Services.AddScoped<IUserDAO, UserDAOForADO>();
builder.Services.AddScoped<IBookDAO, BookDAOForADO>();
builder.Services.AddScoped<IReservationDAO, ReservationDAOForADO>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBusinessLogic, BusinessLogic>();

//builder.Services.AddDbContext<LibraryContext>(opt => opt.UseSqlServer(connectionString));
//builder.Services.AddScoped<IUserDAO, UserDAOForEF>();
//builder.Services.AddScoped<IBookDAO, BookDAOForEF>();
//builder.Services.AddScoped<IReservationDAO, ReservationDAOForEF>();
//builder.Services.AddScoped<IRepository, Repository>();
//builder.Services.AddScoped<IBusinessLogic, BusinessLogic>();


var app = builder.Build();

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


