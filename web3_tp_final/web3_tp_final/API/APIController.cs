using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using web3_tp_final.DTO;
using web3_tp_final.Models;
using System.Linq;

namespace web3_tp_final.API
{
    public class APIController
    {
        private static HttpClient client = new HttpClient();
        public APIController() {
        }

        // Basic API calls
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

        //Users API calls

        public async Task<User> LogIn(string username, string password)
        {
            var response = await client.GetAsync("https://localhost:44308/api/Users/LogIn?username=" + username + "&password=" + password);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(apiResponse);
                return (User) JsonConvert.DeserializeObject<User>(apiResponse);
            }
            else
            {
                Debug.WriteLine(response.Content.ReadAsStringAsync());
                return null;
            }
        }

        //Users API calls
        public async Task<List<User>> GetUsersWithAppointments()
        {
            IEnumerable<Appointment> appointmentss;
            var response = await client.GetAsync("https://localhost:44308/api/Users/");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                List<User> users = (List<User>)JsonConvert.DeserializeObject<List<User>>(apiResponse);

                foreach (User user in users) {
                    appointmentss = await GetAppointmentsForUser(user.UserID);
                    if (appointmentss != null)
                    {
                        user.Appointments = appointmentss;
                    }
                    else
                    {
                        return null;
                    }
                }
                return users;
            }
            return null;
        }

        //Review API calls
        public async Task<Review> PostReview(ReviewDTO form)
        {
            var response = await client.PostAsJsonAsync("https://localhost:44308/api/Reviews", form);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return (Review)JsonConvert.DeserializeObject<Review>(apiResponse);
            }
            else
            {
                return null;
            }
        }

        //Appointment API calls
        public async Task<Appointment> PostAppointment(AppointmentDTO form)
        {
            var response = await client.PostAsJsonAsync("https://localhost:44308/api/Appointments/CreateAppointment/", form );
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return (Appointment)JsonConvert.DeserializeObject<Appointment>(apiResponse);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Appointment>> GetAppointmentsForUser(int id)
        {
            var response = await client.GetAsync("https://localhost:44308/api/Users/" + id + "/Appointments");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return (List<Appointment>)JsonConvert.DeserializeObject<List<Appointment>>(apiResponse);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Availability>> GetAvailabilitiesForUser(int id)
        {
            var response = await client.GetAsync("https://localhost:44308/api/Users/" + id + "/Availabilities");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                return (List<Availability>)JsonConvert.DeserializeObject<List<Availability>>(apiResponse);
            }
            else
            {
                return null;
            }
        }


        //Pets API calls
        public async Task<List<Pet>> GetPetsForAppointment(int id)
        {
            var response = await client.GetAsync("https://localhost:44308/api/Appointments/" + id + "/Pets");
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (List<Pet>)JsonConvert.DeserializeObject<List<Pet>>(apiResponse);
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
