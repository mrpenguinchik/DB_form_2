using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using AvaloniaApplication1.Views;
using eftest;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.DialogsViewModels;

public class BookAddEditvm: AddEditViewModel
{     protected override void OnReturn()
    {
        if (oldBook!=null)
        {
            oldBook.name = name;
            oldBook.price = price;
            oldBook.article = articul;
            oldBook.barcode = barcode;
            oldBook.author = selectedAuthor;
            oldBook.publisher = selectedPublisher;
            oldBook.location = selectedLocation;
            oldBook.genres = newGenres.ToList();
            data = oldBook;
        }
        else
        {
            if (newGenres.Count == 0)
            {
                newGenres.Add(genres[0]);
            }

            data = new modelCurrent.Book()
            {
                name = Name,
                barcode = Barcode,
                price = Price,
                article = Articul,
                author = selectedAuthor,
                location = selectedLocation,
                publisher = selectedPublisher,
                genres = NewGenres.ToList()
            };
        }
    }

    private string name="Введите название книги";

    public string Name
    {
        get => name;
        set => this.RaiseAndSetIfChanged(ref name, value);
    }
    private string barcode="Введите название книги";

    public string Barcode
    {
        get => barcode;
        set => this.RaiseAndSetIfChanged(ref barcode, value);
    }
    private  float price = 0;

    public float Price
    {
        get => price;
        set => this.RaiseAndSetIfChanged(ref price, value);
    }
    private int articul = 123456789;

    public int Articul
    {
        get => articul;
        set => this.RaiseAndSetIfChanged(ref articul, value);
    }
    public BookAddEditvm(List<modelCurrent.Author> authors,
    List<modelCurrent.Genre> genres,
    List<modelCurrent.Location> locations,
    List<modelCurrent.Publisher> publishers):base()
    {
        
   Init(authors,genres,locations,publishers);
   selectedAuthor = Authors[0];
   selectedLocation = Locations[0];
   selectedPublisher = Publishers[0];
    }

    private modelCurrent.Book oldBook;
    public BookAddEditvm(modelCurrent.Book old, List<modelCurrent.Author> authors,
        List<modelCurrent.Genre> genres,
        List<modelCurrent.Location> locations,
        List<modelCurrent.Publisher> publishers):base()
    {
Init(authors,genres,locations,publishers);
oldBook = old;
Name = oldBook.name;
Price = oldBook.price;
Articul = oldBook.article;
Barcode = oldBook.barcode;
selectedAuthor =oldBook.author;
selectedLocation =oldBook.location;
selectedPublisher = oldBook.publisher;
NewGenres = new ObservableCollection<modelCurrent.Genre>(oldBook.genres);
    }

