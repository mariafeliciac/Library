using BusinessLogicLayer.Interfaces;
using ConsoleAppLayer;
using DataAccessLayer;
using DataAccessLayer.DAOForADO;
using DataAccessLayer.DAOForEF;
using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
                    .SetBasePath("C:\\Users\\maria.felicia.conte\\source\\repos\\Library - Conte\\Library - Conte\\ConsoleAppLayer")
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = builder.Build();
        string connectionString = configuration.GetConnectionString("LibraryLocal")!;
        string connectionString2 = configuration.GetSection("ConnectionStrings")["library"]!;

        IDataAccessADO dataAccessADO = new DataAccessADO(connectionString);
        IBusinessLogic businessLogic = new BusinessLogic(new Repository(new UserDAOForADO(dataAccessADO), new BookDAOForADO(dataAccessADO), new ReservationDAOForADO(dataAccessADO))/*,publisherClassEvent*/);

        App app = new App(businessLogic);

        app.Run(args);

    }
}