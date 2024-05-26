using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using Avalonia.Data;
using Avalonia.Data.Converters;
using eftest;

namespace AvaloniaApplication1.Models;

public class Converter : IValueConverter
{
    public static readonly Converter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter,CultureInfo culture)
    {
        if (value is ObservableCollection<modelCurrent.Book> book)
        {
            
            return book.Select(b=> new BookObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Author> author)
        {
            return author.Select(b=> new AuthorObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Publisher> publishers)
        {
            
            return publishers.Select(b=> new PublisherObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Genre> genres)
        {
            
            return genres.Select(b=> new GenreObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Location> locations)
        {
            
            return locations.Select(b=> new LocationObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Customer> customers)
        {
            
            return customers.Select(b=> new CustomerObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Employee> employee)
        {
            return employee.Select(b=> new EmployeeObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.Order> orders)
        {
            return orders.Select(b=> new OrderObservable(b));
        }
        if (value is ObservableCollection<modelCurrent.OrderLine> orderLines)
        {
            return orderLines.Select(b=> new OrderLineObservable(b));
        }
     
        // converter used for the wrong type
        return value;
    }

    public object ConvertBack(object? value, Type targetType, 
        object? parameter, CultureInfo culture)
    {
        if (value is ObservableCollection<BookObservable> bookObservable)
        {
            return  bookObservable.Select(b=>b.ReturnBase());
        }
        return new BindingNotification(new InvalidCastException(), 
            BindingErrorType.Error);
    }
}

public class converted<T>
{
    protected T baseType;
    public T ReturnBase()
    {
        return baseType;
    }
}
public class BookObservable:converted<modelCurrent.Book>
{ 
    
    public string Name {  set; get; }
    public int Price {  set; get; }
    public string? Author {  set; get; }
    public string Publisher {  set; get; }
    public int Article {  set; get; }
    public string Location { set; get; }
    public string Genres  { set; get; }
  

    public BookObservable(modelCurrent.Book BaseBook)
    {
        baseType = BaseBook;
        Name = BaseBook.name;
        Author = BaseBook.author.name;
        Publisher = BaseBook.publisher.name;
        Location = BaseBook.location.name;
        Genres = string.Join(", ",BaseBook.genres);
        Price = baseType.price;
        Article = baseType.article;
    }
}

public class AuthorObservable:converted<modelCurrent.Author>
{
   
    public string? name { get; set; }

    public AuthorObservable( modelCurrent.Author baseAuthor)
    {
        baseType = baseAuthor;
        name = baseAuthor.name;
    }
}
public class PublisherObservable:converted<modelCurrent.Publisher>
{
    public string? name { get; set; }

    public PublisherObservable(modelCurrent.Publisher basePublisher)
    {
        baseType = basePublisher;
        name = basePublisher.name;
    }
}
public class GenreObservable:converted<modelCurrent.Genre>
{
    public string? name { get; set; }

    public GenreObservable(modelCurrent.Genre baseGenre)
    {
        baseType = baseGenre;
        name = baseGenre.name;
    }
}
public class LocationObservable:converted<modelCurrent.Location>
{ 
    public string? name { get; set; }

    public LocationObservable(modelCurrent.Location baseLocation)
    {
        baseType = baseLocation;
        name = baseLocation.name;
    }
}
public class CustomerObservable:converted<modelCurrent.Customer>
{
    public string FIO { get; set; }
    public int bonuses { get; set; }
    
    public CustomerObservable(modelCurrent.Customer baseCustomer)
    {
        baseType = baseCustomer;
        FIO = baseCustomer.FIO;
        bonuses = baseCustomer.bonuses;
    }
}
public class EmployeeObservable:converted<modelCurrent.Employee>
{
    public string FIO { get; set; }
    public int rating { get; set; }
    
    public  EmployeeObservable(modelCurrent.Employee baseEmployee)
    {
        baseType =baseEmployee;
        FIO = baseEmployee.FIO;
        rating = baseEmployee.rating;
    }
}

public class OrderObservable:converted<modelCurrent.Order>
{
   
    public string Employee { get; set; } 
    public string Customer { get; set; } 
    public int sum {  get; set; }
    public  OrderObservable(modelCurrent.Order baseOrder)
    {
        baseType =baseOrder;
       Employee= baseOrder.Employee.FIO;
        Customer = baseOrder.Customer.FIO;
        sum = baseOrder.sum;
    }

}
public class OrderLineObservable:converted<modelCurrent.OrderLine>
{
  
    public string book { get; set; }
    public int price { get; set; }

    public int amount { get; set; }
    
    
    public  OrderLineObservable(modelCurrent.OrderLine baseOrder)
    {
        baseType =baseOrder;
        amount = baseOrder.amount;
        book = baseOrder.book.name;
        price = baseOrder.book.price;
    }
     
}