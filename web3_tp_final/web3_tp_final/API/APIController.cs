using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace web3_tp_final.API
{
    public class APIController
    {
        private static HttpClient client = new HttpClient();
        public APIController() {
        }

        public async Task<IEnumerable<T>> Get<T>()
        {
            string className = this.Pluralize<T>();

            var response = await client.GetAsync("https://localhost:44308/api/" + className);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (List<T>)JsonConvert.DeserializeObject<List<T>>(apiResponse);
        }

        public async Task<T> Post<T>(T model)
        {
            string className = Pluralize<T>(model.GetType().Name);

            var response = await client.PostAsJsonAsync("https://localhost:44308/api/" + className, model);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<T> Get<T>(int? id)
        {
            string className = Pluralize<T>();

            var response = await client.GetAsync("https://localhost:44308/api/" + className + "/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<T> Put<T>(int id, T model)
        {
            string className = Pluralize<T>(model.GetType().Name);

            var response = await client.PutAsJsonAsync("https://localhost:44308/api/" + className + "/" + id, model);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<HttpResponseMessage> Delete<T> (int? id)
        {
            string className = Pluralize<T>();

            var response = await client.DeleteAsync("https://localhost:44308/api/" + className + "/" + id);

            return response;
        }

        private string Pluralize<T>(string word = null)
        {
            if (word == null)
            {
                var objectClass = (T)Activator.CreateInstance(typeof(T));
                return objectClass.GetType().Name + "s";
            }
            else
            {
                return word + "s";
            }
        }
    }
}
