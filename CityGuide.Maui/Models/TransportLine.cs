using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityGuide.Maui.Models
{
    [Table("TransportLines")]
    public class TransportLine
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Type { get; set; } = string.Empty;       // "Metro", "Tramvay", "Otobüs"

        [NotNull]
        public string LineCode { get; set; } = string.Empty;   // "M1", "Tram 1", "90/91"

        [NotNull]
        public string LineName { get; set; } = string.Empty;   // "KIRMIZI HAT", "Tarihi Tramvay Hattı 1"

        public string Route { get; set; } = string.Empty;      // "Sesto F.S. — Rho Fiera"

        public string Status { get; set; } = string.Empty;     // "Normal Servis"

        public string ColorHex { get; set; } = string.Empty;   // "#E31E24"
    }
}
