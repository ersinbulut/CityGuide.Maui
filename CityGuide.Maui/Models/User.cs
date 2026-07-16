using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityGuide.Maui.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string FullName { get; set; }
        //aynı eposta ile birden fazla kullanıcı kaydı yapılmasını engellemek için Unique attribute ekledim
        [Unique, NotNull]
        public string Email { get; set; }
        [NotNull]
        public string Password { get; set; }
    }
}
