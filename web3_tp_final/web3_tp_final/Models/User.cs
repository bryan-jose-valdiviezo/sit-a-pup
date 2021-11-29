using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;

namespace web3_tp_final.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Minimum 3 caractères, maximum 20 caractères")]
        [MinLength(3), MaxLength(20)]
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Entrez une adresse valide (ex: nom@courriel.com)")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vous devez entrer une adresse")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Vous devez entrer un numéro de téléphone valide")]
        //[RegularExpression(@"\d{3}[-]\d{3}[-]\d{4}")]
        public string PhoneNumber { get; set; }

        public List<Pet> Pets { get; set; } = new List<Pet>();

        public List<Availability> Availabilities { get; set; } = new List<Availability>();

        public List<Message> Messages { get; set; } = new List<Message>();

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Appointment> AppointmentOwners { get; set; }

        public IEnumerable<Appointment> AppointmentSitters { get; set; }

        public int AverageRating()
        {
            IEnumerable<Appointment> appointmentsAsSitter = Appointments.Where(appointment => appointment.Sitter.UserID == UserID && appointment.Reviews.Any());
            if(appointmentsAsSitter.Any())
            {
                IEnumerable<int> reviews = appointmentsAsSitter.SelectMany(appointment => appointment.Reviews).Select(review => review.Stars);
                return reviews.Sum() / appointmentsAsSitter.Count();
            }

            return 0;
        }

        public IEnumerable<Appointment> AppointmentAsSitter()
        {
            return Appointments.Where(appointment => appointment.Sitter.UserID == UserID && appointment.Reviews.Any());           
        }
    }
}
