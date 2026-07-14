using CityGuide.Maui.Models;
using CityGuide.Maui.Services;
using SQLite;

namespace CityGuide.Maui.Views;

public partial class CulturePage : ContentPage
{
    private readonly AppDatabase _db = new AppDatabase();
    private List<Event> _allEvents = new List<Event>();
    public CulturePage()
    {
        InitializeComponent();

    }

    // Sayfa ekrana geldiğinde çalışır
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // --- Kategoriler ---
        var categories = await _db.GetCategoriesAsync();
        categories.Insert(0, new Category { Id = 0, CategoryName = "Tümü" });
        CategoriesCollection.ItemsSource = categories;

        // --- Etkinlikler: bir kez çek, bellekte sakla ---
        _allEvents = await _db.GetEventsWithCategoryAsync();
        EventsCollection.ItemsSource = _allEvents;

        // Başlangıçta "Tümü" seçili olsun (listenin ilk öğesi)
        CategoriesCollection.SelectedItem = categories[0];


    }
    // Bir kategori hapına tıklanınca çalışır
    private void OnCategoryTapped(object sender, TappedEventArgs e)
    {
        // Tıklanan hapın bağlı olduğu kategori nesnesini al
        if (sender is not Border border) return;
        if (border.BindingContext is not Category category) return;

        if (category.Id == 0)
        {
            // "Tümü" -> hepsini göster
            EventsCollection.ItemsSource = _allEvents;
        }
        else
        {
            // Seçilen kategoriye ait etkinlikleri süz
            var filtered = _allEvents
                .Where(ev => ev.CategoryId == category.Id)
                .ToList();

            EventsCollection.ItemsSource = filtered;
        }
    }


    // Bir kategori seçilince çalışır
    private void OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Seçili öğeyi al
        if (e.CurrentSelection.FirstOrDefault() is not Category category)
            return;

        if (category.Id == 0)
        {
            // "Tümü" -> hepsini göster
            EventsCollection.ItemsSource = _allEvents;
        }
        else
        {
            // Seçilen kategoriye ait etkinlikleri süz
            var filtered = _allEvents
                .Where(ev => ev.CategoryId == category.Id)
                .ToList();

            EventsCollection.ItemsSource = filtered;
        }
    }

}