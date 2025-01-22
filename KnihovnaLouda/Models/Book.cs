using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace KnihovnaLouda.Models
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title{ get; set; }

        public string Description{ get; set; }

        public DateTime Published {  get; set; }

        public int? AuthorId { get; set; } 
        public Author? Author { get; set; }

        public string? PhotoPath { get; set; }
    }
}
