﻿
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels.DialogsViewModels;
using AvaloniaApplication1.Views;
using AvaloniaApplication1.Views.dialogs;
using Npgsql;
using ReactiveUI;
namespace AvaloniaApplication1.ViewModels;
using static eftest.modelCurrent;

public class MainWindowViewModel : ViewModelBase
{
   public enum TabNames
    {
        books,
        authors,
        publishers,
        genres,
        locations,
        orders,
        employees,
        clients,
        receipts
    }

    private TabNames currentTable=TabNames.books;
    public static ApplicationContext context;
    private bool twoTables=false;
    public bool HasTwoTables { get=>twoTables; set=>this.RaiseAndSetIfChanged(ref twoTables, value); }
    private object dtt;

    public object Data
    {
        get => dtt;
        set=> this.RaiseAndSetIfChanged(ref dtt, value);
    }
    private object selected;

    public object Selected
    {
        get => selected;
        set=> this.RaiseAndSetIfChanged(ref selected, value);
    }
    private object lines;

    public object Orderlines
    {
        get => lines;
        set=> this.RaiseAndSetIfChanged(ref lines, value);
    }
    private int span=2;

    public int Span
    {
        get => span;
        set=> this.RaiseAndSetIfChanged(ref span, value);
    }
    
    public ReactiveCommand<Unit, Unit> BookClick { get; private set; }
    public ReactiveCommand<Unit, Unit> AuthorClick { get; private set; }
    public ReactiveCommand<Unit, Unit>   PublisherClick { get; private set; }
    public  ReactiveCommand<Unit, Unit>  GenreClick { get; private set; }
    public  ReactiveCommand<Unit, Unit> LocationClick { get; private set; }
    public  ReactiveCommand<Unit, Unit> ClientClick { get; private set; } 
    public  ReactiveCommand<Unit, Unit> EmployeeClick { get; private set; } 
    public  ReactiveCommand<Unit, Unit> OrderClick { get; private set; }
    public  ReactiveCommand<Unit, Unit> ReceiptClick { get; private set; }
    public  ReactiveCommand<Unit, Unit> Save { get; private set; } 
    public  ReactiveCommand<Unit, Unit> Add{ get; private set; }
    public  ReactiveCommand<Unit, Unit> Delete { get; private set; }
    public  ReactiveCommand<Unit, Unit> Update { get; private set; }
    public  ReactiveCommand<Unit, Unit> BookSaleStat { get; private set; }
    public  ReactiveCommand<Unit, Unit> AuthorGenreStat { get; private set; }
    public  ReactiveCommand<Unit, Unit> CustomerFavGenre{ get; private set; }
    public  ReactiveCommand<Unit, Unit>  AuthorStat { get; private set; }
    public  ReactiveCommand<Unit, Unit> GenreStat { get; private set; }
    public  ReactiveCommand<Unit, Unit>   IncomeAndOutcomeThisMonthh { get; private set; }
    public MainWindowViewModel()
    {
        context = new ApplicationContext();
        ShowDialog = new Interaction<baseDialog, object?>();
            // dtt = new ObservableCollection<Book>(context.Books.Include(u => u.genres).Include(u => u.author)
           // .Include(u => u.publisher).Include(u => u.location).ToList());
           CommandBinding();
StatisticCommandBinding();
    }
    public void StatisticCommandBinding()
    {
        BookSaleStat = ReactiveCommand.Create(() =>
        {
                Data = new ObservableCollection<BookSaleStats>(context.BookSaleStats.FromSqlRaw("Select * from book_stats").ToList());
     
        });
        AuthorGenreStat = ReactiveCommand.Create(() =>
        {
            Data = new ObservableCollection<AuthorGenreStats>(context.AuthorGenreStats.FromSqlRaw("Select * from author_genre_stats").ToList());
     
        });
        CustomerFavGenre = ReactiveCommand.Create(() =>
        {
            Data = new ObservableCollection<CustomerFavGenres>(context.CustomerFavGenres.FromSqlRaw("Select * from customers_favourite_genres").ToList());
     
        });
        AuthorStat = ReactiveCommand.Create(() =>
        {
            Data = new ObservableCollection<AuthorStats>(context.AuthorStats.FromSqlRaw("Select * from author_stats").ToList());
     
        });
        GenreStat = ReactiveCommand.Create(() =>
        {
            Data = new ObservableCollection<GenreStats>(context.GenreStats.FromSqlRaw("Select * from genre_stats").ToList());
     
        });
        IncomeAndOutcomeThisMonthh = ReactiveCommand.Create(() =>
        {
            Data = new ObservableCollection<IncomeAndOutcomeThisMonth>(context.IncomeAndOutcomeThisMonth.FromSqlRaw("Select * from income_and_outcome_this_month").ToList());
     
        });
    }
    public void CommandBinding()
    {
       
        BookClick=ReactiveCommand.Create(() => { 
            currentTable = TabNames.books;
            HasTwoTables = false;
            Span = 2;  Data = new ObservableCollection<Book>(
            context.Books
                .Include(u => u.genres)
                .Include(u => u.author)
            .Include(u => u.publisher)
                .Include(u => u.location)
                .ToList().Where(x=>!x.deleted)); });
        AuthorClick = ReactiveCommand.Create(() =>
        {
            currentTable = TabNames.authors;
            HasTwoTables = false;
             Span = 2;
            Data = new ObservableCollection<Author>(context.Authors.ToList().Where(x=>!x.deleted));
        });
        PublisherClick = ReactiveCommand.Create(() =>
        {  HasTwoTables = false;
            currentTable = TabNames.publishers;
            Span = 2;
            Data = new ObservableCollection<Publisher>(context.Publishers.ToList().Where(x=>!x.deleted));
        });
        GenreClick  = ReactiveCommand.Create(() =>
        {  HasTwoTables = false;
            currentTable = TabNames.genres;
            Span = 2;
            Data = new ObservableCollection<Genre>(context.Genres.ToList().Where(x=>!x.deleted));
        });
        LocationClick= ReactiveCommand.Create(() =>
        {
            currentTable = TabNames.locations;
            HasTwoTables = false;
            Span = 2;
            Data = new ObservableCollection<Location>(context.Locations.ToList().Where(x=>!x.deleted));
        });
        ClientClick = ReactiveCommand.Create(() =>
        {
            currentTable = TabNames.clients;
            HasTwoTables = false;
            Span = 2;
            Data = new ObservableCollection<Customer>(   context.Customers.ToList().Where(x=>!x.deleted));
        });
        EmployeeClick = ReactiveCommand.Create(() =>
        {
            currentTable = TabNames.employees; 
            HasTwoTables = false;
            Span = 2;
            Data = new ObservableCollection<Employee>(context.Employees.ToList().Where(x=>!x.deleted));
        });
        OrderClick  = ReactiveCommand.Create(() =>
        {
            HasTwoTables = true;
            Span = 1;
            currentTable = TabNames.orders;
            Data = new ObservableCollection<Order>(context.Orders
                .Include(x=>x.OrderLines)
                .ThenInclude(x=>x.book)
                .Include(x=>x.Customer)
                .Include(x=>x.Employee)
                .ToList());
        });
        ReceiptClick = ReactiveCommand.Create(() =>
        {
            currentTable = TabNames.receipts; 
            HasTwoTables = false;
            Span = 2;
            Data = new ObservableCollection<Receipt>(context.Receipts.Include(x=>x.book).ToList().Where(x=>!x.deleted));
        });
        Save=   ReactiveCommand.Create(() =>
        {
            context.SaveChanges();
            
        });
       Add=  ReactiveCommand.CreateFromTask(async () =>
       {
       object newRow=null;
       baseDialog d= StartAddEdit();
       newRow = await ShowDialog.Handle(d);
       
       if (newRow != null)
       {
       
           AddToTable(newRow);
       }
    
       });
      Delete=  ReactiveCommand.Create(() =>
      {      
          if (Selected==null)
          {
              return;
          }
          switch (currentTable)
          {
             
              case TabNames.books:
                  Book b = (Selected as BookObservable).ReturnBase();
              b.deleted=true;
                  context.Books.Update(b);
                  Data = new ObservableCollection<Book>( context.Books.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.authors:
                  Author a = (Selected as AuthorObservable).ReturnBase();
                  a.deleted=true;
                  context.Authors.Update(a);
                  Data = new ObservableCollection<Author>( context.Authors.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.publishers:
                  Publisher p = (Selected as PublisherObservable).ReturnBase();
                  p.deleted=true;
                  context.Publishers.Update(p);
                  Data = new ObservableCollection< Publisher>( context. Publishers.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.genres:
                  Genre g = (Selected as GenreObservable).ReturnBase();
                 g.deleted=true;
                  context.Genres.Update(g);
                  Data = new ObservableCollection<Genre>( context.Genres.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.locations:
                  Location l = (Selected as LocationObservable).ReturnBase();
                  l.deleted=true;
                  context.Locations.Update(l);
                  Data = new ObservableCollection< Location>( context. Locations.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.clients:  
                  Customer c = (Selected as CustomerObservable).ReturnBase();
                  c.deleted=true;
                  context.Customers.Update(c);
                  Data = new ObservableCollection< Customer>( context. Customers.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.employees:
                  Employee e = (Selected as EmployeeObservable).ReturnBase();
                  e.deleted=true;
                  context.Employees.Update(e);
                  Data = new ObservableCollection<Employee>( context.Employees.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.orders:
                  Order o = (Selected as OrderObservable).ReturnBase();
                  o.deleted=true;
                  context.Orders.Update(o);
                  Data = new ObservableCollection<Order>( context. Orders.Local.ToList().Where(x=>!x.deleted));
                  break;
              case TabNames.receipts:
                 Receipt r = (Selected as ReceiptObservable).ReturnBase();
                  r.deleted=true;
                  context.Receipts.Update(r);
                  Data = new ObservableCollection<Receipt>( context. Receipts.Local.ToList().Where(x=>!x.deleted));
                  break;
              default: return;
          }

   
      });
     Update=  ReactiveCommand.CreateFromTask(async () =>
      {
          if (Selected != null)
          {
              StartEdit();
          }

      });
    }

    public async void StartEdit()
    {   object newRow=null;
        baseDialog d=null;

        switch (currentTable)
        {
            case TabNames.books:
                d= createDialog( new BookAddEdit(),new BookAddEditvm(((BookObservable)Selected).ReturnBase(),context.Authors.ToList(),context.Genres.ToList(),context.Locations.ToList(),context.Publishers.ToList()));
            
                break;
            case TabNames.authors:
           
                d = createDialog(new AuthorAddEdit(), new AuthorAddEditvm(((AuthorObservable)Selected).ReturnBase()));
            
                break;
            case TabNames.publishers:
                d= createDialog(  new PublisherAddEdit(),new PublisherAddEditvm(((PublisherObservable)Selected).ReturnBase()));
                break;
            case TabNames.genres:
                d= createDialog(new GenreAddEdit(),new GenreAddEditvm(((GenreObservable)Selected).ReturnBase()));
             
                break;
            case TabNames.locations:
                d= createDialog(  new LocationAddEdit(),new LocationAddEditvm(((LocationObservable)Selected).ReturnBase()));
           
                break;
            case TabNames.clients:
                d= createDialog(  new CustomerAddEdit(),new CustomerAddEditvm(((CustomerObservable)Selected).ReturnBase()));
    
                break;
            case TabNames.employees:
                d= createDialog(  new EmployeeAddEdit(),new EmployeeAddEditvm(((EmployeeObservable)Selected).ReturnBase()));
           
                break;
            case TabNames.orders:
                d= createDialog(  new OrderAddEdit(),new OrderAddEditvm(((OrderObservable)Selected).ReturnBase(),context.Customers.ToList(),context.Employees.ToList(),context.Books.ToList()));
           
                break;
            case TabNames.receipts:
                d= createDialog(  new ReceiptAddEdit(),new ReceiptAddEditvm(((ReceiptObservable)Selected).ReturnBase(),
                    context.Books.Local.ToList()));
                break;
        }
        newRow = await ShowDialog.Handle(d);
        if (newRow == null)
        {
            return;
        }
        switch (currentTable)
        {
            case TabNames.books:
                context.Books.Update((Book)newRow);
                Data =new ObservableCollection<Book>( context.Books.Local.ToList().Where(x=>!x.deleted));
            
                break;
            case TabNames.authors:
                context.Authors.Update((Author)newRow);
                Data =new ObservableCollection<Author>( context.Authors.Local.ToList().Where(x=>!x.deleted));
                break;
            case TabNames.publishers:
                context.Publishers.Update((Publisher)newRow);
                Data =new ObservableCollection<Publisher>( context.Publishers.Local.ToList().Where(x=>!x.deleted));
                break;
            case TabNames.genres:
                context.Genres.Update((Genre)newRow);
                Data =new ObservableCollection<Genre>( context.Genres.Local.ToList().Where(x=>!x.deleted));
             
                break;
            case TabNames.locations:
                context.Locations.Update((Location)newRow);
                Data =new ObservableCollection<Location>( context.Locations.Local.ToList().Where(x=>!x.deleted));
                break;
            case TabNames.clients:
                context.Customers.Update((Customer)newRow);
                Data =new ObservableCollection<Customer>( context.Customers.Local.ToList().Where(x=>!x.deleted));
                break;
            case TabNames.employees:
                context.Employees.Update((Employee)newRow);
                Data =new ObservableCollection<Employee>( context.Employees.Local.ToList().Where(x=>!x.deleted));
                break;
            case TabNames.orders:
                context.Orders.Update((Order)newRow);
                Data =new ObservableCollection<Order>( context.Orders.Local.ToList().Where(x=>!x.deleted));
           
                break;
            case TabNames.receipts:
                context.Receipts.Update((Receipt)newRow);
                Data =new ObservableCollection<Receipt>( context.Receipts.Local.ToList().Where(x=>!x.deleted));
           
                break;
          
        }
    }
    private baseDialog StartAddEdit()
    {
        baseDialog d=null;
        switch (currentTable)
        {
            case TabNames.books:
                d= createDialog( new BookAddEdit(),new BookAddEditvm(
                    context.Authors.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()
                    ,context.Genres.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()
                    ,context.Locations.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()
                    ,context.Publishers.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()));
            
                break;
            case TabNames.authors:
                d= createDialog(new AuthorAddEdit(),new AuthorAddEditvm());
            
                break;
            case TabNames.publishers:
                d= createDialog(  new PublisherAddEdit(),new PublisherAddEditvm());
                break;
            case TabNames.genres:
                d= createDialog(new GenreAddEdit(),new GenreAddEditvm());
             
                break;
            case TabNames.locations:
                d= createDialog(  new LocationAddEdit(),new LocationAddEditvm());
           
                break;
            case TabNames.clients:
                d= createDialog(  new CustomerAddEdit(),new CustomerAddEditvm());
    
                break;
            case TabNames.employees:
                d= createDialog(  new EmployeeAddEdit(),new EmployeeAddEditvm());
           
                break;
            case TabNames.orders:
                d= createDialog(  new OrderAddEdit(),new OrderAddEditvm(context.Customers.ToList().Select(x=>x).Where(x=>!x.deleted).ToList(),
                    context.Employees.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()
                    ,context.Books.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()));
           
                break;
            case TabNames.receipts:
                d= createDialog(  new ReceiptAddEdit(),new ReceiptAddEditvm(context.Books.ToList().Select(x=>x).Where(x=>!x.deleted).ToList()));
                break;
        }

        return d;
    }
    private baseDialog createDialog(baseDialog d, AddEditViewModel vm)
    {
        baseDialog t;
        t = d;
        d.DataContext =vm;
        return t;
        
    }
    private string text;

    public string Text
    {
        get => text;
        set=> this.RaiseAndSetIfChanged(ref text, value);
    }
    public Interaction<baseDialog, object?> ShowDialog { get; private set; }
    public void OrderLinesUpdate(object a)
    { if(HasTwoTables){
        Orderlines= new ObservableCollection<OrderLine>(((converted<Order>)a).ReturnBase().OrderLines);
        }
    }

    public void AddToTable(object newRow)
    {
     
        if (newRow != null)
        {
            switch (currentTable)
            {
                case TabNames.books:
                    context.Books.Add(newRow as Book);
                    Data= new ObservableCollection<Book>(context.Books.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.authors:
                    context.Authors.Add(newRow as Author);
                    Data= new ObservableCollection<Author>(context.Authors.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.genres:
                    context.Genres.Add(newRow as Genre);
                    Data= new ObservableCollection<Genre>(context.Genres.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.locations:
                    context.Locations.Add(newRow as Location);
                    Data= new ObservableCollection<Location>(context.Locations.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.employees:
                    context.Employees.Add(newRow as Employee);
                    Data= new ObservableCollection<Employee>(context.Employees.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.clients:
                    context.Customers.Add(newRow as Customer);
                   Data= new ObservableCollection<Customer>(context.Customers.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.orders:
                    context.Orders.Add(newRow as Order);
                    Data = new ObservableCollection<Order>(context.Orders.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.publishers:
                    context.Publishers.Add(newRow as Publisher);
                Data = new ObservableCollection<Publisher>(context.Publishers.Local.ToList().Where(x=>!x.deleted));
                    break;
                case TabNames.receipts:
                    Text = "debug3";
                    context.Receipts.Add(newRow as Receipt);
                    Data = new ObservableCollection<Receipt>(context.Receipts.Local.ToList().Where(x=>!x.deleted));
                    break;
                default: break;
            }
        }
    }

}