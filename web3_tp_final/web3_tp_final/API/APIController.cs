using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using web3_tp_final.DTO;
using web3_tp_final.Models;

namespace web3_tp_final.API
{
    public class APIController
    {
        private static HttpClient _client = new HttpClient();
        public APIController() {
        }

        // Basic API calls
        public async Task<IEnumerable<T>> Get<T>()
        {
            string className = this.Pluralize<T>();

            var response = await _client.GetAsync("https://localhost:44308/api/" + className);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (List<T>)JsonConvert.DeserializeObject<List<T>>(apiResponse);
        }

        public async Task<T> Post<T>(T model)
        {
            string className = Pluralize<T>(model.GetType().Name);

            var response = await _client.PostAsJsonAsync("https://localhost:44308/api/" + className, model);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<T> Get<T>(int? id)
        {
            string className = Pluralize<T>();

            var response = await _client.GetAsync("https://localhost:44308/api/" + className + "/" + id);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<T> Put<T>(int id, T model)
        {
            string className = Pluralize<T>(model.GetType().Name);

            var response = await _client.PutAsJsonAsync("https://localhost:44308/api/" + className + "/" + id, model);
            string apiResponse = await response.Content.ReadAsStringAsync();
            return (T)JsonConvert.DeserializeObject<T>(apiResponse);
        }

        public async Task<HttpResponseMessage> Delete<T> (int? id)
        {
            string className = Pluralize<T>();

            var response = await _client.DeleteAsync("https://localhost:44308/api/" + className + "/" + id);

            return response;
        }

        //Users API calls

        public async Task<string> GetUsername(int id)
        {
            var response = await _client.GetAsync("https://localhost:44308/api/Users/" + id + "/GetUserName/");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("GetUsername apiresponse: " + apiResponse);
                return apiResponse;
            }
            else
            {
                Debug.WriteLine(response.StatusCode.ToString());
                return null;
            }
        }

        public async Task<User> LogIn(string username, string password)
        {
            var response = await _client.GetAsync("https://localhost:44308/api/Users/LogIn?username=" + username + "&password=" + password);
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

        public async Task<List<User>> GetUsersWithAppointments()
        {
            IEnumerable<Appointment> appointmentss;
            var response = await _client.GetAsync("https://localhost:44308/api/Users/");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                List<User> users = (List<User>)JsonConvert.DeserializeObject<List<User>>(apiResponse);

                foreach (User user in users) {
                    appointmentss = await GetAppointmentsForUser(user.UserID);

                    if (appointmentss != null)
                    {
                        user.Appointments = appointmentss;
                        user.AppointmentSitters = user.AppointmentAsSitter();
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

        public async Task<IEnumerable<User>> GetAvailableSittersForDate(DateTime StartDate, DateTime EndDate)
        {
            IEnumerable<Appointment> appointments;
            string start = StartDate.ToString("yyyy-MM-dd HH:mm:ss");
            string end = EndDate.ToString("yyyy-MM-dd HH:mm:ss");
            var response = await _client.GetAsync("https://localhost:44308/api/Users/GetAvailableSittersForDates?StartDate=" + start + "&EndDate=" + end);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                List<User> users = (List<User>)JsonConvert.DeserializeObject<List<User>>(apiResponse);

                foreach (User user in users)
                {
                    appointments = await GetAppointmentsForUser(user.UserID);
                    if (appointments != null)
                    {
                        user.Appointments = appointments;
                    }
                    else
                    {
                        return null;
                    }
                }
                return users;
            }
            else
            {
                return null;
            }
        }

        //Review API calls
        public async Task<Review> PostReview(ReviewDTO form)
        {
            var response = await _client.PostAsJsonAsync("https://localhost:44308/api/Reviews", form);
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

        //Availability API calls
        public async Task<Availability> PostAvailability(AvailabilityDTO form)
        {
            Debug.WriteLine("In PostAvailability api controller method");
            HttpResponseMessage response = await _client.PostAsJsonAsync("https://localhost:44308/api/Availabilitys/CreateAvailability/", form);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Response success");
                string apiResponse = await response.Content.ReadAsStringAsync();

                return (Availability)JsonConvert.DeserializeObject<Availability>(apiResponse);
            }
            else
            {
                Debug.WriteLine("API Error: " + response.StatusCode.ToString());
                return null;
            }
        }

        //Appointment API calls
        public async Task<Appointment> PostAppointment(AppointmentDTO form)
        {
            var response = await _client.PostAsJsonAsync("https://localhost:44308/api/Appointments/CreateAppointment/", form );
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
            var response = await _client.GetAsync("https://localhost:44308/api/Users/" + id + "/Appointments");
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

        public async Task<Appointment> UpdateAppointmentStatus(int id, string newStatus)
        {
            var response = await _client.GetAsync("https://localhost:44308/api/Appointments/UpdateAppointmentStatus?id=" + id + "&newStatus=" + newStatus);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Success in api call");
                return null;
            }
            else
            {
                Debug.WriteLine(response.Content.ToString());
                return null;
            }
        }

        public async Task<List<Availability>> GetAvailabilitiesForUser(int id)
        {
            var response = await _client.GetAsync("https://localhost:44308/api/Users/" + id + "/Availabilities");
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
            var response = await _client.GetAsync("https://localhost:44308/api/Appointments/" + id + "/Pets");
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

        public async Task<List<Message>> GetConversationBetweenTwoUsers(int id)
        {
           
            var response = await _client.GetAsync("https://localhost:44308/api/Messages/");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                List<Message> messages = (List<Message>)JsonConvert.DeserializeObject<List<Message>>(apiResponse);
                List<Message> messagesARenvoyer = new List<Message>();
                foreach (Message message in messages.ToList())
                {
                    Debug.WriteLine("Nombre de messages dans liste: " + messages.ToList().Count());
                    if (message.Sender == id || message.Recipient==id)
                    {

                        messagesARenvoyer.Add(message);
                        Debug.WriteLine(message.Sender + "  " + message.Content);

                    }
                    
                }
                return messagesARenvoyer;
            }
            return null;
        }

        //Messages API Calls
        public async Task<ConversationDTO> GetConversation(int userID, int recipientID)
        {
            var response = await _client.GetAsync("https://localhost:44308/api/Messages/GetConversation?userID=" + userID + "&recipientID=" + recipientID);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                IEnumerable<Message> messages = (IEnumerable<Message>)JsonConvert.DeserializeObject<IEnumerable<Message>>(apiResponse);
                return new ConversationDTO
                {
                    UserID = userID,
                    RecipientID = recipientID,
                    Conversation = messages
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<List<KeyValuePair<string, Message>>> GetLastMessages(int id)
        {
            List<KeyValuePair<string, Message>> LastMessages = new List<KeyValuePair<string, Message>>();
            string username;
            var response = await _client.GetAsync("https://localhost:44308/api/Messages/" + id + "/GetLastMessages");
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                IEnumerable<Message> messages = (IEnumerable<Message>)JsonConvert.DeserializeObject<IEnumerable<Message>>(apiResponse);
                Debug.WriteLine("Amount of messages : " + messages.Count());
                List<KeyValuePair<string, Message>> lastMessages = new List<KeyValuePair<string, Message>>();
                foreach (Message message in messages)
                {
                    if(message.Sender == id)
                        username = await GetUsername(message.Recipient);
                    else
                        username = await GetUsername(message.Sender);

                    lastMessages.Add(new KeyValuePair<string, Message>(username, message));
                }

                return lastMessages;
            }
            else
            {
                return null;
            }
        }
    }
}
