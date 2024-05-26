using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels;
using eftest;
using ReactiveUI;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
   private MainWindowViewModel? viewmodel;
 
    public MainWindow()
    {
       
        InitializeComponent();
        this.WhenActivated(d => d((DataContext as MainWindowViewModel)!.ShowDialog.RegisterHandler(DoShowDialogAsync)));

    }
    
    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
       var selectedItem = ((DataGrid)sender!)?.SelectedItem;
        if (selectedItem != null)
           (DataContext as MainWindowViewModel)?.OrderLinesUpdate(selectedItem);
       
    }
    private async Task DoShowDialogAsync(InteractionContext<baseDialog, object?> interaction)
    {
        var result = await interaction.Input.ShowDialog<object?>(this);
        interaction.SetOutput(result);
    }

   
}