using Newtonsoft.Json;
using SitAPupAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SitAPupAdmin.API
{
    public class APIController
    {
        private static HttpClient client = new HttpClient();
        public APIController()
        {
        }


        //Admins API Login

        public async Task<Admin> LogIn(string username, string password)
        {
            var response = await client.GetAsync("https://localhost:44308/api/Admins/LogIn?name=" + username + "&password=" + password);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(apiResponse);
                return (Admin)JsonConvert.DeserializeObject<Admin>(apiResponse);
            }
            else
            {
                Debug.WriteLine(response.Content.ReadAsStringAsync());
                return null;
            }
        }



     
    }
}

