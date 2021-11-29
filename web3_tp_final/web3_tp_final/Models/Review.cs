namespace web3_tp_final.Models
{
    public class Review
    {
        public int ReviewID { get; set; }

        public int AppointmentId { get; set; }

        public int UserId { get; set; }

        public int Stars { get; set; }

        public string Comment { get; set; }

        public Appointment Appointment { get; set; }

        public User User { get; set; }
    }
}
