using AvaloniaApplication1.Views;
using eftest ;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class GenreAddEditvm: AddEditViewModel
{
    private string name="Введите название жанра";

    public string Name
    {
        get => name;
        set=> this.RaiseAndSetIfChanged(ref name, value);
    }

    public GenreAddEditvm()
    {
        
    }

    private modelCurrent.Genre oldGenre;
    public GenreAddEditvm(modelCurrent.Genre old)
    {
        oldGenre = old;
        Name = oldGenre.name;
    }
    protected override void OnReturn()
    {
        if (oldGenre == null)
        {
           oldGenre= new modelCurrent.Genre()
           {
               name = Name
           };
        }
        else
        {
            oldGenre.name = Name;
            
        }

        data = oldGenre;

    }
    
}