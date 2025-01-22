using KnihovnaLouda.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnihovnaLouda.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitní definice cizího klíče
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)  // Pokud Author může mít více Books
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); // Nebo jiný DeleteBehavior podle potřeby
        }

        public DbSet<KnihovnaLouda.Models.Book> Books { get; set; } = default!;
        public DbSet<KnihovnaLouda.Models.Author> Authors { get; set; } = default!;
        public DbSet<KnihovnaLouda.Models.Reservation> Reservations { get; set; }
    }
}
