using SQLite;

namespace MauiWebApi.Entities
{
    public class SpecialEvent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DateText { get; set; }=string.Empty;
        public string Category { get; set; }= string.Empty;
        public double Rating { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
    }
}
