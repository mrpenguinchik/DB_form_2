using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class AuthorAddEditvm: AddEditViewModel
{
    private string name="Введите имя";

    public string Name
    {
        get => name;
        set=> this.RaiseAndSetIfChanged(ref name, value);
    }

    public AuthorAddEditvm()
    {
        
    }

    private modelCurrent.Author oldAuthor;
    public AuthorAddEditvm( modelCurrent.Author old)
    {
        oldAuthor = old;
        name = oldAuthor.name;
    }
    protected override void OnReturn()
    {
        if (oldAuthor == null)
        {
           oldAuthor= new modelCurrent.Author()
            {
                name = Name
            };
        }
        else
        {
            oldAuthor.name = Name;
            data = oldAuthor;
        }

        data = oldAuthor;
    }
    
}