namespace KnihovnaLouda.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public string? UserId { get; set; }
        public ApplicationsUser? User { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
