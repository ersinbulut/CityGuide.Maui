using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Maui.Models;

namespace CityGuide.Maui.Services
{
    public class AppDatabase
    {
        private SQLiteAsyncConnection _database;

        // Veritabanını hazırlar: bağlantıyı açar ve tabloları oluşturur.
        // Bir kez kurulduktan sonra tekrar kurmaz.
        private async Task InitAsync()
        {
            if (_database is not null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "cityguide.db");

            // Dosyanın tam yolunu görelim (SQLite Browser'da açmak için)
            System.Diagnostics.Debug.WriteLine($"[DB PATH] {dbPath}");

            _database = new SQLiteAsyncConnection(dbPath);

            // Code-first: modellere bakıp tabloları oluştur (yoksa)
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<Event>();
        }

        // --- Okuma metotları ---

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await InitAsync();
            return await _database.Table<Category>().ToListAsync();
        }

        public async Task<List<Event>> GetEventsAsync()
        {
            await InitAsync();
            return await _database.Table<Event>().ToListAsync();
        }


        // Etkinlikleri çeker VE her birinin kategori adını doldurur (foreign key eşleştirme)
        public async Task<List<Event>> GetEventsWithCategoryAsync()
        {
            await InitAsync();

            // İki tabloyu da çek
            var events = await _database.Table<Event>().ToListAsync();
            var categories = await _database.Table<Category>().ToListAsync();

            // Her etkinlik için, CategoryId'sine uyan kategoriyi bul ve adını yaz
            foreach (var ev in events)
            {
                var matchingCategory = categories.FirstOrDefault(c => c.Id == ev.CategoryId);
                if (matchingCategory is not null)
                {
                    ev.CategoryName = matchingCategory.CategoryName;
                }
                else
                {
                    ev.CategoryName = "Bilinmeyen";
                }
            }

            return events;
        }


        // --- Yazma metotları (uygulama içinden eklemek istersen) ---

        public async Task<int> AddCategoryAsync(Category category)
        {
            await InitAsync();
            return await _database.InsertAsync(category);
        }

        public async Task<int> AddEventAsync(Event newEvent)
        {
            await InitAsync();
            return await _database.InsertAsync(newEvent);
        }
    }
}
