using System;
using System.Net.Http;
using System.Threading.Tasks;
using restclient;

namespace CSharpRestClient
{
    class MainClass
    {
        static HttpClient client = new HttpClient();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }
        
        static FestivalsClient festivalsClient=new FestivalsClient();
        private static int toDelete = 0;


        static Festival getToAdd()
        {
            Festival toAdd = new Festival();
            toAdd.id = 0;
            toAdd.artistName = "dan sandu";
            toAdd.dateTime = "2022-03-16T21:23:55.897";
            toAdd.place = "clujarena";
            toAdd.availableSpots = 10000;
            toAdd.soldSpots = 10000;
            return toAdd;
        }
        
        static Festival getToUpdate()
        {
            Festival toAdd = new Festival();
            toAdd.id = 51;
            toAdd.artistName = "cata halbes";
            toAdd.dateTime = "2022-12-16T21:23:55.897";
            toAdd.place = "mama manu";
            toAdd.availableSpots = 33;
            toAdd.soldSpots = 22;
            return toAdd;
        }

        static async Task RunAsync()
        {
            Festival festivalToAdd = getToAdd();
            Festival festivalToUpdate = getToUpdate();
            getAll().Wait();
            Console.WriteLine(await festivalsClient.GetById(51));
            Festival aux = await festivalsClient.Create(festivalToAdd);
            Console.WriteLine(aux);
            getAll().Wait();
            Console.WriteLine(await festivalsClient.Delete(aux.id));
            getAll().Wait();
            Console.WriteLine(await festivalsClient.Update(festivalToUpdate));
            getAll().Wait();
        }

        static async Task getAll()
        {
            Festival[] listResult;
            listResult = await festivalsClient.GetAll();
            Console.WriteLine("----------------------------------------------");
            foreach (var festival in listResult)
            {
                Console.WriteLine(festival);
            }
            Console.WriteLine("----------------------------------------------");
        }
    }
}