using System;
using System.Configuration;
using System.Windows.Forms;
using client.forms;
using networking;
using services.interfaces;

namespace client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ISuperService superService = InstantiateSuperService();
            MainController controller = new MainController(superService);
            LoginForm form = new LoginForm(controller);
            Application.Run(form);
        }
        
        private static ISuperService InstantiateSuperService()
        {
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

            Console.WriteLine("Using host:" + host + "port:" + port);
            ISuperService superService = new SuperServiceProxy(host, port);
            return superService;
        }
    }
}