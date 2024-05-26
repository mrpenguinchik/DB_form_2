using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AvaloniaApplication1.Views;
using eftest;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using ReactiveUI;
namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class OrderAddEditvm: AddEditViewModel
{
    private modelCurrent.Customer customer;
    private modelCurrent.Employee employee;
    private modelCurrent.Book book;
    public modelCurrent.Customer SelectedCustomer  {
        get => customer;
        set=> this.RaiseAndSetIfChanged(ref customer, value);
    }
    
    public modelCurrent.Employee SelectedEmployee  {
        get => employee;
        set=> this.RaiseAndSetIfChanged(ref employee, value);
    }
    public modelCurrent.Book SelectedBook  {
        get => book;
        set=> this.RaiseAndSetIfChanged(ref book, value);
    }
    
    private List<modelCurrent.Customer> allcustomers;
    private List<modelCurrent.Employee> allemployees;
    private List<modelCurrent.Book> allbooks;
    private List<modelCurrent.Customer> customers;
    private List<modelCurrent.Employee> employees;
    private List<modelCurrent.Book> books;
    public List<modelCurrent.Customer> Customers  {
        get => customers;
        set=> this.RaiseAndSetIfChanged(ref customers, value);
    }
    
    public List<modelCurrent.Employee> Employees  {
        get => employees;
        set=> this.RaiseAndSetIfChanged(ref employees, value);
    }
    public List<modelCurrent.Book> Books  {
        get => books;
        set=> this.RaiseAndSetIfChanged(ref books, value);
    }

    public ObservableCollection<modelCurrent.OrderLine> orderLines=new ObservableCollection<modelCurrent.OrderLine>();
  public ObservableCollection<modelCurrent.OrderLine> NewOrderLines
    {
        get => orderLines;
        set=> this.RaiseAndSetIfChanged(ref orderLines, value);
    }

    private modelCurrent.OrderLine selectedOrderLine;
    public modelCurrent.OrderLine SelectedOrderLine    {
        get => selectedOrderLine;
        set=> this.RaiseAndSetIfChanged(ref selectedOrderLine, value);
    }

    public string findCustomer="";
    public string findEmployee="";
    public string findBook="";
    public string FindCustomer  {
        get => findCustomer;
        set=> this.RaiseAndSetIfChanged(ref findCustomer, value);
    }
    public string FindEmployee  {
        get => findEmployee;
        set=> this.RaiseAndSetIfChanged(ref findEmployee, value);
    }
    public string FindBook  {
        get => findBook;
        set=> this.RaiseAndSetIfChanged(ref findBook, value);
    }
    public int amount=0;
    public int Amount  {
        get => amount;
        set=> this.RaiseAndSetIfChanged(ref amount, value);
    }
    private modelCurrent.Order newOrder;
    public OrderAddEditvm(List< modelCurrent.Customer> customers, List<modelCurrent.Employee> employees, List<modelCurrent.Book> books)
    {
        newOrder = new modelCurrent.Order();
        Init(customers,employees,books);
    }
    private void Init(List< modelCurrent.Customer> customers, List<modelCurrent.Employee> employees, List<modelCurrent.Book> books)
    {
        allcustomers = customers;
        allemployees = employees;
        allbooks = books;
        Customers = allcustomers;
        Employees = employees;
        Books = allbooks;
        this.WhenAnyValue(x => x.FindCustomer)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchCustomer!);
        this.WhenAnyValue(x => x.FindEmployee)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchEmployee!);
        this.WhenAnyValue(x => x.FindBook)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchBook!);
  
        AddOrderLine=   ReactiveCommand.Create(() =>
        {
            if (SelectedBook != null)
            {
                NewOrderLines.Add(new modelCurrent.OrderLine()
                {
                    book = SelectedBook,
                    order = newOrder,
                    amount = Amount
                });
            }

        });
        DeleteOrderLine=   ReactiveCommand.Create(() =>
        {
            if (SelectedOrderLine != null)
            {
                NewOrderLines.Remove(SelectedOrderLine);
            }
        });
    }
    public ReactiveCommand<Unit, Unit> AddOrderLine { get; private set; }
    public ReactiveCommand<Unit, Unit> DeleteOrderLine { get; private set; }
    private void DoSearchCustomer(string search)
    {
        if (search != "")
        {
           Customers = allcustomers.Select(x => x).Where(x => x.FIO.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
            Customers = allcustomers;
        }
    }
    private void DoSearchEmployee(string search)
    {
        if (search != "")
        {
            Employees = allemployees.Select(x => x).Where(x => x.FIO.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
          Employees = allemployees;
        }
    }
    private void DoSearchBook(string search)
    {
        if (search != "")
        {
            Employees = allemployees.Select(x => x).Where(x => x.FIO.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
            Employees = allemployees;
        }
    }
    protected override void OnReturn()
    {
        newOrder.Customer = SelectedCustomer;
        newOrder.Employee = SelectedEmployee;
        newOrder.OrderLines = NewOrderLines.ToList();
        int sum=0;
        foreach (modelCurrent.OrderLine line in newOrder.OrderLines)
        {
            sum += line.book.price * line.amount;
        }

        newOrder.sum = sum;
        data = newOrder;
    }
   
 
    public  OrderAddEditvm(modelCurrent.Order old,List< modelCurrent.Customer> customers, List<modelCurrent.Employee> employees, List<modelCurrent.Book> books)
    { 
        newOrder = old;
        Init(customers,employees,books);
        SelectedCustomer = newOrder.Customer;
        SelectedEmployee = newOrder.Employee;
        NewOrderLines = new ObservableCollection<modelCurrent.OrderLine>(newOrder.OrderLines);
        
    }
}