using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class LocationAddEditvm: AddEditViewModel
{
    private string name="Введите название отдела";

    public string Name
    {
        get => name;
        set=> this.RaiseAndSetIfChanged(ref name, value);
    }
    public LocationAddEditvm()
    {
        
    }

    private modelCurrent.Location oldLocation;
    public LocationAddEditvm(modelCurrent.Location old)
    {
        oldLocation = old;
        Name = oldLocation.name;
    }
    protected override void OnReturn()
    {
        if (oldLocation == null)
        {
            oldLocation= new modelCurrent.Location()
            {
                name = Name
            };
        }
        else
        {
            oldLocation.name = Name;
            
        }

        data = oldLocation;
    }
    
}