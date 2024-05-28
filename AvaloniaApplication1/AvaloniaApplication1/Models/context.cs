using Microsoft.EntityFrameworkCore;

using static eftest.modelCurrent;
using System.Collections.Generic;

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Location> Locations { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<BooksGenre> BooksGenres { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Receipt> Receipts { get; set; } = null!;
    public DbSet<OrderLine> OrderLines { get; set; } = null!;
    public DbSet<BookSaleStats> BookSaleStats { get; set; } = null!;
    public DbSet<AuthorStats> AuthorStats { get; set; } = null!;
    public DbSet<AuthorGenreStats>AuthorGenreStats { get; set; } = null!;
    public DbSet<GenreStats> GenreStats { get; set; } = null!;
    public DbSet<CustomerFavGenres> CustomerFavGenres { get; set; } = null!;
    public DbSet<IncomeAndOutcomeThisMonth> IncomeAndOutcomeThisMonth { get; set; } = null!;
    
public string Conn="Host=localhost;Port=5432;Database=bookShop;Username=postgres;Password=1234";
    public ApplicationContext()
    {
        Database.EnsureCreated();
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Conn);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        modelBuilder
              .Entity<Book>()
              .HasMany(c => c.genres)
              .WithMany(s => s.books)
              .UsingEntity<BooksGenre>(
                 j => j
                  .HasOne(pt => pt.genre)
                  .WithMany(t => t.BooksGenres)
                  .HasForeignKey(pt => pt.genreid),
              j => j
                  .HasOne(pt => pt.book)
                  .WithMany(p => p.booksGenres)
                  .HasForeignKey(pt => pt.bookid),
              j =>
              {
                  j.HasKey(t => new { t.genreid, t.bookid });
              });

    }

  
}