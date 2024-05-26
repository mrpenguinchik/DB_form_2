using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class PublisherAddEditvm: AddEditViewModel
{
    private string name="Введите название издательства";

    public string Name
    {
        get => name;
        set=> this.RaiseAndSetIfChanged(ref name, value);
    }

    public PublisherAddEditvm()
    {
        
    }

    private modelCurrent.Publisher oldPublisher;
    public PublisherAddEditvm(modelCurrent.Publisher old)
    {
        oldPublisher = old;
        Name = oldPublisher.name;
    }
    protected override void OnReturn()
    {
        if (oldPublisher == null)
        {
            oldPublisher= new modelCurrent.Publisher()
            {
                name = Name
            };
        }
        else
        {
            oldPublisher.name = Name;
            
        }

        data = oldPublisher;
    }
    
}