using System;
using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class EmployeeAddEditvm: AddEditViewModel
{
    private string name="Введите ФИО сотрудника";
    private int rating=0;
    public string Name
    {
        get => name;
        set
        {
                this.RaiseAndSetIfChanged(ref name, value);
     
        }
    }

    public int Rating
    {
        get => rating;
        set
        {
            this.RaiseAndSetIfChanged(ref rating, value);
        }

}
    public EmployeeAddEditvm()
    {
        
    }

    private modelCurrent.Employee oldEmployee;
    public EmployeeAddEditvm(modelCurrent.Employee old)
    {
        oldEmployee = old;
        Name = oldEmployee.FIO;
        Rating = oldEmployee.rating;
    }
    protected override void OnReturn()
    {
        if (oldEmployee == null)
        {
            oldEmployee= new modelCurrent.Employee()
            {
                FIO= Name,
                rating = Rating
            };
        }
        else
        {
            oldEmployee.FIO = Name;
            oldEmployee.rating = Rating;
        }

        data = oldEmployee;
    }

    
}