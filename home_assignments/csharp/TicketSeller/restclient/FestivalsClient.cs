using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace restclient
{
    public class FestivalsClient
    {
        static HttpClient client = new HttpClient();

        public static String URL = "http://localhost:8080/ticketseller/festivals";

        public FestivalsClient()
        {
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Festival> Create(Festival festival)
        {
            Festival result = null;
            string stringjson = JsonConvert.SerializeObject(festival);
            var stringContent = new StringContent(stringjson, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PostAsync(String.Format(URL), stringContent);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Festival>();
            }

            return result;
        }

        public async Task<Festival> Delete(int id)
        {
            Festival result = null;
            HttpResponseMessage response = await client.DeleteAsync(String.Format("{0}/{1}", URL, id));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Festival>();
            }
            return result;
        }

        public async Task<Festival> Update(Festival festival)
        {
            Festival result = null;
            string stringjson = JsonConvert.SerializeObject(festival);
            var stringContent = new StringContent(stringjson, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response =
                await client.PutAsync(String.Format("{0}/{1}", URL, festival.id), stringContent);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Festival>();
            }

            return result;
        }

        public async Task<Festival> GetById(int id)
        {
            Festival result = null;
            HttpResponseMessage response = await client.GetAsync(String.Format("{0}/{1}", URL, id));
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Festival>();
            }
            return result;
        }

        public async Task<Festival[]> GetAll()
        {
            Festival[] result = null;
            HttpResponseMessage response = await client.GetAsync(URL);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Festival[]>();
            }

            return result;
        }
    }

    public class Festival
    {
        public int id { get; set; }
        public string artistName { get; set; }
        public string dateTime { get; set; }
        public string place { get; set; }
        public int availableSpots { get; set; }
        public int soldSpots { get; set; }
        
        public override string ToString()
        {
            return "Festival{" +
                   "id=" + id +
                   ", artistName='" + artistName + '\'' +
                   ", dateTime=" + dateTime +
                   ", place='" + place + '\'' +
                   ", availableSpots=" + availableSpots +
                   ", soldSpots=" + soldSpots +
                   '}';
        }
    }
}