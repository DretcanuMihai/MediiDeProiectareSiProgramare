using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using log4net.Config;
using model.validators.implementations;
using model.validators.interfaces;
using networking;
using persistence.implementations;
using persistence.interfaces;
using server.implementations;
using services.interfaces;

namespace server
{
    internal class StartProtoServer
    {
        public static void Main(string[] args)
        {
            ISuperService superService = InstantiateSuperService();
            String host = "127.0.0.1";
            int port = 55555;
            try
            {
                host = ConfigurationManager.AppSettings["host"];
                port = Int32.Parse(ConfigurationManager.AppSettings["port"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Using host:"+host+"port:"+port);
            SerialServer server = new SerialServer(host, port, superService);
            server.Start();
        }

        private static ISuperService InstantiateSuperService()
        {
            XmlConfigurator.Configure(new FileInfo("log4j2.xml"));
            string connectionString = GetConnectionStringByName("festivalsDB");
            IDictionary<String, String> props = new SortedList<String, String>();
            props.Add("ConnectionString", connectionString);

            IFestivalRepository festivalRepository = new FestivalDbRepository(props);
            ITicketRepository ticketRepository = new TicketDbRepository(props);
            IUserRepository userRepository = new UserDbRepository(props);
            IFestivalValidator festivalValidator = new FestivalValidator();
            ITicketValidator ticketValidator = new TicketValidator();
            IUserValidator userValidator = new UserValidator();

            IUserService userService = new UserService(userValidator, userRepository);
            IFestivalService festivalService = new FestivalService(festivalRepository, festivalValidator,
                ticketRepository, ticketValidator);
            return new SuperService(festivalService, userService);
        }

        private static string GetConnectionStringByName(string name)
        {
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}