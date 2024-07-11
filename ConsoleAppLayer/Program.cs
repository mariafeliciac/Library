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
        var build = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = build.Build();
        string connectionString = configuration.GetConnectionString("LibraryDatabase");


        IDataAccessADO dataAccessADO = new DataAccessADO(connectionString);
        IBusinessLogic businessLogic = new BusinessLogic(new Repository(new UserDAOForADO(dataAccessADO), new BookDAOForADO(dataAccessADO), new ReservationDAOForADO(dataAccessADO))/*,publisherClassEvent*/);

        App app = new App(businessLogic);

        app.Run(args);

    }
}