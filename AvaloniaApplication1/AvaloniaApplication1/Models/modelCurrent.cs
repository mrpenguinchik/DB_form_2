using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eftest
{
    public class modelCurrent
    {
        [Table("books")]
        public class Book
        {
            [Key]
            [Column("id")]
           public int id { get; set; }
            [Column("name")]
            public  string name { get; set; }
            [Column("price")]
            public float price { get; set; }
            [Column("barcode")]
            public  string barcode { get; set; }
            [Column("article")]
            public   int article { get; set; }
            [Column("authorid")]
            [ForeignKey("books_authorid_fkey")]
            public   int    authorId { get; set; }
            [Column("publisherid")]
            [ForeignKey("books_publisherid_fkey")]
            public   int publisherId { get; set; }
            [Column("locationid")]
            [ForeignKey("books_locationid_fkey")]
            public   int locationId {  get; set; }
          
            public Author author { get; set; } = new();
    
            public Publisher publisher {  get; set; } = new();
           
            public Location location { get; set; } = new();
            public List<Genre> genres {  get; set; } = new();
            public List<BooksGenre> booksGenres {  get; set; } = new();
            public override string ToString()
            {
                return name;
            }
            [Column("deleted")]
            public bool deleted;
        }
        [Table("authors")]
        public class Author
        {
            [Key]
            [Column("id")]
            public int id { get; set; }
            [Column("name")]
            public string? name { get; set; }
            [Column("deleted")]
            public bool deleted;
            public override string? ToString() { return name; }
            public List<Book> books { get; set; } = new();
            
        }
        [Table("publisher")]
        public class Publisher
        {
            [Key]
            [Column("id")]
            public int id { get; set; }
            [Column("name")]
            public string name { get; set; }
            public override string ToString() { return name; }
            [Column("deleted")]
            public bool deleted;
            public List<Book> books { get; set; } = new();
        }
        [Table("location")]
        public class Location
        {
            [Key]
            [Column("id")]
            public int id { get; set; }
            [Column("name")]
            public string name { get; set; }
            [Column("deleted")]
            public bool deleted;
            public override string ToString() { return name; }
            public List<Book> books { get; set; } = new();
        }
        [Table("genre")]
        public class Genre
        {
            [Key]
            [Column("id")]

            public int id { get; set; }
            [Column("name")]
            public string name { get; set; }
            public List<Book> books { get; set; }=new();
            [Column("deleted")]
            public bool deleted;
            public List<BooksGenre> BooksGenres { get; set; } = new();
            public override string ToString()
            {
                return name;
            }
        }
        [Table("booksgenres")]
        public class BooksGenre
        {
            [Key]
            [Column("bookid")]
            [ForeignKey("booksgenres_bookid_fkey")]
            public int bookid { get; set; }
 
            public Book book { get; set; }
           
            [Column("genreid")]
            [ForeignKey("booksgenres_genreid_fkey")]
            public int genreid { get; set; }
            public Genre genre { get; set; }
            [Column("deleted")]
            public bool deleted;

        }
        [Table("customers")]
        public class Customer
        {
            [Key]
            [Column("id")]

            public int id { get; set; }
            [Column("fio")]
            public string FIO { get; set; }
            [Column("bonuses")]
            public int bonuses { get; set; }
            [Column("deleted")]
            public bool deleted;
            public List<Order> orders { get; set; }= new();
        
            public override string ToString()
            {
                return FIO;
            }
        }
        [Table("employees")]
        public class Employee
        {
            [Key]
            [Column("id")]

            public int id { get; set; }
            [Column("fio")]
            public string FIO { get; set; }
            [Column("rating")]
            public int rating { get; set; }
            [Column("salary")]
            public float salary{ get; set; }
            public List<Order> orders { get; set; } = new();
            [Column("deleted")]
            public bool deleted;
            public override string ToString()
            {
                return FIO;
            }
            
        }
        [Table("orders")]
        public class Order
        {
            [Key]
            [Column("id")] 
            public int id { get; set; }
            
            [Column("customerid")]
            [ForeignKey("customerid_fkey")]
            public int customerid { get; set; }
            
            [Column("employeeid")]
            [ForeignKey("employeeid_fkey")]
            public int employeeid { get; set; }
            
            [Column("sum")]
            public float sum {  get; set; }
            public Employee Employee { get; set; } = new();
            public Customer Customer { get; set; } = new();
            public List<OrderLine> OrderLines { get; set; }= new();
            [Column("deleted")]
            public bool deleted;
            
            public override string ToString()
            {
                return id.ToString();
            }
        }
        [Table("orderlines")]
        public class OrderLine
        {
            [Key]
            [Column("id")]

            public int id { get; set; }
            [Column("bookid")]
            [ForeignKey("orderlines_bookid_fkey")]
            public int bookid { get; set; }
            [Column("orderid")]
            [ForeignKey("orderlines_orderid_fkey")]
            public int orderid { get; set; }
            [Column("amount")]
            public int amount { get; set; }
            public Order order { get; set; } = new();
            public Book book { get; set; }
            public override string ToString()
            {
                return book.name+"  "+amount.ToString();
            }
            [Column("deleted")]
            public bool deleted;
        }
        [Table("receipt")]
        public class Receipt
        {
            [Key]
            [Column("id")]

            public int id { get; set; }
            [Column("bookid")]
            [ForeignKey("receipt_bookid_fkey")]
            public int bookid { get; set; }
            [Column("amount_of_books")]
            public int amount { get; set; }
            public Book book { get; set; }

            [Column("sum")]
            public float sum { get; set; }
            public override string ToString()
            {
                return book.name+"  "+amount.ToString();
            }
            [Column("deleted")]
            public bool deleted;
        }
        [Keyless]
        public class BookSaleStats
        {     [Column("book_name")]
            public string book_name { get; set; }
            [Column("amount")]
            
            public int amount_of_sales { get; set; }
            [Column("price")]
            public float sum_of_money { get; set; }
            
        }
        [Keyless]
        public class AuthorStats
        {     [Column("author_name")]
            public string author_name { get; set; }
            [Column("amount")]
            
            public int amount_of_sales { get; set; }
            [Column("price")]
            public float sum_of_money { get; set; }
            
        }
        [Keyless]
        public class AuthorGenreStats
        {    [Column("author_name")]
            public string author_name { get; set; }
            [Column("genre_name")]
            public string genre_name { get; set; }
            [Column("am")]
            
            public int amount_of_sales { get; set; }
            [Column("price")]
            public float sum_of_money { get; set; }
            
        }
        [Keyless]
        public class CustomerFavGenres
        {     [Column("fio")]
            public string FIO_of_customer { get; set; }
            [Column("name")]
            public string genre_name { get; set; }
            [Column("am")]
            
            public int amount_of_sales { get; set; }
            [Column("price")]
            public float sum_of_money { get; set; }
            
        }
        [Keyless]
        public class GenreStats
        {        [Column("name")]
            public string genre_name { get; set; }
            [Column("am")]
            
            public int amount_of_sales { get; set; }
            [Column("price")]
            public float sum_of_money { get; set; }
            
        }
        [Keyless]
        public class IncomeAndOutcomeThisMonth
        {     [Column("income")]
            public float income { get; set; }
            [Column("outcome")]
            
            public float  outcome { get; set; }
            [Column("fin")]
            public float final { get; set; }
            
        }
    }
}
