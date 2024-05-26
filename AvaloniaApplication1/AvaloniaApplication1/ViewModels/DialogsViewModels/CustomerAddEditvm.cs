using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class CustomerAddEditvm: AddEditViewModel
{
    private string name="Введите ФИО клиента";
    private int bonus=0;
    public string Name
    {
        get => name;
        set=> this.RaiseAndSetIfChanged(ref name, value);
    }
    public int Bonus
    {
        get => bonus;
        set=> this.RaiseAndSetIfChanged(ref bonus, value);
    }
    public CustomerAddEditvm()
    {
        
    }

    private modelCurrent.Customer oldCustomer;
    public CustomerAddEditvm(modelCurrent.Customer old)
    {
        oldCustomer = old;
        Name = oldCustomer.FIO;
        Bonus = oldCustomer.bonuses;
    }
    protected override void OnReturn()
    {
        if (oldCustomer == null)
        {
            oldCustomer= new modelCurrent.Customer()
            {
                FIO= Name,
                    bonuses = Bonus
            };
        }
        else
        {
            oldCustomer.FIO = Name;
            oldCustomer.bonuses = Bonus;
        }

        data = oldCustomer;
    }
    
}