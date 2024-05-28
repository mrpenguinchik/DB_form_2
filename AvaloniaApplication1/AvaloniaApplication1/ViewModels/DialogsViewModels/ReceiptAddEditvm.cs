using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class ReceiptAddEditvm: AddEditViewModel
{
    
    public ReceiptAddEditvm(List<modelCurrent.Book> boocksAll)
    {
        Init(boocksAll);
    }
    public ReceiptAddEditvm(modelCurrent.Receipt old,List<modelCurrent.Book> boocksAll)
    {    Init(boocksAll);
        oldReceipt = old;
       book = oldReceipt.book;
        amount = oldReceipt.amount;
        sum = oldReceipt.sum;
   
    }

    public void Init(List<modelCurrent.Book> boocksAll)
    {   allBooks = boocksAll;
        books = allBooks;
        book = books[0];
        this.WhenAnyValue(x => x.FindBook)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchBook!);
    }
    private void DoSearchBook(string search)
    {
        if (search != "")
        {
           Books = allBooks.Select(x => x).Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
             Books = allBooks;
        }
    }
    private modelCurrent.Receipt oldReceipt;
    private List<modelCurrent.Book> books;
    private List<modelCurrent.Book> allBooks;
    private modelCurrent.Book book;
    private int amount=0;
    private float sum=0;
    private string findBook="";
   public List<modelCurrent.Book> Books {
       get => books;
       set=> this.RaiseAndSetIfChanged(ref books, value);
   }
   public  modelCurrent.Book Book {
       get => book;
       set=> this.RaiseAndSetIfChanged(ref book, value);
   }
   public int Amount {
       get => amount;
       set=> this.RaiseAndSetIfChanged(ref amount, value);
   }
   public  float Sum {
       get => sum;
       set=> this.RaiseAndSetIfChanged(ref sum, value);
   }
   public  string FindBook {
       get => findBook;
       set=> this.RaiseAndSetIfChanged(ref findBook, value);
   }

    protected override void OnReturn()
    {
        if (oldReceipt == null)
        {
            oldReceipt= new modelCurrent.Receipt()
            {
                book= Book,
                amount = Amount,
                sum = Sum
            };
        }
        else
        {
            oldReceipt.book = book;
            oldReceipt.amount = Amount;
            oldReceipt.sum = Sum;
        }

        data = oldReceipt;
    }
    
}