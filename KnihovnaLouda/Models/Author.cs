using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KnihovnaLouda.Models
{
    public class Author
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }

        public string Name { get; set; }

        public string? PhotoPath { get; set; }

        public ICollection<Book>? Books { get; set; }
    }
}