    private void Init(List<modelCurrent.Author> authors,
        List<modelCurrent.Genre> genres,
        List<modelCurrent.Location> locations,
        List<modelCurrent.Publisher> publishers)
    {
        allAuthors = authors;
        Authors = allAuthors;
       allGenres = genres;
       Genres = allGenres;
        allLocations = locations;
        Locations = allLocations;
        allPublishers =publishers;
        Publishers = allPublishers;
     
        this.WhenAnyValue(x => x.FindAuthor)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchAuthor!);
        this.WhenAnyValue(x => x.FindPublisher)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchPublisher!);
        this.WhenAnyValue(x => x.FindGenre)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchGenre!);
        this.WhenAnyValue(x => x.FindLocation)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearchLocation!);
        AddGenre=   ReactiveCommand.Create(() =>
        {
            if (SelectedGenre != null && !NewGenres.Contains(SelectedGenre))
            { 
                NewGenres.Add(SelectedGenre);
            }
            
        });
       DeleteGenre=   ReactiveCommand.Create(() =>
        {
            if (SelectedNewGenre != null)
            {
                NewGenres.Remove(SelectedNewGenre);
            }
        });
    }
    public ReactiveCommand<Unit, Unit> AddGenre { get; private set; }
    public ReactiveCommand<Unit, Unit> DeleteGenre { get; private set; }
    
    private void DoSearchAuthor(string search)
    {
        if (search != "")
        {
            Authors = allAuthors.Select(x => x).Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
            Authors = allAuthors;
        }
    }
    private void DoSearchPublisher(string search)
    {
        if (search != "")
        {
            Publishers = allPublishers.Select(x => x).Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
            Publishers = allPublishers;
        }
    }
    private void DoSearchGenre(string search)
    {
        if (search != "")
        {
             Genres= allGenres.Select(x => x).Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
           Genres = allGenres;
        }
    }
    private void DoSearchLocation(string search)
    {
        if (search != "")
        {
           Locations = allLocations.Select(x => x).Where(x => x.name.ToLower().Contains(search.ToLower())).ToList();
        }
        else
        {
            Locations = allLocations;
        }
    }
    private string findAuthor="";

    public string FindAuthor
    {
        get => findAuthor;
        set=> this.RaiseAndSetIfChanged(ref findAuthor, value);
    }
    private string findPublisher="";

    public string FindPublisher
    {
        get => findPublisher;
        set=> this.RaiseAndSetIfChanged(ref findPublisher, value);
    }
    private string findLocation="";

    public string FindLocation
    {
        get => findLocation;
        set=> this.RaiseAndSetIfChanged(ref findLocation, value);
    }
    private string findGenre="";

    public string FindGenre
    {
        get => findGenre;
        set=> this.RaiseAndSetIfChanged(ref findGenre, value);
    }

    private List<modelCurrent.Author> allAuthors;
    private List<modelCurrent.Publisher> allPublishers;
    private List<modelCurrent.Genre> allGenres;
    private List<modelCurrent.Location> allLocations;
    private List<modelCurrent.Author> authors;
    private List<modelCurrent.Genre> genres;
    private List<modelCurrent.Location> locations;
    private List<modelCurrent.Publisher> publishers;
    private ObservableCollection<modelCurrent.Genre> newGenres=new ObservableCollection<modelCurrent.Genre>();
    public ObservableCollection<modelCurrent.Genre> NewGenres
    {
        get => newGenres;
        set=> this.RaiseAndSetIfChanged(ref newGenres, value);
        
    }
    
    private modelCurrent.Genre selectedNewGenre;

    public modelCurrent.Genre SelectedNewGenre
    {
        get => selectedNewGenre;
        set=> this.RaiseAndSetIfChanged(ref selectedNewGenre, value);
    }
    public List<modelCurrent.Author> Authors
    {
        get => authors;
        set=> this.RaiseAndSetIfChanged(ref authors, value);
    }

    private modelCurrent.Author selectedAuthor;

    public modelCurrent.Author SelectedAuthor
    {
        get => selectedAuthor;
        set=> this.RaiseAndSetIfChanged(ref selectedAuthor, value);
    }
    public List<modelCurrent.Publisher> Publishers
    {
        get => publishers;
        set=> this.RaiseAndSetIfChanged(ref publishers, value);
    }

    private modelCurrent.Publisher selectedPublisher;

    public modelCurrent.Publisher SelectedPublisher
    {
        get => selectedPublisher;
        set=> this.RaiseAndSetIfChanged(ref selectedPublisher, value);
    }
    public List<modelCurrent.Genre> Genres
    {
        get => genres;
        set=> this.RaiseAndSetIfChanged(ref genres, value);
    }

    private modelCurrent.Genre selectedgGenre;

    public modelCurrent.Genre SelectedGenre
    {
        get => selectedgGenre;
        set=> this.RaiseAndSetIfChanged(ref selectedgGenre, value);
    }
    public List<modelCurrent.Location> Locations
    {
        get => locations;
        set=> this.RaiseAndSetIfChanged(ref locations, value);
    }

    private modelCurrent.Location selectedLocation;

    public modelCurrent.Location SelectedLocation
    {
        get => selectedLocation;
        set=> this.RaiseAndSetIfChanged(ref selectedLocation, value);
    }
}