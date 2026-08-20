
using CityGuide.Maui.Models;
using CityGuide.Maui.Services;

namespace CityGuide.Maui.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly AppDatabase _db = new AppDatabase();
    private const int CurrentUserId = 1;

    // Tüm mekanları bellekte tut (filtreleme için)
    private List<Place> _allPlaces = new List<Place>();
    private string _selectedCategory = "Tümü";

    public FavoritesPage()
    {
        InitializeComponent();

        // Filtre haplarını doldur
        FilterCollection.ItemsSource = new List<string>
        {
            "Tümü", "Restoran", "Müze", "Rota", "Kafe"
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadPlacesAsync();

        // Başlangıçta "Tümü" seçili
        FilterCollection.SelectedItem = "Tümü";
    }

    private async Task LoadPlacesAsync()
    {
        var places = await _db.GetPlacesAsync();

        // Her mekan için favori mi diye işaretle
        foreach (var place in places)
        {
            place.IsFavorite = await _db.IsFavoriteAsync(CurrentUserId, place.Id);
        }

        _allPlaces = places;
        ApplyFilter();
    }

    // Seçili kategoriye göre listeyi süz
    private void ApplyFilter()
    {
        if (_selectedCategory == "Tümü")
        {
            PlacesCollection.ItemsSource = _allPlaces;
        }
        else
        {
            PlacesCollection.ItemsSource = _allPlaces
                .Where(p => p.CategoryName == _selectedCategory)
                .ToList();
        }
    }

    private void OnFilterSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not string category)
            return;

        _selectedCategory = category;
        ApplyFilter();
    }

    // Kalbe tıklanınca
    private async void OnFavoriteTapped(object sender, TappedEventArgs e)
    {
        if (sender is not Label label) return;
        if (label.BindingContext is not Place place) return;

        if (place.IsFavorite)
            await _db.RemoveFavoriteAsync(CurrentUserId, place.Id);
        else
            await _db.AddFavoriteAsync(CurrentUserId, place.Id);

        await LoadPlacesAsync();
    }
}
