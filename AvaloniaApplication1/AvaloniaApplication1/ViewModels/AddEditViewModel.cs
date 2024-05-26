using System;
using System.Reactive;
using AvaloniaApplication1.Views;
using eftest;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels;

public class AddEditViewModel: ViewModelBase
{
    protected object data;
    public  ReactiveCommand<Unit, object?> Close { get; private set; }
    public AddEditViewModel()
    {
      
        Close=ReactiveCommand.Create(() =>
        {  OnReturn();
            return data;
        });
    }

   protected virtual void OnReturn()
    {
        
    }
}