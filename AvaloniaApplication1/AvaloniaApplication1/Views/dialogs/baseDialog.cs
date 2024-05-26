using System;
using Avalonia.ReactiveUI;
using AvaloniaApplication1.ViewModels;
using ReactiveUI;
namespace AvaloniaApplication1.Views;

public class baseDialog: ReactiveWindow<AddEditViewModel>
{
    public baseDialog()
    {  
        this.WhenActivated(d => d((DataContext as AddEditViewModel)!.Close.Subscribe(Close)));
    }
 

}